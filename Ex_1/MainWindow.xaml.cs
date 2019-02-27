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


namespace Ex_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public ObservableCollection<Expenses> incurredExpenses = new ObservableCollection<Expenses>();

        ObservableCollection<Expenses> matchedExpenses = new ObservableCollection<Expenses>();



        string[] names = { "Travel", "Office", "Entertainment" };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Create expense objects

            Random randomFactory = new Random();


            //create 3 expense objects

            Expenses exp1 = GetRandomExpense(randomFactory);
            Expenses exp2 = GetRandomExpense(randomFactory);
            Expenses exp3 = GetRandomExpense(randomFactory);
            Expenses exp4 = GetRandomExpense(randomFactory);


            //add to list collection
            //add to collection
            incurredExpenses.Add(exp1);
            incurredExpenses.Add(exp2);
            incurredExpenses.Add(exp3);
            incurredExpenses.Add(exp4);


            //Populate list items in listbox
            lbxExp.ItemsSource = incurredExpenses;

            //Get total


            decimal total = Expenses.TotalExpenses;
            totalExpBlk.Text = string.Format("{0:C}", total);

            //populate combo box
            cbxFilter.ItemsSource = names;
        }


        private Expenses GetRandomExpense(Random randomFactory)
        {
            Random rf = new Random();

            int randNumber = rf.Next(0, 3);
            string randomCategory = names[randNumber];

            decimal randomAmount = (decimal)randomFactory.Next(1, 10001) / 100;

            DateTime randomDate = DateTime.Now.AddDays(-randomFactory.Next(0, 32));

            Expenses randomExpense = new Expenses(randomCategory, randomAmount, randomDate);

            return randomExpense;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            //identify which expense is selected
            Expenses selectedExpense = lbxExp.SelectedItem as Expenses;

            if (selectedExpense != null)
            {
                //remove that expense
                Expenses.TotalExpenses -= selectedExpense.Cost;
                incurredExpenses.Remove(selectedExpense);

                decimal total = Expenses.TotalExpenses;
                totalExpBlk.Text = string.Format("{0:C}", total);
            }

        }

        //Add another expense
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Random randomFactory = new Random();
            //Expenses exp = GetRandomExpense(randomFactory);
            //incurredExpenses.Add(exp);

            //decimal total = Expenses.TotalExpenses;
            //totalExpBlk.Text = string.Format("{0:C}", total);

            AddExpWindow addExp = new AddExpWindow();
            addExp.Owner = this;
            addExp.ShowDialog();

         }

        //Search from search box
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //read from search box
            string searchEntry = tbxSearch.Text;
            if (!string.IsNullOrEmpty(searchEntry))
            {
                matchedExpenses.Clear();
                //Match with items in colletion
                foreach (Expenses exps in incurredExpenses)
                {
                    string expenseType = exps.Name;

                    if (expenseType.Equals(searchEntry))
                    {
                        matchedExpenses.Add(exps);
                    }
                }
                //Display matches
                lbxExp.ItemsSource = matchedExpenses;
            }

        }

        private void CbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get user selection
            string selectedExpenseType = cbxFilter.SelectedItem as string;

            if (selectedExpenseType != null)

            {
                matchedExpenses.Clear();
                //search user selection
                foreach (Expenses exp in incurredExpenses )
                {
                    //match user selection
                    string expCategory = exp.Name;

                    if (expCategory.Equals(selectedExpenseType))
                    {
                        matchedExpenses.Add(exp);
                    }
                        

                }
                

                //display
                lbxExp.ItemsSource = matchedExpenses;
            }


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //get string of onjects - json formatted
            string json = JsonConvert.SerializeObject(incurredExpenses, Formatting.Indented);

            //write that to file
            using (StreamWriter sw = new StreamWriter(@"C:\Users\S00190494\Documents\expenses.json"))
            {
                sw.Write(json);
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            //connect to fil
            using (StreamReader sr = new StreamReader(@"C:\Users\S00190494\Documents\expenses.json"))
            {
                //read text
                string json = sr.ReadToEnd();

                //convert to json
                incurredExpenses = JsonConvert.DeserializeObject<ObservableCollection<Expenses>>(json);

                //display
                lbxExp.ItemsSource = incurredExpenses;
            }

        }
    }
}
