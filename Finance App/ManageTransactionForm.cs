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
    public partial class ManageTransactionForm : Form
    {
        DataStore DataStore = Variables.dataStore;
        DataStore.TransactionsRow DataRow;
        Transaction transaction = new Transaction();

        public ManageTransactionForm(int id)
        {
            InitializeComponent();
            DataTable tblCategories = DataStore.Tables["Categories"];
            foreach (DataRow row in tblCategories.Rows)
            {
                cmbCategory.Items.Add(row["Title"].ToString());
            }

            DataRow = (DataStore.TransactionsRow)DataStore.Tables["Transactions"].Rows.Find(id);
            transaction.LoadDatasetRow(DataRow);

            txtDescription.Text = transaction.Description;
            cmbTransactionType.Text = transaction.Type.ToString();
            txtAmount.Text = transaction.Amount.ToString();
            cmbCategory.Text = transaction.CategoryName;
            chkRecurring.Checked = transaction.IsReccuring;
            dtpDate.Value = transaction.Date;
        }

        private void UpdateTransaction(object sender, EventArgs e)
        {
            transaction.Description = txtDescription.Text;
            transaction.Amount = double.Parse(txtAmount.Text);
            transaction.Date = DateTime.Parse(dtpDate.Text);
            transaction.IsReccuring = chkRecurring.Checked;
            if (cmbTransactionType.SelectedItem.ToString() == "Income")
            {
                transaction.Type = TransactionType.Income;
            }
            else
            {
                transaction.Type = TransactionType.Expense;
            }
            DataTable tblCategories = DataStore.Tables["Categories"];
            DataRow[] results = tblCategories.Select("Title = '" + cmbCategory.Text + "'");
            foreach (DataRow row in results)
            {
                transaction.CategoryId = (int)row["Id"];
            }
            transaction.CreateDatasetRow(DataStore);

            MessageBox.Show("Transaction updated successfully!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
