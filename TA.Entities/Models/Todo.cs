﻿namespace TA.Entities.Models
{
    public class Todo : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
