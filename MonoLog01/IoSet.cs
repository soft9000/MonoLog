using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzLog
{
    /// <summary>
    /// Allow customization of the standard input & output streams.
    /// </summary>
    public class IoSet
    {
        private System.IO.TextReader _In;
        private System.IO.TextWriter _Out;
        private System.IO.TextWriter _Error;

        public IoSet()
        {
            this._In = Console.In;
            this._Out = Console.Out;
            this._Error = Console.Error;
        }

        #region Validating IO Setters
        public bool SetInput(System.IO.TextReader aval)
        {
            if (aval == null)
            {
                return false;
            }
            this._In = aval;
            return true;
        }

        public bool SetOutput(System.IO.TextWriter aval)
        {
            if (aval == null)
            {
                return false;
            }
            this._Out = aval;
            return true;
        }

        public bool SetError(System.IO.TextWriter aval)
        {
            if (aval == null)
            {
                return false;
            }
            this._Error = aval;
            return true;
        }
        #endregion

        #region IO Readers
        public System.IO.TextWriter Out
        {
            get { return _Out; }
        }

        public System.IO.TextReader In
        {
            get { return _In; }
        }

        public System.IO.TextWriter Error
        {
            get { return _Error; }
        }
        #endregion

    }
}
