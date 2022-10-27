using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NominaConArchivo
{
    public partial class frmNomina : Form
    {
        public frmNomina()
        {
            InitializeComponent();
        }

        private void BtnProcesar_Click(object sender, EventArgs e)
        {
            if (txtArchivo.Text == "")
            {
                MessageBox.Show("Debe ingresar un archivo", "Nómina",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtResultado.AppendText(string.Format("{0,-35} {1,12} {2,12} {3,12}" + Environment.NewLine, "NOMBRES Y APELLIDOS", "H.TRABAJADAS", "V.HORA", "V.BRUTO"));

            StreamReader lector = new StreamReader(txtArchivo.Text);
            
            string registro = lector.ReadLine();
            while(registro != null)
            {
                int pos = registro.IndexOf(',');
                string nombre = registro.Substring(0, pos);
                registro = registro.Substring(pos + 1);
                pos = registro.IndexOf(',');
                int horas = Convert.ToInt32(registro.Substring(0, pos));
                registro = registro.Substring(pos + 1);
                pos = registro.IndexOf(',');
                decimal valor = Convert.ToDecimal(registro.Substring(0, pos));
                decimal sbruto = horas * valor;

                txtResultado.AppendText(string.Format("{0,-35} {1,10} {2,12:C} {3,12:C}" + Environment.NewLine, nombre, horas, valor, sbruto));
                registro = lector.ReadLine();
            }
            lector.Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            DialogResult rta = ofdDialogo.ShowDialog();
            if (rta == DialogResult.Cancel) return;
            txtArchivo.Text = ofdDialogo.FileName;
        }
    }
}
