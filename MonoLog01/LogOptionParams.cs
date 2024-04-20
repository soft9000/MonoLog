using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EzLog
{
    /// <summary>
    /// Used by LogOption 'delegates to perform add-on 'Ops.
    /// </summary>
    public class LogOptionParams : IoSet
    {

        internal int whence;
        internal string option;
        internal string[] opts;

        /// <summary>
        /// Wrappering up the parameters for easier testing & maintenance.
        /// </summary>
        /// <param name="where">Location of the string in the array.</param>
        /// <param name="sFlag">The parameter's string</param>
        /// <param name="opts">The inclusive set of command-line parameters.</param>
        public LogOptionParams(int whence, string sFlag, string[] opts) : base()
        {
            this.whence = whence;
            if (sFlag == null)
            {
                sFlag = "";
            }
            if (opts == null)
            {
                opts = new String[0];
            }
            this.option = sFlag;
            this.opts = opts;
        }

        /// <summary>
        /// Sanity allows us to use this instance as required.
        /// </summary>
        /// <returns>False == unsane.</returns>
        public bool IsSane()
        {
            if(whence < 0 || option == null || opts == null)
            {
                return false;
            }
            return true;
        }

    }
}
