using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTicker.Interfaces
{
    public interface ITickerFactory
    {
        ITicker GetTicker(string name);
    }
}
