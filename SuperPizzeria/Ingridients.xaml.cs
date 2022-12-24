using System;
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
    /// Логика взаимодействия для Ingrdients.xaml
    /// </summary>
    public partial class Ingridients : Window
    {
        ApplicationContext db;
        private List<Ingridient> ingList;
        public Ingridients()
        {
            InitializeComponent();
            db = new ApplicationContext();
            db.Ingridients.Load();
            ingList = db.Ingridients.Local.ToList();
            IngridientsList.ItemsSource = ingList;
            this.Closing += Ingridients_Closing;

        }

        private void Ingridients_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Ingridient ing = new Ingridient();
            db.Ingridients.Add(ing);
            IngridientsList.ItemsSource = null;
            ingList = db.Ingridients.Local.ToList();
            IngridientsList.ItemsSource = ingList;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(sender);
        }

        private bool IsAllFull()
        {
            
            foreach(Ingridient ing in ingList)
            {
                if (ing.Name is null || ing.Cost<=0)
                    return false;
            }
            return true;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(IsAllFull())
                    db.SaveChanges();
                else
                    MessageBox.Show("Прежде чем сохранять список, заполните все ингредиенты корректнымми данными", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Сначала заполните данные у созданных ингридиентов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Ingridient ing = (Ingridient)IngridientsList.SelectedItem;
            db.Ingridients.Remove(ing);
            Delete.IsEnabled = false;
            IngridientsList.ItemsSource = null;
            ingList = db.Ingridients.Local.ToList();
            IngridientsList.ItemsSource = ingList;
        }

        private void IngridientsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Delete.IsEnabled = true;
        }
    }
}
