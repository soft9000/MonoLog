using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EzLog
{
    public abstract class AbsLogConfig
    {
        abstract public string getFilePath();
        abstract public string getDefaultTime();
    }
}
