using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    class TransHandler
    {
        public static List<string> getTrans()
        {
            List<string> Trans = new List<string>();
            SqlConnection connection = Constants.con;
            Constants.opencon();
            string cmdstring = "exec sp_getTrans";

            SqlCommand cmd = new SqlCommand(cmdstring, connection);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {
                        Trans.Add(dr["transName"].ToString());
                    }
                }

                catch (Exception e)
                {
                    return null;
                }
            }

            return Trans;
            //  Constants.closecon();
        }
    }
}
