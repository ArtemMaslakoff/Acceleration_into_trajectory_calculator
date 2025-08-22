using AIT_Calculator.Models;
using System.Windows.Controls;
using System.Windows.Input;

namespace AIT_Calculator.Views.Setable
{
    /// <summary>
    /// Логика взаимодействия для InitialConditionPage.xaml
    /// </summary>
    public partial class InitialConditionPage : UserControl
    {
        public CarDataModel CarDataModel;
        public InitialConditionPage(CarDataModel carDataModel)
        {
            CarDataModel = carDataModel;
            InitializeComponent();
            DataContext = CarDataModel;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            // Запрещаем минус
            if (e.Text == "-")
            {
                e.Handled = true;
                return;
            }

            // Разрешаем только цифры и точку
            if (!char.IsDigit(e.Text, 0) && e.Text != ".")
            {
                e.Handled = true;
                return;
            }

            // Проверяем, что точка не дублируется
            if (e.Text == "." && textBox.Text.Contains("."))
            {
                e.Handled = true;
                return;
            }

            // Разрешаем ввод
            e.Handled = false;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;

            // Всегда разрешаем Backspace и Delete
            if (e.Key == Key.Back || e.Key == Key.Delete)
            {
                e.Handled = false;
                return;
            }

            // Разрешаем служебные клавиши
            if (e.Key == Key.Left || e.Key == Key.Right ||
                e.Key == Key.Tab || e.Key == Key.Escape)
            {
                e.Handled = false;
                return;
            }

            // Разрешаем комбинации Ctrl
            if ((e.Key == Key.C || e.Key == Key.V || e.Key == Key.X || e.Key == Key.A) &&
                (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                e.Handled = false;
                return;
            }

            // Разрешаем цифры и точку
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Decimal || e.Key == Key.OemPeriod)
            {
                e.Handled = false;
                return;
            }

            // Запрещаем минус
            if (e.Key == Key.OemMinus || e.Key == Key.Subtract)
            {
                e.Handled = true;
                return;
            }

            // Все остальные клавиши запрещаем
            e.Handled = true;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;

            // Если текст стал пустым после удаления, устанавливаем "0"
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "0";
                textBox.CaretIndex = 1; // Курсор после нуля
            }
            // Если остался только "-", заменяем на "0"
            else if (textBox.Text == "-")
            {
                textBox.Text = "0";
                textBox.CaretIndex = 1;
            }
        }
    }
}
