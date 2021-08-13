using LinqKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IRepository<Order> GenericRepository;
        private IOrderRepository<Order> OrderRepository;
        private IRepository<OrderProduct> OrderProductRepository;
        private IRepository<OrderProductOrdered> OrderProductOrderedRepository;
        private IRepository<OrderEmail> OrderEmailRepository;
        private IRepository<OrderTracking> OrderTrackingRepository;
        private IRepository<Finishing> FinishingRepository;

        public OrderController(
            IRepository<Order> GenericRepository,
            IOrderRepository<Order> OrderRepository,
            IRepository<Finishing> FinishingRepository,
            IRepository<OrderProduct> OrderProductRepository,
            IRepository<OrderProductOrdered> OrderProductOrderedRepository,
            IRepository<OrderEmail> OrderEmailRepository,
            IRepository<OrderTracking> OrderTrackingRepository
            )
        {
            this.GenericRepository = GenericRepository;
            this.OrderRepository = OrderRepository;
            this.OrderProductRepository = OrderProductRepository;
            this.OrderProductOrderedRepository = OrderProductOrderedRepository;
            this.OrderEmailRepository = OrderEmailRepository;
            this.OrderTrackingRepository = OrderTrackingRepository;
            this.FinishingRepository = FinishingRepository;

        }

        [HttpPost()]
        [Route("save")]
        [Authorize()]
        public IActionResult Save([FromBody] Order order)
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if ((id == null) || (order == null))
                {
                    return BadRequest("Identificação do usuário não encontrada ou Pedido não enviado.");
                }
                order.ApplicationUserId = id;
                GenericRepository.Insert(order);

                OrderEmailRepository.Insert(new OrderEmail()
                {
                    OrderId = order.Id,
                    Send = false,
                    TypeEmailId = 1
                });
                OrderTrackingRepository.Insert(new OrderTracking()
                {
                    DateTracking = DateTime.Now,
                    OrderId = order.Id,
                    StatusOrderId = 1,
                    StatusPaymentOrderId = 1
                });
                return new JsonResult(order);
            }
            catch (Exception ex)
            {
                return BadRequest("Falha no envio do Pedido. Entre em contato com o administrador do sistema. " + ex);
            }
        }

        [HttpGet()]
        [Route("getAllFinishing")]
        public IActionResult GetAllFinishing()
        {
            try
            {
                return new JsonResult(FinishingRepository.GetAll().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpGet()]
        [Route("getLastOrderByUser")]
        [Authorize()]
        public IActionResult GetLastOrderByUser()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada ou Pedido não enviado.");
                }
                return new JsonResult(GenericRepository.Where(x => x.ApplicationUserId == id).OrderByDescending(x => x.Id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }

        }

        [HttpGet()]
        [Route("getByUser")]
        [Authorize()]
        public IActionResult GetByUser()
        {
            try
            {
                ClaimsPrincipal currentUser = this.User;
                var id = currentUser.Claims.FirstOrDefault(z => z.Type.Contains("primarysid")).Value;
                if (id == null)
                {
                    return BadRequest("Identificação do usuário não encontrada.");
                }
                return new JsonResult(OrderRepository.GetByUser(id).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(string.Concat("Falha no carregamento dos pedidos ", ex.Message));
            }
        }

        [HttpGet("{id}")]
        [Authorize()]
        public IActionResult Get(int id)
        {
            try
            {
                return new JsonResult(OrderRepository.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível carregar o pedido: " + ex.Message);
            }
        }

        [HttpPost()]
        [Route("sendPaymentCielo")]
        [Authorize()]
        public IActionResult SendPaymentCielo([FromBody] SendPaymentCielo sendPaymentCielo)
        {
            try
            {
                if (sendPaymentCielo != null)
                {
                    var orderBase = GenericRepository.Get(sendPaymentCielo.OrderId);
                    orderBase.PaymentId = sendPaymentCielo.PaymentId;
                    orderBase.CapturedAmount = sendPaymentCielo.CapturedAmount;
                    orderBase.Installments = sendPaymentCielo.Installments;
                    GenericRepository.Update(orderBase);
                    OrderTrackingRepository.Insert(new OrderTracking()
                    {
                        DateTracking = DateTime.Now,
                        OrderId = orderBase.Id,
                        StatusOrderId = 1,
                        StatusPaymentOrderId = 2
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Falha no envio do Pagamento. Entre em contato com o administrador do sistema. " + ex);
            }

            return Ok(); ;
        }
    }
}
