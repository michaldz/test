using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interview.Models;

namespace Interview.Services.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetPayments();
        Payment GetPayment(string id);
        Payment CreatePayment(Payment payment);
        Payment UpdatePayment(Payment payment);
        void DeletePayment(string id);

    }
}