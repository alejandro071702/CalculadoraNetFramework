﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CalculadoraNetFramework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonAddEvent();
        }

        private void buttonAddEvent()
        {
            foreach (object o in this.Controls)
            {
                if (o is System.Windows.Forms.Button)
                {
                    System.Windows.Forms.Button b = (System.Windows.Forms.Button)o;
                    if (b.Text == "=") continue;
                    b.Click += new System.EventHandler(Digit_Click);
                }
            }
        }

        bool errorMsg = false;
        private void Digit_Click(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(System.Windows.Forms.Button))
            {
                Console.Write("Unexpected click event");
                return;
            }

            if (errorMsg)
            {
                Display.Text = "";
                errorMsg = false;
            }

            System.Windows.Forms.Button b = (System.Windows.Forms.Button)sender;

            Display.AppendText(b.Text);
        }

        const string pattern = @"^\s*(-?\d+(\.\d+)?)(\s*([+\-*/])\s*(-?\d+(\.\d+)?))?\s*$";
        private void Evaluate_Expression (object sendder, EventArgs e)
        {

            if (Regex.IsMatch(Display.Text, pattern))
            {
                try
                {
                    // Utiliza DataTable para evaluar la expresión
                    var result = new DataTable().Compute(Display.Text, null);
                    Display.Text = $"{result}";
                }
                catch (Exception ex)
                {
                    // Maneja posibles errores de evaluación
                    Display.Text = "MATH ERROR";
                    errorMsg = true;
                }
            } else
            {
                //MessageBox.Show("Invalid Input\n");
                Display.Text = "SYNTAX ERROR";
                errorMsg = true;
            }
        }
    }
}
