using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tree_Structures.ViewModel;

namespace Tree_Structures.View
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            TreeColorPicker.SelectedColor = WindowSettings.TreeBackgroundColor;
            TutorialsColorPicker.SelectedColor = WindowSettings.TutorialsBackgroundColor;
        }

        private void TreeColorButton_Click(object sender, RoutedEventArgs e)
        {
            TutorialsColorPicker.Visibility = Visibility.Collapsed;
            TreeColorPicker.Visibility = TreeColorPicker.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private void TutorialsColorButton_Click(object sender, RoutedEventArgs e)
        {
            TreeColorPicker.Visibility = Visibility.Collapsed;
            TutorialsColorPicker.Visibility = TutorialsColorPicker.Visibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible;
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            await App.WindowManager.SwitchToWindowAsync(App.WindowManager.MainWindow);
        }

        private void TreeColorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            var color = TreeColorPicker.SelectedColor;
            WindowSettings.TreeBackgroundColor = color;

            var brush = new SolidColorBrush(color);
            Application.Current.Resources["TreeCanvasBackgroundBrush"] = brush;
        }

        private void TutorialsColorPicker_ColorChanged(object sender, RoutedEventArgs e)
        {
            var color = TutorialsColorPicker.SelectedColor;
            WindowSettings.TutorialsBackgroundColor = color;

            var brush = new SolidColorBrush(color);
            Application.Current.Resources["TutorialsCanvasBackgroundBrush"] = brush;
        }
    }
}
