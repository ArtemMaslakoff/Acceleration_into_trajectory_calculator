using AIT_Calculator.Models;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIT_Calculator.Views.Calculatable
{
    /// <summary>
    /// Логика взаимодействия для OtherPointsPage.xaml
    /// </summary>
    public partial class OtherPointsPage : UserControl
    {
        public CarDataModel CarDataModel;
        public OtherPointsPage(CarDataModel carDataModel)
        {
            CarDataModel = carDataModel;
            InitializeComponent();
            DataContext = CarDataModel;
        }
    }
}
