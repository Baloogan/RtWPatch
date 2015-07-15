using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private byte[] file;
        private const int offset = 0x00400C00;
        private const byte OPCODE_NOP = 0x90;
        void IPatch.DoPatch(string filename)
        {
            file = System.IO.File.ReadAllBytes(filename);

            Avoid_game_over_check();

            System.IO.File.WriteAllBytes(filename, file);

            MessageBox.Show(Name + " patch applied.");
        }
        private void Avoid_game_over_check()
        {
            const int start = 0x00521374;
            const int end = 0x005213BA;
            for (int i = start; i < end; ++i)
                file[i - offset] = OPCODE_NOP;
        }
    }
}
