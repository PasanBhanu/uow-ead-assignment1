using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_App.Models
{
    internal class Category
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public TransactionType Type { get; set; }

        // Create new category in DB
        public void CreateDatasetRow(DataStore dataStore)
        {
            DataStore.CategoriesRow row = dataStore.Categories.NewCategoriesRow();
            row.Title = Title;
            row.Type = (int) Type;
            dataStore.Categories.AddCategoriesRow(row);
        }

        // Load category from DB
        public void LoadDatasetRow(DataStore.CategoriesRow row)
        {
            Id = row.Id;
            Title = row.Title;
            Type = (TransactionType) row.Type;
        }

        // Update category row in DB
        public void UpdateDatasetRow(DataStore.CategoriesRow row) { 
            row.Title = Title;
            row.Type = (int)Type;
            row.AcceptChanges();
        }      
    }
}
