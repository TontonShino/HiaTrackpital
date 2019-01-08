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
    public partial class Message : Form
    {
        public Message(string message)
        {
            InitializeComponent();
            l_result.Text = message;
            l_result.Update();
        }

        private void B_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
