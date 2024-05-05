using Domain.EntitiesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EntitiesBaseClass
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
