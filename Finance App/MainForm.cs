﻿using Finance_App.Models;
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
            categoryIncome.MonthlyBudget = 0;
            categoryIncome.CreateDatasetRow(DataStore);

            Category categoryExpense = new Category();
            categoryExpense.Title = "Default Expense";
            categoryExpense.Type = TransactionType.Expense;
            categoryExpense.MonthlyBudget = 0;
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
            }
        }
    }
}
