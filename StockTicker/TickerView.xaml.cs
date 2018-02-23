using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Models;
using ViewModels;

namespace StockTicker
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class TickerView : UserControl
    {
        public TickerView()
        {
            InitializeComponent();

            TickerModel ticker = new TickerModel("GOOG", 300);
            TickerViewModel viewModel = new TickerViewModel(ticker);
            DataContext = viewModel;
        }
    }
}
