using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    /// Логика взаимодействия для Administration.xaml
    /// </summary>

    public partial class Administration : Window
    {
        ApplicationContext db;
        ArrayList ingsList;
        public Administration(int mode)
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Pizzas.Load();
            if (mode == 0)
            {
                this.Closing += Administration_Closing;
                AddPizza.Click += AddPizza_Click;
                Back.Click += Back_Click;
            }
            else
            {
                PizzaMode();
                ingsList = new ArrayList();
                AddPizza.Click += AddPizzaUser_Click;
                Back.Click += BackUser_Click;
                
            }
            MenuGrid.ItemsSource = db.Pizzas.Local.ToBindingList();
            IngridientsList.ItemsSource = db.Ingridients.Local.ToBindingList();

        }

        private void Administration_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
            AuthReg ar = new AuthReg();
            ar.Show();
        }

        private void EmployeeRegButton_Click(object sender, RoutedEventArgs e)
        {
            AuthReg ar = new AuthReg(true);
            ar.Show();
            this.Closing -= Administration_Closing;
            db.Dispose();
            this.Close();
        }


        private void BackUser_Click(object sender, RoutedEventArgs e)
        {
            this.Closing -= Administration_Closing;
            db.Dispose();
            this.Close();


        }

        private void IngridientsControl_Click(object sender, RoutedEventArgs e)
        {
            Ingridients ingr = new Ingridients();
            ingr.ShowDialog();
            

        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.SaveChanges();
                db.Pizzas.Load();
                MenuGrid.ItemsSource = db.Pizzas.Local.ToBindingList();

            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Сначала заполните данные у созданных ингридиентов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        private void AddPizzaMenu_Click(object sender, RoutedEventArgs e)
        {
            PizzaMode();

            ingsList = new ArrayList();

        }

        private void AddPizza_Click(object sender, RoutedEventArgs e)
        {

            if(IngridientsList.SelectedItems.Count < 2 || PizzaName.Text.Length < 5)
            {
                MessageBox.Show("Некорректные данные для создания пиццы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            var ings = IngridientsList.SelectedItems;
            
            foreach (Ingridient ing in ings)
            {
                ingsList.Add(ing.Id);
            }
            Pizza pizza = new Pizza() { Name = Convert.ToString(PizzaName.Text),  IngridientsList = Functions.ArrayListToString(ingsList) };
            PizzaName.Text = "";
            db.Pizzas.Add(pizza);
            PizzasMode();
        }

        private void AddPizzaUser_Click(object sender, RoutedEventArgs e)
        {

            if (IngridientsList.SelectedItems.Count < 2 || PizzaName.Text.Length < 5)
            {
                MessageBox.Show("Некорректные данные для создания пиццы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var ings = IngridientsList.SelectedItems;

            foreach (Ingridient ing in ings)
            {
                ingsList.Add(ing.Id);
            }
            Pizza pizza = new Pizza() { Name = Convert.ToString(PizzaName.Text), IngridientsList = Functions.ArrayListToString(ingsList), IsOnMenu = true };
            PizzaName.Text = "";
            db.Pizzas.Add(pizza);
            SaveChanges_Click(sender, e);
            BackUser_Click(sender, e);
        }


        private void PizzasMode()
        {
            PizzasGrid.Visibility = Visibility.Visible;
            PizzaGrid.Visibility = Visibility.Hidden;
            AddPizza.Visibility = Visibility.Hidden;
            Back.Visibility = Visibility.Hidden;
            DeletePizza.Visibility = Visibility.Visible;
            //EditPizza.Visibility = Visibility.Visible;
            EmployeeRegButton.Visibility = Visibility.Visible;
            IngridientsControl.Visibility = Visibility.Visible;
            OrderAccounting.Visibility = Visibility.Visible;
            GridLabel.Content = "Список пицц";
            Spravka.Visibility = Visibility.Visible;
            SaveChanges.Visibility = Visibility.Visible;
        }

        private void PizzaMode()
        {
            PizzasGrid.Visibility = Visibility.Hidden;
            PizzaGrid.Visibility = Visibility.Visible;
            AddPizza.Visibility = Visibility.Visible;
            Back.Visibility = Visibility.Visible;
            DeletePizza.Visibility = Visibility.Hidden;
            EmployeeRegButton.Visibility = Visibility.Hidden;
            IngridientsControl.Visibility = Visibility.Hidden;
            OrderAccounting.Visibility = Visibility.Hidden;
            Spravka.Visibility = Visibility.Hidden;

            //EditPizza.Visibility = Visibility.Hidden;
            GridLabel.Content = "Создание пиццы";
            SaveChanges.Visibility = Visibility.Hidden;
            db.Ingridients.Load();
            IngridientsList.ItemsSource = db.Ingridients.Local.ToBindingList();
            IngridientsList.UnselectAll();
        }


        private void EditPizza_Click(object sender, RoutedEventArgs e)
        {
            Binding bind = new Binding();
            bind.ElementName = "Pizza";
            bind.Path = new PropertyPath("Name");
            IngridientsInGrid.Binding = bind;
        }

        private void DeletePizza_Click(object sender, RoutedEventArgs e)
        {
            Pizza pizza = (Pizza)MenuGrid.SelectedItem;
            db.Pizzas.Remove(pizza);
            DeletePizza.IsEnabled = false;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            PizzasMode();


        }


        private void IngridientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IngridientCost.Text = "0";
            var ings = IngridientsList.SelectedItems;
            foreach (Ingridient ing in ings)
            {
                IngridientCost.Text = Convert.ToString(Convert.ToInt32(IngridientCost.Text) + ing.Cost);
            }
        }

        private void OrderAccounting_Click(object sender, RoutedEventArgs e)
        {
            OrdersAccounting mw = new OrdersAccounting(1);
            mw.ShowDialog();
        }

        private void MenuGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeletePizza.IsEnabled = true;
        }

        private void Spravka_Click(object sender, RoutedEventArgs e)
        {
            string inforamtion = "С интсрументами администратора вы с лёгкостью составите меню своей пиццерии, добавите новые пиццы, ингредиенты, зарегистрируете нового сотрудника и экспортируете полный список заказов в MS Word" +
                "\n\nНажмите на кнопку Добавление пиццы, чтобы открыть конструктор пицц, далее выберите подъодящее название и ингредиенты и нажмите добавить пиццу." +
                "\n\nКнопка Удалить позволит удалить выбранную в списке пиццу." +
                "\n\nКнопка Сохранить изменения сохранит любые изменнения меню." +
                "\n\nКнопка управление ингредиентами позволит открыть и управлять списком ингредиентов." +
                "\n\nКнопка Учёт заказов переведёт вас в меню отображения заказов, где их можно экспортировать.";
            MessageBox.Show(inforamtion, "Справка", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
