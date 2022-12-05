using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    class UnitsHandler
    {
        public static List<string> getUnits()
        {
            List<string> units = new List<string>();
            SqlConnection connection = Constants.con;
            Constants.opencon();
            string cmdstring = "exec sp_getunits";

            SqlCommand cmd = new SqlCommand(cmdstring, connection);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows == true)
            {
                try
                {
                    while (dr.Read())
                    {
                        units.Add(dr["eng_unit"].ToString());
                    }
                }

                catch (Exception e)
                {
                    return null;
                }
            }

            return units;
            //  Constants.closecon();
        }
    }
}
