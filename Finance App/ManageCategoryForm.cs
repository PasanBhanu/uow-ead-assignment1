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

            // Load category object
            DataRow = (DataStore.CategoriesRow)DataStore.Tables["Categories"].Rows.Find(id);
            category.LoadDatasetRow(DataRow);

            txtCategoryName.Text = category.Title;
            cmbCategoryType.Text = category.Type.ToString();
        }

        private void UpdateCategory(object sender, EventArgs e)
        {
            // Validations
            if (txtCategoryName.Text == "" || cmbCategoryType.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all the data fields!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Update category object
            category.Title = txtCategoryName.Text;
            if (cmbCategoryType.SelectedItem.ToString() == "Income")
            {
                category.Type = TransactionType.Income;
            }
            else
            {
                category.Type = TransactionType.Expense;
            }
            category.UpdateDatasetRow(DataRow);

            MessageBox.Show("Category updated successfully!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
