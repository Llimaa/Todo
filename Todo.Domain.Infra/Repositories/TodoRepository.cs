using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Queries;
using Todo.Domain.Repositories;

namespace Todo.Domain.Infra.Respositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly DataContext _contex;

        public TodoRepository(DataContext contex)
        {
            _contex = contex;
        }

        public IEnumerable<TodoItem> GetAll(string user)
        {
            return _contex.Todos
            .AsNoTracking()
            .Where(TodoQueries.GetAll(user))
            .OrderBy(x => x.Date);
        }

        public IEnumerable<TodoItem> GetAllDone(string user)
        {
            return _contex.Todos
            .AsNoTracking()
            .Where(TodoQueries.GetAllDone(user))
            .OrderBy(x => x.Date);
        }

        public IEnumerable<TodoItem> GetAllUnDone(string user)
        {
            return _contex.Todos
          .AsNoTracking()
          .Where(TodoQueries.GetAllUnDOne(user))
          .OrderBy(x => x.Date);
        }

        public TodoItem GetById(Guid id, string user)
        {
            return _contex.Todos.FirstOrDefault(TodoQueries.GetById(id, user));
        }

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done)
        {
            return _contex.Todos
            .AsNoTracking()
            .Where(TodoQueries.GetByPeriod(user, date, done))
            .OrderBy(x => x.Date);
        }

        public void Create(TodoItem todo)
        {
            _contex.Todos.Add(todo);
            _contex.SaveChanges();
        }

        public void update(TodoItem todo)
        {
            _contex.Entry(todo).State = EntityState.Modified;
            _contex.SaveChanges();
        }
    }
}