using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Tree_Structures.View;

namespace Tree_Structures.ViewModel
{
    public static class WindowSettings
    {
        public static TreeViewWindow TreeWindow { get; set; }
        public static TutorialsWindow TutorialsWindow { get; set; }
        public static Color TreeBackgroundColor { get; set; } = Colors.Cornsilk;
        public static Color TutorialsBackgroundColor { get; set; } = Colors.Cornsilk;

        public static void InitializeResources()
        {
            Application.Current.Resources["TreeCanvasBackgroundBrush"] = new SolidColorBrush(TreeBackgroundColor);
            Application.Current.Resources["TutorialsCanvasBackgroundBrush"] = new SolidColorBrush(TutorialsBackgroundColor);
        }
    }
}
