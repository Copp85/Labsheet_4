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
using System.Windows.Shapes;

namespace Ex_1
{
    /// <summary>
    /// Interaction logic for AddExpWindow.xaml
    /// </summary>
    public partial class AddExpWindow : Window
    {
        public AddExpWindow()
        {
            InitializeComponent();
            cbxType.ItemsSource = new string[] { "Entertainment", "Office", "Travel" };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //read data from screen
            string type = cbxType.SelectedItem as string;
            decimal amt = Convert.ToDecimal(tbxAmount.Text);
            DateTime date = dpDate.SelectedDate.Value;

            //create expense object
            Expenses newExp = new Expenses(type, amt, date);

            //get reference to main window
            MainWindow main = Owner as MainWindow;

            //add to collection
            main.incurredExpenses.Add(newExp);

            //Closed the window
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            //Close the window
            this.Close();
        }
    }
}
