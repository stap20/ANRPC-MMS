using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANRPC_Inventory
{
    public partial class CurrencyConverter2 : Form
    {
        public CurrencyConverter2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fromCurrency = "EUR";
            string toCurrency = "USD";
            int amount = 1000;

            //
            // STEP 1 : Print all avaiable currencies on screen
            //

            // Get all available currency tags
           //
            /*
            string[] availableCurrency = CurrencyConverter.GetCurrencyTags();
            // Print currency tags comma seperated
            Console.WriteLine("Available Currencies");
            Console.WriteLine(string.Join(",", availableCurrency));
            Console.WriteLine("\n");

            //
            // STEP 2 : Allow the User to input the currency rates
            //

            Console.WriteLine("Insert Currency you want to convert FROM");
            fromCurrency = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("Insert Currency you want to convert TO");
            toCurrency = Console.ReadLine();
            Console.WriteLine("\n");
            */
            //
            // STEP 3 : Calculate and display the currency exchange rate
            //

            // Calls a method to get the exchange rate between 2 currencies
           double exchangeRate = CurrencyConverter.GetExchangeRate(fromCurrency, toCurrency, amount);
            // Print result of currency exchange
            //Console.WriteLine("FROM " + amount + " " + fromCurrency.ToUpper() + " TO " + toCurrency.ToUpper() + " = " + exchangeRate);
            MessageBox.Show(exchangeRate.ToString());
            // Wait for key press to close console window
           // Console.ReadLine();
        }
    }
}
