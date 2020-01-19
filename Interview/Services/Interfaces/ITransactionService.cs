using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models;

namespace Interview.Services.Interfaces
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetTransactions();
        Transaction GetTransaction(string id);
        Transaction CreateTransaction(Transaction transaction);
        Transaction UpdateTransaction(Transaction transaction);
        void DeleteTransaction(string id);

    }
}