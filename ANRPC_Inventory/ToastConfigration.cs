using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    internal class ToastConfigration
    {
        public string title { get; set; }
        public string description { get; set; }
        public FontAwesome.Sharp.IconChar icon { get; set; }
        public System.Drawing.Color bodyColor { get; set; }
        public System.Drawing.Color iconColor { get; set; }
        public System.Drawing.Color titleColor { get; set; }
        public System.Drawing.Color descriptionColor { get; set; }
    }
}
