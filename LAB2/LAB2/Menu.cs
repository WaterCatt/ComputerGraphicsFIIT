using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB2
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new Task1())
            {
                form.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var form = new Task2())
            {
                form.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var form = new Task3())
            {
                form.ShowDialog();
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
        }
    }
}
