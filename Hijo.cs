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
    public partial class Hijo : Form
    {
        static int numTexto = 0;
        public string ubicacion { get; set; }

        public Hijo()
        {
            InitializeComponent();
            numTexto++;
            this.Text = "Documento " + numTexto.ToString();
            ubicacion = string.Empty;
        }

        private void frmHijo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (rtxt.Modified)
            {
                DialogResult dr = MessageBox.Show("¿Desea guardar los cambios?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else if (dr == DialogResult.Yes)
                {
                    guardar();
                }
            }
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxt.Cut();
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxt.Copy();
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxt.Paste();
        }

        internal void guardarComo()
        {
            Stream streamOpenFile;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((streamOpenFile = saveFileDialog.OpenFile()) != null)
                {
                    StreamWriter sw = new StreamWriter(streamOpenFile);
                    sw.Write(rtxt.Text);
                    ubicacion = saveFileDialog.FileName;
                    sw.Close();
                    streamOpenFile.Close();
                    rtxt.Modified = false;
                }
            }
        }

        internal void guardar()
        {
            if (!File.Exists(ubicacion))
            {
                this.guardarComo();
            }
            else
            {
                File.WriteAllText(ubicacion, rtxt.Text);
                rtxt.Modified = false;
            }
        }
    }
}
