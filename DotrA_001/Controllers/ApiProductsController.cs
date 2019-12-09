using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Text;

namespace DotrA_001.Controllers
{
    
    public class ApiProductsController : ApiController
    {
        private DotrADb db = new DotrADb();
        // GET: api/ApiProducts
        public IHttpActionResult GetProduct(int id)
        {
            var product = db.Products.FirstOrDefault((p) => p.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
