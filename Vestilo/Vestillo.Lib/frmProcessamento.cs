using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vestillo.Lib
{
    public partial class frmProcessamento : Form
    {
        public frmProcessamento()
        {
            InitializeComponent();
        }

        public string MensagemProcessamento
        {
            set
            {
                lblProcessamento.BeginInvoke(new Action(() =>  {lblProcessamento.Text = value;}));       
            }
        }

        private void frmProcessamento_Load(object sender, EventArgs e)
        {
        }
    }
}
