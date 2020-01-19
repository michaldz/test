using Interview.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interview.Models;
using System.Web.Http.Results;
using System.Web.Http;

namespace Interview.Tests.Controllers
{
    [TestClass]
    public class PaymentsControllerTest
    {
        [TestMethod]
        public void GetPayments_ShouldReturnAllPayments()
        {
            // Arrange
            PaymentsController controller = new PaymentsController();

            // Act
            IHttpActionResult actionResult = controller.GetPayments();
            var contentResult = actionResult as OkNegotiatedContentResult<IEnumerable<Payment>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsTrue(contentResult.Content.Count() > 0);
        }
    }
}
