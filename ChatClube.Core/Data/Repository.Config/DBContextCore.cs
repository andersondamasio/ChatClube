using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.chatclube.Repository.Config
{
    public class DBContextCore
    {
        public static string DBPath;
        public static DBContextType DBContextType;
    }

    [Flags]
    public enum DBContextType
    {
        SQLite,
        SQLServer
    }
}
