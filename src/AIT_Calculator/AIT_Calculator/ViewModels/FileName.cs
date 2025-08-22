
//< !--Для параметра L -->
//<TextBox Style="{StaticResource ValidatedTextBoxStyle}"
//         Text="{Binding LText, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}"
//         PreviewTextInput="TextBox_PreviewTextInput"
//         PreviewKeyDown="TextBox_PreviewKeyDown"
//         TextChanged="TextBox_TextChanged"/>

//<!-- Для параметра A -->
//<TextBox Style="{StaticResource ValidatedTextBoxStyle}"
//         Text="{Binding AText, UpdateSourceTrigger=PropertyChanged, ValidatesOnDataErrors=True}"
//         PreviewTextInput="TextBox_PreviewTextInput"
//         PreviewKeyDown="TextBox_PreviewKeyDown"
//         TextChanged="TextBox_TextChanged"/>

//<!-- И так далее для остальных параметров -->



//private string _yText = "0";
//public string YText
//{
//    get => _yText;
//    set
//    {
//        if (value == _yText) return;

//        if (string.IsNullOrEmpty(value) || value == "-")
//        {
//            value = "0";
//        }

//        if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
//        {
//            _yText = value;
//            Y = result;
//            ClearErrors(nameof(YText));
//        }
//        else
//        {
//            SetError(nameof(YText), "Введите корректное число");
//        }
//        OnPropertyChanged();
//    }
//}