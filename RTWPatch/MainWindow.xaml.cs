using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace RTWPatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private string filename;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Welcome to RTWPatch by Baloogan. Back up your goddamn RTW directory before pressing OK. Seriously. Do it now.");

            string file_location = @"C:\NWS\Rule the Waves\RTW.exe";

            while (!File.Exists(file_location))
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Title = "Select RTW.exe";
                dlg.DefaultExt = ".exe";
                dlg.Filter = "RTW.exe (*.exe)|*.exe";
                Nullable<bool> result = dlg.ShowDialog();
                if (result == true)
                {
                    filename = dlg.FileName;
                    textBox_binary_location.Text = filename;
                }
            }

            List<IPatch> patches = new List<IPatch>();
            patches.Add(new Patch_NoEndDate());
            patches.Add(new Patch_NoTonnageLimit());
            listBox_patches.DataContext = patches;
        }

        private void button_apply_selected_patch_Click(object sender, RoutedEventArgs e)
        {
            if (listBox_patches.SelectedItem == null)
            {
                MessageBox.Show("Select an item in the list above.");
                return;
            }
            IPatch patch = (IPatch)listBox_patches.SelectedItem;

            patch.DoPatch(filename);
        }

        private void button_apply_all_patches_Click(object sender, RoutedEventArgs e)
        {
            foreach (var a in listBox_patches.Items.Cast<IPatch>())
            {
                a.DoPatch(filename);
            }
        }
    }
}
