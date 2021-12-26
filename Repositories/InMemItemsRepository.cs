using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repository
{


  public class InMemItemsRepository : IItemsRepository
  {
    private readonly List<Item> items = new()
    {
      new Item() { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTime.UtcNow },
      new Item() { Id = Guid.NewGuid(), Name = "Iron sword", Price = 20, CreatedDate = DateTime.UtcNow },
      new Item() { Id = Guid.NewGuid(), Name = "Shield", Price = 18, CreatedDate = DateTime.UtcNow }
    };

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
      return await Task.FromResult(items);
    }

    public async Task<Item> GetItemAsync(Guid id)
    {
      return await Task.FromResult(items.Where(item => item.Id == id).SingleOrDefault());
    }

    public async Task CreateItemAsync(Item item)
    {
      items.Add(item);
      await Task.CompletedTask;
    }

    public async Task UpdateItemAsync(Item item) {
      var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
      items[index] = item;
      await Task.CompletedTask;
    }

    public async Task DeleteItemAsync(Item item) {
      var index = items.FindIndex(existingItem => existingItem.Id == item.Id);
      items.RemoveAt(index);
      await Task.CompletedTask;
    }
  }
}