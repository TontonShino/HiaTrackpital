using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HIA_Server
{
    public partial class Ihm : Form
    {
        static Serveur srv=null;
        public Ihm()
        {
            InitializeComponent();
            
            
        }

        //Bouton lancer le Serveur
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            Thread thread = new Thread(new ThreadStart(srv.launch));
            thread.Start();*/
            srv = new Serveur();
            bgw_console.RunWorkerAsync(srv);
            

        }
        
        private void launch()
        {
            srv.Launch();
        }
        private void bgw_console_DoWork(object sender, DoWorkEventArgs e)
        {
            launch();
            //rtxb_logs.Text = rtxb_logs.Text + srv.log;
        }
    }
}
