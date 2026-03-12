using OfficeOpenXml;
using System.Data;
using WinMySQL.Clases;

namespace WinMySQL.Views
{
    public partial class frmAlumnos : Form
    {
        Datos datos = new Datos();
        DataSet ds;
        public frmAlumnos()
        {
            InitializeComponent();

        }

        private void frmAlumnos_Load(object sender, EventArgs e)
        {//load cuando se ejecuta cuando se abre el form y ya no se vuelve a ejecutar
            DataSet ds = datos.ejecutar("Select Matricula,Nombre,Apellido,Correo From Alumnos");
            if (ds != null)
            {
                dgvAlumnos.DataSource = ds.Tables[0];
            }
        }

        private void frmAlumnos_Activated(object sender, EventArgs e)
        {
            //siempre cuando seleccionas una ventana
            try
            {
                ds = datos.ejecutar("Select * from Alumnos");
                if (ds != null)
                {
                    dgvAlumnos.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvAlumnos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            frmAlumno alumno = new frmAlumno(
                Convert.ToInt32(dgvAlumnos.CurrentRow.Cells[0].Value),
                Convert.ToInt32(dgvAlumnos.CurrentRow.Cells[1].Value),
                dgvAlumnos.CurrentRow.Cells[2].Value.ToString(),
                dgvAlumnos.CurrentRow.Cells[3].Value.ToString(),
                dgvAlumnos.CurrentRow.Cells[4].Value.ToString());
            alumno.ShowDialog();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int idAlumno = Convert.ToInt32(dgvAlumnos.CurrentRow.Cells[0].Value);
            if (MessageBox.Show("Desea eliminar al Alumno:" +
                dgvAlumnos.CurrentRow.Cells[1].Value.ToString(), "Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool f = datos.ejecutarComando($"Delete from Alumnos where idAlumnos=" +
                    $"{idAlumno}");
                if (f)
                {
                    MessageBox.Show("Registro eliminado", "Sistema");

                }
                else
                {
                    MessageBox.Show("Error al eliminar", "Sistema");

                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAlumno alumno = new frmAlumno();
            alumno.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String path;
            DialogResult dr = ofdExcel.ShowDialog();
            if (dr == DialogResult.OK)
            {
                path = ofdExcel.FileName;
                ExcelPackage.License.SetNonCommercialPersonal("Alessandra");//libreria no comercial
                using (var package = new ExcelPackage(new FileInfo(path)))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    int columnCount = worksheet.Dimension.Columns;
                    DataTable dt = new DataTable();
                    for (int col = 1; col <= columnCount; col++)
                    {
                        dt.Columns.Add(worksheet.Cells[1, col].Value.ToString());
                    }
                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow drNew = dt.NewRow();
                        for (int col = 1; col <= columnCount; col++)
                        {
                            drNew[col - 1] = worksheet.Cells[row, col].Value.ToString();
                        }
                        dt.Rows.Add(drNew);
                        String comando = $"Insert Into Alumnos(Matricula,Nombre,Apellido,Correo) VALUES('{drNew.ItemArray[0]}','{drNew.ItemArray[1]}','" +
                            $"{drNew.ItemArray[2]}','{drNew.ItemArray[3]}')";
                        datos.ejecutarComando(comando);
                    }
                }
            }

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            Busqueda();
        }
        private void Busqueda() { 
            DataSet ds = datos.ejecutar($"Select Matricula,Nombre,Apellido,Correo From Alumnos where Nombre like '%{txtBuscar.Text}%'");
            if (ds != null)
            {
                dgvAlumnos.DataSource = ds.Tables[0];
            }
        }
    }
}
