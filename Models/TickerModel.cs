using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Models
{
    public class TickerModel
    {
        string _name;
        int _price;

        public event EventHandler<int> PriceChanged;

        public string Name
        {
            get { return _name; }
        }

        public int Price
        {
            get { return _price; }
        }

        public bool Stop { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Stock Name</param>
        /// <param name="refreshTime">Refresh time in mS</param>
        public TickerModel(string name, int refreshTime)
        {
            _name = name;
            Random rnd = new Random();
            _price = rnd.Next(0, 1000);

            Task.Run(() => UpdatePrice(refreshTime));
        }

        /// <summary>
        /// Loop to update the stock price
        /// </summary>
        /// <param name="refreshTime">Time for updating stock in mS</param>
        void UpdatePrice(int refreshTime)
        {
            Random rnd = new Random();
            while (!Stop)
            {
                int priceChange = rnd.Next(-10, 10);
                _price += priceChange;
                OnPriceChanged(priceChange);

                Thread.Sleep(refreshTime);
            }
        }

        void OnPriceChanged(int priceChange)
        {
            PriceChanged?.Invoke(this, priceChange);
        }
    }
}
