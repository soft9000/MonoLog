using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// User editable "just data" definition. Comments presently unsupported.
    /// Inspired by our "TagValue" classes in Java. 
    /// (https://github.com/soft9000/com.soft9000b/tree/master/S9kGit/src/com/soft9000b/tv)
    /// </summary>
    public class TagLine
    {
        public  const string SEP = "\t";
        public  const string ENDL = "\n";
        private string _tag = null;
        private string _value = null;

        /// <summary>
        /// The tag integrity is automatically assured.
        /// </summary>
        public string Tag
        {
            get { return _tag; }
            set {
                if (value == null) value = "";
                _tag = value.Trim().Replace(SEP, "_");
                _tag = _tag.Trim().Replace(ENDL, "_");
            }
        }

        /// <summary>
        /// The value's integrity is automatically assured.
        /// </summary>
        public string Value
        {
            get { return _value; }
            set {
                if (value == null) value = ""; 
                _value = value.Trim().Replace(SEP, "_");
                _value = value.Trim().Replace(ENDL, "_");
            }
        }

        /// <summary>
        /// Default constructor IsNull().
        /// </summary>
        public TagLine()
        {

        }

        /// <summary>
        /// Constructor will auto-clean, as well.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        public TagLine(string tag, string value)
        {
            Tag = tag; Value = value;
        }

        /// <summary>
        /// Nulls cannot be written.
        /// </summary>
        /// <returns>True if either the tag, or the value, shoud not be written.</returns>
        public bool IsNull()
        {
            if (this._tag == null || this._value == null) return true;
            return false;
        }

        /// <summary>
        /// Humanly edited, anything is possible. Sane items should neither 
        /// contain separators, nor be null.
        /// </summary>
        /// <returns></returns>
        public bool IsSane()
        {
            if (IsNull()) return false;
            if (this._tag.Contains(SEP) || this._value.Contains(SEP))
            {
                return false;
            }
            if (this._tag.Contains(ENDL) || this._value.Contains(ENDL))
            {
                return false;
            } return true;
        }

        /// <summary>
        /// Create a parasable, representational string for this tag-value pair.
        /// Newline is not added.
        /// </summary>
        /// <returns>The parasable, representational string.</returns>
        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Tag);
            sb.Append(TagLine.SEP);
            sb.Append(this.Value);
            return sb.ToString();
        }

        /// <summary>
        /// Factory used it re-create the ToString representation.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool Parse(string line)
        {
            if (line == null || line.Length == 0) return false;
            line = line.Trim();
            string[] cols = line.Split(SEP.ToCharArray());
            if (cols.Length != 2) return false;
            this._tag = cols[0];
            this._value = cols[1];
            return true;
        }
    }
}
