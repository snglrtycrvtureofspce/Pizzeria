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

namespace SuperPizzeria
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthReg : Window
    {
        bool isEmployeeMode;
        public AuthReg()
        {
            InitializeComponent();
            AuthRegFrame.Content = new Authorization(this);
        }

        public AuthReg(bool isEmployeeMode)
        {
            InitializeComponent();
            IsEmployeeMode = isEmployeeMode;
            AuthRegFrame.Content = new Registration(this);
        }

        public bool IsEmployeeMode { get => isEmployeeMode; set => isEmployeeMode = value; }
    }
}
