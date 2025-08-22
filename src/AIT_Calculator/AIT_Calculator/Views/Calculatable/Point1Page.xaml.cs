using AIT_Calculator.Models;
using System.Windows.Controls;

namespace AIT_Calculator.Views.Calculatable
{
    /// <summary>
    /// Логика взаимодействия для Point1Page.xaml
    /// </summary>
    public partial class Point1Page : UserControl
    {
        public CarDataModel CarDataModel;
        public Point1Page(CarDataModel carDataModel)
        {
            CarDataModel = carDataModel;
            InitializeComponent();
            DataContext = CarDataModel;
        }
    }
}
