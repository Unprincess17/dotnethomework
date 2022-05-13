using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using todo.Models;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        private readonly OrderContext OrderDb;

        //构造函数把OrderContext 作为参数，Asp.net core 框架可以自动注入OrderContext对象
        public TodoController(OrderContext context)
        {
            this.OrderDb = context;
        }

        // GET: api/todo/{id}  id为路径参数
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrders(String id)
        {
            var Order = OrderDb.Orders.FirstOrDefault(t => t.OrderId == id);
            if (Order == null)
            {
                return NotFound();
            }
            return Order;
        }

        // GET: api/todo
        // GET: api/todo/pageQuery?name=课程&&isComplete=true
        //[HttpGet]
        //public ActionResult<List<Order>> GetOrders(string name)
        //{
        //    var query = buildQuery(name);
        //    return query.ToList();
        //}

        //// GET: api/todo/pageQuery?skip=5&&take=10  
        //// GET: api/todo/pageQuery?name=课程&&isComplete=true&&skip=5&&take=10
        //[HttpGet("pageQuery")]
        //public ActionResult<List<Order>> queryTodoItem(string ID, int skip, int take)
        //{
        //    var query = buildQuery(ID).Skip(skip).Take(take);
        //    return query.ToList();
        //}

        //private IQueryable<Order> buildQuery(string ID/*, bool? isComplete*/)
        //{
        //    IQueryable<Order> query = OrderDb.Orders;    
        //    if (ID != null)
        //    {
        //        query = query.Where(t => t.CustomerId.Contains(ID));
        //    }
        //    //if (isComplete != null)
        //    //{
        //    //    query = query.Where(t => t.IsComplete == isComplete);
        //    //}
        //    return query;
        //}


        // POST: api/todo
        [HttpPost]
        public ActionResult<Order> PostTodoItem(Order todo)
        {
            try
            {
                OrderDb.Orders.Add(todo);
                OrderDb.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return todo;
        }

        // PUT: api/todo/{id}
        [HttpPut("{id}")]
        public ActionResult<Order> PutTodoItem(string id, Order todo)
        {
            if (id != todo.OrderId)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                OrderDb.Entry(todo).State = EntityState.Modified;
                OrderDb.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

        // DELETE: api/todo/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteTodoItem(string id)
        {
            try
            {
                var todo = OrderDb.Orders.FirstOrDefault(t => t.OrderId == id);
                if (todo != null)
                {
                    OrderDb.Remove(todo);
                    OrderDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }

    }
}