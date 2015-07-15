using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows;

namespace RTWPatch
{
    class Patch_NoTonnageLimit : IPatch
    {
        public string Name
        {
            get
            {
                return "Higher Tonnage Limit (80k)";
            }
        }
        private byte[] file;
        private const int offset = 0x00400C00;
        private const byte OPCODE_NOP = 0x90;
        private const byte OPCODE_JMP = 0xEB;
        void IPatch.DoPatch(string filename)
        {
            file = File.ReadAllBytes(filename);
            Avoid_52k_check();
            Avoid_UpClick_check();
            IncreaseMaxValue();
            File.WriteAllBytes(filename, file);

            string hp_table_path = Path.Combine(Path.GetDirectoryName(filename), "Data", "SpeedHPTable.dat");
            File.Delete(hp_table_path);

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RTWPatch.SpeedHPTable.dat"))
            using (var fs = File.Create(hp_table_path))
                stream.CopyTo(fs);

            MessageBox.Show(Name + " patch applied.");
        }
        private void IncreaseMaxValue()
        {
            file[0x00254742] = 0x80;//80k
            file[0x00254743] = 0x38;
            file[0x00254744] = 0x01;
        }
        private void Avoid_UpClick_check()
        {
            const int start = 0x004D9CD8;
            const int end = 0x004D9CDA;
            for (int i = start; i < end; ++i)
                file[i - offset] = OPCODE_NOP;
        }
        private void Avoid_52k_check()
        {
            const int spot = 0x001A8F55;
            file[spot] = OPCODE_JMP;
        }
    }
}
