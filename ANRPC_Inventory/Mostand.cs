using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    public class Mostand
    {
        public string displayName { get; set; }
        public string formName { get; set; }
        public int formNo { get; set; }
        public bool isForeign { get; set; }

        public Mostand(string displayName, string formName,int formNo,bool isForeign = false)
        {
            this.displayName = displayName;
            this.formName = formName;
            this.formNo = formNo;
            this.isForeign = isForeign;
        }

    }
}
