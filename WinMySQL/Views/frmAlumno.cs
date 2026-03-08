using WinMySQL.Clases;

namespace WinMySQL.Views
{
    public partial class frmAlumno : Form
    {
        int id = 0;
        bool updating = false;
        Datos dt = new Datos();
        public frmAlumno()
        {
            InitializeComponent();
        }
        public frmAlumno(int id, int matricula, string nombre, string apellido, string correo)
        {
            InitializeComponent();
            this.id = id;
            
            txtMatricula.Text = matricula.ToString();
            txtNombre.Text = nombre;
            txtApellido.Text = apellido;
            txtCorreo.Text = correo;
            updating = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (updating == false)
            {
                bool resultado = dt.ejecutarComando(
                    $"Insert into Alumnos (Matricula,Nombre,Apellido,Correo) " +
                    $"values ('{Convert.ToInt32(txtMatricula.Text)}','{txtNombre.Text}','{txtApellido.Text}','{txtCorreo.Text}')"
                    );
                if (resultado)
                {
                    MessageBox.Show("Alumno agregado correctamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al agregar al Alumno");
                }
            }
            else
            {
                bool resultado = dt.ejecutarComando(
                $"Update Alumnos set Matricula='{Convert.ToInt32(txtMatricula.Text)}', Nombre='{txtNombre.Text}', Apellido='{txtApellido.Text}', Correo='{txtCorreo.Text}' " +
                $"where idAlumnos={id}");

                if (resultado)
                {
                    MessageBox.Show("Alumno Actualizado correctamente");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al actualizar al Alumno");
                }
            }

        }
    }
}
