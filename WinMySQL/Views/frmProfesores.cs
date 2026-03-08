using System.Data;
using WinMySQL.Clases;

namespace WinMySQL.Views
{
    public partial class frmProfesores : Form
    {
        Datos datos = new Datos();
        DataSet ds;
        public frmProfesores()
        {
            InitializeComponent();
        }

        private void txtAgregar_Click(object sender, EventArgs e)
        {
            frmProfesor profesor = new frmProfesor();
            profesor.ShowDialog();
        }

        private void frmProfesores_Activated(object sender, EventArgs e)
        {
            try
            {
                ds = datos.ejecutar("Select * from Profesores");
                if (ds != null)
                {
                    dgvProfesores.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex) { }
        }

        private void dgvProfesores_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmProfesor profesor = new frmProfesor(
                Convert.ToInt32(dgvProfesores.CurrentRow.Cells[0].Value),
                dgvProfesores.CurrentRow.Cells[1].Value.ToString(),
                dgvProfesores.CurrentRow.Cells[2].Value.ToString(),
                dgvProfesores.CurrentRow.Cells[3].Value.ToString(),
                dgvProfesores.CurrentRow.Cells[4].Value.ToString());
            profesor.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idProfesor = Convert.ToInt32(dgvProfesores.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Deseas eliminar al profesor:"
                + dgvProfesores.CurrentRow.Cells[1].Value.ToString() + " " + dgvProfesores.CurrentRow.Cells[2].Value.ToString(),
                "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
            {
                bool f = datos.ejecutarComando($"Delete from Profesores where idProfesores=" +
                    $"{idProfesor}");
                if (f)
                {
                    MessageBox.Show("Registro Eliminado", "Sistemas");
                }
                else
                {
                    MessageBox.Show("Error", "Sistema");
                }
            }

        }
    }
}
