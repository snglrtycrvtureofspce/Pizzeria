using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using SuperPizzeria.Classes;

namespace SuperPizzeria
{
    /// <summary>
    /// Логика взаимодействия для Profiler.xaml
    /// </summary>
    public partial class Profiler : Window
    {
        ApplicationContext db;
        Order selectedOrder;
        int UserId;
        public Profiler(int UserId)
        {
            InitializeComponent();
            this.UserId = UserId;
            db = new ApplicationContext();
            UserName.DataContext = db.Users.Find(UserId);
            db.Orders.Load();
            Orders.ItemsSource = db.Orders.Local.ToBindingList();
        }


        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            selectedOrder.Status = 2;
            Accept.IsEnabled = false;
            Decline.IsEnabled = false;
            db.SaveChanges();
            Orders.Items.Refresh();
            db.Dispose();
            Orders.SelectionChanged -= Orders_SelectionChanged;
            Orders.ItemsSource = null;
            db = new ApplicationContext();
            db.Orders.Where(i => UserId == i.UserId).Load();
            Orders.ItemsSource = db.Orders.Local.ToBindingList();
            Orders.SelectionChanged += Orders_SelectionChanged;

        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            selectedOrder.Status = 2;
            selectedOrder.Sale = 0;
            Accept.IsEnabled = false;
            Decline.IsEnabled = false;
            db.SaveChanges();
            Orders.Items.Refresh();
            db.Dispose();
            Orders.SelectionChanged -= Orders_SelectionChanged;
            Orders.ItemsSource = null;
            db = new ApplicationContext();
            db.Orders.Where(i => UserId == i.UserId).Load();
            Orders.ItemsSource = db.Orders.Local.ToBindingList();
            Orders.SelectionChanged += Orders_SelectionChanged;

        }


        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedOrder = (Order)Orders.SelectedItem;
            if (selectedOrder.Status == 1)
            {
                Accept.IsEnabled = true;
                Decline.IsEnabled = true;
            }



        }


    }
}
