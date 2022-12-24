using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SuperPizzeria.Classes;

namespace SuperPizzeria
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db;



        private bool switcher = false;
        private int UserId;
        List<Pizza> pizzasList;

        public MainWindow(int userId=2)
        {
            InitializeComponent();
            SortOptions.SelectedIndex = 0;
            UserId = userId;
            db = new ApplicationContext();
            db.Pizzas.Where(i=>i.IsOnMenu).Load();
            db.CartUnits.Where(i => i.UserId == UserId && i.Order == 0).Load();
            pizzasList = db.Pizzas.Local.ToList();
            Menu.ItemsSource = pizzasList;
            Cart.ItemsSource = db.CartUnits.Local.ToBindingList();
            List<Order> orderList = db.Orders.Where(i => i.UserId == UserId).ToList();
            foreach(Order order in orderList)
            {
                if(order.Status == 1)
                {
                    MessageBox.Show($"У вас есть неподтверждённые заказы!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            switch (SortOptions.SelectedIndex)
            {
                case 0:
                    {
                        pizzasList = pizzasList.OrderBy(i => i.Id).ToList();
                        break;
                    }
                case 1:
                    {
                        pizzasList = pizzasList.OrderBy(i => i.Cost).ToList();
                        break;
                    }
                case 2:
                    {
                        pizzasList = pizzasList.OrderByDescending(i => i.Cost).ToList();
                        break;
                    }
            }

            Menu.ItemsSource = null;
            Menu.ItemsSource = pizzasList;
            Menu.Items.Refresh();
        }

        public void ReloadMenu(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
            db = new ApplicationContext();
            Menu.ItemsSource = null;
            db.Pizzas.Where(i => i.IsOnMenu).Load();
            Menu.ItemsSource = db.Pizzas.Local.ToBindingList();

        }

        

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
            AuthReg ar = new AuthReg();
            ar.Show();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            AmountOfPizzas.Text = Convert.ToString(Convert.ToInt32(AmountOfPizzas.Text) + 1);
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if(Convert.ToInt32(AmountOfPizzas.Text)>1)
                AmountOfPizzas.Text = Convert.ToString(Convert.ToInt32(AmountOfPizzas.Text) - 1);

        }

        private void Switch_Click(object sender, RoutedEventArgs e)
        {
            Visibility cartVisibility = switcher ? Visibility.Hidden : Visibility.Visible;
            Visibility menuVisibility = switcher ? Visibility.Visible : Visibility.Hidden;
            switcher = !switcher;
            GeneralLabel.Content = switcher ? "Корзина" : "Меню";
            CartGrid.Visibility = cartVisibility;
            MenuGrid.Visibility = menuVisibility;
            MenuItems.Visibility = menuVisibility;
            CartItems.Visibility = cartVisibility;
            if (db.CartUnits.Where(i => i.UserId == UserId && i.Order == 0).Count() == 0)
                OrderButton.IsEnabled = false;
            else
                OrderButton.IsEnabled = true;
            
            db.CartUnits.Where(i => i.UserId == UserId && i.Order == 0).Load();
            Cart.ItemsSource = db.CartUnits.Local.ToBindingList();
            Cart.Items.Refresh();


        }

        private void Cart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AmountOfPizzasCart.DataContext = Cart.SelectedItem;
            MinusButtonCart.IsEnabled = true;
            PlusButtonCart.IsEnabled = true;
            Delete.IsEnabled = true;

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int amount = Convert.ToInt32(AmountOfPizzas.Text);
            int pizzaId = ((Pizza)Menu.SelectedItem).Id;
            if (db.CartUnits.Where(i => i.Pizza == pizzaId && i.Order == 0).Count() > 0)
            {
                CartUnit cu = db.CartUnits.Where(i => i.Pizza == pizzaId && i.Order == 0).ToArray()[0];
                cu.Amount += amount;
            }
            else
            {
                db.CartUnits.Add(new CartUnit { Amount = amount, Pizza = pizzaId, UserId = UserId, Order = 0 });
            }

            db.SaveChanges();
            Cart.Items.Refresh();
        }

        private void PlusButtonCart_Click(object sender, RoutedEventArgs e)
        {
            CartUnit cu = (CartUnit)Cart.SelectedItem;
            cu.Amount = cu.Amount + 1;
            db.SaveChanges();
            Cart.Items.Refresh();
            AmountOfPizzasCart.DataContext = null;
            AmountOfPizzasCart.DataContext = Cart.SelectedItem;
        }

        private void MinusButtonCart_Click(object sender, RoutedEventArgs e)
        {
            CartUnit cu = (CartUnit)Cart.SelectedItem;
            if (cu.Amount > 1)
                cu.Amount--;
            db.SaveChanges();
            Cart.Items.Refresh();
            AmountOfPizzasCart.DataContext = null;
            AmountOfPizzasCart.DataContext = Cart.SelectedItem;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CartUnit cu = (CartUnit)Cart.SelectedItem;
            db.CartUnits.Remove(cu);
            db.SaveChanges();
            Cart.Items.Refresh();
            MinusButtonCart.IsEnabled = false;
            PlusButtonCart.IsEnabled = false;
            Delete.IsEnabled = false;
            if (db.CartUnits.Where(i => i.UserId == UserId && i.Order == 0).Count() == 0)
                OrderButton.IsEnabled = false;
            else
                OrderButton.IsEnabled = true;

        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            var orderUnits = db.CartUnits.Where(i => i.UserId == UserId && i.Order == 0);
            Cart.Items.Refresh();

            db.Orders.Add(new Order() { DateOfOrder = DateTime.Now, UserId = UserId, Sale = 0, Status = 0 });
            db.SaveChanges();
            Cart.Items.Refresh();

            Order lastOrder = db.Orders.ToList().Last();
            foreach (CartUnit orderUnit in orderUnits)
            {
                orderUnit.Order = lastOrder.Id;
            }
            db.SaveChanges();
            Cart.Items.Refresh();
            Cart.ItemsSource = null;
            Cart.Items.Clear();
            db.Dispose();
            db = new ApplicationContext();


            Cart.Items.Refresh();

        }

        private void UserProfile_Click(object sender, RoutedEventArgs e)
        {
            Profiler profile= new Profiler(UserId);
            profile.ShowDialog();
        }

        private void CreatePizza_Click(object sender, RoutedEventArgs e)
        {
            Administration ad = new Administration(1);
            ad.Closing += ReloadMenu;
            ad.ShowDialog();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string text = SearchQuery.Text;
            if (text.Length == 0)
            {
                db.Pizzas.Where(i => i.IsOnMenu).Load();
                pizzasList = db.Pizzas.Local.ToList();
            }
            else
            {
                Regex nameReg = new Regex(@"[а-я]+");

                string pizzaName = "";
                foreach (Match match in nameReg.Matches(text.ToLower()))
                {
                    pizzaName += match.Groups[0].Value + " ";
                }
                pizzaName = pizzaName.Substring(0, pizzaName.Length - 1);
                List<Pizza> newPizzas = new List<Pizza>();
                foreach (Pizza p in db.Pizzas.Where(p => p.Name.ToLower().IndexOf(pizzaName.ToLower()) != -1))
                {
                    newPizzas.Add(p);
                }

                pizzasList = newPizzas;
            }

            Menu.ItemsSource = null;
            Menu.ItemsSource = pizzasList;
            Menu.Items.Refresh();
        }

        private void Sales_Click(object sender, RoutedEventArgs e)
        {
            string salesInfo = "";
            foreach (var sale in db.Sales.ToList())
            {
                salesInfo += sale.Summary + " " + sale.Multiplier + "\n";
            }
            MessageBox.Show(salesInfo, "Скидки");
        }


        private void Spravka_Click(object sender, RoutedEventArgs e)
        {
            string inforamtion = "Данное приложение позволяет легко и без всякие лишних усилий заказать себе набор из пицц своей мечта." +
                "\n\nПеред вами представленно меню, где находятся все пиццы данного приложения, которые в него включены. " +
                "Нажмите на пиццу в меню, а затем на кнопку добавить в корзину для добавление пиццы в свой будущий заказ. При этом есть возможность " +
                "изменять количество пицц, добавляемых в заках. В самой же корзине, имеется возможность удаления пиццы из заказа и изменения количества пицц." +
                "\n\nПомима работы с корзиной пользователь может зайти в свой профиль при помощи соответсвующей кнопки. В профиле расположены все заказы кон" +
                "кретного пользователя, кнопки подтверждения и отклонения скидки, предложенной сотрудником" +
                "\n\nКнопка скидки покажет существующие скидки на пиццы, скидки предлогаются сотрудниками пиццерии." +
                "\n\nКнопка конструктор пиццы позволит собрать свою собственную пиццу для добавления в заказ." +
                "\n\nКнопка Меню\\Корзина предназначенга для вкючения необходимого пользователю режима\n\n" +
                "Система поиска и сортировки пиццы поможет пользователю в кратчайшие сроки найти необходимую ему пиццу";
            MessageBox.Show(inforamtion, "Справка", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
