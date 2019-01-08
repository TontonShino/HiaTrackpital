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
    public partial class error : Form
    {
        public error(string err)
        {
            
            InitializeComponent();
            l_error.Text = err;
        }

        private void B_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
