using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    public class TagLine
    {
        public  const string SEP = "\t";
        public  const string ENDL = "\n";
        private string _tag = null;

        public string Tag
        {
            get { return _tag; }
            set { _tag = value.Trim().Replace(SEP, "_"); }
        }
        private string _value = null;

        public string Value
        {
            get { return this._value; }
            set { this._value = value.Trim().Replace(SEP, "_"); }
        }

        public TagLine()
        {

        }

        public TagLine(string tag, string value)
        {
            this._tag = tag; this._value = value;
        }

        public bool IsNull()
        {
            if (this._tag == null || this._value == null) return true;
            return false;
        }

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

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Tag);
            sb.Append(TagLine.SEP);
            sb.Append(this.Value);
            return sb.ToString();
        }

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
