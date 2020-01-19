using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Data.Interfaces;
using Interview.Models;
using Interview.Services.Interfaces;
using Interview.Data;

namespace Interview.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IGenericDataRepository<Transaction> _transactionRepository;
        public TransactionService()
        {
            //TODO: Add dependency Injection Container
            _transactionRepository = new JsonFileDataRepository<Transaction>();
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactionRepository.GetAll();
        }

        public Transaction GetTransaction(string id)
        {
            var transaction = _transactionRepository.Get(id);
            if (transaction != null)
            {
                return transaction;
            }
            throw new KeyNotFoundException();
        }

        public Transaction CreateTransaction(Transaction transaction)
        {
            if (transaction.PostingDate == null)
            {
                transaction.PostingDate = DateTime.UtcNow;
            }
            return _transactionRepository.Add(transaction);
        }
        public Transaction UpdateTransaction(Transaction transaction)
        {
            var _transaction = _transactionRepository.Update(transaction);
            if (_transaction != null)
            {
                return _transaction;
            }
            throw new KeyNotFoundException();
        }

        public void DeleteTransaction(string id)
        {
            if (!_transactionRepository.Delete(id))
            {
                throw new KeyNotFoundException();
            }
        }
    }
}