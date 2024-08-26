using AppTask.DataBase;
using AppTask.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTask.DataBase.Repositories
{
    
    public class TaskModelRepository : ITaskModelRepository
    {
        private AppTaskContext _db;
        public TaskModelRepository()
        {
            _db = new AppTaskContext();
        }
        public void Add(TaskModel task)
        {
           _db.Tasks.Add(task);
            _db.SaveChanges();
        }
        public void Update(TaskModel task)
        {
            _db.Tasks.Update(task);
            _db.SaveChanges();
        }
        public void Delete(TaskModel task)
        {
           task = GetById(task.Id);
            foreach (var subtasks in task.SubTasks)
            {
                subtasks.Delete = DateTimeOffset.Now;
                _db.SubTasks.Remove(subtasks);
            }
            task.Deleted = DateTimeOffset.Now;

           _db.Tasks.Update(task);
            _db.SaveChanges();
        }

        public IList<TaskModel> GetAll(Guid userId)
        {
            return _db.Tasks.Where(u => u.UserId == userId).OrderByDescending(a=>a.PrevisionDate.Date).ToList();
        }

        public TaskModel GetById(Guid id)
        {
            return _db.Tasks.Include(a => a.SubTasks).FirstOrDefault(a => a.Id == id);
        }

       
    }
}
