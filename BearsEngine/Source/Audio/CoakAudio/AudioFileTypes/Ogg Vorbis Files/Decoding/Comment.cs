﻿using System.Text;


namespace BearsEngine.Audio.OpenAL.OggVorbis;

    // the comments are not part of vorbis_info so that vorbis_info can be
    // static storage
    internal class Comment
{
	private static string _vorbis ="vorbis";

	//private static int OV_EFAULT=-129;
	private static int OV_EIMPL=-130;

	// unlimited user comment fields.  libvorbis writes 'libvorbis'
	// whatever vendor is set to in encode
	internal byte[][] user_comments;
	internal int[] comment_lengths; 
	internal int comments;
	internal byte[] vendor;

	internal void init()
	{
		user_comments=null;
		comments=0;
		vendor=null;
	}

	internal void add(string comment)
	{
		Encoding AE = Encoding.UTF8;
		byte[] comment_byt = AE.GetBytes(comment);
		add(comment_byt);
	}

	private void add(byte[] comment)
	{
		byte[][] foo=new byte[comments+2][];
		if(user_comments!=null)
		{
			Array.Copy(user_comments, 0, foo, 0, comments);
		}
		user_comments=foo;

		int[] goo=new int[comments+2];
		if(comment_lengths!=null)
		{
			Array.Copy(comment_lengths, 0, goo, 0, comments);
		}
		comment_lengths=goo;

		byte[] bar=new byte[comment.Length+1];
		Array.Copy(comment, 0, bar, 0, comment.Length);
		user_comments[comments]=bar;
		comment_lengths[comments]=comment.Length;
		comments++;
		user_comments[comments]=null;
	}

	internal void add_tag(string tag, string contents)
	{
		if(contents==null) contents="";
		add(tag+"="+contents);
	}
        	
 
	// This is more or less the same as strncasecmp - but that doesn't exist
	// * everywhere, and this is a fairly trivial function, so we include it
	static bool tagcompare(byte[] s1, byte[] s2, int n)
	{
		int c=0;
		byte u1, u2;
		while(c < n)
		{
			u1=s1[c]; u2=s2[c];
			if(u1>='A')u1=(byte)(u1-'A'+'a');
			if(u2>='A')u2=(byte)(u2-'A'+'a');
			if(u1!=u2){ return false; }
			c++;
		}
		return true;
	}

	internal string query(string tag)
	{
		return query(tag, 0);
	}

	internal string query(string tag, int count)
	{
		Encoding AE = Encoding.UTF8;
		byte[] tag_byt = AE.GetBytes(tag);
		
		int foo=query(tag_byt, count);
		if(foo==-1)return null;
		byte[] comment=user_comments[foo];
		for(int i=0; i<comment_lengths[foo]; i++)
		{
			if(comment[i]=='=')
			{
				char[] comment_uni = AE.GetChars(comment);
				return new string(comment_uni, i+1, comment_lengths[foo]-(i+1));
			}
		}
		return null;
	}

	private int query(byte[] tag, int count)
	{
		int i=0;
		int found = 0;
		int taglen = tag.Length;
		byte[] fulltag = new byte[taglen+2];
		Array.Copy(tag, 0, fulltag, 0, tag.Length);
		fulltag[tag.Length]=(byte)'=';

		for(i=0;i<comments;i++)
		{
			if(tagcompare(user_comments[i], fulltag, taglen))
			{
				if(count==found)
				{
					// We return a pointer to the data, not a copy
					//return user_comments[i] + taglen + 1;
					return i;
				}
				else{ found++; }
			}
		}
		return -1;
	}

	internal int unpack(csBuffer opb)
	{
		int vendorlen=opb.read(32);
		if(vendorlen<0)
		{
			//goto err_out;
			clear();
			return-1;
		}
		vendor=new byte[vendorlen+1];
		opb.read(vendor,vendorlen);
		comments=opb.read(32);
		if(comments<0)
		{
			//goto err_out;
			clear();
			return-1;
		}
		user_comments=new byte[comments+1][];
		comment_lengths=new int[comments+1];
    
		for(int i=0;i<comments;i++)
		{
			int len=opb.read(32);
			if(len<0)
			{
				//goto err_out;
				clear();
				return-1;
			}
			comment_lengths[i]=len;
			user_comments[i]=new byte[len+1];
			opb.read(user_comments[i], len);
		}	  
		if(opb.read(1)!=1)
		{
			//goto err_out; // EOP check
			clear();
			return-1;

		}
		return 0;
		//  err_out:
		//    comment_clear(vc);
		//    return(-1);
	}

	int pack(csBuffer opb)
	{
            string temp ="Xiphophorus libVorbis I 20000508";

		Encoding AE = Encoding.UTF8;
		byte[] temp_byt = AE.GetBytes(temp);
		byte[] _vorbis_byt = AE.GetBytes(_vorbis);
		
		// preamble
		opb.write(0x03,8);
		opb.write(_vorbis_byt);

		// vendor
		opb.write(temp.Length,32);
		opb.write(temp_byt);

		// comments

		opb.write(comments,32);
		if(comments!=0)
		{
			for(int i=0;i<comments;i++)
			{
				if(user_comments[i]!=null)
				{
					opb.write(comment_lengths[i],32);
					opb.write(user_comments[i]);
				}
				else
				{
					opb.write(0,32);
				}
			}
		}
		opb.write(1,1);
		return 0;
	}

	internal int header_out(Packet op)
	{
		csBuffer opb=new();
		opb.writeinit();

		if(pack(opb)!=0) return OV_EIMPL;

		op.packet_base = new byte[opb.bytes()];
		op.packet=0;
		op.bytes=opb.bytes();
		Array.Copy(opb.buf(), 0, op.packet_base, 0, op.bytes);
		op.b_o_s=0;
		op.e_o_s=0;
		op.granulepos=0;
		return 0;
	}
 
	internal void clear()
	{
		for(int i=0;i<comments;i++)
			user_comments[i]=null;
		user_comments=null;
		vendor=null;
	}

	internal string getVendor()
	{
		Encoding AE = Encoding.UTF8;
		char[] vendor_uni = AE.GetChars(vendor);
		return new string(vendor_uni);
	}

	internal string getComment(int i)
	{
		Encoding AE = Encoding.UTF8;
		if(comments<=i)return null;
		
		char[] user_comments_uni = AE.GetChars(user_comments[i]);
		return new string(user_comments_uni);
	}

	internal string toString()
	{
		Encoding AE = Encoding.UTF8;
            string long_string = "Vendor: " + new string(AE.GetChars(vendor));

		for(int i=0; i < comments; i++)
			long_string = long_string + "\nComment: " + new string(AE.GetChars(user_comments[i]));
		
		long_string = long_string + "\n";

		return long_string;
	}
}
