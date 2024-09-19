using ApplicationService.Handler.Services;
using Domain.CaseAktifAggregate;
using Domain.IRepositoryAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Api.Controllers
{
    public class CustomerOrderController : ControllerBase
    {
        private readonly CaseAktifDbContext _context;
        private readonly ICustomerOrderRepository _customerOrderRepository;
        public CustomerOrderController(CaseAktifDbContext context, ICustomerOrderRepository customerOrderRepository)
        {
            _context = context;
            _customerOrderRepository = customerOrderRepository;
        }


        // GET: api/customerorder/{id}
        //NOT: direk _context'den alınabileceği gibi repository de linq ile berare sorgusu yazılıp genericrepositorydeki GetByIdAsync kullanılabilir
        //Örnek olması için bir tane repository tanımlaması yapıp eklemesini yapacağım.
        //
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            //Şeklind de repository üzerinden alabiliriz.
            //var order = await _customerOrderRepository.GetByIdAsync(id);
            var order = await _context.CustomerOrder
                                       .Include(o => o.Customer)
                                       .Include(o => o.Products)
                                       .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }


        // PUT: api/customerorder/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] CustomerOrder order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.CustomerOrder.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/customerorder/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.CustomerOrder.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.CustomerOrder.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/customerorder
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CustomerOrder order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rabbitMQService = new RabbitMQProducerService();
            var message = $"Yeni sipariş oluşturuldu: {order.Id}  müşteri için : {order.Customer.Name}";
            rabbitMQService.SendOrderMessage(message);

            _context.CustomerOrder.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }
    }
}