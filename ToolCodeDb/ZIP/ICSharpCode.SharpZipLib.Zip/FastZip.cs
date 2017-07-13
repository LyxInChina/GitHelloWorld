using ICSharpCode.SharpZipLib.Core;
using System;
using System.Collections;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	public class FastZip
	{
		public enum Overwrite
		{
			Prompt,
			Never,
			Always
		}

		public delegate bool ConfirmOverwriteDelegate(string fileName);

		private bool continueRunning_;

		private byte[] buffer_;

		private ZipOutputStream outputStream_;

		private ZipFile zipFile_;

		private string targetDirectory_;

		private string sourceDirectory_;

		private NameFilter fileFilter_;

		private NameFilter directoryFilter_;

		private FastZip.Overwrite overwrite_;

		private FastZip.ConfirmOverwriteDelegate confirmDelegate_;

		private bool restoreDateTimeOnExtract_;

		private bool restoreAttributesOnExtract_;

		private bool createEmptyDirectories_;

		private FastZipEvents events_;

		private IEntryFactory entryFactory_ = new ZipEntryFactory();

		private string password_;

		public bool CreateEmptyDirectories
		{
			get
			{
				return this.createEmptyDirectories_;
			}
			set
			{
				this.createEmptyDirectories_ = value;
			}
		}

		public string Password
		{
			get
			{
				return this.password_;
			}
			set
			{
				this.password_ = value;
			}
		}

		public INameTransform NameTransform
		{
			get
			{
				return this.entryFactory_.NameTransform;
			}
			set
			{
				this.entryFactory_.NameTransform = value;
			}
		}

		public IEntryFactory EntryFactory
		{
			get
			{
				return this.entryFactory_;
			}
			set
			{
				if (value == null)
				{
					this.entryFactory_ = new ZipEntryFactory();
					return;
				}
				this.entryFactory_ = value;
			}
		}

		public bool RestoreDateTimeOnExtract
		{
			get
			{
				return this.restoreDateTimeOnExtract_;
			}
			set
			{
				this.restoreDateTimeOnExtract_ = value;
			}
		}

		public bool RestoreAttributesOnExtract
		{
			get
			{
				return this.restoreAttributesOnExtract_;
			}
			set
			{
				this.restoreAttributesOnExtract_ = value;
			}
		}

		public FastZip()
		{
		}

		public FastZip(FastZipEvents events)
		{
			this.events_ = events;
		}

		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
		}

		public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
		{
			this.CreateZip(File.Create(zipFileName), sourceDirectory, recurse, fileFilter, null);
		}

		public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
		{
			this.NameTransform = new ZipNameTransform(sourceDirectory);
			this.sourceDirectory_ = sourceDirectory;
			using (this.outputStream_ = new ZipOutputStream(outputStream))
			{
				if (this.password_ != null)
				{
					this.outputStream_.Password = this.password_;
				}
				FileSystemScanner fileSystemScanner = new FileSystemScanner(fileFilter, directoryFilter);
				FileSystemScanner expr_47 = fileSystemScanner;
				expr_47.ProcessFile = (ProcessFileHandler)Delegate.Combine(expr_47.ProcessFile, new ProcessFileHandler(this.ProcessFile));
				if (this.CreateEmptyDirectories)
				{
					FileSystemScanner expr_71 = fileSystemScanner;
					expr_71.ProcessDirectory = (ProcessDirectoryHandler)Delegate.Combine(expr_71.ProcessDirectory, new ProcessDirectoryHandler(this.ProcessDirectory));
				}
				if (this.events_ != null)
				{
					if (this.events_.FileFailure != null)
					{
						FileSystemScanner expr_A8 = fileSystemScanner;
						expr_A8.FileFailure = (FileFailureHandler)Delegate.Combine(expr_A8.FileFailure, this.events_.FileFailure);
					}
					if (this.events_.DirectoryFailure != null)
					{
						FileSystemScanner expr_D6 = fileSystemScanner;
						expr_D6.DirectoryFailure = (DirectoryFailureHandler)Delegate.Combine(expr_D6.DirectoryFailure, this.events_.DirectoryFailure);
					}
				}
				fileSystemScanner.Scan(sourceDirectory, recurse);
			}
		}

		public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
		{
			this.ExtractZip(zipFileName, targetDirectory, FastZip.Overwrite.Always, null, fileFilter, null, this.restoreDateTimeOnExtract_);
		}

		public void ExtractZip(string zipFileName, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime)
		{
			if (overwrite == FastZip.Overwrite.Prompt && confirmDelegate == null)
			{
				throw new ArgumentNullException("confirmDelegate");
			}
			this.continueRunning_ = true;
			this.overwrite_ = overwrite;
			this.confirmDelegate_ = confirmDelegate;
			this.targetDirectory_ = targetDirectory;
			this.fileFilter_ = new NameFilter(fileFilter);
			this.directoryFilter_ = new NameFilter(directoryFilter);
			this.restoreDateTimeOnExtract_ = restoreDateTime;
			using (this.zipFile_ = new ZipFile(zipFileName))
			{
				if (this.password_ != null)
				{
					this.zipFile_.Password = this.password_;
				}
				IEnumerator enumerator = this.zipFile_.GetEnumerator();
				while (this.continueRunning_ && enumerator.MoveNext())
				{
					ZipEntry zipEntry = (ZipEntry)enumerator.Current;
					if (zipEntry.IsFile)
					{
						if (this.directoryFilter_.IsMatch(Path.GetDirectoryName(zipEntry.Name)) && this.fileFilter_.IsMatch(zipEntry.Name))
						{
							this.ExtractEntry(zipEntry);
						}
					}
					else if (zipEntry.IsDirectory && this.directoryFilter_.IsMatch(zipEntry.Name) && this.CreateEmptyDirectories)
					{
						this.ExtractEntry(zipEntry);
					}
				}
			}
		}

		private void ProcessDirectory(object sender, DirectoryEventArgs e)
		{
			if (!e.HasMatchingFiles && this.CreateEmptyDirectories)
			{
				if (this.events_ != null)
				{
					this.events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
				}
				if (e.ContinueRunning && e.Name != this.sourceDirectory_)
				{
					ZipEntry entry = this.entryFactory_.MakeDirectoryEntry(e.Name);
					this.outputStream_.PutNextEntry(entry);
				}
			}
		}

		private void ProcessFile(object sender, ScanEventArgs e)
		{
			if (this.events_ != null && this.events_.ProcessFile != null)
			{
				this.events_.ProcessFile(sender, e);
			}
			if (e.ContinueRunning)
			{
				ZipEntry entry = this.entryFactory_.MakeFileEntry(e.Name);
				this.outputStream_.PutNextEntry(entry);
				this.AddFileContents(e.Name);
			}
		}

		private void AddFileContents(string name)
		{
			if (this.buffer_ == null)
			{
				this.buffer_ = new byte[4096];
			}
			using (FileStream fileStream = File.OpenRead(name))
			{
				if (this.events_ != null && this.events_.Progress != null)
				{
					StreamUtils.Copy(fileStream, this.outputStream_, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, name);
				}
				else
				{
					StreamUtils.Copy(fileStream, this.outputStream_, this.buffer_);
				}
			}
			if (this.events_ != null)
			{
				this.continueRunning_ = this.events_.OnCompletedFile(name);
			}
		}

		private void ExtractFileEntry(ZipEntry entry, string targetName)
		{
			bool flag = true;
			if (this.overwrite_ != FastZip.Overwrite.Always && File.Exists(targetName))
			{
				flag = (this.overwrite_ == FastZip.Overwrite.Prompt && this.confirmDelegate_ != null && this.confirmDelegate_(targetName));
			}
			if (flag)
			{
				if (this.events_ != null)
				{
					this.continueRunning_ = this.events_.OnProcessFile(entry.Name);
				}
				if (this.continueRunning_)
				{
					try
					{
						using (FileStream fileStream = File.Create(targetName))
						{
							if (this.buffer_ == null)
							{
								this.buffer_ = new byte[4096];
							}
							if (this.events_ != null && this.events_.Progress != null)
							{
								StreamUtils.Copy(this.zipFile_.GetInputStream(entry), fileStream, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, this, entry.Name);
							}
							else
							{
								StreamUtils.Copy(this.zipFile_.GetInputStream(entry), fileStream, this.buffer_);
							}
							if (this.events_ != null)
							{
								this.continueRunning_ = this.events_.OnCompletedFile(entry.Name);
							}
						}
						if (this.restoreDateTimeOnExtract_)
						{
							File.SetLastWriteTime(targetName, entry.DateTime);
						}
						if (this.RestoreAttributesOnExtract && entry.IsDOSEntry && entry.ExternalFileAttributes != -1)
						{
							FileAttributes fileAttributes = (FileAttributes)entry.ExternalFileAttributes;
							fileAttributes &= (FileAttributes.ReadOnly | FileAttributes.Hidden | FileAttributes.Archive | FileAttributes.Normal);
							File.SetAttributes(targetName, fileAttributes);
						}
					}
					catch (Exception e)
					{
						if (this.events_ != null)
						{
							this.continueRunning_ = this.events_.OnFileFailure(targetName, e);
						}
						else
						{
							this.continueRunning_ = false;
						}
					}
				}
			}
		}

		private void ExtractEntry(ZipEntry entry)
		{
			bool flag = false;
			string text = entry.Name;
			if (entry.IsFile)
			{
				flag = (FastZip.NameIsValid(text) && entry.IsCompressionMethodSupported());
			}
			else if (entry.IsDirectory)
			{
				flag = FastZip.NameIsValid(text);
			}
			string path = null;
			string text2 = null;
			if (flag)
			{
				if (Path.IsPathRooted(text))
				{
					string pathRoot = Path.GetPathRoot(text);
					text = text.Substring(pathRoot.Length);
				}
				if (text.Length > 0)
				{
					text2 = Path.Combine(this.targetDirectory_, text);
					if (entry.IsDirectory)
					{
						path = text2;
					}
					else
					{
						path = Path.GetDirectoryName(Path.GetFullPath(text2));
					}
				}
				else
				{
					flag = false;
				}
			}
			if (flag && !Directory.Exists(path))
			{
				if (entry.IsDirectory)
				{
					if (!this.CreateEmptyDirectories)
					{
						goto IL_F8;
					}
				}
				try
				{
					Directory.CreateDirectory(path);
				}
				catch (Exception e)
				{
					flag = false;
					if (this.events_ != null)
					{
						if (entry.IsDirectory)
						{
							this.continueRunning_ = this.events_.OnDirectoryFailure(text2, e);
						}
						else
						{
							this.continueRunning_ = this.events_.OnFileFailure(text2, e);
						}
					}
					else
					{
						this.continueRunning_ = false;
					}
				}
			}
			IL_F8:
			if (flag && entry.IsFile)
			{
				this.ExtractFileEntry(entry, text2);
			}
		}

		private static int MakeExternalAttributes(FileInfo info)
		{
			return (int)info.Attributes;
		}

		private static bool NameIsValid(string name)
		{
			return name != null && name.Length > 0 && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
		}
	}
}
