﻿namespace BearsEngine.Audio.OpenAL.OggVorbis;

    /// <summary>
    /// Summary description for csBuffer.
    /// </summary>
    internal class csBuffer
{
	private static int BUFFER_INCREMENT = 256;

	private static uint[] mask={
								  0x00000000,0x00000001,0x00000003,0x00000007,0x0000000f,
								  0x0000001f,0x0000003f,0x0000007f,0x000000ff,0x000001ff,
								  0x000003ff,0x000007ff,0x00000fff,0x00001fff,0x00003fff,
								  0x00007fff,0x0000ffff,0x0001ffff,0x0003ffff,0x0007ffff,
								  0x000fffff,0x001fffff,0x003fffff,0x007fffff,0x00ffffff,
								  0x01ffffff,0x03ffffff,0x07ffffff,0x0fffffff,0x1fffffff,
								  0x3fffffff,0x7fffffff,0xffffffff
							  };
	int ptr = 0;
	byte[] buffer = null;
	int endbit = 0;
	int endbyte = 0;
	int storage = 0;

	internal void writeinit() 
	{
		buffer = new byte[BUFFER_INCREMENT];
		ptr = 0;
		buffer[0] = (byte)'\0';
		storage = BUFFER_INCREMENT;
	}

	internal void write(byte[] s) 
	{
		for(int i = 0; i < s.Length; i++) 
		{
			if(s[i] == 0) break;
			write(s[i], 8);
		}
	}

	internal void read (byte[] s, int bytes)
	{
		int i = 0;
		while(bytes--!=0) 
		{
			s[i++]=(byte)read(8);
		}
	}

	void reset() 
	{
		ptr = 0;
		buffer[0] = (byte)'\0';
		endbit = endbyte = 0;
	}

	internal void writeclear()
	{
		buffer = null;
	}

	internal void readinit(byte[] buf, int start, int bytes)
	{
		ptr = start;
		buffer = buf;
		endbit = endbyte = 0;
		storage = bytes;
	}

	internal void write(int vvalue, int bits)
	{
		if(endbyte + 4 >= storage) 
		{
			byte[] foo = new byte[storage + BUFFER_INCREMENT];
			Array.Copy(buffer, 0, foo, 0, storage);
			buffer = foo;
			storage += BUFFER_INCREMENT;
		}

		vvalue = (int)((uint)vvalue & mask[bits]);
		bits += endbit;
		buffer[ptr] |= (byte)(vvalue << endbit);

		if(bits >= 8)
		{
			buffer[ptr+1] = (byte)((uint)vvalue >> (8-endbit));
			if(bits >= 16)
			{
				buffer[ptr+2] = (byte)((uint)vvalue >> (16-endbit));
				if (bits >= 24)
				{
					buffer[ptr+3] = (byte)((uint)vvalue >> (24-endbit));
					if(bits >= 32)
					{
						if(endbit > 0)
							buffer[ptr+4] = (byte)((uint)vvalue >> (32-endbit));
						else
							buffer[ptr+4]=0;
					}
				}
			}
		}

		endbyte += bits / 8;
		ptr += bits/8;
		endbit = bits & 7;
	}

	internal int look(int bits)
	{
		int ret;
		uint m = mask[bits];

		bits += endbit;

		if(endbyte + 4 >= storage)
		{
			if(endbyte+(bits-1)/8 >= storage)
				return -1;
		}

		ret = ((buffer[ptr]) & 0xff) >> endbit;

		if(bits > 8)
		{
			ret |= ((buffer[ptr+1]) & 0xff) << (8 - endbit);
			if(bits > 16)
			{
				ret |= ((buffer[ptr+2])&0xff) << (16-endbit);
				if(bits > 24)
				{
					ret |= ((buffer[ptr+3])&0xff) << (24-endbit);
					if((bits > 32) && (endbit != 0))
					{
						ret |= ((buffer[ptr+4])&0xff) << (32-endbit);
					}
				}
			}
		}
		ret = (int)(m & ret);
		return ret;
	}

	internal int look1()
	{
		if(endbyte >= storage)
			return-1;
		return(buffer[ptr] >> endbit) & 1;
	}

	internal void adv(int bits)
	{
		bits += endbit;
		ptr += bits / 8;
		endbyte += bits / 8;
		endbit = bits & 7;
	}

	internal void adv1()
	{
		++endbit;
		if(endbit > 7)
		{
			endbit = 0;
			ptr++;
			endbyte++;
		}
	}

	internal int read(int bits)
	{
		int ret;
		uint m=mask[bits];

		bits += endbit;

		if(endbyte+4 >= storage)
		{
			ret = -1;
			if(endbyte + (bits-1)/8 >= storage)
			{
				ptr += bits/8;
				endbyte += bits/8;
				endbit = bits&7;
				return ret;
			}
		}

		ret = ((buffer[ptr]) & 0xff) >> endbit;
		if(bits > 8)
		{
			ret|=((buffer[ptr+1])&0xff)<<(8-endbit);
			if(bits > 16)
			{
				ret|=((buffer[ptr+2])&0xff)<<(16-endbit);
				if(bits > 24)
				{
					ret|=((buffer[ptr+3])&0xff)<<(24-endbit);

					if((bits > 32) && (endbit != 0))
					{
						ret|=((buffer[ptr+4])&0xff)<<(32-endbit);
					}
				}
			}
		}

		ret &= (int)m;

		ptr += bits/8;
		endbyte += bits/8;
		endbit = bits&7;
		return ret;
	}

	internal int read1()
	{
		int ret;
		if(endbyte>=storage)
		{
			ret = -1;
			endbit++;
			if(endbit > 7)
			{
				endbit = 0;
				ptr++;
				endbyte++;
			}
			return ret;
		}

		ret=(buffer[ptr] >> endbit) & 1;

		endbit++;
		if(endbit > 7)
		{
			endbit = 0;
			ptr++;
			endbyte++;
		}
		return ret;
	}

	internal int bytes()
	{
		return endbyte + (endbit+7)/8;
	}

	internal int bits()
	{
		return endbyte * 8+endbit;
	}

	internal static int ilog(int v)
	{
		int ret=0;
		while(v > 0)
		{
			ret++;
			v >>= 1;
		}
		return ret;
	}

	internal byte[] buf()
	{
		return buffer;
	}

	internal csBuffer()
	{
		// Really a noop?
	}
}
