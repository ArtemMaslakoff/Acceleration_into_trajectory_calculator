using AIT_Calculator.Models;
using AIT_Calculator.Services;
using AIT_Calculator.Views;
using AIT_Calculator.Views.Calculatable;
using AIT_Calculator.Views.Setable;
using AIT_Calculator.Views.Visualizations;
using System.Windows;
using System.Windows.Controls;
using Point1Page = AIT_Calculator.Views.Calculatable.Point1Page;

namespace AIT_Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public CarDataModel CarDataModel;
    public MainWindow()
    {
        CarDataModel = new CarDataModel();
        InitializeComponent();
    }

    private void OpenInitialConditionPage(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new InitialConditionPage(CarDataModel);
    }
    private void OpenAccelerationMatricesPage(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new AccelerationMatricesPage(CarDataModel);
    }

    private void OpenPoint1Page(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new Point1Page(CarDataModel);
    }
    private void OpenPoint2Page(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new Point2Page(CarDataModel);
    }
    private void OpenOtherPointsPage(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new OtherPointsPage(CarDataModel);
    }
    private void OpenPoints1And2TrajectoriesPage(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new Points1And2TrajectoriesPage(CarDataModel);
    }
    private void OpenAnimationPage(object sender, RoutedEventArgs e)
    {
        MainContentControl.Content = new AnimationPage(CarDataModel);
    }

    private void SaveProject_Click(object sender, RoutedEventArgs e)
    {
        var saveFileDialog = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "AIT Project Files (*.ait)|*.ait",
            DefaultExt = ".ait",
            AddExtension = true
        };

        if (saveFileDialog.ShowDialog() == true)
        {
            ProjectFileService.SaveProject(CarDataModel, saveFileDialog.FileName);
        }
    }

    private void OpenProject_Click(object sender, RoutedEventArgs e)
    {
        var openFileDialog = new Microsoft.Win32.OpenFileDialog
        {
            Filter = "AIT Project Files (*.ait)|*.ait",
            DefaultExt = ".ait"
        };

        if (openFileDialog.ShowDialog() == true)
        {
            try
            {
                CarDataModel = ProjectFileService.LoadProject(openFileDialog.FileName);
                DataContext = CarDataModel;

                if (MainContentControl.Content != null)
                {
                    MainContentControl.Content = Activator.CreateInstance(
                        MainContentControl.Content.GetType(),
                        CarDataModel);
                }
                else
                {
                    MainContentControl.Content = new InitialConditionPage(CarDataModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке проекта: {ex.Message}");
                // Дополнительная обработка ошибки
            }
        }
    }
}