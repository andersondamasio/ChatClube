using System;
using System.Collections.Generic;
using System.Text;

namespace com.chatclube.Utils
{
    public static class Geral
    {
        public static bool IsDebug()
        {
            return false;


#if DEBUG
            return true;
#endif
            return false;
        }
  
    }
}
