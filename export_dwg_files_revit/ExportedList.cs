using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Export_3d_views
{
    public partial class ExportedList : Form
    {
        public ExportedList(List<string> list)
        {
            InitializeComponent();

            foreach (string item in list)
            {
                //MessageBox.Show(item);
                listBox1.Items.Add(item);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
     
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
