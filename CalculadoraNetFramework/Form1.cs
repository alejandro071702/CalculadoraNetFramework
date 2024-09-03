using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CalculadoraNetFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(Button))
            {
                Console.Write("Unexpected click event");
                return;
            }

            Button b = (Button)sender;

            Display.Text += b.Text;
        }
    }
}
