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

namespace Tree_Structures.View
{
    /// <summary>
    /// Логика взаимодействия для AuthorWindow.xaml
    /// </summary>
    public partial class AuthorWindow : Window
    {
        private string _currentButton = "";

        public AuthorWindow()
        {
            InitializeComponent();
        }

        private void PersonalDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentButton == "personal")
            {
                InfoBorder.Visibility = Visibility.Collapsed;
                _currentButton = "";
                return;
            }

            InfoText.Text = "Автор десктопного приложения\n\n" +
                           "ФИО: Живоглод Никита Андреевич\n" +
                           "Возраст: 21 год\n" +
                           "Образование: БНТУ, ФИТР - 2 курс\n";

            InfoBorder.Visibility = Visibility.Visible;
            _currentButton = "personal";
        }

        private void IdeaButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentButton == "idea")
            {
                InfoBorder.Visibility = Visibility.Collapsed;
                _currentButton = "";
                return;
            }

            InfoText.Text = "История создания этого проекта началась на первом курсе университета во время изучения " +
                           "древовидных структур данных. Наблюдая за тем, как студенты осваивают этот материал, " +
                           "стало очевидно - визуализация значительно упрощает понимание принципов работы деревьев. " +
                           "Так родилась идея создать приложение, которое поможет студентам в изучении этой темы.\n\n" +
                           "Первая версия проекта включала базовый функционал с тремя типами деревьев:\n" +
                           "• Бинарное дерево поиска\n" +
                           "• АВЛ-дерево\n" +
                           "• Красно-чёрное дерево\n\n" +
                           "На втором курсе проект эволюционировал в полноценное десктопное приложение " +
                           "для обучения студентов. Основные улучшения коснулись трёх направлений:\n\n" +
                           "1. Расширение функционала\n" +
                           "   Добавлены новые структуры данных:\n" +
                           "   • Двоичная куча (min-heap)\n" +
                           "   • Двоичная куча (max-heap)\n" +
                           "   • B-дерево\n\n" +
                           "2. Обучающий материал\n" +
                           "   Для каждой структуры данных разработан подробный туториал, " +
                           "включающий визуальные примеры и программный код. Это позволяет " +
                           "студентам последовательно изучать материал, сразу видя практическое применение.\n\n" +
                           "3. Пользовательский интерфейс\n" +
                           "   Полностью переработан дизайн приложения с учётом современных " +
                           "тенденций UI/UX дизайна, что сделало работу с программой " +
                           "более удобной и приятной.\n";

            InfoBorder.Visibility = Visibility.Visible;
            _currentButton = "idea";
        }

        private void TechnologiesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentButton == "tech")
            {
                InfoBorder.Visibility = Visibility.Collapsed;
                _currentButton = "";
                return;
            }

            InfoText.Text = "Технологии, использованные в проекте:\n\n" +
                           "Архитектура и паттерны:\n" +
                           "• MVVM (Model-View-ViewModel) - основной архитектурный паттерн\n" +
                           "• Command Pattern для обработки пользовательских действий\n" +
                           "• Observer Pattern для связывания данных\n\n" +

                           "Основной стек технологий:\n" +
                           "• C# и .NET Framework как база приложения\n" +
                           "• WPF (Windows Presentation Foundation) для создания UI\n" +
                           "• XAML разметка с использованием Binding и Triggers\n\n" +

                           "Инструменты разработки:\n" +
                           "• Visual Studio 2022 как основная IDE\n" +
                           "• Git для контроля версий\n" +
                           "• GitHub для хранения репозитория\n\n" +

                           "NuGet-пакеты:\n" +
                           "• AvalonEdit (v6.3.0.90) - продвинутый редактор кода для отображения примеров в туториалах\n" +
                           "• Microsoft.Xaml.Behaviors.Wpf (v1.1.31) - расширенная поддержка поведений в XAML\n" +
                           "• PixiEditor.ColorPicker (v3.4.1) - современный элемент выбора цвета для настройки интерфейса\n" +
                           "• WPF-UI (v3.0.5) - набор стильных UI-компонентов в стиле Fluent Design\n";

            InfoBorder.Visibility = Visibility.Visible;
            _currentButton = "tech";
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            await App.WindowManager.SwitchToWindowAsync(App.WindowManager.MainWindow);
        }
    }
}
