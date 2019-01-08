using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUtilisateur
{
    [Serializable]
    public class Utilisateur
    {
        public int Identifiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { set; get; }
        public string Adresse_mail { get; set; }
        public string Password { get; set; }
        public string Fonction { get; set; }
        public int IdAcess { get; set; }
        public char Trkp_consultation { get; set; }
        public char Trkp_statut { get; set; }
        public char Trkp_user { get; set; }
        public char statistique { get; set; }

        public Utilisateur()
        {

        }
    }
}
