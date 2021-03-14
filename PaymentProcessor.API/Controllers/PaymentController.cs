using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentProcessor.API.Model;
using PaymentProcessor.API.Repositories;
using PaymentProcessor.API.Services;
using PaymentProcessor.Data.Entities;
using System;

namespace PaymentProcessor.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentProcessor paymentProcessor,
            IMapper mapper)
        {
            _paymentProcessor = paymentProcessor;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("process")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ProcessPayment(PaymentModel paymentModel)
        {
            if (!ModelState.IsValid)
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            try
            {
                var payment = _mapper.Map<Payment>(paymentModel);
                _paymentProcessor.ProcessPayment(payment);
                return new StatusCodeResult(StatusCodes.Status200OK);
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
