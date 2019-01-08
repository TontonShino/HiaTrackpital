using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIA_Server
{
    class Localisation
    {
        private int idLocalisation { get; set; } // 
        private int numPiece { set; get; }
        private string date { set; get; }
        private DateTime dateConverted { set; get; }


        public Localisation()
        {

        }

        public Localisation(int p_localisation, int p_numPiece, string p_date)
        {
            idLocalisation = p_localisation;
            numPiece = p_numPiece;
            date = p_date;
            dateConverted = DateTime.Parse(date);
        }
    }
}