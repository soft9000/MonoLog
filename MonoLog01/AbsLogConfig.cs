using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzLog
{
    /// <summary>
    /// The idea is to allow many logger types. Here is the envisioned minimum set of 'ops.
    /// </summary>
    public abstract class AbsLogConfig
    {
        /// <summary>
        /// A unique alphanumeric name / token for the confihuration. 
        /// </summary>
        /// <returns>The unique configuration name</returns>
        abstract public string GetConfigName();

        /// <summary>
        /// The fully qualified path and name for the configuration file.
        /// </summary>
        /// <returns>The configuration-file name.</returns>
        abstract public string GetConfigFile();

        /// <summary>
        /// The fully qualified path and name for the log file.
        /// </summary>
        /// <returns>The log-file name.</returns>
        abstract public string GetLogFile();

        /// <summary>
        /// Called every time a date and time string is required.
        /// </summary>
        /// <returns>Present time, formatted as desired.</returns>
        abstract public string GetTime();
    }
}
