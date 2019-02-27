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

using System.Collections.ObjectModel;

using Newtonsoft.Json;
using System.IO;

namespace Ex_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> myBooks = new ObservableCollection<Book>();
        ObservableCollection<Book> matchedBooks = new ObservableCollection<Book>();

        string[] genres = { "Non-fiction", "Science-Fiction", "Educational", };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create book objects
            Book bk1 = new Book() { Title= "Foundation", Author ="Isaac Asimov", Genre = "Science-Fiction" };
            Book bk2 = new Book() { Title = "Comptia A+", Author = "Mike Meyers", Genre = "Educational" };
            Book bk3 = new Book() { Title = "The demon haunted world", Author = "Carl Sagan", Genre = "Non-fiction" };

            //Populate list items in listbox
            lbxBk.ItemsSource = myBooks;

            //add books to list
            myBooks.Add(bk1);
            myBooks.Add(bk2);
            myBooks.Add(bk3);

            //populate combo box
            cbxFilter.ItemsSource = genres;
        }


        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //identify which book is selected
            Book selectedBook = lbxBk.SelectedItem as Book;

            if (selectedBook != null)
            {
                //remove that book
                 myBooks.Remove(selectedBook);

                
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           

            AddBookWindow addBk = new AddBookWindow();
            addBk.Owner = this;
            addBk.ShowDialog();

        }

        //Search from search box
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //read from search box
            string searchEntry = tbxSearch.Text;
            if (!string.IsNullOrEmpty(searchEntry))
            {
                matchedBooks.Clear();
                //Match with items in colletion
                foreach (Book bks in matchedBooks)
                {
                    string BookName = bks.Title;

                    if (BookName.Equals(searchEntry))
                    {
                        matchedBooks.Add(bks);
                    }
                }
                //Display matches
                lbxBk.ItemsSource = matchedBooks;
            }

        }

        private void CbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get user selection
            string selectedBookGenre = cbxFilter.SelectedItem as string;

            if (selectedBookGenre != null)

            {
                matchedBooks.Clear();
                //search user selection
                foreach (Book bks in myBooks)
                {
                    //match user selection
                    string genreName = bks.Genre;

                    if (genreName.Equals(selectedBookGenre))
                    {
                        matchedBooks.Add(bks);
                    }
                    
                }

                //display
                lbxBk.ItemsSource = matchedBooks;
            }


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //get string of onjects - json formatted
            string json = JsonConvert.SerializeObject(myBooks, Formatting.Indented);

            //write that to file
            using (StreamWriter sw = new StreamWriter(@"C:\Users\S00190494\Documents\books.json"))
            {
                sw.Write(json);
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            //connect to file
            using (StreamReader sr = new StreamReader(@"C:\Users\S00190494\Documents\books.json"))
            {
                //read text
                string json = sr.ReadToEnd();

                //convert to json
                myBooks = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);

                //display
                lbxBk.ItemsSource = myBooks;
            }

        }

    }
}
