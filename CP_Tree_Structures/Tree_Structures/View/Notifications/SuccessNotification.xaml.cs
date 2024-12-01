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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tree_Structures.View
{
    /// <summary>
    /// Логика взаимодействия для SuccessNotification.xaml
    /// </summary>
    public partial class SuccessNotification : UserControl
    {
        public SuccessNotification()
        {
            InitializeComponent();
        }

        public void SetMessage(string message)
        {
            SuccessMessage.Text = message;
        }
    }
}
