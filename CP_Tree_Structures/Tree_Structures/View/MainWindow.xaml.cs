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

namespace Tree_Structures
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window currentActiveWindow;

        public MainWindow()
        {
            InitializeComponent();
            this.MouseDown += Window_MouseDown;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void SwitchToNewWindow(Window newWindow)
        {
            // Если есть активное окно, плавно скрываем его
            if (currentActiveWindow != null && currentActiveWindow.IsLoaded)
            {
                var oldWindow = currentActiveWindow; // Сохраняем ссылку на старое окно
                var fadeOut = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    Duration = TimeSpan.FromMilliseconds(200)
                };

                fadeOut.Completed += (s, e) =>
                {
                    oldWindow.Hide(); // Скрываем старое окно после завершения анимации

                    // Показ нового окна с анимацией появления
                    newWindow.Opacity = 0;
                    var fadeIn = new DoubleAnimation
                    {
                        From = 0.0,
                        To = 1.0,
                        Duration = TimeSpan.FromMilliseconds(200)
                    };

                    newWindow.Show();
                    newWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                    // Обновляем ссылку на текущее активное окно
                    currentActiveWindow = newWindow;

                    // Дополнительно закрываем старое окно, если его больше не нужно использовать
                    oldWindow.Close();
                };

                oldWindow.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            }
            else
            {
                // Если активного окна нет, сразу показываем новое окно
                newWindow.Opacity = 0;
                var fadeIn = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(200)
                };

                newWindow.Show();
                newWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                currentActiveWindow = newWindow;
            }
        }

        private void TreeSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var newTreeWindow = new TreeViewWindow();
            SwitchToNewWindow(newTreeWindow);
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // Когда будет реализовано окно настроек:
            // var settingsWindow = new SettingsWindow();
            // SwitchToNewWindow(settingsWindow);
            MessageBox.Show("Settings window will be implemented later.");
        }

        private void TutorialsButton_Click(object sender, RoutedEventArgs e)
        {
            // Когда будет реализовано окно туториалов:
            // var tutorialsWindow = new TutorialsWindow();
            // SwitchToNewWindow(tutorialsWindow);
            MessageBox.Show("Tutorials window will be implemented later.");
        }

        private void AuthorButton_Click(object sender, RoutedEventArgs e)
        {
            // Когда будет реализовано окно автора:
            // var authorWindow = new AuthorWindow();
            // SwitchToNewWindow(authorWindow);
            MessageBox.Show("Author window will be implemented later.");
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
