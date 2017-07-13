using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Core
{
	public sealed class StreamUtils
	{
		public static void ReadFully(Stream stream, byte[] buffer)
		{
			StreamUtils.ReadFully(stream, buffer, 0, buffer.Length);
		}

		public static void ReadFully(Stream stream, byte[] buffer, int offset, int count)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			while (count > 0)
			{
				int num = stream.Read(buffer, offset, count);
				if (num <= 0)
				{
					throw new EndOfStreamException();
				}
				offset += num;
				count -= num;
			}
		}

		public static void Copy(Stream source, Stream destination, byte[] buffer, ProgressHandler progressHandler, TimeSpan updateInterval, object sender, string name)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length < 128)
			{
				throw new ArgumentException("Buffer is too small", "buffer");
			}
			if (progressHandler == null)
			{
				throw new ArgumentNullException("progressHandler");
			}
			bool flag = true;
			DateTime now = DateTime.Now;
			long num = 0L;
			long num2 = 0L;
			if (source.CanSeek)
			{
				num2 = source.Length - source.Position;
			}
			ProgressEventArgs progressEventArgs = new ProgressEventArgs(name, num, num2);
			progressHandler(sender, progressEventArgs);
			bool flag2 = false;
			while (flag)
			{
				int num3 = source.Read(buffer, 0, buffer.Length);
				if (num3 > 0)
				{
					num += (long)num3;
					destination.Write(buffer, 0, num3);
				}
				else
				{
					destination.Flush();
					flag = false;
				}
				if (DateTime.Now - now > updateInterval)
				{
					flag2 = (num == num2);
					now = DateTime.Now;
					progressEventArgs = new ProgressEventArgs(name, num, num2);
					progressHandler(sender, progressEventArgs);
					flag = progressEventArgs.ContinueRunning;
				}
			}
			if (!flag2)
			{
				progressEventArgs = new ProgressEventArgs(name, num, num2);
				progressHandler(sender, progressEventArgs);
			}
		}

		public static void Copy(Stream source, Stream destination, byte[] buffer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length < 128)
			{
				throw new ArgumentException("Buffer is too small", "buffer");
			}
			bool flag = true;
			while (flag)
			{
				int num = source.Read(buffer, 0, buffer.Length);
				if (num > 0)
				{
					destination.Write(buffer, 0, num);
				}
				else
				{
					destination.Flush();
					flag = false;
				}
			}
		}

		private StreamUtils()
		{
		}
	}
}
