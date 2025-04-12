using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using miformulario.CLASES;

namespace miformulario
{
    public partial class Form1 : Form
    {

        CRUD miCrud = new CRUD();
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonSaludar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HOla te saludo desde el formulario 👊👊👊");
        }

        private void buttonCrear_Click(object sender, EventArgs e)
        {
            string nombre = textBoxEstudiante.Text;
            string carnet = textBoxCarnet.Text;
            string email = textBoxEmail.Text;
            string seccion = textBoxSeccion.Text;
            string respuesta = miCrud.AgregarAlumno(carnet, nombre, email, seccion);
            MessageBox.Show(respuesta);
        }
    }
}
