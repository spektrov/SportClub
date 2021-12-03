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
            return new Tuple<TextBox, TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox>(values[0] as TextBox,
                values[1] as TextBox, values[2] as TextBox, values[3] as DatePicker,
                values[4] as ComboBox, values[5] as TextBox, values[6] as TextBox);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Can not convert target to Command Parameter");
        }
    }

    public class ResetFilterTrainersParametersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var rest = new Tuple<TextBox, TextBox, TextBox, TextBox>
                (values[7] as TextBox, values[8] as TextBox, values[9] as TextBox, values[10] as TextBox);

            return new Tuple<TextBox, TextBox, TextBox, DatePicker, ComboBox, TextBox, TextBox,
                Tuple<TextBox, TextBox, TextBox, TextBox>> (values[0] as TextBox,
                values[1] as TextBox, values[2] as TextBox, values[3] as DatePicker,
                values[4] as ComboBox, values[5] as TextBox, values[6] as TextBox, rest);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Can not convert target to Command Parameter");
        }
    }
}
