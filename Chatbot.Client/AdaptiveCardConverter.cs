using AdaptiveCards;
using AdaptiveCards.Rendering.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace Chatbot.Client
{
    public class AdaptiveCardConverter : IValueConverter
    {
        private readonly AdaptiveCardRenderer _renderer = new AdaptiveCardRenderer();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var renderedCard = _renderer.RenderCard((AdaptiveCard)value);
            return renderedCard.FrameworkElement;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
