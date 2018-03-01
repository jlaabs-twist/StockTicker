using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTicker.Interfaces
{
    public interface ITicker : IDisposable
    {
        string Name { get; }
        int Price { get; }
        IObservable<int> PriceChanged { get; }
    }
}
