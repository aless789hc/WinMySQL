using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Windows.Forms;
using WinMySQL.Clases;

namespace WinMySQL.Views
{
    public partial class frmProfesor : Form
    {
        int id = 0;
        bool updating = false;
        Datos dt = new Datos();
        public frmProfesor()
        {
            InitializeComponent();
        }
        public frmProfesor(int id, string nombre, string apellido, string correo, string telefono) {
            InitializeComponent();
            this.id = id;
            txtNombre.Text = nombre; 
            txtApellido.Text = apellido;
            txtCorreo.Text = correo;    
            txtTelefono.Text = telefono;
            updating = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (updating == false)
            {
                bool resultado = dt.ejecutarComando(
                    $"Insert into Profesores (Nombre,Apellido,Correo,Telefono) " +
                    $"values ('{txtNombre.Text}','{txtApellido.Text}','{txtCorreo.Text}','{txtTelefono.Text}')");

                if (resultado)
                {
                    MessageBox.Show("Profesor agregado correctamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al agregar al Profesor");
                }
            }
            else
            {
                bool resultado = dt.ejecutarComando(
                    $"Update Profesores set Nombre='{txtNombre.Text}', Apellido='{txtApellido.Text}', Correo='{txtCorreo.Text}', Telefono='{txtTelefono.Text}' " +
                    $"where idProfesores={id}");
                if (resultado)
                {
                    MessageBox.Show("Profesor actualizado correctamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar al Profesor");
                }
            }
        }
    }
}
