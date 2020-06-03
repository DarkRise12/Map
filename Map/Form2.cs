using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Map
{
    public partial class Form2 : Form
    {
        public string Name;
        public string Lat;
        public string Lng;
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox2.Text) && string.IsNullOrEmpty(textBox3.Text)))
            {
                Name = textBox1.Text;
                Lat = textBox2.Text;
                Lng = textBox3.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Пустые строки");
            }
        }
    }
}
