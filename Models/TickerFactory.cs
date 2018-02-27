using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTicker.Interfaces;

namespace StockTicker.Models
{
    public class TickerFactory : ITickerFactory
    {
        public ITicker GetTicker(string name)
        {
            return new TickerModel(name);
        }
    }
}
