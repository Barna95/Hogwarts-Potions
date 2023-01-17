using HogwartsPotions.Data.Services;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("/potions")]
    public class PotionController : ControllerBase
    {
        private readonly IPotionService _service;

        public PotionController(IPotionService service)
        {
            _service = service;
        }

        [HttpGet("/potions")]
        public async Task<List<Potion>> GetPotions()
        {
            return await _service.GetAllPotions();
        }

        [HttpPost("/potions")]
        public async Task AddPotion([FromBody] Potion potion)
        {
            await _service.AddPotion(potion);
        }

        [HttpGet("/potions/{id}")]
        public async Task<Potion> GetPotionById(long id)
        {
            return await _service.GetPotion(id);
        }

        [HttpPut("/potions/{id}")]
        public void UpdatePotionById(long id, [FromBody] Potion updatedPotion)
        {
            _service.UpdatePotion(id, updatedPotion);
        }

        [HttpDelete("/potions/{id}")]
        public async Task DeletePotionById(long id)
        {
            await _service.DeletePotion(id);
        }
    }
}
