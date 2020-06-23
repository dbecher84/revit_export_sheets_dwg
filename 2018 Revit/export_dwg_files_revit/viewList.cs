using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Form = System.Windows.Forms.Form;

namespace Export_Sheets
{
    public partial class viewList : Form
    {
        List<string> fullList = new List<string>();

        public viewList(List<string> list)
        {
            InitializeComponent();

            foreach (string item in list)
            {
                //MessageBox.Show(item);
                listBox1.Items.Add(item);
                fullList.Add(item);
            }
        }

        public List<string> ExportViewList { get; set; }

        //create view list to send for export

        public List<string> CreateViewList()
        {
            List<string> viewListWorking = new List<string>();
            if (listBox2.Items != null)
            {
                foreach (var item in listBox2.Items)
                {
                    string text = item.ToString();
                    //MessageBox.Show(text);
                    viewListWorking.Add(text);
                }
                ExportViewList = viewListWorking;
                return ExportViewList;
            }
            else
            {
                MessageBox.Show("List Box was empty");
                return ExportViewList;
            }
        }

      
        //add item to second list on double click
        private void DoubleClick1(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox2.Items.Add(listBox1.SelectedItem);
            }
        }

        //remove item from second list on double click
        private void DoubleClick2(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                listBox2.Items.Remove(listBox2.SelectedItem);
            }
        }

        //clear list 2
        private void ClearAll(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        //add all view to list 2
        private void AddAll(object sender, EventArgs e)
        {
            foreach (var item in listBox1.Items)
            {
                listBox2.Items.Add(item);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //add all 3d views to list for export
        private void button1_Click(object sender, EventArgs e)
        {
            CreateViewList();
        }

        //view list to filter list box 1 
        private void Filter_Load(object sender, EventArgs e)
        {
            string uText = textBox1.Text;
            string cText = uText.Trim();
            //MessageBox.Show(cText);
            List<string> filteredList = new List<string>();
            foreach (string item in listBox1.Items)
            {
                if (item.Contains(cText))
                {
                    filteredList.Add(item);
                }
            }
            listBox1.Items.Clear();
            foreach (string item in filteredList)
            {
                listBox1.Items.Add(item);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (string item in fullList)
            {
                listBox1.Items.Add(item);
            }
        }

        private void viewList_Load(object sender, EventArgs e)
        {

        }
    }
}
