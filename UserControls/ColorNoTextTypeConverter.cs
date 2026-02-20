using System;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;

namespace csharp_editor.UserControls
{
    public class ColorNoTextTypeConverter : ColorConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return false;
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return null;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            // Only allow conversion from Color, not from string
            if (sourceType == typeof(string))
                return false;
            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
                throw new NotSupportedException("Direct text editing of color is not supported. Use the color picker.");
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is System.Drawing.Color color)
            {
                // Show as #RRGGBBAA
                return $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
