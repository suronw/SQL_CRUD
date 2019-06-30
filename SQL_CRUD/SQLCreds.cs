using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL_CRUD
{
    public static class SQLCreds
    {
        public static string Instance = @"";
        public static bool Integrated_Security = true;
        public static string User_Name = @"";
        public static string Password = "";
        //public static string Database = "";
        public static string Database = ".";
        public static string Connection_String
        {
            get
            {
                if (Integrated_Security)
                {
                    //return $@"Data Source=.\{ Instance };Initial Catalog={ Database };Integrated Security={ Integrated_Security }";
                    return $@"Data Source={ Instance };Initial Catalog={ Database };Integrated Security={ Integrated_Security }";
                }
                else
                {
                    return $@"Data Source={ Instance };Initial Catalog={ Database };User ID={ User_Name };Password={ Password }";

                }
            }
        }

    }
}
