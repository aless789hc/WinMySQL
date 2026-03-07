using WinMySQL.Views;

namespace WinMySQL
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void materiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmMaterias materias = new frmMaterias();   
            materias.ShowDialog();
        }
    }
}
