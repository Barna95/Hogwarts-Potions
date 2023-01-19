using System;
using HogwartsPotions.Data.Services;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("[controller]")]
    public class PotionController : ControllerBase
    {
        private readonly IPotionService _service;

        public PotionController(IPotionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Potion>> GetPotions()
        {
            return await _service.GetAllPotions();
        }

        [HttpPost]
        public async Task AddPotion([FromBody] Potion potion)
        {
            await _service.AddPotion(potion);
        }

        [HttpGet("{id}")]
        public async Task<Potion> GetPotionById(long id)
        {
            return await _service.GetPotion(id);
        }

        [HttpPut("{id}")]
        public void UpdatePotionById(long id, [FromBody] Potion updatedPotion)
        {
            _service.UpdatePotion(id, updatedPotion);
        }

        [HttpDelete("{id}")]
        public async Task DeletePotionById(long id)
        {
            await _service.DeletePotion(id);
        }

        [HttpGet("student/{studentId}")]
        public async Task<List<Potion>> GetAllPotionsOfAStudent(long studentId)
        {
            return await _service.GetAllPotionsOfAStudent(studentId);
        }

        [HttpPost("brew")]
        public async Task<Potion> BrewPotion([FromForm] long id)
        {
            return await _service.BrewPotion(id);
        }
    }
}
