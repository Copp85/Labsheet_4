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

namespace Ex_2
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();
            cbxGenre.ItemsSource = new string[] { "Non-fiction", "Science-Fiction", "Educational" };
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //read data from screen
            string title = tbxtitle.Text;
            string author = tbxAuthor.Text;
            string genre = cbxGenre.SelectedItem as string;
          

            //create book object
            Book newBook = new Book(title, author, genre);

            //get reference to main window
            MainWindow main = Owner as MainWindow;

            //add to collection

            main.myBooks.Add(newBook);

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
