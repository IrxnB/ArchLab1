using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1Lib.Model
{
    public interface IEntity<K>
    {
        K Id { get; set; }
        void Update(IEntity<K> newEntity);
    }
}
