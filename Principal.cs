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
            int num = this.MdiChildren.Length;
        }

        private void colorFondoTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Paleta de colores para seleccíonar un color.
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
                hijoActivo.guardarComo();
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Hijo hijoActivo = (Hijo)this.ActiveMdiChild;

            if (hijoActivo == null)
            {
                hijoActivo = new Hijo();
                hijoActivo.MdiParent = this;
                hijoActivo.Show();
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != null)
            {
                string filePath = openFileDialog.FileName;      // Ruta de archivo seleccionado.
                Stream fileStream = openFileDialog.OpenFile();
                StreamReader reader = new StreamReader(fileStream);
                string fileContent = reader.ReadToEnd();    // Texto de documento.

                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                if (rtxt.Modified == false)     // Si no ha sido modificado el richtext del formulario hijo existente, le introduzco el contenido del documento abierto.
                {
                    rtxt.Text = fileContent;
                    string nombreArchivo = openFileDialog.SafeFileName;

                    // Eliminación de extensión en nombre de archivo
                    nombreArchivo = nombreArchivo.EndsWith(".doc") || nombreArchivo.EndsWith(".docx") || nombreArchivo.EndsWith(".rtf") ||
                         nombreArchivo.EndsWith(".txt") || nombreArchivo.EndsWith(".html") || nombreArchivo.EndsWith(".htm") ||
                         nombreArchivo.EndsWith(".xml") || nombreArchivo.EndsWith(".wpd") || nombreArchivo.EndsWith(".sxw") ||
                         nombreArchivo.EndsWith(".uot") || nombreArchivo.EndsWith(".pdf") || nombreArchivo.EndsWith(".odt")
                    ? Path.GetFileNameWithoutExtension(nombreArchivo) : nombreArchivo;

                    try
                    {
                        hijoActivo.Text = nombreArchivo;
                        // Actualizamos el listado de documentos de la ventana para que se modifique el nombre.
                        this.ActivateMdiChild(null);
                        this.ActivateMdiChild(hijoActivo);
                        hijoActivo.ubicacion = filePath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error", "Extensión no reconocida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else   // Sino, creo un nuevo formulario para meterle el contenido.
                {
                    Hijo nuevoHijo = new Hijo();
                    string nombreArchivo2 = openFileDialog.SafeFileName;

                    // Eliminación de extensión en nombre de archivo
                    nombreArchivo2 = nombreArchivo2.EndsWith(".doc") || nombreArchivo2.EndsWith(".docx") || nombreArchivo2.EndsWith(".rtf") ||
                        nombreArchivo2.EndsWith(".txt") || nombreArchivo2.EndsWith(".html") || nombreArchivo2.EndsWith(".htm") ||
                        nombreArchivo2.EndsWith(".xml") || nombreArchivo2.EndsWith(".wpd") || nombreArchivo2.EndsWith(".sxw") ||
                        nombreArchivo2.EndsWith(".uot") || nombreArchivo2.EndsWith(".pdf") || nombreArchivo2.EndsWith(".odt")
                        ? Path.GetFileNameWithoutExtension(nombreArchivo2) : nombreArchivo2;

                    try
                    {
                        nuevoHijo.Text = nombreArchivo2.Substring(0, nombreArchivo2.IndexOf("."));
                        nuevoHijo.MdiParent = this;
                        nuevoHijo.Show();

                        RichTextBox rtxt2 = (RichTextBox)nuevoHijo.ActiveControl;
                        rtxt2.Text = fileContent;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error", "Extensión no reconocida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                reader.Close();
            }
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
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // The MDI client area is represented by a control of type MdiClient
                //foreach (Control control in this.Controls) 
                //{ 
                //    if (control is MdiClient) 
                //    { 
                //        control.BackColor = Color.Red; 
                //        break; 
                //    } 
                //}
                Controls.OfType<MdiClient>().FirstOrDefault().BackColor = colorDialog.Color;
            }
        }

        private void colorTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;

            if (hijoActivo != null && colorDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.ForeColor = colorDialog.Color;
            }
        }

        private void formatoTextoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form hijoActivo = this.ActiveMdiChild;
            if (hijoActivo != null && fontDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextBox rtxt = (RichTextBox)hijoActivo.ActiveControl;
                rtxt.Font = fontDialog.Font;

            }
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
            Hijo hijoActivo = (Hijo)this.ActiveMdiChild;
            if (hijoActivo != null)
            {
                hijoActivo.guardar();
            }
        }
    }
}
