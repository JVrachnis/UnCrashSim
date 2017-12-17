using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnCrashSim
{
    public partial class Form2 : Form
    {
        private  Infrastructure infrastructure;
        public Form2(ref Infrastructure inf)
        { 
            //inf.ToString
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
