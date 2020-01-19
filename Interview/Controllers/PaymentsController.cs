using Interview.Models;
using Interview.Services;
using Interview.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Interview.Controllers
{
    [RoutePrefix("api/payments")]
    public class PaymentsController : ApiController
    {
        const string paymentRouteName = "paymentRouteName";
        const int unprocessableEntityHttpStatusCode = 422;

        private readonly IPaymentService paymentService;
        public PaymentsController()
        {
            //TODO: Add dependency Injection Container
            paymentService = new PaymentService();
        }

        /// <summary>
        /// Get Payments
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Error</response>
        [ResponseType(typeof(IEnumerable<Payment>))]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPayments()
        {
            try
            {
                return Ok(paymentService.GetPayments());
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        /// <summary>
        /// Get payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [ResponseType(typeof(Payment))]
        [Route("{id:Guid}", Name = paymentRouteName)]
        [HttpGet]
        public IHttpActionResult GetPayment(string id)
        {
            try
            {
                return Ok(paymentService.GetPayment(id));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Create new payment
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Error</response>
        [ResponseType(typeof(Payment))]
        [Route("")]
        [HttpPost]
        public IHttpActionResult Post([FromBody]Payment payment)
        {
            try
            {
                var newPayment = paymentService.CreatePayment(payment);
                return CreatedAtRoute<Payment>(paymentRouteName, new { id = newPayment.Id }, newPayment);
            }
            catch (ArgumentException)
            {
                return StatusCode(HttpStatusCode.Conflict);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Update Payment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        /// <response code="200">Updated</response>
        /// <response code="404">Not Found</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Error</response> 
        [ResponseType(typeof(Payment))]
        [Route("{id:Guid}")]
        [HttpPut]
        public IHttpActionResult Put(string id, [FromBody]Payment payment)
        {
            try
            {
                payment.Id = id;
                return Ok(paymentService.UpdatePayment(payment));
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException)
            {
                return StatusCode((HttpStatusCode)unprocessableEntityHttpStatusCode);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Delete Payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Updated</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [Route("{id:Guid}")]
        [HttpDelete]
        public IHttpActionResult Delete(string id)
        {
            try
            {
                paymentService.DeletePayment(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
