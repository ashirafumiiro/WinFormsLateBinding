using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FormLoadDrive
{
    public partial class MainForm : Form
    {
        string programPath = Assembly.GetExecutingAssembly().CodeBase;
        string executingFolder = "";
        public MainForm()
        {
            InitializeComponent();
            executingFolder = Directory.GetParent(new Uri(programPath).LocalPath).ToString();
        }

        private void LoadLabClick(object sender, EventArgs e)
        {
            string labUrl = Path.Combine(executingFolder, "Libs", "LabLibrary.dll");
            Assembly asm = null;
            try
            {
                if (!File.Exists(labUrl))
                {
                    MessageBox.Show("LabLib");
                }
                asm = Assembly.LoadFrom(labUrl);
                //DisplayTypesInAsm(asm);
                LoadForm(asm);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //static void DisplayTypesInAsm(Assembly asm)
        //{
        //    string str = "***** Types in Assembly *****\n";
        //    str  += $"->{asm.FullName}\n";
        //    Type[] types = asm.GetTypes();
        //    foreach (Type t in types)
        //        str += $"Type: {t}\n";
        //    MessageBox.Show(str);
        //}

        void LoadForm(Assembly asm)
        {
            try
            {
                Type type = asm.GetType("LabLibrary.LabForm"); // get form class
                object obj = Activator.CreateInstance(type);  // create an instance of it
                ((Form)obj).ShowDialog();                     // Cast it to a Form and show it.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } 
}
