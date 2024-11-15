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
using Tree_Structures.View.Pages;

namespace Tree_Structures.View
{
    /// <summary>
    /// Логика взаимодействия для TutorialsWindow.xaml
    /// </summary>
    public partial class TutorialsWindow : Window
    {
        public TutorialsWindow()
        {
            InitializeComponent();
            TutorialFrame.Navigate(new BinarySearchTreePage());
        }

        private void TreeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (TreeTypeComboBox.SelectedItem.ToString())
            {
                case "Бинарное дерево":
                    TutorialFrame.Navigate(new BinarySearchTreePage());
                    break;
                    // Добавьте другие случаи для разных типов деревьев
            }
        }
    }
}
