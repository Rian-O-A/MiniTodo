﻿using Flunt.Notifications;
using Flunt.Validations;
using MiniTodo.Models;

namespace MiniTodo.ViewModels
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }

        public Todo MapTo()
        {

            var contract = new Contract<Notification>()
                .Requires()
                .IsNotNull(Title, "Informe o titulo da tarefa")
                .IsGreaterThan(Title, 5, "O titulo deve conter mais de 5 caracteres");

            AddNotifications(contract);
            return new Todo(Guid.NewGuid(), Title, false);       
        }
    }
}
