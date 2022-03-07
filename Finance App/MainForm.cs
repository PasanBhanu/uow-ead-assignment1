using Finance_App.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Finance_App
{
    public partial class MainForm : Form
    {
        DataStore DataStore = Variables.dataStore;

        public MainForm()
        {
            InitializeComponent();

            // Add Default Data
            Category categoryIncome = new Category();
            categoryIncome.Title = "Default Income";
            categoryIncome.Type = TransactionType.Income;
            categoryIncome.CreateDatasetRow(DataStore);

            Category categoryExpense = new Category();
            categoryExpense.Title = "Default Expense";
            categoryExpense.Type = TransactionType.Expense;
            categoryExpense.CreateDatasetRow(DataStore);
        }

        private void ViewCategories(object sender, EventArgs e)
        {
            CategoriesForm categoriesForm = new CategoriesForm();
            categoriesForm.ShowDialog();
        }

        private void AddTransaction(object sender, EventArgs e)
        {
            AddTransactionForm addTransactionForm = new AddTransactionForm();
            addTransactionForm.ShowDialog();
            addTransactionForm.Dispose();
            LoadTransactions();
        }

        private void EditTransaction(object sender, EventArgs e)
        {
            int id = int.Parse(listTransactions.SelectedItems[0].SubItems[0].Text);
            ManageTransactionForm manageTransactionForm = new ManageTransactionForm(id);
            manageTransactionForm.ShowDialog();
            manageTransactionForm.Dispose();
            LoadTransactions();
        }

        private void DeleteTransaction(object sender, EventArgs e)
        {
            int id = int.Parse(listTransactions.SelectedItems[0].SubItems[0].Text);
            DialogResult dialogResult = MessageBox.Show("Are you sure do you want to delete this transaction?", "Simply Finance App", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                DataStore.Tables["Transactions"].Rows.Remove(DataStore.Tables["Transactions"].Rows.Find(id));
                LoadTransactions();
            }
        }

        private void LoadFormData(object sender, EventArgs e)
        {
            LoadTransactions();
        }
        public void LoadTransactions()
        {
            DateTime today = DateTime.Today;
            DateTime weekStart = DateTime.Today.AddDays(-(int)today.DayOfWeek);
            double totalDailyIncome = 0;
            double totalDailyExpense = 0;
            double totalWeeklyIncome = 0;
            double totalWeeklyExpense = 0;

            listTransactions.Items.Clear();
            foreach (DataStore.TransactionsRow row in DataStore.Tables["Transactions"].Rows)
            {
                string[] listItem = new string[6];
                listItem[0] = row["Id"].ToString();
                listItem[1] = DateTime.Parse(row["Date"].ToString()).ToString("d");
                listItem[2] = row["Description"].ToString();
                listItem[3] = row.GetParentRow("Categories_Transactions")["Title"].ToString();
                listItem[4] = ((TransactionType)row["Type"]).ToString();
                listItem[5] = row["Amount"].ToString();

                ListViewItem item = new ListViewItem(listItem);
                listTransactions.Items.Add(item);

                
                if (DateTime.Parse(row["Date"].ToString()) == today)
                {
                    if (((TransactionType)row["Type"]) == TransactionType.Income)
                    {
                        totalDailyIncome += double.Parse(row["Amount"].ToString());
                    }
                    else
                    {
                        totalDailyExpense += double.Parse(row["Amount"].ToString());
                    }
                }

                if (DateTime.Parse(row["Date"].ToString()) >= weekStart)
                {
                    if (((TransactionType)row["Type"]) == TransactionType.Income)
                    {
                        totalWeeklyIncome += double.Parse(row["Amount"].ToString());
                    }
                    else
                    {
                        totalWeeklyExpense += double.Parse(row["Amount"].ToString());
                    }
                }
            }
            lblTotalDailyExpense.Text = totalDailyExpense.ToString();
            lblTotalDailyIncome.Text = totalDailyIncome.ToString();
            lblTotalWeeklyIncome.Text = totalWeeklyIncome.ToString();
            lblTotalWeeklyExpense.Text = totalWeeklyExpense.ToString();

            btnEditTransaction.Enabled = false;
            btnDeleteTransaction.Enabled = false;
        }

        private void ViewReport(object sender, EventArgs e)
        {
            ReportForm reportForm = new ReportForm();
            reportForm.ShowDialog();
        }

        private void DisplayTransactionOptions(object sender, EventArgs e)
        {
            if (listTransactions.SelectedItems.Count == 1)
            {
                btnEditTransaction.Enabled = true;
                btnDeleteTransaction.Enabled = true;
            }
            else
            {
                btnEditTransaction.Enabled = false;
                btnDeleteTransaction.Enabled = false;
            }
        }
    }
}
