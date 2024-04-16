using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    public class TagLines : IEnumerable, IComparable
    {
        private List<TagLine> lines = new List<TagLine>();

        public IEnumerator GetEnumerator() {
            return lines.GetEnumerator();
        }

        public bool IsNull() {
            return lines.Count == 0;
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TagLine line in lines)
            {
                sb.Append(line.ToString());
                sb.Append(TagLine.ENDL);
            }
            return sb.ToString();
        }

        public bool Add(TagLine newline)
        {
            foreach(TagLine line in lines) {
                if (line.Tag.Equals(newline.Tag))
                    return false;
            }
            lines.Add(newline);
            return true;
        }

        public TagLine Get(string tag)
        {
            List<string> list = new List<string>();
            foreach (TagLine line in lines)
            {
                if(line.Tag.Equals(tag)) return line;
            }
            return null;
        }

        public string[] GetTags()
        {
            List<string> list = new List<string>();
            foreach (TagLine line in lines) {
                list.Add(line.Tag);
            }
            return list.ToArray();
        }

        public static bool FileWrite(string file, TagLines lines)
        {
            Boolean br = false;
            using (StreamWriter ofi = new StreamWriter(file, false))
            {
                foreach (TagLine line in lines)
                {
                    ofi.WriteLine(line.ToString());
                }
                ofi.Flush();
                ofi.Close();
                br = true;
            }
            return br;
        }

        public static TagLines FileRead(string file)
        {
            TagLines result = new TagLines();
            using (StreamReader ifi = new StreamReader(file))
            {
                string line = "";
                while (line != null)
                {
                    line = ifi.ReadLine();
                    if (line == null)
                    {
                        ifi.Close();
                        return result;
                    }
                    TagLine tline = new TagLine();
                    if (tline.Parse(line) == true)
                    {
                        if (tline.IsSane()) result.Add(tline);
                    }
                }

            }
            return null;
        }

        public int CompareTo(object obj)
        {
            if (obj is TagLines)
            {
                TagLines that = (TagLines)obj;
                if (this.lines.Count != that.lines.Count) 
                    return that.lines.Count - this.lines.Count;
                foreach(TagLine line in this.lines) {
                    TagLine thatL = that.Get(line.Tag);
                    {
                        int icomp = thatL.ToString().CompareTo(line.ToString());
                        if (icomp != 0)
                        {
                            return icomp;
                        }
                    }
                }
                return 0;
            }
            
            return obj.ToString().CompareTo(this.ToString());
        }
    }
}
