using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SQLite;

namespace Fijicare.Helper
{
    public class StepDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<StepDatabase> Instance = new AsyncLazy<StepDatabase>(async () =>
        {
            var instance = new StepDatabase();
            CreateTableResult result = await Database.CreateTableAsync<StepTable>();
            await Database.CreateTableAsync<StepTemp>();
            return instance;
        });


        public StepDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

    }
    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }

    public class StepTable
    {
        public StepTable()
        {
            Created = DateTime.Now;
        }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public long DailySteps  { get; set; }
        public string CreateDate { get; set; }
        public string StepSinceReboot { get; set; }
        public DateTime Created { get; set; }
    }

    public class StepTemp
    {
        public StepTemp()
        {
            Created = DateTime.Now;
        }
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public long Steps { get; set; }
        public string CreateDate { get; set; }
        // public string Createtime { get; set; }
        public DateTime Created { get; set; }
    }
}
    
