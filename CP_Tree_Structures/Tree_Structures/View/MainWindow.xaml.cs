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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tree_Structures.View;
using Tree_Structures.ViewModel;

namespace Tree_Structures
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window currentActiveWindow;
        private Color _treeBackgroundColor;
        private Color _tutorialsBackgroundColor;

        public MainWindow()
        {
            InitializeComponent();
            this.MouseDown += Window_MouseDown;
            _treeBackgroundColor = WindowSettings.TreeBackgroundColor;
            _tutorialsBackgroundColor = WindowSettings.TutorialsBackgroundColor;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private async void TreeSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var newTreeWindow = new TreeViewWindow();
            await App.WindowManager.SwitchToWindowAsync(newTreeWindow);
        }


        private async void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            await App.WindowManager.SwitchToWindowAsync(settingsWindow);
        }

        private async void TutorialsButton_Click(object sender, RoutedEventArgs e)
        {
            var tutorialsWindow = new TutorialsWindow();
            await App.WindowManager.SwitchToWindowAsync(tutorialsWindow);
        }

        private async void AuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var authorWindow = new AuthorWindow();
            await App.WindowManager.SwitchToWindowAsync(authorWindow);
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
