using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANRPC_Inventory
{
    class CurrencyData3
    {
        string name;
        string officialName;
        double exchangeRate;
        double inverseRate;

        public CurrencyData3(string name, string officialName, double exchangeRate, double inverseRate)
        {
            this.name = name;
            this.officialName = officialName;
            this.exchangeRate = exchangeRate;
            this.inverseRate = inverseRate;
        }

        public string getName()
        {
            return this.name;
        }
        public string getOfficialName()
        {
            return this.officialName;
        }
        public double getExchangeRate()
        {
            return this.exchangeRate;
        }
        public double getInverseRate()
        {
            return this.inverseRate;
        }
    }
}
