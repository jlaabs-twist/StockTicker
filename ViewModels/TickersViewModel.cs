using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Models;

namespace ViewModels
{
    public class TickersViewModel
    {
        ObservableCollection<TickerViewModel> _tickers;
        ICommand _createTicker;

        string _newName;

        public ObservableCollection<TickerViewModel> Tickers
        {
            get { return _tickers; }
        }

        public string NewName
        {
            get { return _newName; }
        }

        public ICommand CreateTicker
        {
            get { return _createTicker; }
        }

        public TickersViewModel()
        {
            _tickers = new ObservableCollection<TickerViewModel>();
            _newName = "Name";
            _createTicker = new RelayCommand(param => AddTicker(), param =>CanAddTicker());
        }

        void AddTicker()
        {
            TickerModel ticker = new TickerModel(_newName, 300);
            _tickers.Add(new TickerViewModel(ticker));
        }

        bool CanAddTicker()
        {
            return true;
        }
    }
}
