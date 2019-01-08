using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharedUtilisateur;

namespace HIA
{
    public partial class infos : Form
    {
        public infos(Utilisateur user)
        {
            InitializeComponent();
            //Mise à jour des infos
            l_nameUser.Text = user.Nom;
            l_prenomUser.Text = user.Prenom;
            l_mailUser.Text = user.Adresse_mail;

            //Champ accès consultation
            if(user.Trkp_consultation == 'o')
            {
                l_consultationUser.Text = "oui";
            }
            else
            {
                l_consultationUser.Text = "non";
            }
            //Champ gestion des statuts et équipements
            if(user.Trkp_statut=='o')
            {
                l_statut.Text = "oui";
            }
            else
            {
                l_statut.Text = "non";
            }
            //Champ gestion des accès gestion d'utilisateur (accès)
            if(user.Trkp_user=='o')
            {
                l_access_user.Text = "oui";
            }
            else
            {
                l_access_user.Text = "non";
            }
        }
        //Bouton ok
        private void B_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
