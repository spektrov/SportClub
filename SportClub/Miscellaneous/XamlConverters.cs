using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using SportClub.Model;



namespace SportClub.Miscellaneous
{
    public class ResetFilterClientParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Tuple<TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox>(values[0] as TextBox,
                values[1] as TextBox, values[2] as DatePicker, values[3] as ComboBox, values[4] as TextBox, values[5] as TextBox);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Can not convert target to Command Parameter");
        }
    }
}
