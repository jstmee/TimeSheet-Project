using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSheet.IBL
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Object id);
        void Add(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();


    }
}
