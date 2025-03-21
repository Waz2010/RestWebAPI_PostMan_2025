using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FruitsAPIController : ControllerBase
    {

        public List<string> Fruits = new List<string> { 
            "Apple", 
            "Banan", 
            "Mango",
            "Cherry", 
            "Grapps" };

        [HttpGet]
        public List<string> GetFruits ()
        {
            return Fruits;
        }

        [HttpGet("{id}")]
        public string GetFruitsByIndex(int id)
        {
            if (Fruits.Count < id)
            {
                return "There is no Fruit with this ID :" + id;
            } 
            return Fruits.ElementAt(id);
        }

    }
}
