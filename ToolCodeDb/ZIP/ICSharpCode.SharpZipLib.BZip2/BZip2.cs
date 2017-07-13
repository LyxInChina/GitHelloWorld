using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.BZip2
{
	public sealed class BZip2
	{
		public static void Decompress(Stream inStream, Stream outStream)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			try
			{
				using (BZip2InputStream bZip2InputStream = new BZip2InputStream(inStream))
				{
					for (int num = bZip2InputStream.ReadByte(); num != -1; num = bZip2InputStream.ReadByte())
					{
						outStream.WriteByte((byte)num);
					}
				}
			}
			finally
			{
				if (outStream != null)
				{
					((IDisposable)outStream).Dispose();
				}
			}
		}

		public static void Compress(Stream inStream, Stream outStream, int blockSize)
		{
			if (inStream == null)
			{
				throw new ArgumentNullException("inStream");
			}
			if (outStream == null)
			{
				throw new ArgumentNullException("outStream");
			}
			try
			{
				using (BZip2OutputStream bZip2OutputStream = new BZip2OutputStream(outStream, blockSize))
				{
					for (int num = inStream.ReadByte(); num != -1; num = inStream.ReadByte())
					{
						bZip2OutputStream.WriteByte((byte)num);
					}
				}
			}
			finally
			{
				if (inStream != null)
				{
					((IDisposable)inStream).Dispose();
				}
			}
		}

		private BZip2()
		{
		}
	}
}
