using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using System.IO;

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

                    // Excluir botones que no deben tener el evento Digit_Click
                    if (b.Text == "=" || b.Text == "CE" || b.Text == "Cargar") continue;

                    // Asignar evento solo a los botones que representan dígitos u operadores
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
        private void Evaluate_Expression(object sender, EventArgs e)
        {
            if (Regex.IsMatch(Display.Text, pattern))
            {
                try
                {
                    // Utiliza DataTable para evaluar la expresión
                    var result = new DataTable().Compute(Display.Text, null);

                    // Guarda la operación actual en una variable
                    string operacion = Display.Text;

                    // Actualiza el Display con el resultado
                    Display.Text = $"{result}";

                    // Pasa tanto la operación como el resultado al textBox1
                    textBox1.AppendText(operacion + " = " + result.ToString() + "\r\n");
                }
                catch (Exception ex)
                {
                    // Maneja posibles errores de evaluación
                    Display.Text = "MATH ERROR";
                    errorMsg = true;
                }
            }
            else
            {
                // Si la expresión no es válida, muestra un mensaje de error
                Display.Text = "SYNTAX ERROR";
                errorMsg = true;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonDot_Click(object sender, EventArgs e)
        {

        }

        private void buttonDivision_Click(object sender, EventArgs e)
        {

        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {

        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {

        }

        private void buttonProduct_Click(object sender, EventArgs e)
        {

        }

        private void button0_Click(object sender, EventArgs e)
        {

        }

        private void Display_TextChanged(object sender, EventArgs e)
        {

        }

        private void CE_Click(object sender, EventArgs e)
        {
            // Limpiar el contenido del TextBox
            Display.Text = "";
            textBox1.Text = "";
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.AddExtension = true;
            sd.Filter = "Text files|*.txt";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                // Guarda el contenido del textBox1 en un archivo de texto
                System.IO.File.WriteAllText(sd.FileName, textBox1.Text);

                // Mensaje de confirmación
                MessageBox.Show("Historial guardado correctamente.");
            }
        }



        private void button11_Click(object sender, EventArgs e) // Cargar archivo
        {
            Display.Text = "";
            OpenFileDialog sd = new OpenFileDialog();
            sd.AddExtension = true;
            sd.Filter = "Text files|*.txt";

            if (sd.ShowDialog() == DialogResult.OK)
            {
                // Leer el contenido del archivo en el TextBox
                textBox1.Text = File.ReadAllText(sd.FileName);

                double sum = 0; // Variable para la suma total

                // Iterar sobre cada línea del TextBox
                for (int i = 0; i < textBox1.Lines.Length; i++)
                {
                    // Buscar el signo "=" en cada línea
                    int index = textBox1.Lines[i].IndexOf('=');
                    if (index >= 0)
                    {
                        // Obtener la parte de la línea después del signo "="
                        string line = textBox1.Lines[i].Substring(index + 1).Trim();

                        try
                        {
                            // Sumar los resultados de las operaciones
                            sum += double.Parse(line);
                        }
                        catch (FormatException)
                        {
                            // Manejar posibles errores de formato
                            MessageBox.Show("Formato de número no válido en el historial.");
                        }
                    }
                }

                // Mostrar la suma en el Display
                Display.Text = "Suma = " + sum.ToString();
            }
        }

    }
}
