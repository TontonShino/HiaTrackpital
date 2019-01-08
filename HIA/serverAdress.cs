using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIA
{
    public partial class serverAdress : Form
    {
        public string addrr { get; set; }
        public string oldaddr { get; set; }
        public serverAdress(string ipserver)
        {
            
            InitializeComponent();
            inp_ipserver.Text = ipserver;
            oldaddr = ipserver;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }


        //Validation
        private void b_validate_Click(object sender, EventArgs e)
        {
            this.addrr = inp_ipserver.Text;
            this.Close();
        }

        //Femeture
        private void b_cancel_Click(object sender, EventArgs e)
        {
            this.addrr = oldaddr;
            this.Close();
        }
    }
}
