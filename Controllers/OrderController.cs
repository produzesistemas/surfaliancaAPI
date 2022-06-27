using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Linq;
using System.Security.Claims;
using UnitOfWork;

namespace surfaliancaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderRepository orderRepository;
        private IOrderEmailRepository orderEmailRepository;
        private IOrderTrackingRepository orderTrackingRepository;

        public OrderController(
            IOrderRepository orderRepository,
            IOrderEmailRepository orderEmailRepository,
            IOrderTrackingRepository orderTrackingRepository
            )
        {
            this.orderRepository = orderRepository;
            this.orderEmailRepository = orderEmailRepository;
            this.orderTrackingRepository = orderTrackingRepository;
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
                orderRepository.Insert(order);

                orderEmailRepository.Insert(new OrderEmail()
                {
                    OrderId = order.Id,
                    Send = false,
                    TypeEmailId = 1
                });
                orderTrackingRepository.Insert(new OrderTracking()
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
                return new JsonResult(orderRepository.Where(x => x.ApplicationUserId == id).OrderByDescending(x => x.Id).FirstOrDefault());
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
                return new JsonResult(orderRepository.Where(x => x.ApplicationUserId == id).OrderByDescending(x => x.Id).FirstOrDefault());
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
                return new JsonResult(orderRepository.Get(id));
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
                    var orderBase = orderRepository.Get(sendPaymentCielo.OrderId);
                    orderBase.PaymentId = sendPaymentCielo.PaymentId;
                    //orderBase.CapturedAmount = sendPaymentCielo.CapturedAmount;
                    //orderBase.Installments = sendPaymentCielo.Installments;
                    orderRepository.Update(orderBase);
                    orderTrackingRepository.Insert(new OrderTracking()
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

        [HttpPost()]
        [Route("cancel")]
        [Authorize()]
        public IActionResult CancelarPedido(FilterDefault filter)
        {
            try
            {
                if (filter != null)
                {
                    var orderBase = orderRepository.Get(filter.Id);
                    if (orderBase == null)
                    {
                        return BadRequest("Pedido não encontrado.");
                    }
                    orderRepository.Update(orderBase);
                    orderEmailRepository.Insert(new OrderEmail()
                    {
                        OrderId = orderBase.Id,
                        Send = false,
                        TypeEmailId = 3
                    });
                    orderTrackingRepository.Insert(new OrderTracking()
                    {
                        DateTracking = DateTime.Now,
                        OrderId = orderBase.Id,
                        StatusOrderId = 3,
                        StatusPaymentOrderId = 3
                    });
                    return Ok();

                }
            }
            catch (Exception ex)
            {
                return BadRequest("Falha no envio do Pagamento. Entre em contato com o administrador do sistema. " + ex);
            }

            return Ok();
        }


    }
}
