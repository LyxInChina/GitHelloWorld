using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.GZip
{
	public class GZipOutputStream : DeflaterOutputStream
	{
		protected Crc32 crc = new Crc32();

		private bool headerWritten_;

		public GZipOutputStream(Stream baseOutputStream) : this(baseOutputStream, 4096)
		{
		}

		public GZipOutputStream(Stream baseOutputStream, int size) : base(baseOutputStream, new Deflater(-1, true), size)
		{
		}

		public void SetLevel(int level)
		{
			if (level < 1)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.deflater_.SetLevel(level);
		}

		public int GetLevel()
		{
			return this.deflater_.GetLevel();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			if (!this.headerWritten_)
			{
				this.WriteHeader();
			}
			this.crc.Update(buffer, offset, count);
			base.Write(buffer, offset, count);
		}

		public override void Close()
		{
			try
			{
				this.Finish();
			}
			finally
			{
				if (base.IsStreamOwner)
				{
					this.baseOutputStream_.Close();
				}
			}
		}

		public override void Finish()
		{
			if (!this.headerWritten_)
			{
				this.WriteHeader();
			}
			base.Finish();
			int totalIn = this.deflater_.TotalIn;
		    unchecked
		    {
		        int num = (int) (this.crc.Value & (long) ((ulong) -1));
		        byte[] array = new byte[]
		        {
		            (byte) num,
		            (byte) (num >> 8),
		            (byte) (num >> 16),
		            (byte) (num >> 24),
		            (byte) totalIn,
		            (byte) (totalIn >> 8),
		            (byte) (totalIn >> 16),
		            (byte) (totalIn >> 24)
		        };
		        this.baseOutputStream_.Write(array, 0, array.Length);
		    }
		}

		private void WriteHeader()
		{
			if (!this.headerWritten_)
			{
				this.headerWritten_ = true;
				int num = (int)((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000L);
				byte[] array = new byte[]
				{
					31,
					139,
					8,
					0,
					0,
					0,
					0,
					0,
					0,
					255
				};
				array[4] = (byte)num;
				array[5] = (byte)(num >> 8);
				array[6] = (byte)(num >> 16);
				array[7] = (byte)(num >> 24);
				byte[] array2 = array;
				this.baseOutputStream_.Write(array2, 0, array2.Length);
			}
		}
	}
}
