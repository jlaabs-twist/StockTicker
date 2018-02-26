using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using StockTicker.Models;
using StockTicker.Interfaces;
using Caliburn.Micro;


namespace StockTicker.ViewModels
{
    public class TickersViewModel: INotifyPropertyChanged
    {
        ITickerFactory _tickerFactory;
        ObservableCollection<TickerViewModel> _tickers;

        string _newName;

        public event PropertyChangedEventHandler PropertyChanged;

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

        public TickersViewModel(ITickerFactory tickerFactory)
        {
            _tickerFactory = tickerFactory;
            _tickers = new ObservableCollection<TickerViewModel>();
            _newName = "Name";
        }

        public void AddTicker()
        {
            ITicker ticker = _tickerFactory.GetTicker(_newName);
            _tickers.Add(new TickerViewModel(ticker));
            OnPropertyChanged("CanRemoveTicker");
        }

        public void RemoveTicker()
        {
            int lastIndex = _tickers.Count - 1;
            _tickers[lastIndex].Dispose();
            _tickers.RemoveAt(lastIndex);
            OnPropertyChanged("CanRemoveTicker");
        }

        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
