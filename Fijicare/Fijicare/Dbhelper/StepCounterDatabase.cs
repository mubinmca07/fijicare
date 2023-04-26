using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Fijicare.Helper;

namespace Fijicare.Dbhelper
{

    public class StepCounterDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<StepCounterDatabase> Instance = new AsyncLazy<StepCounterDatabase>(async () =>
        {
            var instance = new StepCounterDatabase();
            CreateTableResult result = await Database.CreateTableAsync<StepTable>();
            return instance;
        });

        private StepCounterDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<StepTable>> GetItemsAsync()
        {
            return Database.Table<StepTable>().ToListAsync();
        }

        public Task<List<StepTemp>> GetItempAsync()
        {
            return Database.Table<StepTemp>().ToListAsync();
        }

        public Task<List<StepTable>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<StepTable>("SELECT * FROM [StepTable] WHERE 1=1");
        }

        public Task<List<StepTable>> GetAllItemByDateAsync(DateTime dateTime)
        {
            return Database.QueryAsync<StepTable>("SELECT * FROM [StepTable] WHERE 1=1");
        }

        public Task<StepTable> GetItemAsync(int id)
        {
            return Database.Table<StepTable>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<List<StepTable>> GetTodayDataAsync(DateTime startdate, DateTime endDate, long interval)
        {
            string start = startdate.ToString("dd-MM-yyyy 00:00:00");
            string end = endDate.ToString("dd-MM-yyyy 23:59:59");
            string query = "SELECT * FROM [StepTable] WHERE CreateDate between  '" + start + "' AND '" + end + "'";
            return Database.QueryAsync<StepTable>(query);
        }


        public Task<List<StepTable>> GetDataByWeekAsync(DateTime startdate, DateTime endDate, long interval)
        {
            string start = startdate.ToString("dd-MM-yyyy 00:00:00");
            string end = endDate.ToString("dd-MM-yyyy 23:59:59");
            string query = "SELECT * FROM [StepTable]  WHERE CreateDate between  '" + start + "' AND '" + end + "'";
            return Database.QueryAsync<StepTable>(query);
        }

        public Task<List<StepTable>> GetTodayDataAsync()
        {
            string start = DateTime.Now.ToString("dd-MM-yyyy 00:00:00");
            string end = DateTime.Now.ToString("dd-MM-yyyy 23:59:59");
            string query = "SELECT * FROM [StepTable] WHERE CreateDate between  '"+ start +"' AND '"+ end+"'";
            return Database.QueryAsync<StepTable>(query);
        }

        public Task<int> SaveItemAsync(StepTable item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> SaveItemAsync(StepTemp item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }
        public Task<int> DeleteItemAsync(StepTable item)
        {
            return Database.DeleteAsync(item);
        }

    }
}
