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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        Window wind;
        public Authorization(Window win)
        {
            InitializeComponent();
            wind = win;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((AuthReg)wind).AuthRegFrame.Content = new Registration(wind);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.Where(i => i.Login == LoginField.Text && i.Password == PasswordField.Text);
                if (users.Count() > 0)
                {
                    switch (users.ToArray()[0].Status)
                    {
                        case 1:
                            {
                                MainWindow mw = new MainWindow(users.ToArray()[0].Id);
                                mw.Show();
                                break;
                            }
                        case 2:
                            {
                                OrdersAccounting mw = new OrdersAccounting(0);
                                mw.Show();
                                break;
                            }
                        case 3:
                            {
                                Administration mw = new Administration(0);
                                mw.Show();
                                break;
                            }

                    }

                     
                    wind.Close();
                }
                else
                {
                    MessageBox.Show("Пользователя с такими данными не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }
    }
}
