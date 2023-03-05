using pantry_be.Models;

namespace pantry_be.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems(int accountId);

        Task<Item> AddItem(int accountId,int unit, Item item);

        Task<Item> UpdateItem(int id, int unit, Item item);

        Task DeleteItem(int id);

        Task<Item> GetItemById(int id);
    }
}
