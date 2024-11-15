using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;

namespace Tree_Structures.ViewModel
{
    public class WindowManager
    {
        private Window _currentActiveWindow;
        public Window MainWindow { get; }

        public WindowManager(Window mainWindow)
        {
            MainWindow = mainWindow;
        }

        private async Task AnimateWindowInAsync(Window window)
        {
            if (window == null) return;  // Добавляем проверку на null
            window.Opacity = 0; // Сначала окно невидимо
            window.Show(); // Показываем окно

            var fadeIn = new DoubleAnimation(0.0, 1.0, TimeSpan.FromMilliseconds(150));
            var fadeInCompletion = new TaskCompletionSource<bool>();
            fadeIn.Completed += (s, e) => fadeInCompletion.SetResult(true);
            window.BeginAnimation(UIElement.OpacityProperty, fadeIn);

            await fadeInCompletion.Task;
        }

        private async Task AnimateWindowOutAsync(Window window)
        {
            if (window == null) return;  // Добавляем проверку на null

            var fadeOut = new DoubleAnimation(1.0, 0.0, TimeSpan.FromMilliseconds(150));
            var fadeOutCompletion = new TaskCompletionSource<bool>();
            fadeOut.Completed += (s, e) => fadeOutCompletion.SetResult(true);
            window.BeginAnimation(UIElement.OpacityProperty, fadeOut);
            await fadeOutCompletion.Task;
            window.Hide();
        }

        // Основной метод для переключения окон
        public async Task SwitchToWindowAsync(Window newWindow)
        {
            if (newWindow == null) return;  // Добавляем проверку на null

            // Сначала анимируем появление нового окна
            await AnimateWindowInAsync(newWindow);

            // Если текущее окно не null, анимируем его исчезновение
            if (_currentActiveWindow != null && _currentActiveWindow != newWindow)
            {
                await AnimateWindowOutAsync(_currentActiveWindow);
            }

            // Обновляем текущее активное окно
            _currentActiveWindow = newWindow;
        }
    }

}
