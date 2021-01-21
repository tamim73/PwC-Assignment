using System;

namespace Lib.Entites
{
    public abstract class BaseEntity
    {

        public int Id { get; set; }

        public DateTime CreationDateTime { get; set; }
    }
}
