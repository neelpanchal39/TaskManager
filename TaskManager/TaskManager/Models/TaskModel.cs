using System;
using SQLite;

namespace TaskManager.Models
{
	public class TaskModel
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public StatusTypes StatusType { get; set; }
        public string StatusColor { get; set; }
    }
}

