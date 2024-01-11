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

namespace Word
{
    public partial class Principal : Form
    {
        // Borrar
        public Principal()
        {
            InitializeComponent();
            Hijo nuevoHijo = new Hijo();
            nuevoHijo.MdiParent = this;
            nuevoHijo.Show();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hijo nuevoHijo = new Hijo();
            nuevoHijo.MdiParent = this;
            nuevoHijo.Show();
        }

        private void colorFondoTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Paleta de colores para seleccíonar un color.
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();

            // Pinta fondo del documento del color seleccionado.
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                RichTextBox miCaja = (RichTextBox)hijoActivo.ActiveControl;
                miCaja.BackColor = colorDialog.Color;
            }
        }

        private void guardarcomoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hijo hijoActivo = (Hijo)this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                hijoActivo.guardarComo(saveFD);                
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hijo hijoActivo = (Hijo)this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();

                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";  // A la L de | aparece el texto a mostrar y a la derecha el formato a buscar a nivel interno y mostrar
                if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != null)
                {
                    string filePath = openFileDialog.FileName;      // Ruta de archivo seleccionado
                    Stream fileStream = openFileDialog.OpenFile();
                    StreamReader reader = new StreamReader(fileStream);
                    string fileContent = reader.ReadToEnd();    // Texto de documento

                    RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                    if (rtxt.Modified == false)     // Si no ha sido modificado el richtext del formulario hijo existente, le introduzco el contenido del documento abierto.
                    {
                        rtxt.Text = fileContent;
                        string nombreArchivo = openFileDialog.SafeFileName;
                        hijoActivo.Text = nombreArchivo.Substring(0, nombreArchivo.IndexOf("."));
                        // Actualizamos el listado de documentos de la ventana para que se modifique el nombre.
                        this.ActivateMdiChild(null);
                        this.ActivateMdiChild(hijoActivo);
                        hijoActivo.filepath = filePath;
                    }
                    else   // Sino, creo un nuevo formulario para meterle el contenido.
                    {
                        Hijo nuevoHijo = new Hijo();
                        string nombreArchivo2 = openFileDialog.SafeFileName;
                        nuevoHijo.Text = nombreArchivo2.Substring(0, nombreArchivo2.IndexOf("."));
                        nuevoHijo.MdiParent = this;
                        nuevoHijo.Show();

                        RichTextBox rtxt2 = (RichTextBox)nuevoHijo.ActiveControl;
                        rtxt2.Text = fileContent;
                    }
                }
            }



            //MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.Cut();
            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.Copy();
            }
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.Paste();
            }
        }

        private void seleccionartodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.SelectAll();
            }
        }

        private void deshacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.Undo();
            }
        }

        private void rehacerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.Redo();
            }
        }

        private void colorFondoFormularioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.ShowDialog();

            this.BackColor = colorDialog.Color;
            //Form hijoActivo = this.ActiveMdiChild;
            //if(hijoActivo != null)
            //{
            //    hijoActivo.BackColor = colorDialog.Color;
            //}
        }

        private void colorTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            Form hijoActivo = this.ActiveMdiChild;

            if (hijoActivo != null && colorDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.ForeColor = colorDialog.Color;
            }
        }

        private void formatoTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cascadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
