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
        public string filepath { get; set; }

        public Hijo()
        {
            InitializeComponent();
            numTexto++;
            this.Text = "Documento " + numTexto.ToString();
        }

        private void frmHijo_FormClosing(object sender, FormClosingEventArgs e)
        {

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

        internal void guardarComo(SaveFileDialog saveFD)
        {
            //if (rtxtHijoActivo.Modified)
            //{
            Stream streamOpenFile;
            if (saveFD.ShowDialog() == DialogResult.OK)
            {
                if ((streamOpenFile = saveFD.OpenFile()) != null)
                {
                    StreamWriter sw = new StreamWriter(streamOpenFile);
                    sw.Write(rtxt.Text);

                    sw.Close();
                    streamOpenFile.Close();
                }
            }
            //}
        }
    }
}
