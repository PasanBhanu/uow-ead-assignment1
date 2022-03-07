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
        public double MonthlyBudget { get; set; }

        public void CreateDatasetRow(DataStore dataStore)
        {
            DataStore.CategoriesRow row = dataStore.Categories.NewCategoriesRow();
            row.Title = Title;
            row.Type = (int) Type;
            row.MonthlyBudget = MonthlyBudget;
            dataStore.Categories.AddCategoriesRow(row);
        }

        public void LoadDatasetRow(DataStore.CategoriesRow row)
        {
            Id = row.Id;
            Title = row.Title;
            Type = (TransactionType) row.Type;
            MonthlyBudget = row.MonthlyBudget;
        }

        public void UpdateDatasetRow(DataStore.CategoriesRow row) { 
            row.Title = Title;
            row.Type = (int)Type;
            row.MonthlyBudget = MonthlyBudget;
            row.AcceptChanges();
        }      
    }
}
