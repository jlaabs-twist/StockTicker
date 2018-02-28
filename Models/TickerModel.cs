using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StockTicker.Interfaces;
using System.Reactive.Subjects;
using System.Reactive.Linq;

namespace StockTicker.Models
{
    public class TickerModel : ITicker
    {
        const int RefreshTime = 250;
        string _name;
        int _price;
        IObservable<int> _priceChanged;

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

            _priceChanged = Observable.Timer(TimeSpan.FromSeconds(0),
                TimeSpan.FromMilliseconds(RefreshTime)).Select((x) => rnd.Next(-10, 10));

            _priceChanged.Subscribe(x => _price += x);
        }      
    }
}
