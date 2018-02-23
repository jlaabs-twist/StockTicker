using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using Models;

namespace ViewModels
{
    public class TickerViewModel: IDisposable, INotifyPropertyChanged
    {
        TickerModel _ticker;

        string _name;
        int _price;
        int _priceChange;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get { return _name; }
        }

        public int Price
        {
            get { return _price; }
            private set
            {
                if(_price != value)
                {
                    _price = value;
                    OnPropertyChanged("Price");
                    OnPropertyChanged("Color");
                }
            }
        }

        public int PriceChange
        {
            get { return _priceChange; }
            private set
            {
                if(_priceChange != value)
                {
                    _priceChange = value;
                    OnPropertyChanged("PriceChange");
                }
            }
        }

        public string Color
        {
            get
            {
                if(_priceChange < 0)
                {
                    return "Red";
                }
                else if (_priceChange > 0)
                {
                    return "Green";
                }
                return "Gray";
            }
        }

        public TickerViewModel(TickerModel ticker)
        {
            _ticker = ticker;
            _name = _ticker.Name;
            _price = _ticker.Price;
            _priceChange = 0;

            _ticker.PriceChanged += Price_Changed;
        }        

        public void Price_Changed(object sender, int priceChange)
        {
            PriceChange = priceChange;
            Price += priceChange;            
        }

        void OnPropertyChanged(string property)
        {
            PropertyChangedEventArgs args = new PropertyChangedEventArgs(property);
            PropertyChanged?.Invoke(this, args);
        }

        #region IDisposable Support
        public void Dispose()
        {
            if(_ticker != null)
            {                
                _ticker.PriceChanged -= Price_Changed;
                _ticker.Dispose();
                _ticker = null;
            }
        }
        #endregion
    }
}
