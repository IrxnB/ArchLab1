using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchLab1Lib.Model
{
    public interface IEntity<K, TSelf> where TSelf : IEntity<K, TSelf>
    {
        K Id { get; set; }
        void Update(TSelf newEntity) ;
    }
}
