
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AssigmentPRN
{
    public partial class ReportChartFlightWindow : Window
    {
        List<Number> _data;

        public ReportChartFlightWindow()
        {
            InitializeComponent();

        }
        public List<Number> Data2
        {
            get
            {
                if (_data == null)
                {
                    _data = MainViewModel.list2(); // Assuming list() is a static method
                }
                return _data;
            }
        }
        public List<Number> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = MainViewModel.list(); // Assuming list() is a static method
                }
                return _data;
            }
        }
    }
}