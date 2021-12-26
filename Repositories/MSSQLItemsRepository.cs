using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Catalog.Entities;
using Microsoft.Data.SqlClient;

namespace Catalog.Repository
{
  public class MSSQLItemsRepository : IItemsRepository, IDisposable
  {
    public IDbConnection Connection { get; private set; }

    public MSSQLItemsRepository(IDbConnection conn)
    {
      this.Connection = conn;
      Connection.Open();
    }

    public async Task CreateItemAsync(Item item)
    {
      string sql = @"INSERT INTO Catalog (Name, Price, CreatedDate, UUID)
        VALUES (@Name, @Price, @CreatedDate, @UUID)";
      var command = new SqlCommand(sql, (SqlConnection)Connection);
      command.Parameters.AddWithValue("@Name", item.Name);
      command.Parameters.AddWithValue("@Price", item.Price);
      command.Parameters.AddWithValue("@CreatedDate", item.CreatedDate);
      command.Parameters.AddWithValue("@UUID", item.Id);

      await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteItemAsync(Item item)
    {
      string sql = @"DELETE FROM Catalog WHERE UUID = @UUID";
      var command = new SqlCommand(sql, (SqlConnection)Connection);
      command.Parameters.AddWithValue("@UUID", item.Id);

      await command.ExecuteNonQueryAsync();
    }

    public async Task<Item> GetItemAsync(Guid id)
    {
      string sql = @"SELECT UUID, Name, Price, CreatedDate FROM Catalog WHERE UUID = @UUID";
      var command = new SqlCommand(sql, (SqlConnection)Connection);
      command.Parameters.AddWithValue("@UUID", id);

      var getReader = command.ExecuteReaderAsync();
      var reader = await getReader;

      if (reader.Read()) {
        Item item = new Item(){
          Id = Guid.Parse((string)reader["UUID"]),
          Name = (string)reader["Name"],
          Price = (decimal)reader["Price"],
          CreatedDate = (DateTime)reader["CreatedDate"]
        };

        return item;
      }
      reader.Close();
  
      return null;
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
      var items = new List<Item>();

      string sql = @"SELECT UUID, Name, Price, CreatedDate FROM Catalog";
      var command = new SqlCommand(sql, (SqlConnection)Connection);
      
      var getReader = command.ExecuteReaderAsync();
      var reader = await getReader;

      while (reader.Read()) {
          Item item = new Item(){
            Id = Guid.Parse((string)reader["UUID"]),
            Name = (string)reader["Name"],
            Price = (decimal)reader["Price"],
            CreatedDate = (DateTime)reader["CreatedDate"]
          };

          items.Add(item);
      }
      reader.Close();
      
      return items;
    }

    public async Task UpdateItemAsync(Item item)
    {
      string sql = @"UPDATE Catalog
        SET Name = @Name,
        Price = @Price
        WHERE UUID = @UUID";
      var command = new SqlCommand(sql, (SqlConnection)Connection);
      command.Parameters.AddWithValue("@Name", item.Name);
      command.Parameters.AddWithValue("@Price", item.Price);
      command.Parameters.AddWithValue("@UUID", item.Id);

      await command.ExecuteNonQueryAsync();
    }

    public void Dispose() {
      Connection.Close();
    }
  }
}