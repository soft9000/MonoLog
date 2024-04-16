using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzLog
{
    public abstract class AbsLogConfig
    {
        abstract public string GetConfigName();
        abstract public string GetConfigFile();
        abstract public string GetLogFile();
        abstract public string GetTime();
    }
}
