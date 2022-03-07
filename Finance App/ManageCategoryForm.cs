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
    public partial class ManageCategoryForm : Form
    {
        DataStore DataStore = Variables.dataStore;
        DataStore.CategoriesRow DataRow;
        Category category = new Category();

        public ManageCategoryForm(int id)
        {
            InitializeComponent();
            DataRow = (DataStore.CategoriesRow)DataStore.Tables["Categories"].Rows.Find(id);
            category.LoadDatasetRow(DataRow);

            txtCategoryName.Text = category.Title;
            cmbCategoryType.Text = category.Type.ToString();
            txtMonthlyBudget.Text = category.MonthlyBudget.ToString();
        }

        private void UpdateCategory(object sender, EventArgs e)
        {
            category.Title = txtCategoryName.Text;
            if (cmbCategoryType.SelectedItem.ToString() == "Income")
            {
                category.Type = TransactionType.Income;
            }
            else
            {
                category.Type = TransactionType.Expense;
            }
            category.MonthlyBudget = Double.Parse(txtMonthlyBudget.Text);
            category.UpdateDatasetRow(DataRow);

            MessageBox.Show("Category updated successfully!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
