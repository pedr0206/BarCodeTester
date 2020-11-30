using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarCodeTester
{
    public class HymatikDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public HymatikDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(DbUser).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(DbUser)).ConfigureAwait(false);
                }
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(DbAddress).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(DbAddress)).ConfigureAwait(false);
                }

                
                
                /* TEST USERS AND ADDRESSES IN DB
                 * await SaveAddressAsync(new DbAddress()
                {
                    Address = "Teste de addresse",
                    ZipCode = 1234,
                    City = "Outra coisa"
                });

                await SaveUserAsync(new DbUser()
                {
                    Name = "Pedro",
                    PhoneNumber = "31313131"
                });

                await SaveUserAsync(new DbUser()
                {
                    Name = "Mark",
                    PhoneNumber = "32323232"
                });*/

                initialized = true;
            }
        }

        public Task<List<DbUser>> GetUsersAsync()
        {
            return Database.Table<DbUser>().ToListAsync();
        }

        /*public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }*/

        public Task<DbUser> GetUserAsync(int id)
        {
            return Database.Table<DbUser>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveUserAsync(DbUser user)
        {
            if (user.ID != 0)
            {
                return Database.UpdateAsync(user);
            }
            else
            {
                return Database.InsertAsync(user);
            }
        }

        public Task<int> DeleteUserAsync(DbUser user)
        {
            return Database.DeleteAsync(user);
        }

        public Task<List<DbAddress>> GetAddressesAsync()
        {
            return Database.Table<DbAddress>().ToListAsync();
        }

        public Task<DbAddress> GetAddressAsync(int id)
        {
            return Database.Table<DbAddress>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAddressAsync(DbAddress address)
        {
            if (address.ID != 0)
            {
                return Database.UpdateAsync(address);
            }
            else
            {
                return Database.InsertAsync(address);
            }
        }

        public Task<int> DeleteAddressAsync(DbAddress address)
        {
            return Database.DeleteAsync(address);
        }
    }
}
