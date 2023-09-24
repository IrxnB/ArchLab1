using ArchLab1Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1.Model
{
    internal interface Repository<E, K> where E : IEntity<K, E>
    {
        List<E> ReadAll();
        E? ReadById(K id);
        void Create(E entity);
        void DeleteById(K id);
        bool ExistsById(K id);
        void Update(E entity);
    }
}
