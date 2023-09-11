using ArchLab1Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1.Model
{
    internal interface Repository<E, K>
    {
        List<E> readAll();
        E readById(K id);
        void create(E entity);
        void deleteById(K id);
    }
}
