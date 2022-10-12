using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    public static class FinancialYearHandler
    {

        public static List<string> getFinancialYear()
        {
            List<string> financialYears = new List<string>();
            SqlConnection connection = Constants.con;
            Constants.opencon();
            string cmdstring = "exec sp_getFinancialYear";
            
            SqlCommand cmd = new SqlCommand(cmdstring, connection);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {
                        financialYears.Add(dr["FinancialYear"].ToString());
                    }
                }

                catch(Exception e)
                {
                    return null;
                }
            }

                return financialYears;
          //  Constants.closecon();
        }
    }
}
