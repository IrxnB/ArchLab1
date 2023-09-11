using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1.Model
{
    internal class CsvRepositury<E, K> : Repository<E, K>
    {

        StreamReader? reader;

        public void create(E entity)
        {
            throw new NotImplementedException();
        }

        public void deleteById(K id)
        {
            throw new NotImplementedException();
        }

        public List<E> readAll()
        {
            throw new NotImplementedException();
        }

        public E readById(K id)
        {
            throw new NotImplementedException();
        }
    }
}
