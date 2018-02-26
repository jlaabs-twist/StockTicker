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
        ICommand _createTicker;
        ICommand _removeTicker;

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

        public ICommand CreateTicker
        {
            get { return _createTicker; }
        }

        public ICommand RemoveTicker
        {
            get { return _removeTicker; }
        }

        public TickersViewModel()
        {
            _tickers = new ObservableCollection<TickerViewModel>();
            _newName = "Name";
            _createTicker = new RelayCommand(param => AddTicker(), param =>CanAddTicker());
            _removeTicker = new RelayCommand(param => RemoveTickerExecute(), param => CanRemoveTicker());
        }

        void AddTicker()
        {
            TickerModel ticker = new TickerModel(_newName);
            _tickers.Add(new TickerViewModel(ticker));
        }

        bool CanAddTicker()
        {
            return true;
        }

        void RemoveTickerExecute()
        {
            int lastIndex = _tickers.Count - 1;
            _tickers[lastIndex].Dispose();
            _tickers.RemoveAt(lastIndex);
        }

        bool CanRemoveTicker()
        {
            return _tickers.Count >= 1;
        }
    }
}
