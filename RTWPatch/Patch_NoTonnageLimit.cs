using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTWPatch
{
    class Patch_NoTonnageLimit : IPatch
    {
        public string Name
        {
            get
            {
                return "No Tonnage Limit (No tonnage limit at 52k or 70k)";
            }
        }

        void IPatch.DoPatch(string filename)
        {
            throw new NotImplementedException();
        }

    }
}
