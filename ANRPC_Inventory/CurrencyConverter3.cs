using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Xml;

namespace ANRPC_Inventory
{
    class CurrencyConverter3
    {
        static Dictionary<string, CurrencyData3> currency = new Dictionary<string, CurrencyData3>();
        //static string src_currencyUrl = @"http://www.floatrates.com/daily/"; //get currencies based on egp currency
        //static XmlDocument xml_currencyPage = new XmlDocument();

        private static void loadCurrencyData(string refrenceCurrencyName)
        {
            //xml_currencyPage.Load(src_currencyUrl + refrenceCurrencyName + ".xml");
        }

        public static void init(string refrenceCurrencyName = "egp")
        {
            currency.Clear();
            //loadCurrencyData(refrenceCurrencyName.ToLower());
            Constants.opencon();

            try
            {
                string cmdstring = "SELECT * FROM Currency";
                SqlCommand cmd3 = new SqlCommand(cmdstring, Constants.con);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    string name = dr3["name"].ToString();
                    string officialName = dr3["officialName"].ToString();
                    double exchange = Convert.ToDouble(dr3["inverseRate"].ToString());
                    double invExchange = Convert.ToDouble(dr3["exchangeRate"].ToString());
                    CurrencyData3 currency_data = new CurrencyData3(name, officialName, invExchange, exchange);
                    currency[name] = currency_data;
                }
                /*
                 * item because currency data stored in xml page in tag with name item
                 * baseCurrency refer to tag stored on it in base currency
                 * targetCurrency refer to tag stored on it in traget currency
                 * exchangeRate refer to tag stored on it in exchange rate
                 * inverseRate refer to tag stored on it in inverse rate
                 * 
                 * here we stored inverse currency as exchange because we have base currency 
                 * all of that relative to so we need to reverse them
                 */
                /* foreach (XmlNode node in xml_currencyPage.DocumentElement.SelectNodes("item"))
                 {
                     string target_currency = node.SelectSingleNode("targetCurrency").InnerText.ToLower();
                     string target_officialName = node.SelectSingleNode("targetName").InnerText;
                     Console.WriteLine(node.SelectSingleNode("exchangeRate").InnerText);
                     double exchange = Convert.ToDouble(node.SelectSingleNode("exchangeRate").InnerText);
                     double invExchange = Convert.ToDouble(node.SelectSingleNode("inverseRate").InnerText);
                     CurrencyData3 currency_data = new CurrencyData3(target_currency, target_officialName, invExchange, exchange);

                     currency[target_currency] = currency_data;
                 }*/

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static double convertFromToCurrency(string fromCurrencyName, string toCurrencyName, double amount)
        {

            double res = 0.0;
            if (currency.ContainsKey(fromCurrencyName.ToLower()) && currency.ContainsKey(toCurrencyName.ToLower()))
            {
                res = convertFromCurrency(fromCurrencyName, amount);

                res = res * currency[toCurrencyName].getInverseRate();
            }
            else if (currency.ContainsKey(fromCurrencyName.ToLower()) && toCurrencyName.ToLower() == "egp")
            {
                res = convertFromCurrency(fromCurrencyName.ToLower(), amount);
            }
            else if (currency.ContainsKey(toCurrencyName.ToLower()) && fromCurrencyName.ToLower() == "egp")
            {
                res = convertToCurrency(toCurrencyName.ToLower(), amount);
            }

            return res;
        }

        public static double convertFromCurrency(string currencyName, double amount)
        {
            double res = 0;
            if (currency.ContainsKey(currencyName.ToLower()))
            {
                res = amount * currency[currencyName.ToLower()].getExchangeRate();
            }

            return res;
        }

        public static double convertToCurrency(string currencyName, double amount)
        {
            double res = 0.0;

            if (currency.ContainsKey(currencyName.ToLower()))
            {
                res = amount * currency[currencyName.ToLower()].getExchangeRate();
            }

            return res;
        }

        public static string getCurrencyName(string currencyName)
        {
            string res = "";
            if (currency.ContainsKey(currencyName.ToLower()))
            {
                res = currency[currencyName].getOfficialName();
            }

            return res;
        }

        public static object getCurrencyData(string currencyName)
        {
            CurrencyData3 res = null;
            if (currency.ContainsKey(currencyName.ToLower()))
            {
                res = currency[currencyName.ToLower()];
            }

            return res;
        }
    }
}
