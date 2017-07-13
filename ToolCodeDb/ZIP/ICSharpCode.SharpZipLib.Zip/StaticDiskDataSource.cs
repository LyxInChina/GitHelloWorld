using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	internal class StaticDiskDataSource : IStaticDataSource
	{
		private string fileName_;

		public StaticDiskDataSource(string fileName)
		{
			this.fileName_ = fileName;
		}

		public Stream GetSource()
		{
			return File.OpenRead(this.fileName_);
		}
	}
}
