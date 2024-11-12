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

        private async Task SwitchToNewWindowAsync(Window newWindow)
        {
            currentActiveWindow = this;

            // Если текущее активное окно есть, выполняем анимацию
            if (currentActiveWindow != null && currentActiveWindow.IsLoaded)
            {
                var oldWindow = currentActiveWindow;

                // Показ нового окна с анимацией появления
                newWindow.Opacity = 0;
                var fadeIn = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(150) // Плавное появление нового окна
                };

                newWindow.Show();
                var fadeInCompletion = new TaskCompletionSource<bool>();
                fadeIn.Completed += (s, e) => fadeInCompletion.SetResult(true);
                newWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                await fadeInCompletion.Task; // Ждём завершения появления нового окна

                // Начинаем скрытие старого окна после появления нового
                var fadeOut = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    Duration = TimeSpan.FromMilliseconds(150) // Плавное скрытие старого окна
                };

                var fadeOutCompletion = new TaskCompletionSource<bool>();
                fadeOut.Completed += (s, e) => fadeOutCompletion.SetResult(true);
                oldWindow.BeginAnimation(UIElement.OpacityProperty, fadeOut);

                await fadeOutCompletion.Task; // Ждём завершения анимации скрытия старого окна

                oldWindow.Hide();
                oldWindow.Close(); // Закрываем старое окно

                // Обновляем ссылку на текущее активное окно
                currentActiveWindow = newWindow;
            }
            else
            {
                // Если активного окна нет, просто показываем новое окно с анимацией
                newWindow.Opacity = 0;
                var fadeIn = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    Duration = TimeSpan.FromMilliseconds(150)
                };

                newWindow.Show();
                var fadeInCompletion = new TaskCompletionSource<bool>();
                fadeIn.Completed += (s, e) => fadeInCompletion.SetResult(true);
                newWindow.BeginAnimation(UIElement.OpacityProperty, fadeIn);

                await fadeInCompletion.Task; // Ждём завершения анимации появления

                currentActiveWindow = newWindow;
            }
        }


        private async void TreeSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            var newTreeWindow = new TreeViewWindow();
            await SwitchToNewWindowAsync(newTreeWindow);
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
