using System;

namespace Lib.Entites
{
    public abstract class BaseEntity
    {

        public BaseEntity()
        {
            CreationDateTime = DateTime.Now;
        }

        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; }
    }
}
