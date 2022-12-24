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
using Microsoft.Office.Interop.Word;
using SuperPizzeria.Classes;

namespace SuperPizzeria
{
    /// <summary>
    /// Логика взаимодействия для EmployeeMode.xaml
    /// </summary>
    public partial class OrdersAccounting : System.Windows.Window
    {
        ApplicationContext db;
        Order selectedOrder;
        bool firstKey = false;
        bool secondKey = false;
        int Status;

        public OrdersAccounting(int status)
        {
            InitializeComponent();
            db = new ApplicationContext();
            this.Status = status;
            if (Status == 0)
            {
                db.Orders.Where(i => i.Status == 0).Load();
            }
            else
            {
                db.Orders.Load();
                SuggestSale.Visibility = Visibility.Hidden;
                Sales.Visibility = Visibility.Hidden;
                AccountOrders.Visibility = Visibility.Visible;
                SalesLabel.Visibility = Visibility.Hidden;
            }
            db.Sales.Load();
            Orders.ItemsSource = db.Orders.Local.ToBindingList();
            Sales.ItemsSource = db.Sales.Local.ToBindingList();
            this.Closing += EmployeeMode_Closing;
        }

        private void EmployeeMode_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
            if (Status == 0)
            {
                AuthReg ar = new AuthReg();
                ar.Show();
            }
        }

        private void Sales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            secondKey = true;
            if (secondKey && firstKey)
            {
                SuggestSale.IsEnabled = true;
            }
        }

        private void SuggestSale_Click(object sender, RoutedEventArgs e)
        {
            selectedOrder.Sale = ((Sale)Sales.SelectedItem).Id;
            selectedOrder.Status = 1;
            SuggestSale.IsEnabled = false;
            firstKey = false;
            db.SaveChanges();
            Orders.Items.Refresh();
            db.Dispose();
            Orders.SelectionChanged -= Orders_SelectionChanged;
            Orders.ItemsSource = null;
            db = new ApplicationContext();
            if (Status == 0)
            {
                db.Orders.Where(i => i.Status == 0).Load();
            }
            else
            {
                db.Orders.Load();
            }
            Orders.ItemsSource = db.Orders.Local.ToBindingList();
            Orders.SelectionChanged += Orders_SelectionChanged;

        }


        private void Orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedOrder = (Order)Orders.SelectedItem;
            if(selectedOrder.Status == 0 )
                firstKey = true;
            if(secondKey && firstKey)
            {
                SuggestSale.IsEnabled = true;
            }


        }

        private void AccountOrders_Click(object sender, RoutedEventArgs e)
        {
            var application = new Microsoft.Office.Interop.Word.Application();
            Document document = application.Documents.Add();
            Microsoft.Office.Interop.Word.Paragraph actorParagrap = document.Paragraphs.Add();
            Microsoft.Office.Interop.Word.Range actorRange = actorParagrap.Range;
            actorRange.Text = "Заказы пиццерии";
            actorParagrap.set_Style("Обычный");
            actorRange.InsertParagraphAfter();
            List<Order> orders = db.Orders.ToList();
            Microsoft.Office.Interop.Word.Paragraph tableParagrap = document.Paragraphs.Add();
            Microsoft.Office.Interop.Word.Range tableRange = tableParagrap.Range;
            Microsoft.Office.Interop.Word.Table priceTable = document.Tables.Add(tableRange, orders.Count() + 1, 6);

            priceTable.Borders.InsideLineStyle = priceTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            priceTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
            priceTable.Borders.InsideLineStyle = priceTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            priceTable.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Microsoft.Office.Interop.Word.Range cellRange;

            cellRange = priceTable.Cell(1, 1).Range;
            cellRange.Text = "Номер";
            cellRange = priceTable.Cell(1, 2).Range;
            cellRange.Text = "Клиент";
            cellRange = priceTable.Cell(1, 3).Range;
            cellRange.Text = "Состав";//Название пицц
            cellRange = priceTable.Cell(1, 4).Range;
            cellRange.Text = "Дата";
            cellRange = priceTable.Cell(1, 5).Range;
            cellRange.Text = "Цена";
            cellRange = priceTable.Cell(1, 6).Range;
            cellRange.Text = "Скидка";
            for (int i = 0; i < orders.Count(); ++i)
            {
                Order order = orders[i];

                cellRange = priceTable.Cell(i + 2, 1).Range;
                cellRange.Text = order.Id.ToString();

                cellRange = priceTable.Cell(i + 2, 2).Range;
                cellRange.Text = order.GetUserName;

                cellRange = priceTable.Cell(i + 2, 3).Range;
                cellRange.Text = order.GetPizzasNames;

                cellRange = priceTable.Cell(i + 2, 4).Range;
                cellRange.Text = order.GetOrderDate;

                cellRange = priceTable.Cell(i + 2, 5).Range;
                cellRange.Text = order.GetOrderCost.ToString();

                cellRange = priceTable.Cell(i + 2, 6).Range;
                cellRange.Text = order.GetSaleName;

            }



            application.Visible = true;

        }
        private void Spravka_Click(object sender, RoutedEventArgs e)
        {
            string inforamtion = "В данном меню, созданном для автоматизации работы сотрудников представлен функционал предложения скидок пользователям." +
                "\nДля предложения скидки выберите заказ, далее укажите необходимую скидку, затем нажмите на кнопку Предложить скидку.";
            MessageBox.Show(inforamtion, "Справка", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
