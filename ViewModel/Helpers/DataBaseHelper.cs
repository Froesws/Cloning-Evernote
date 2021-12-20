using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.ViewModel.Helpers
{
    public class DataBaseHelper
    {
        private static readonly string DBFILE = Path.Combine(Environment.CurrentDirectory, "notesDB3");

        public static bool Insert<T>(T item)
        {
            bool isSuccessed = false;
            using (SQLiteConnection conn = new SQLiteConnection(DBFILE))
            {
                _ = conn.CreateTable<T>();
                int rows = conn.Insert(item);
                if (rows > 0)
                {
                    isSuccessed = true;
                }
            }
            return isSuccessed;
        }

        public static bool Delete<T>(T item)
        {
            bool isSuccessed = false;
            using (SQLiteConnection conn = new SQLiteConnection(DBFILE))
            {
                _ = conn.CreateTable<T>();
                int rows = conn.Delete(item);
                if (rows > 0)
                {
                    isSuccessed = true;
                }
            }
            return isSuccessed;
        }

        public static bool Update<T>(T item)
        {
            bool isSuccessed = false;
            using (SQLiteConnection conn = new SQLiteConnection(DBFILE))
            {
                _ = conn.CreateTable<T>();
                int rows = conn.Update(item);
                if (rows > 0)
                {
                    isSuccessed = true;
                }
            }
            return isSuccessed;
        }

        public static IList<T> Read<T>() where T : new()
        {
            IList<T> items = null;
            using (SQLiteConnection conn = new SQLiteConnection(DBFILE))
            {
                _ = conn.CreateTable<T>();
                items = conn.Table<T>().ToList(); ;
            }
            return items;
        }
    }
}
