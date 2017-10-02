using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using TrainingXamarin.Model;

namespace TrainingXamarin.Data
{
    public class TodoDataBase
    {
        readonly SQLiteAsyncConnection mDatabase;

        public TodoDataBase(string dbPath)
        {
            mDatabase = new SQLiteAsyncConnection(dbPath);
            mDatabase.CreateTableAsync<Todo>().Wait();
        }

        public Task<List<Todo>> GetItemAsync()
        {
            return mDatabase.Table<Todo>().ToListAsync();
        }

        public Task<List<Todo>> GetItemNotDoneAsync()
        {
            return mDatabase.QueryAsync<Todo>("SELECT * FROM [Todo] WHERE [IsDone] = 0");
        }

        public Task<Todo> GetItemAsync(int id)
        {
            return mDatabase.Table<Todo>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Todo item)
        {
            if (item.ID != 0)
            {
                return mDatabase.UpdateAsync(item);
            }
            else
            {
                return mDatabase.InsertAsync(item);
            }
        }

        public Task<int> DeleteAsync(Todo item)
        {
            return mDatabase.DeleteAsync(item);
        }
    }
}
