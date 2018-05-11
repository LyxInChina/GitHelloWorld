using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogHelper
{
    public interface ILogHelper
    {
        void Info(string msg, Exception ex);
        void Warn(string msg, Exception ex);
        void Error(string msg, Exception ex);
    }
}
