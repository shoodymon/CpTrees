using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Tree_Structures.ViewModel;

namespace Tree_Structures
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static WindowManager WindowManager { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            WindowSettings.InitializeResources();

            // Инициализируем WindowManager с единственным экземпляром MainWindow
            var mainWindow = new MainWindow();
            WindowManager = new WindowManager(mainWindow);

            // Показ стартового окна через WindowManager
            await WindowManager.SwitchToWindowAsync(mainWindow);
        }
    }
}
