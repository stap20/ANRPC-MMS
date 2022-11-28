using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    internal class TimeLineCircleDetails
    {
        public DrawedCircleText mainText { get; set; }

        public DrawedCircleText circleDetailsText { get; set; }

        public int donePercent { get; set; }

        public bool isDone { get; set; }

        public int duration { get; set; }

    }
}
