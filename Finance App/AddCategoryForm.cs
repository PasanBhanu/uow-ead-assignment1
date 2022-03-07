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
            // Validations
            if (txtCategoryName.Text == "" || cmbCategoryType.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill all the data fields!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DataTable tblCategories = DataStore.Tables["Categories"];
            DataRow[] results = tblCategories.Select("Title = '" + txtCategoryName.Text + "'");
            if (results.Length > 0)
            {
                MessageBox.Show("Category with the same name already exists. Please use a different name!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Create new category object
            Category category = new Category();
            category.Title = txtCategoryName.Text;
            if (cmbCategoryType.SelectedItem.ToString() == "Income")
            {
                category.Type = TransactionType.Income;
            }
            else { 
                category.Type = TransactionType.Expense; 
            }
            category.CreateDatasetRow(DataStore);

            MessageBox.Show("Category added successfully!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}
