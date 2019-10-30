using Dz4.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dz4.DataAccess
{
    public interface IRepository<T> where T : Entity
    {
        void Add(T element);
        void Update(T element);
        ICollection<T> GetAll();
        void Delete(Guid Id);
    }
}
