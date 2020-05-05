using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPIExample.Controllers
{
    public class ItemsController : ApiController
    {
        // Option 2 > Use decorators
        [HttpGet]
        public List<string> ObtainAllItems()
        {
            return new List<string>();
        }

        [HttpGet]
        public string FilterItemsById(int id)
        {
            return "Lenovo T480s";
        }

        [HttpDelete]
        public bool RemoveItemFromDb(int id)
        {
            return true;
        }

    }
}
