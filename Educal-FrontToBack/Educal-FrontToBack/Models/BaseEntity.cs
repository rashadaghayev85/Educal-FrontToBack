﻿namespace Educal_FrontToBack.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public bool SoftDeleted { get; set; } =false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
