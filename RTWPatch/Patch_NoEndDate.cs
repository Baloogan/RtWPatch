using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTWPatch
{
    class Patch_NoEndDate : IPatch
    {
        public string Name
        {
            get
            {
                return "No End Date (Play past 1925)";
            }
        }

        void IPatch.DoPatch(string filename)
        {
            throw new NotImplementedException();
        }
        
    }
}
