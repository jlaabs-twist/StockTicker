using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using StockTicker.Models;

namespace StockTicker.ViewModels
{
    public class TickersViewModel
    {
        ObservableCollection<TickerViewModel> _tickers;

        string _newName;

        public ObservableCollection<TickerViewModel> Tickers
        {
            get { return _tickers; }
        }

        public string NewName
        {
            get { return _newName; }
            set
            {
                if(_newName != value)
                {
                    _newName = value;
                }
            }
        }

        public bool CanRemoveTicker
        {
            get { return _tickers.Count >= 1; }
        }

        public TickersViewModel()
        {
            _tickers = new ObservableCollection<TickerViewModel>();
            _newName = "Name";
        }

        public void AddTicker()
        {
            TickerModel ticker = new TickerModel(_newName);
            _tickers.Add(new TickerViewModel(ticker));
        }

        public bool CanAddTicker()
        {
            return true;
        }

        public void RemoveTicker()
        {
            int lastIndex = _tickers.Count - 1;
            _tickers[lastIndex].Dispose();
            _tickers.RemoveAt(lastIndex);
        }
    }
}
