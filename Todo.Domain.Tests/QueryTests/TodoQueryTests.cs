using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Queries;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;

        public TodoQueryTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa1", "UsuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa2", "UsuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa3", "marcoslima", DateTime.Now));
            _items.Add(new TodoItem("Tarefa4", "UsuarioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa5", "marcoslima", DateTime.Now));
        }

        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_apenas_do_usuario_marcoslima()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("marcoslima"));
            Assert.AreEqual(2, result.Count());
        }

    }
}