using EJERCICIO_2_3.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EJERCICIO_2_3.Controller
{
    public class Database
    {
        private readonly SQLiteAsyncConnection dbase;

        public Database(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);
            dbase.CreateTableAsync<Audio>();
        }

        public Task<int> CreateAudio(Audio Audio)
        {
            return dbase.InsertAsync(Audio);
        }

        public Task<List<Audio>> GetAllAudio()
        {
            return dbase.Table<Audio>().ToListAsync();
        }

    }
}
