using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RecruimentTask.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecruimentTask.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private ProductContext productContext = new ProductContext();
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return productContext.GetAllProducts();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Product Get(Guid id)
        {
            return productContext.GetProduct(id);
        }

        // POST api/<controller>
        [HttpPost]
        public Guid Post([FromBody]ProductCreateInputModel model)
        {
            return productContext.AddProduct(model);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(ProductUpdateInputModel model)
        {
            productContext.UpdateProduct(model);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            productContext.DeleteProduct(id);
        }
    }
}
