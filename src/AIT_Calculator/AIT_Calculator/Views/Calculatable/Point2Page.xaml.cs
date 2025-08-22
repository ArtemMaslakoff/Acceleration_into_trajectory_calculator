using AIT_Calculator.Models;
using System.Windows.Controls;

namespace AIT_Calculator.Views.Calculatable
{
    /// <summary>
    /// Логика взаимодействия для Point2Page.xaml
    /// </summary>
    public partial class Point2Page : UserControl
    {
        public CarDataModel CarDataModel;
        public Point2Page(CarDataModel carDataModel)
        {
            CarDataModel = carDataModel;
            InitializeComponent();
            DataContext = CarDataModel;
        }
    }
}
