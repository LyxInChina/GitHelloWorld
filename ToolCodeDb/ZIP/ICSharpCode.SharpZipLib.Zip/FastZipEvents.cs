using ICSharpCode.SharpZipLib.Core;
using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	public class FastZipEvents
	{
		public ProcessDirectoryHandler ProcessDirectory;

		public ProcessFileHandler ProcessFile;

		public ProgressHandler Progress;

		public CompletedFileHandler CompletedFile;

		public DirectoryFailureHandler DirectoryFailure;

		public FileFailureHandler FileFailure;

		private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);

		public TimeSpan ProgressInterval
		{
			get
			{
				return this.progressInterval_;
			}
			set
			{
				this.progressInterval_ = value;
			}
		}

		public bool OnDirectoryFailure(string directory, Exception e)
		{
			bool result = false;
			if (this.DirectoryFailure != null)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(directory, e);
				this.DirectoryFailure(this, scanFailureEventArgs);
				result = scanFailureEventArgs.ContinueRunning;
			}
			return result;
		}

		public bool OnFileFailure(string file, Exception e)
		{
			bool result = false;
			if (this.FileFailure != null)
			{
				ScanFailureEventArgs scanFailureEventArgs = new ScanFailureEventArgs(file, e);
				this.FileFailure(this, scanFailureEventArgs);
				result = scanFailureEventArgs.ContinueRunning;
			}
			return result;
		}

		public bool OnProcessFile(string file)
		{
			bool result = true;
			if (this.ProcessFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				this.ProcessFile(this, scanEventArgs);
				result = scanEventArgs.ContinueRunning;
			}
			return result;
		}

		public bool OnCompletedFile(string file)
		{
			bool result = true;
			if (this.CompletedFile != null)
			{
				ScanEventArgs scanEventArgs = new ScanEventArgs(file);
				this.CompletedFile(this, scanEventArgs);
				result = scanEventArgs.ContinueRunning;
			}
			return result;
		}

		public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
		{
			bool result = true;
			if (this.ProcessDirectory != null)
			{
				DirectoryEventArgs directoryEventArgs = new DirectoryEventArgs(directory, hasMatchingFiles);
				this.ProcessDirectory(this, directoryEventArgs);
				result = directoryEventArgs.ContinueRunning;
			}
			return result;
		}
	}
}
