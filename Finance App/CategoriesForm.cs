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
    public partial class CategoriesForm : Form
    {
        DataStore DataStore = Variables.dataStore;

        public CategoriesForm()
        {
            InitializeComponent();
        }

        private void AddCategory(object sender, EventArgs e)
        {
            AddCategoryForm addCategoryForm = new AddCategoryForm();
            addCategoryForm.ShowDialog();
            addCategoryForm.Dispose();
            LoadCategories();
        }

        private void EditCategory(object sender, EventArgs e)
        {
            int id = int.Parse(listCategories.SelectedItems[0].SubItems[0].Text);
            ManageCategoryForm manageCategoryForm = new ManageCategoryForm(id);
            manageCategoryForm.ShowDialog();
            manageCategoryForm.Dispose();
            LoadCategories();
        }

        private void DeleteCategory(object sender, EventArgs e)
        {
            int id = int.Parse(listCategories.SelectedItems[0].SubItems[0].Text);

            // Validations
            DataTable tblTransactions = DataStore.Tables["Transactions"];
            DataRow[] results = tblTransactions.Select("CategoryId = '" + id + "'");
            if (results.Length > 0)
            {
                MessageBox.Show("This category has transactions. Delete not permitted!", "Simply Finance App", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // Delete category
            DialogResult dialogResult = MessageBox.Show("Are you sure do you want to delete this category?", "Simply Finance App", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                DataStore.Tables["Categories"].Rows.Remove(DataStore.Tables["Categories"].Rows.Find(id));
                LoadCategories();
            }
        }

        private void LoadFormData(object sender, EventArgs e)
        {
            LoadCategories();
        }

        public void LoadCategories()
        {
            listCategories.Items.Clear(); 
            foreach (DataStore.CategoriesRow row in DataStore.Tables["Categories"].Rows)
            {
                string[] listItem = new string[3];
                listItem[0] = row["Id"].ToString();
                listItem[1] = row["Title"].ToString();
                listItem[2] = ((TransactionType) row["Type"]).ToString();

                ListViewItem item = new ListViewItem(listItem);
                listCategories.Items.Add(item);
            }

            btnEditCategory.Enabled = false;
            btnDeleteCategory.Enabled = false;
        }

        private void DisplayCategoryOptions(object sender, EventArgs e)
        {
            if (listCategories.SelectedItems.Count == 1)
            {
                btnEditCategory.Enabled = true;
                btnDeleteCategory.Enabled = true;
            } else
            {
                btnEditCategory.Enabled = false;
                btnDeleteCategory.Enabled = false;
            }
        }
    }
}
