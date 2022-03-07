using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance_App.Models
{
    internal class Transaction
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public double Amount { get; set; }
        public bool IsReccuring { get; set; }
        public DateTime Date { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public void CreateDatasetRow(DataStore dataStore)
        {
            DataStore.TransactionsRow row = dataStore.Transactions.NewTransactionsRow();
            row.Description = Description;
            row.Type = (int)Type;
            row.Amount = Amount;
            row.Date = Date;
            row.CategoryId = CategoryId;
            row.IsRecurring = IsReccuring;
            dataStore.Transactions.AddTransactionsRow(row);
        }

        public void LoadDatasetRow(DataStore.TransactionsRow row)
        {
            Id = row.Id;
            Description = row.Description;
            Type = (TransactionType)row.Type;
            Amount = row.Amount;
            Date = row.Date;
            CategoryId = row.CategoryId;
            IsReccuring = row.IsRecurring;
            CategoryName = row.GetParentRow("Categories_Transactions")["Title"].ToString();
        }

        public void UpdateDatasetRow(DataStore.TransactionsRow row)
        {
            row.Description = Description;
            row.Type = (int)Type;
            row.Amount = Amount;
            row.Date = Date;
            row.CategoryId = CategoryId;
            row.IsRecurring = IsReccuring;
            row.AcceptChanges();
        }
    }
}
