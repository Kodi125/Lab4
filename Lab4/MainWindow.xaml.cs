/* #############################
 * 
 * Author: Johnathon Mc Grory
 * Date : 7 / 3 / 2020
 * Description : Lab 4 Code
 * 
 * ############################# */
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

namespace Lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NORTHWNDEntities1 db = new NORTHWNDEntities1();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void lbxStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var query = from p in db.Products
                        where p.UnitsInStock < 50
                        orderby p.ProductName
                        select p.ProductName;

            string selected = lbxStock.SelectedItem as string;

            switch (selected)
            {
                case "Low":
                    //do nothing as query sorted from above
                    break;

                case "Normal":
                    query = from p in db.Products
                            where p.UnitsInStock >= 50 && p.UnitsInStock <= 100
                            orderby p.ProductName
                            select p.ProductName;
                    break;
                case "Overstocked":
                    query = from p in db.Products
                            where p.UnitsInStock > 100
                            orderby p.ProductName
                            select p.ProductName;
                    break;
            }
            //update products list
            lbxProducts.ItemsSource = query.ToList();
        }

        private void lbxSuppliers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //using the selected value path 
            int supplierID = Convert.ToInt32(lbxSuppliers.SelectedValue);

            //MessageBox.Show(supplierId.ToString());

            var query = from p in db.Products
                        where p.SupplierID == supplierID
                        orderby p.ProductName
                        select p.ProductName;

            //update product list
            lbxProducts.ItemsSource = query.ToList();
        }

        private void lbxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string country = (string)(lbxCountries.SelectedValue);

            var query = from p in db.Products
                        where p.Supplier.Country == country
                        orderby p.ProductName
                        select p.ProductName;

            //update product list
            lbxProducts.ItemsSource = query.ToList();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //populate stock level resources
            lbxStock.ItemsSource = Enum.GetNames(typeof(StockLevel));

            //populate the suppliers listbox using anonymous type 
            var query1 = from s in db.Suppliers
                         orderby s.CompanyName
                         //anonymous type as there is no object defined
                         select new
                         {
                             SupplierName = s.CompanyName,
                             SupplierID = s.SupplierID,
                             Country = s.Country
                         };
            lbxSuppliers.ItemsSource = query1.ToList();

            //Populate the countries list
            //here you can either speecify the database location and select and orderby again OR you can make use of quer1
            //which has already specified the database and just change the OrderBy and Select statements

            //var query2 = from s in db.Suppliers
            //             orderby s.Country
            //             select s.Country;

            //lambda syntax 
            var query2 = query1
                .OrderBy(s => s.Country)
                .Select(s => s.Country);

            var countries = query2.ToList();

            lbxCountries.ItemsSource = countries.Distinct();
        }// end of window loaded 
    }
    public enum StockLevel
    {
        Low,
        Normal,
        Overstocked
    };
}
