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
    public class PaymentService : IPaymentService
    {
        private readonly IGenericDataRepository<Payment> _paymentRepository;
        public PaymentService()
        {
            //TODO: Add dependency Injection Container
            _paymentRepository = new JsonFileDataRepository<Payment>();
        }

        public IEnumerable<Payment> GetPayments()
        {
            return _paymentRepository.GetAll();
        }

        public Payment GetPayment(string id)
        {
            var payment = _paymentRepository.Get(id);
            if (payment != null)
            {
                return payment;
            }
            throw new KeyNotFoundException();
        }

        public Payment CreatePayment(Payment payment)
        {
            if (payment.PostingDate == null)
            {
                payment.PostingDate = DateTime.UtcNow;
            }
            return _paymentRepository.Add(payment);
        }
        public Payment UpdatePayment(Payment payment)
        {
            var _payment = _paymentRepository.Update(payment);
            if (_payment != null)
            {
                return _payment;
            }
            throw new KeyNotFoundException();
        }

        public void DeletePayment(string id)
        {
            if (!_paymentRepository.Delete(id))
            {
                throw new KeyNotFoundException();
            }
        }
    }
}