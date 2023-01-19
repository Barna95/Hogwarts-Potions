using System.Collections.Generic;
using System.Threading.Tasks;
using HogwartsPotions.Data.Services;
using HogwartsPotions.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HogwartsPotions.Controllers
{
    [ApiController, Route("[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _service;

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<Room>> GetAllRooms()
        {
            return await _service.GetAllRooms();
        }

        [HttpPost]
        public async Task AddRoom([FromBody] Room room)
        {
            await _service.AddRoom(room);
        }

        [HttpGet("{id}")]
        public async Task<Room> GetRoomById(long id)
        {
            return await _service.GetRoom(id);
        }

        [HttpPut("{id}")]
        public void UpdateRoomById(long id, [FromBody] Room updatedRoom)
        {
            _service.UpdateRoom(id, updatedRoom);
        }

        [HttpDelete("{id}")]
        public async Task DeleteRoomById(long id)
        {
            await _service.DeleteRoom(id);
        }

        [HttpGet("/rat-owners")]
        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _service.GetRoomsForRatOwners();
        }
    }
}
