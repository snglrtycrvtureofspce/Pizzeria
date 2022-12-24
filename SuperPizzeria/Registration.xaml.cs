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
using SuperPizzeria.Classes;

namespace SuperPizzeria
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        Window wind;
        public Registration(Window win)
        {
            InitializeComponent();
            if (((AuthReg)win).IsEmployeeMode) Name.Content = "Регистрация сотрудника";
            else Name.Content = "Регистрация";
            wind = win;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((AuthReg)wind).AuthRegFrame.Content = new Authorization(wind);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!Functions.VerifyPassword(PasswordField.Text))
            {
                MessageBox.Show("Введён некорретный пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.Users.Where(i => i.Login == LoginField.Text);
                if (users.Count() > 0)
                {
                    MessageBox.Show("Пользователи с таким логином уже существуют", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int statusOfUser = !((AuthReg)wind).IsEmployeeMode ? 1 : 2;
                User user = new User { Login = LoginField.Text, Password = PasswordField.Text, Status = statusOfUser };
                db.Users.Add(user);
                db.SaveChanges();
                MessageBox.Show($"Вы успешно зарегистрировались по ником {LoginField.Text}", "Поздравляю", MessageBoxButton.OK, MessageBoxImage.Information);

            }


        }
    }
}
