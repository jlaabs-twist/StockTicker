using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace StockTicker.Models
{
    public class TickerModel:IDisposable
    {
        const int RefreshTime = 250;
        string _name;
        int _price;
        bool _stop = false;

        public event EventHandler<int> PriceChanged;

        public string Name
        {
            get { return _name; }
        }

        public int Price
        {
            get { return _price; }
        }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Stock Name</param>
        public TickerModel(string name)
        {
            _name = name;
            Random rnd = new Random();
            _price = rnd.Next(0, 1000);

            Task.Run(() => UpdatePrice());
        }       

        /// <summary>
        /// Loop to update the stock price
        /// </summary>
        void UpdatePrice()
        {
            Random rnd = new Random();
            while (!_stop)
            {
                int priceChange = rnd.Next(-10, 10);
                _price += priceChange;
                OnPriceChanged(priceChange);

                Thread.Sleep(RefreshTime);
            }
        }        

        void OnPriceChanged(int priceChange)
        {
            PriceChanged?.Invoke(this, priceChange);
        }

        public void Dispose()
        {
            _stop = true;
        }
    }
}
