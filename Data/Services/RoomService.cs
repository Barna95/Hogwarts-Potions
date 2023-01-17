using HogwartsPotions.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using HogwartsPotions.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace HogwartsPotions.Data.Services
{
    public class RoomService : IRoomService
    {
        private readonly HogwartsContext _context;
        public RoomService(HogwartsContext context)
        {
            _context = context;
        }

        public async Task AddRoom(Room room)
        { 
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> GetRoom(long roomId)
        {
            return await _context.Rooms.FirstOrDefaultAsync(r => r.ID == roomId);
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return await _context.Rooms.Include(r => r.Residents).ToListAsync();
        }

        public async Task UpdateRoom(long id, Room room)
        {
            var updateRoom = _context.Rooms.FirstOrDefault(r => r.ID == id);
            updateRoom.Capacity = room.Capacity;
            updateRoom.Residents = room.Residents;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(long id)
        {
            var deleteRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.ID == id);
            _context.Rooms.Remove(deleteRoom);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Room>> GetRoomsForRatOwners()
        {
            return await _context.Rooms
                .Include(r => r.Residents)
                .Where(room => !room.Residents
                    .Any(resident =>
                        resident.PetType == PetType.Cat ||
                        resident.PetType == PetType.Owl))
                .ToListAsync();
        }
    }
}
