using Microsoft.EntityFrameworkCore;
using pantry_be.Models;
using System.Collections.Immutable;
using System.Formats.Asn1;

namespace pantry_be.Services
{
    public class ItemService : IItemService
    {
        private readonly DataContext context;

        public ItemService(DataContext context)
        {
            this.context = context;
        }
        public async Task<List<Item>> GetAllItems(int accountId)
        {
            var account = await context.Accounts.Include(a => a.Users).Include(a => a.Friends).Include(a => a.Items).FirstOrDefaultAsync(a => a.Id == accountId);
            if (account == null)
            {
                throw new Exception("Account not found");
            }
            var items =  account.Items.ToList();

            return items;
        }


        public async Task<Item> AddItem(int accountId ,int unit, Item item)
        {
            var itemNameExists = await context.Items.FirstOrDefaultAsync(i => i.AccountId == accountId && i.Name == item.Name);
            if (itemNameExists == null)
            {
                item.AccountId = accountId;
                item.Weight = item.Weight * unit;
                context.Items.Add(item);                     
                await context.SaveChangesAsync();
                return item;
            }
            throw new Exception("Item name already exists");
        }



        public async Task<Item> UpdateItem(int id, int unit, Item item)
        {
            var existingItem = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
            if (existingItem != null)
            {
                existingItem.Name = item.Name;
                existingItem.Img = item.Img;
                existingItem.Weight = item.Weight * unit;
                existingItem.Calories = item.Calories;
                await context.SaveChangesAsync();
                return existingItem;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }


        public async Task DeleteItem(int id)
        {
            var itemToDelete = await context.Items.FindAsync(id);
            if (itemToDelete == null)
            {
                throw new Exception("Product not found");
            }
            context.Items.Remove(itemToDelete);
            await context.SaveChangesAsync();
        }

        public async Task<Item> GetItemById(int id)
        {
            var item = await context.Items.FindAsync(id);
            if (item == null)
            {
                throw new Exception("Product not found");
            }
            return item;
        }



    }
}
