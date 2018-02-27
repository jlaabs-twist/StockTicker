using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StockTicker.Interfaces;
using System.Reactive.Subjects;

namespace StockTicker.Models
{
    public class TickerModel : ITicker, IDisposable
    {
        const int RefreshTime = 250;
        string _name;
        int _price;
        bool _stop = false;
        Subject<int> _priceChanged;

        public string Name
        {
            get { return _name; }
        }

        public int Price
        {
            get { return _price; }
        }

        public IObservable<int> PriceChanged
        {
            get { return _priceChanged; }
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
            _priceChanged = new Subject<int>();

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
                ChangePrice(priceChange);

                Thread.Sleep(RefreshTime);
            }
        }        

        void ChangePrice(int priceChange)
        {
            _priceChanged.OnNext(priceChange);
        }

        public void Dispose()
        {
            _stop = true;
        }        
    }
}
