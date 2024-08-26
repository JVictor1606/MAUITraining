﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppTask.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = String.Empty;

        public string AcessToken { get; set; } = String.Empty;

        public DateTimeOffset AcessTokenCreated { get; set; }

        public DateTimeOffset Created { get; set; }

        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}
