using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HogwartsPotions.Data.Services
{
    public interface IRoomService
    {
        public Task AddRoom(Room room);
        public Task<Room> GetRoom(long roomId);
        public Task<List<Room>> GetAllRooms();
        public Task UpdateRoom(long id, Room room);
        public Task DeleteRoom(long id);
        public Task<List<Room>> GetRoomsForRatOwners();
    }
}
