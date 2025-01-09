using System;
using System.Collections.ObjectModel;
using System.Diagnostics;

using SQLite;
using TaskManager.Models;

namespace TaskManager.Services
{
    public class DatabaseServices
    {
        static SQLiteConnection Connection = null;

        public static SQLiteConnection DbConnection()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TaskManager.db3");
                Debug.WriteLine("TaskManager Database Path ======" + dbPath);
                Connection = new SQLiteConnection(dbPath);
                Connection.CreateTable<TaskModel>();
                return Connection;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception ======" + ex.Message);
                return null;
            }
        }


        public static List<TaskModel> GetAllData(SQLiteConnection db)
        {
            try
            {
                var query = $"SELECT * FROM TaskModel";
                var res = db.Query<TaskModel>(query);
                return res;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public static int InsertTasks(TaskModel taskModel)
        {
            try
            {
                if (taskModel.ID != 0)
                {
                    Connection.Update(taskModel);
                    return taskModel.ID;
                }
                else
                {
                    return Connection.Insert(taskModel);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception ======" + ex.Message);
                return -1;
            }
        }

        public static int DeleteTask(int id)
        {
            return Connection.Delete<TaskModel>(id);
        }
    }
}

