using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANRPC_Inventory
{
    public static class HelperClass
    {
        public static void comboBoxFiller(ComboBox combo, List<string> source, string displayMember, String valueMember,Form form )
        {
            combo.Items.Clear();
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
            combo.DataSource = source;
            combo.BindingContext = form.BindingContext;
        }
    }
}
