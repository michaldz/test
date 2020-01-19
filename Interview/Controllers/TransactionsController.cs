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
    [RoutePrefix("api/transactions")]
    public class TransactionsController : ApiController
    {
        const string transactionRouteName = "transactionRouteName";
        const int unprocessableEntityHttpStatusCode = 422;

        private readonly ITransactionService transactionService;
        public TransactionsController()
        {
            //TODO: Add dependency Injection Container
            transactionService = new TransactionService();
        }

        /// <summary>
        /// Get Transactions
        /// </summary>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="500">Internal Error</response>
        [ResponseType(typeof(IEnumerable<Transaction>))]
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetTransactions()
        {
            try
            {
                return Ok(transactionService.GetTransactions());
            }
            catch (Exception)
            {
                return InternalServerError();
            }

        }

        /// <summary>
        /// Get Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [ResponseType(typeof(Transaction))]
        [Route("{id:Guid}", Name = transactionRouteName)]
        [HttpGet]
        public IHttpActionResult GetTransaction(string id)
        {
            try
            {
                return Ok(transactionService.GetTransaction(id));
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
        /// Create Transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <response code="201">Created</response>
        /// <response code="409">Conflict</response>
        /// <response code="500">Internal Error</response>
        [ResponseType(typeof(Transaction))]
        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateTransaction([FromBody]Transaction transaction)
        {
            try
            {
                var newTransaction = transactionService.CreateTransaction(transaction);
                return CreatedAtRoute<Transaction>(transactionRouteName, new { id = newTransaction.Id }, newTransaction);
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
        /// Update Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <response code="200">Updated</response>
        /// <response code="404">Not Found</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Error</response> 
        [ResponseType(typeof(Transaction))]
        [Route("{id:Guid}")]
        [HttpPut]
        public IHttpActionResult UpdateTransaction(string id, [FromBody]Transaction transaction)
        {
            try
            {
                transaction.Id = id;
                return Ok(transactionService.UpdateTransaction(transaction));
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
        /// Delete Transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Updated</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Error</response>
        [Route("{id:Guid}")]
        [HttpDelete]
        public IHttpActionResult DeleteTransaction(string id)
        {
            try
            {
                transactionService.DeleteTransaction(id);
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
