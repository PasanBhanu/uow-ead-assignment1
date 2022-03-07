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
    public partial class AddCategoryForm : Form
    {
        DataStore DataStore = Variables.dataStore;

        public AddCategoryForm()
        {
            InitializeComponent();
        }

        private void AddCategory(object sender, EventArgs e)
        {
            Category category = new Category();
            category.Title = txtCategoryName.Text;
            if (cmbCategoryType.SelectedItem.ToString() == "Income")
            {
                category.Type = TransactionType.Income;
            }
            else { 
                category.Type = TransactionType.Expense; 
            }
            category.MonthlyBudget = Double.Parse(txtMonthlyBudget.Text);
            category.CreateDatasetRow(DataStore);

            MessageBox.Show("Category added successfully!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
