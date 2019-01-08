using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIA_Server
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        //static void Main()
        {
            //Ihm ihm = new Ihm();
            Serveur hiaServer = new Serveur(); //instanciation d'un serveur

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new hia());
            //Application.Run(new Ihm());
        }
    }
}
