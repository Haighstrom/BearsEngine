using System.IO;

namespace BearsEngine.Audio.OpenAL.OggVorbis;

    //CoakOgg shamlessly stolen from DragonOgg http://sourceforge.net/projects/dragonogg/?source=navbar, but uses only the interactive media player namespace
    internal class VorbisFile : AudioFile 
{
	internal static int CHUNKSIZE=8500;
	internal static int SEEK_SET=0;
	internal static int SEEK_CUR=1;
	internal static int SEEK_END=2;

	internal static int OV_FALSE=-1;
	internal static int OV_EOF=-2;
	internal static int OV_HOLE=-3;

	internal static int OV_EREAD=-128;
	internal static int OV_EFAULT=-129;
	internal static int OV_EIMPL=-130;
	internal static int OV_EINVAL=-131;
	internal static int OV_ENOTVORBIS=-132;
	internal static int OV_EBADHEADER=-133;
	internal static int OV_EVERSION=-134;
	internal static int OV_ENOTAUDIO=-135;
	internal static int OV_EBADPACKET=-136;
	internal static int OV_EBADLINK=-137;
	internal static int OV_ENOSEEK=-138;

        internal bool skable = false;
        internal long offset;

        internal int links;
        internal long[] offsets;
        internal long[] dataoffsets;
        internal int[] serialnos;
        internal long[] pcmlengths;
        internal Info[] vi;
        internal Comment[] vc;

	// Decoding working state local storage


	private VorbisFile()
	{
	}

	internal VorbisFile(string file) : this()
	{
		FileStream inst=null;
		
		try{ inst=new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);}
		catch(Exception e)
		{
			throw new csorbisException("VorbisFile: "+e.Message);
		}
		int ret=open(inst, null, 0);
		if(ret==-1)
		{
			throw new csorbisException("VorbisFile: open return -1");
		}
	}        

        internal int get_data(VorbisFileInstance instance)
	{
		int index=instance.oy.buffer(CHUNKSIZE);
            byte[] buffer = instance.oy.data;
		//  int bytes=callbacks.read_func(buffer, index, 1, CHUNKSIZE, datasource);
		int bytes=0;
		try
		{
			bytes=datasource.Read(buffer, index, CHUNKSIZE);
		}
		catch(Exception e)
		{
			Console.Error.WriteLine("OpenAL Exception in VorbisFile. get_data: " + e.Message);
			return OV_EREAD;
		}
            instance.oy.wrote(bytes);
		if(bytes==-1)
		{
			bytes=0;
		}
		return bytes;
	}

        internal void seek_helper(long offst, VorbisFileInstance instance)
	{
		//callbacks.seek_func(datasource, offst, SEEK_SET);
		fseek(datasource, offst, SEEK_SET);
		this.offset=offst;
		instance.oy.reset();
	}

        internal int get_next_page(Page page, long boundary, VorbisFileInstance instance)
	{
		if(boundary>0) boundary+=offset;
		while(true)
		{
			int more;
			if(boundary>0 && offset>=boundary)return OV_FALSE;
                more = instance.oy.pageseek(page);
			if(more<0){offset-=more;}
			else
			{
				if(more==0)
				{
					if(boundary==0)return OV_FALSE;
					//	  if(get_data()<=0)return -1;
					int ret=get_data(instance);
					if(ret==0) return OV_EOF;
					if(ret<0) return OV_EREAD; 
				}
				else
				{
					int ret=(int)offset; //!!!
					offset+=more;
					return ret;
				}
			}
		}
	}

        private int get_prev_page(Page page, VorbisFileInstance instance)
	{
		long begin=offset; //!!!
		int ret;
		int offst=-1;
		while(offst==-1)
		{
			begin-=CHUNKSIZE;
			if(begin<0)
				begin=0;
			seek_helper(begin, instance);
			while(offset<begin+CHUNKSIZE)
			{
				ret=get_next_page(page, begin+CHUNKSIZE-offset, instance);
				if(ret==OV_EREAD){ return OV_EREAD; }
				if(ret<0){ break; }
				else{ offst=ret; }
			}
		}
            seek_helper(offst, instance); //!!!
		ret=get_next_page(page, CHUNKSIZE, instance);
		if(ret<0)
		{
			//System.err.println("Missed page fencepost at end of logical bitstream Exiting");
			//System.exit(1);
			return OV_EFAULT;
		}
		return offst;
	}

        internal int bisect_forward_serialno(long begin, long searched, long end, int currentno, int m, VorbisFileInstance instance)
	{
		long endsearched=end;
		long next=end;
		Page page=new();
		int ret;

		while(searched<endsearched)
		{
			long bisect;
			if(endsearched-searched<CHUNKSIZE)
			{
				bisect=searched;
			}
			else
			{
				bisect=(searched+endsearched)/2;
			}

                seek_helper(bisect, instance);
                ret = get_next_page(page, -1, instance);
			if(ret==OV_EREAD) return OV_EREAD;
			if(ret<0 || page.serialno()!=currentno)
			{
				endsearched=bisect;
				if(ret>=0)next=ret;
			}
			else
			{
				searched=ret+page.header_len+page.body_len;
			}
		}
            seek_helper(next, instance);
            ret = get_next_page(page, -1, instance);
		if(ret==OV_EREAD) return OV_EREAD;

		if(searched>=end || ret==-1)
		{
			links=m+1;
			offsets=new long[m+2];
			offsets[m+1]=searched;
		}
		else
		{
			ret=bisect_forward_serialno(next, offset, end, page.serialno(), m+1, instance);
			if(ret==OV_EREAD)return OV_EREAD;
		}
		offsets[m]=begin;
		return 0;  
	}

	// uses the local ogg_stream storage in vf; this is important for
	// non-streaming input sources
        internal int fetch_headers(Info vi, Comment vc, int[] serialno, Page og_ptr, VorbisFileInstance instance)
	{
		//System.err.println("fetch_headers");
		Page og=new();
		Packet op=new();
		int ret;

		if(og_ptr==null)
		{
                ret = get_next_page(og, CHUNKSIZE, instance);
			if(ret==OV_EREAD)return OV_EREAD;
			if(ret<0) return OV_ENOTVORBIS;
			og_ptr=og;
		}
  
		if(serialno!=null)serialno[0]=og_ptr.serialno();

		instance.os.init(og_ptr.serialno());
  
		// extract the initial header from the first page and verify that the
		// Ogg bitstream is in fact Vorbis data
  
		vi.init();
		vc.init();
  
		int i=0;
		while(i<3)
		{
                instance.os.pagein(og_ptr);
			while(i<3)
			{
                    int result = instance.os.packetout(op);
				if(result==0)break;
				if(result==-1)
				{
					Console.Error.WriteLine("Corrupt header in logical bitstream.");
					//goto bail_header;
					vi.clear();
					vc.clear();
                        instance.os.clear();
					return -1;
				}
				if(vi.synthesis_headerin(vc, op)!=0)
				{
					Console.Error.WriteLine("Illegal header in logical bitstream.");
					//goto bail_header;
					vi.clear();
					vc.clear();
                        instance.os.clear();
					return -1;
				}
				i++;
			}
			if(i<3)
				if(get_next_page(og_ptr, 1, instance)<0)
				{
					Console.Error.WriteLine("Missing header in logical bitstream.");
					//goto bail_header;
					vi.clear();
					vc.clear();
                        instance.os.clear();
					return -1;
				}
		}
		return 0; 
	}

	// last step of the OggVorbis_File initialization; get all the
	// vorbis_info structs and PCM positions.  Only called by the seekable
	// initialization (local stream storage is hacked slightly; pay
	// attention to how that's done)
        internal void prefetch_all_headers(Info first_i, Comment first_c, int dataoffset,
            VorbisFileInstance instance)
	{
		Page og=new();
		int ret;
  
		vi=new Info[links];
		vc=new Comment[links];
		dataoffsets=new long[links];
		pcmlengths=new long[links];
		serialnos=new int[links];
  
		for(int i=0;i<links;i++)
		{
			if(first_i!=null && first_c!=null && i==0)
			{
				// we already grabbed the initial header earlier.  This just
				// saves the waste of grabbing it again
				// !!!!!!!!!!!!!
				vi[i]=first_i;
				//memcpy(vf->vi+i,first_i,sizeof(vorbis_info));
				vc[i]=first_c;
				//memcpy(vf->vc+i,first_c,sizeof(vorbis_comment));
				dataoffsets[i]=dataoffset;
			}
			else
			{
				// seek to the location of the initial header
                    seek_helper(offsets[i], instance); //!!!
                    if (fetch_headers(vi[i], vc[i], null, null, instance) == -1)
				{
					Console.Error.WriteLine("Error opening logical bitstream #"+(i+1)+"\n");
					dataoffsets[i]=-1;
				}
				else
				{
					dataoffsets[i]=offset;
					instance.os.clear();
				}
			}

			// get the serial number and PCM length of this link. To do this,
			// get the last page of the stream
			long end=offsets[i+1]; //!!!
                seek_helper(end, instance);

			while(true)
			{
				ret=get_prev_page(og, instance);
				if(ret==-1)
				{
					// this should not be possible
					Console.Error.WriteLine("Could not find last page of logical "+
						"bitstream #"+(i)+"\n");
					vi[i].clear();
					vc[i].clear();
					break;
				}
				if(og.granulepos()!=-1)
				{
					serialnos[i]=og.serialno();
					pcmlengths[i]=og.granulepos();
					break;
				}
			}
		}
	}

	int open_seekable()
	{
            VorbisFileInstance instance = makeInstance();
		Info initial_i=new();
		Comment initial_c=new();
		int serialno;
		long end;
		int ret;
		int dataoffset;
		Page og=new();
		// is this even vorbis...?
		int[] foo=new int[1];
            ret = fetch_headers(initial_i, initial_c, foo, null, instance);
		serialno=foo[0];
		dataoffset=(int)offset; //!!
		instance.os.clear();
		if(ret==-1)return(-1);
		// we can seek, so set out learning all about this file
		skable=true;
		//(callbacks.seek_func)(datasource, 0, SEEK_END);
		fseek(datasource, 0, SEEK_END);
		//offset=end=(callbacks.tell_func)(datasource);
		offset=ftell(datasource);
		end=offset;
		// We get the offset for the last page of the physical bitstream.
		// Most OggVorbis files will contain a single logical bitstream
		end=get_prev_page(og, instance);
		// moer than one logical bitstream?
		if(og.serialno()!=serialno)
		{
			// Chained bitstream. Bisect-search each logical bitstream
			// section.  Do so based on serial number only
			if(bisect_forward_serialno(0,0,end+1,serialno,0, instance)<0)
			{
				clear();
				return OV_EREAD;
			}
		}
		else
		{
			// Only one logical bitstream
			if(bisect_forward_serialno(0,end,end+1,serialno,0, instance)<0)
			{
				clear();
				return OV_EREAD;
			}
		}
		prefetch_all_headers(initial_i, initial_c, dataoffset, instance);
		//return(raw_seek(0));
            return 0;
	}

        internal VorbisFileInstance makeInstance()
        {
            VorbisFileInstance instance = new(this);
            return instance;
        }

        // CWL: This doesn't appear to be needed for most applications.  It
        // complicates having multiple instances, and so was removed.
	/*int open_nonseekable()
	{
		//System.err.println("open_nonseekable");
		// we cannot seek. Set up a 'single' (current) logical bitstream entry
		links=1;
		vi=new Info[links]; vi[0]=new Info(); // ??
		vc=new Comment[links]; vc[0]=new Comment(); // ?? bug?

		// Try to fetch the headers, maintaining all the storage
		int[]foo=new int[1];
		if(fetch_headers(vi[0], vc[0], foo, null)==-1)return(-1);
		current_serialno=foo[0];
		make_decode_ready();
		return 0;
	}*/

	//The helpers are over; it's all toplevel interface from here on out
	// clear out the OggVorbis_File struct
	int clear()
	{
		/*vb.clear();
		vd.clear();
		os.clear();*/
    
		if(vi!=null && links!=0)
		{
			for(int i=0;i<links;i++)
			{
				vi[i].clear();
				vc[i].clear();
			}
			vi=null;
			vc=null;
		}
		if(dataoffsets!=null)dataoffsets=null;
		if(pcmlengths!=null)pcmlengths=null;
		if(serialnos!=null)serialnos=null;
		if(offsets!=null)offsets=null;
		//oy.clear();
		//if(datasource!=null)(vf->callbacks.close_func)(vf->datasource);
		//memset(vf,0,sizeof(OggVorbis_File));
		return(0);
	}

        internal static int fseek(Stream fis,
		//int64_t off,
		long off,
		int whence)
	{
		if(fis.CanSeek == true)
		{
			try
			{
				if(whence==SEEK_SET)
				{
					fis.Seek(off, 0);
				}
				else if(whence==SEEK_END)
				{
					fis.Seek(fis.Length - off, 0);
				}
				else
				{
					Console.Error.WriteLine("seek: "+whence+" is not supported");
				}
			}
			catch(Exception e)
			{
				Console.Error.WriteLine(e.Message);
			}
			return 0;
		}
		try
		{
			if(whence==0){ fis.Seek(0, 0); }
			fis.Seek(off, 0);
		}
		catch(Exception e)
		{
			Console.Error.WriteLine(e.Message);
			return -1;
		}
		return 0;
	}

        static long ftell(Stream fis)
        {
            if (fis.CanSeek == true)
            {
                return (fis.Position);
            }
            return 0;
        }

	// inspects the OggVorbis file and finds/documents all the logical
	// bitstreams contained in it.  Tries to be tolerant of logical
	// bitstream sections that are truncated/woogie. 
	//
	// return: -1) error
	//          0) OK

	int open(Stream iis, byte[] initial, int ibytes)
	{
		return open_callbacks(iis, initial, ibytes);
	}

	int open_callbacks(Stream iis, byte[] initial, int ibytes)
	{
		int ret;
		datasource=iis;
		//callbacks = _callbacks;
		// init the framing state
            //instance.oy.init();

		// perhaps some data was previously read into a buffer for testing
		// against other stream types.  Allow initialization from this
		// previously read data (as we may be reading from a non-seekable
		// stream)
		if(initial!=null)
		{
                // CWL: Doesn't seem needed
                /*int index = instance.oy.buffer(ibytes);
                Array.Copy(initial, 0, instance.oy.data, index, ibytes);
                instance.oy.wrote(ibytes);*/
		}
		// can we seek? Stevens suggests the seek test was portable
		if(iis.CanSeek == true)
            { 
                ret=open_seekable(); 
            }
		else
            {
                // CWL: Disabled since they're no longer used
                throw new NotImplementedException("Non-seekable streams are not implemented");
                //ret=open_nonseekable(); 
            }
		if(ret!=0)
		{
			datasource=null;
			clear();
		}
		return(ret);
	}

	// How many logical bitstreams in this physical bitstream?
        internal int streams()
	{
		return links;
	}

	// Is the FILE * associated with vf seekable?
        internal bool seekable()
	{
		return skable;
	}

	// returns the bitrate for a given logical bitstream or the entire
	// physical bitstream.  If the file is open for random access, it will
	// find the *actual* average bitrate.  If the file is streaming, it
	// returns the nominal bitrate (if set) else the average of the
	// upper/lower bounds (if set) else -1 (unset).
	// 
	// If you want the actual bitrate field settings, get them from the
	// vorbis_info structs

        internal int bitrate(int i)
	{
		if(i>=links)return(-1);
		if(!skable && i!=0)return(bitrate(0));
		if(i<0)
		{
			long bits=0;
			for(int j=0;j<links;j++)
			{
				bits+=(offsets[j+1]-dataoffsets[j])*8;
			}
			return((int)Math.Round(bits/time_total(-1)));
		}
		else
		{
			if(skable)
			{
				// return the actual bitrate
				return((int)Math.Round((offsets[i+1]-dataoffsets[i])*8/time_total(i)));
			}
			else
			{
				// return nominal if set
				if(vi[i].bitrate_nominal>0)
				{
					return vi[i].bitrate_nominal;
				}
				else
				{
					if(vi[i].bitrate_upper>0)
					{
						if(vi[i].bitrate_lower>0)
						{
							return (vi[i].bitrate_upper+vi[i].bitrate_lower)/2;
						}
						else
						{
							return vi[i].bitrate_upper;
						}
					}
					return(-1);
				}
			}
		}
	}

	// returns: total raw (compressed) length of content if i==-1
	//          raw (compressed) length of that logical bitstream for i==0 to n
	//          -1 if the stream is not seekable (we can't know the length)

        internal long raw_total(int i)
	{
		if(!skable || i>=links)return(-1);
		if(i<0)
		{
			long acc=0;               // bug?
			for(int j=0;j<links;j++)
			{
				acc+=raw_total(j);
			}
			return(acc);
		}
		else
		{
			return(offsets[i+1]-offsets[i]);
		}
	}

	// returns: total PCM length (samples) of content if i==-1
	//          PCM length (samples) of that logical bitstream for i==0 to n
	//          -1 if the stream is not seekable (we can't know the length)
        internal long pcm_total(int i)
	{
		if(!skable || i>=links)return(-1);
		if(i<0)
		{
			long acc=0;
			for(int j=0;j<links;j++)
			{
				acc+=pcm_total(j);
			}
			return(acc);
		}
		else
		{
			return(pcmlengths[i]);
		}
	}

	// returns: total seconds of content if i==-1
	//          seconds in that logical bitstream for i==0 to n
	//          -1 if the stream is not seekable (we can't know the length)
        internal float time_total(int i)
	{
		if(!skable || i>=links)return(-1);
		if(i<0)
		{
			float acc=0;
			for(int j=0;j<links;j++)
			{
				acc+=time_total(j);
			}
			return(acc);
		}
		else
		{
			return((float)(pcmlengths[i])/vi[i].rate);
		}
	}

	// tell the current stream offset cursor.  Note that seek followed by
	// tell will likely not give the set offset due to caching
        internal long raw_tell()
	{
		return(offset);
	}       

        internal Info[] getInfo() { return vi; }
        internal Comment[] getComment() { return vc; }
        internal int host_is_big_endian()
        {
            return 0;
            //the above isn't really right...
        }
	//IDisposable implementation
        internal override void Dispose()
	{
		datasource.Close();
		datasource.Dispose();
		datasource = null;
	}
}
