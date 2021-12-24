using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionSystem
{
    internal interface ILocker<T>
    {
        T Value { get; }

        void Lock();
    }
}
