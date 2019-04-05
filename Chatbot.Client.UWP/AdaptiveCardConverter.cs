using AdaptiveCards.Rendering.Uwp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Chatbot.Client.UWP
{
    public class AdaptiveCardConverter : IValueConverter
    {
        private readonly AdaptiveCardRenderer _renderer = new AdaptiveCardRenderer();

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var adaptiveCard = (AdaptiveCards.AdaptiveCard)value;
            var renderedCard = _renderer.RenderAdaptiveCard(AdaptiveCard.FromJsonString(adaptiveCard.ToJson()).AdaptiveCard);
            return renderedCard.FrameworkElement;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
