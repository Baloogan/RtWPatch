using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTWPatch
{
    public interface IPatch
    {
        string Name { get; }
        void DoPatch(string filename);
    }
}
