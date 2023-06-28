using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace HwReciever;

public static class Utils
{
    public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj == null) yield return (T)Enumerable.Empty<T>();
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
        {
            DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
            if (ithChild == null) continue;
            if (ithChild is T t) yield return t;
            foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
        }
    }
}
