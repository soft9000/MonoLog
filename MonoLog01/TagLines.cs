using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// Simple collection wrapper.
    /// User editable "just data" collection of tag:value definitions.  
    /// Comments presently unsupported. A little overkill on the sanity 
    /// checking - it's a maintenance thing.
    /// </summary>
    public class TagLines : IEnumerable, IComparable
    {
        private List<TagLine> lines = new List<TagLine>();

        /// <summary>
        /// List enumerator.
        /// </summary>
        /// <returns>Enumerator to the encapsulated TagLine Collection.</returns>
        public IEnumerator GetEnumerator() {
            return lines.GetEnumerator();
        }

        /// <summary>
        /// See if anything is stored in the TagLine Collection.
        /// </summary>
        /// <returns>True if the colleciton is empty.</returns>
        public bool IsNull() {
            return lines.Count == 0;
        }

        /// <summary>
        /// Create a parasable, representational string for this tag-value pair.
        /// Newlines ARE added.
        /// </summary>
        /// <returns>The parasable, representational string.</returns>
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

        /// <summary>
        /// Add a UNIQUELY TAGGED TagLine to the Collection. Will not add a duplicate TAGgged item.
        /// </summary>
        /// <param name="newline">The new line to add. Need neither be null, nor sane, to be added.</param>
        /// <returns>False usually means the TAG is duplicate. Use Get() to check.</returns>
        public bool Add(TagLine newline)
        {
            foreach(TagLine line in lines) {
                if (line.Tag.Equals(newline.Tag))
                    return false;
            }
            lines.Add(newline);
            return true;
        }

        /// <summary>
        /// Retrieve a TagLine instance by TAG NAME.
        /// </summary>
        /// <param name="tag">The Tag name.</param>
        /// <returns>Null if there is no item so tagged in the Collection.</returns>
        public TagLine Get(string tag)
        {
            List<string> list = new List<string>();
            foreach (TagLine line in lines)
            {
                if(line.Tag.Equals(tag)) return line;
            }
            return null;
        }

        /// <summary>
        /// Get all of the TAG NAMEs in the Collection.
        /// </summary>
        /// <returns>Results will be empty, or full of Tag names.</returns>
        public string[] GetTags()
        {
            List<string> list = new List<string>();
            foreach (TagLine line in lines) {
                list.Add(line.Tag);
            }
            return list.ToArray();
        }

        /// <summary>
        /// Save the ToString() representation to a file. Insane items will be ignored.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="lines"></param>
        /// <returns>False if any items were able to be saved.</returns>
        public static bool FileWrite(string file, TagLines lines)
        {
            Boolean br = false;
            if (file == null || lines == null) return br;
            using (StreamWriter ofi = new StreamWriter(file, false))
            {
                foreach (TagLine line in lines)
                {
                    if (line.IsSane())
                    {
                        TUI.Message(line.ToString(), ofi);
                        br = true;
                    }
                }
                ofi.Flush();
                ofi.Close();
                if (br == false)
                {
                    File.Delete(file);
                }

            }
            return br;
        }

        /// <summary>
        /// Read the ToString() representation(s) from a file. Sanity checking.
        /// </summary>
        /// <param name="file">The name of the file to load.</param>
        /// <returns>Null is returned on error.</returns>
        public static TagLines FileRead(string file)
        {
            if (file == null) return null;
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

        /// <summary>
        /// Handy way to compare two objects. Supports eccentrically-ordered collections.
        /// </summary>
        /// <param name="obj">Any .Net Framework object.</param>
        /// <returns>Classic -1, 0, 1 results.</returns>
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
