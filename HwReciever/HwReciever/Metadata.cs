using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HwReciever;

public static class Metadata
{
    public static readonly DependencyProperty InfoProperty = DependencyProperty.RegisterAttached(
            "Info",
            typeof(string),
            typeof(Metadata),
            new FrameworkPropertyMetadata(null));

    public static void SetInfo(UIElement element, string value) => element.SetValue(InfoProperty, value);

    public static string GetInfo(UIElement element) => (string)element.GetValue(InfoProperty);
}

