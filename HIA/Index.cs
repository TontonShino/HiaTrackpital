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
using SharedEquipement;


namespace HIA
{
    
    public partial class Index : Form
    {
        bool connexion = false;

        Utilisateur mydata;
        static Client con = new Client();
        List<Equipement> equipements = new List<Equipement>();
        List<Utilisateur> utilisateurs = new List<Utilisateur>();
        List<string> lstType = new List<string>();
        
        


        public Index()
        {

            dgv_Equipements = new DataGridView();//init de la datagridview

            InitializeComponent();

            mydata = new Utilisateur();//Init des données de l'utilisateur
            //Tabs.Enabled = false; //Desactivation de la tab dès le départ pour s'assurer que l'utilisateur passe dabord par la connexion
            Tabs.TabPages.Remove(tabEquipement);
            Tabs.TabPages.Remove(tabGestUser);
            InitCmb();
            dgv_Equipements.RowHeadersVisible = false;
            dgv_GestUser.RowHeadersVisible = false;
  
        }



        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(chb_description.Checked==true && chb_sn.Checked)
            {
                chb_sn.Checked = false;
                chb_description.Checked = false;
            }
        }

        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //bouton recherche
        private void Button1_Click(object sender, EventArgs e)
        {
            Search();
        }



        //Bouton de connexion
        private void Button3_Click_1(object sender, EventArgs e)
        {
            //methode Client qui envoi les identifiants 
            string result, caption;
            MessageBoxButtons button = MessageBoxButtons.OK;
            result = con.Connect(tx_id.Text, tx_mdp.Text);

            switch (result)
            {
                case "authentified":
                    connexion = true;
                    mydata = con.GetMyData();
                    caption = "Vous êtes identifié";
                    //result = result + "\n";
                    result = mydata.Adresse_mail +" "+ mydata.Nom + mydata.Prenom;

                    if(mydata.Trkp_consultation=='o' || mydata.statistique=='o')
                    {
                        result = result + "OK";
                    }
                    else
                    {
                        result = result + " Vous ne disposez pas d'accès à la consultation";
                    }
                    break;
                case "identifiant incorrect":
                    connexion = false;
                    caption = "Identifiant incorrect";
                    break;
                default:
                    connexion = false;
                    caption = "Cause inconnu veuillez réessayer";
                    break;
            }
            
            DialogResult dialog = MessageBox.Show(result, caption, button);

            

            CheckState();
        }

        //Méthode qui vérifie si l'on doit laisser ou pas les éléments de la fenetre actifs en fonction du statut de la connexion
        private void CheckState()
        {

            //Si la connexion existe  ou a été établie et que l'utilisateur possède bien les accès de consultation
            if(connexion==true && mydata.Trkp_consultation=='o')
            {
                Tabs.TabPages.Add(tabEquipement);
                //On rend les contrôles actif
                Tabs.TabPages.Remove(tabAccueil);
                //Tabs.Enabled = true;
                b_connecter.Enabled = false;
                b_disconnect.Enabled = true;
                tx_mdp.Visible = false;
                tx_id.Enabled = false;
                ltot.Visible = false;
                l_totalequip.Visible = false;
                
                if(mydata.Trkp_user=='o')//Si il possede les droits gest user
                {
                    Tabs.TabPages.Add(tabGestUser);
                    
                }



            }
            else if (connexion == true && mydata.statistique == 'o') //Accès statistique
            {
                l_equipementToSignal.Visible = false;
                cmb_changeState.Visible = false;
                cmb_statut.Visible = false;
                chb_description.Visible = false;
                chb_sn.Visible = false;
                b_research.Text = "Rechercher";
                b_research.BackgroundImage = null;
                b_getServerData.Visible = false;
                label1.Text = "Statistique";
                b_signaler.Visible = false;
                if(ltot.Visible==false && l_totalequip.Visible==false)
                {
                    ltot.Visible = true;
                    l_totalequip.Visible = true;
                }
                Tabs.TabPages.Add(tabEquipement);


                
                Search();
            }
            else
            {
                Tabs.TabPages.Remove(tabAccueil);
                Tabs.TabPages.Remove(tabEquipement);
                Tabs.TabPages.Remove(tabGestUser);
                Tabs.TabPages.Add(tabAccueil);
                //On redirige à la reconnexion
                //Tabs.Enabled = false;
                b_connecter.Enabled = true;
                b_disconnect.Enabled = false;
                tx_mdp.Visible = true;
                tx_id.Enabled = true;
                con.Disconnect();

            }
            
        }

        private void B_disconnect_Click(object sender, EventArgs e)
        {
            //envoi au serveur demande de déconnexion
            con.Disconnect();
            connexion = false;
            CheckState();
            mydata = null;
        }

        //button de récupération de liste des équipements
        private void Button2_Click(object sender, EventArgs e)
        {
            
            Liste();
        }

        //Méthode refresh de la liste
        public void Liste()
        {
            equipements = null;
            dgv_Equipements.Rows.Clear();
            equipements = con.GetListEquipements();
            RefreshEquipementLst(equipements);

        }


        public void RefreshEquipementLst(List<Equipement> equ)
        {

            equipements = equ;
            dgv_Equipements.Rows.Clear();
            // id, date heure, nom , piece, etage,n/s,description , type
            for (int i = 0; i < equipements.Count; i++)
            {
                dgv_Equipements.Rows.Add(
                    equipements[i].IdEquipement,
                    equipements[i].DateTime,
                    equipements[i].Nom, equipements[i].Piece,
                    equipements[i].Etage,
                    equipements[i].Serial_number,
                    equipements[i].Description,
                    equipements[i].Type,
                    equipements[i].Statut);
            }
            ChangeBackground(equ);
            l_hourMaj.Text = Convert.ToString(DateTime.Now);
            l_hourMaj.Update();
            l_totalequip.Text = Convert.ToString(equ.Count);
            l_totalequip.Update();

        }

        //Méthode qui met en couleur les équipements
        private void ChangeBackground(List<Equipement> equi)
        {

            for(int i=0;i<equi.Count;i++)
            {
                switch (equi[i].Statut)
                {
                    case "En service":
                        dgv_Equipements.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        break;
                    case "En panne":
                        dgv_Equipements.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        break;
                    default:
                        dgv_Equipements.Rows[i].DefaultCellStyle.BackColor = Color.Orange;
                        break;

                }

            }
        }
        //Méthode d'initialisation des données
        private void InitCmb()
        {
            
            //init de la comboBox piece
            cmb_piece.Items.Add("Tout");
            for (int i=0; i<=45;i++)
            {
                cmb_piece.Items.Add(i);
            }

            //Init de la combo box etat
            cmb_etage.Items.Add("Tout");
            for(int i=-1;i <=3;i++)
            {
                cmb_etage.Items.Add(i);
            }

            //Init du type
            cmb_type.Items.Add("Tout");
            cmb_type.Items.Add("Lit");
            cmb_type.Items.Add("Fauteuil");
            cmb_type.Items.Add("Outil");
            cmb_type.Items.Add("Siège");
            cmb_type.Items.Add("Ordinateur");
            cmb_type.Items.Add("Autre");

            //Init du statut
            cmb_statut.Items.Add("Tout");
            cmb_statut.Items.Add("En service");
            cmb_statut.Items.Add("En panne");
            cmb_statut.Items.Add("En maintenance");
            cmb_statut.Items.Add("Volé/Perdu");
            cmb_statut.Items.Add("Vendu");
            cmb_statut.Items.Add("Défectueux");
            cmb_statut.Items.Add("Hors Parc");

            //Init du cmb change state
            cmb_changeState.Items.Add("En service");
            cmb_changeState.Items.Add("En panne");
            cmb_changeState.Items.Add("En maintenance");
            cmb_changeState.Items.Add("Volé/Perdu");
            cmb_changeState.Items.Add("Vendu");
            cmb_changeState.Items.Add("Défectueux");
            cmb_changeState.Items.Add("Hors Parc");

            cmb_piece.SelectedIndex = 0;
            cmb_etage.SelectedIndex = 0;
            cmb_type.SelectedIndex = 0;
            cmb_statut.SelectedIndex = 0;
        }
        //Méthode de recherche d'équipement récupération des données présent dans le formulaire
        public void Search()
        {
            List<Equipement> tmpLst = new List<Equipement>();
            string equip = Convert.ToString(inp_ToResarch.Text);
            string resarchType = GetResearchType();//SN ou Description ou vide
            int etage = GetEtage();//Piece ou tout
            int piece = GetPiece();
            string statut = GetStatut();//Statut ou tout
            string type = GetTypeEquipement();//Type ou tout
            
            List<string> requete = new List<string>();

            //Préparation de la requete
            if(resarchType==null)
            {
                requete.Add(string.Format("nom LIKE '%{0}%'",equip));
            }
            else
            {
                requete.Add(string.Format("{0} LIKE '%{1}%'",resarchType,equip));
            }

            if(etage == -1000)
            {
                //Ne fais rien
            }
            else
            {
                requete.Add(string.Format("idEtage={0}",etage));
            }
            
            if(piece==-1000)
            {
                //Ne fais rien
            }
            else
            {
                requete.Add(string.Format("idpiece={0}",piece));
            }

            if(statut=="Tout")
            {
                //Ne fais rien
            }
            else
            {
                requete.Add(string.Format("statut=\"{0}\"",statut));
            }

            if(type=="Tout")
            {

            }
            else
            {
                requete.Add(string.Format("type=\"{0}\"",type));
            }

            RefreshEquipementLst(con.SendReqEquipement(requete));//Raffraichissement de la liste au travers de la requete

            //demande à la bdd

        }

        private void Chb_description_CheckedChanged(object sender, EventArgs e)
        {
            if(chb_description.Checked==true && chb_sn.Checked==true)
            {
                chb_sn.Checked = false;
                chb_description.Checked = false;
            }
        }

        //Méthode qui récupère le type de recherche
        private string GetResearchType()
        {
            if(chb_description.Checked==true || chb_sn.Checked==true)
            {
                if(chb_description.Checked==true)
                {
                    return "description";
                }
                else
                {
                    return "serial_number";
                }
            }
            else
            {
                return null;
            }
        }
        private int GetPiece()
        {
            if (cmb_piece.Text != "Tout")
            {
                return  Convert.ToInt32(cmb_piece.Text);
            }
            else
            {
                return -1000;
            }
        }

        private int GetEtage()
        {
            if(cmb_etage.Text!="Tout")
            {
                return Convert.ToInt32(cmb_etage.Text);
            }
            else
            {
                return -1000;
            }
        }
        private string GetStatut()
        {
            if(cmb_statut.Text=="Tout")
            {
                return "Tout";
            }
            else
            {
                return cmb_statut.Text;
            }
        }
        //Recupératio du type
        private string GetTypeEquipement()
        {
            if(cmb_type.Text != "Tout")
            {
                return cmb_type.Text;
            }
            else
            {
                return "Tout";
            }
        }
        //Méthode de concaténation de la requete
        private string AddToRequest(string req)
        {
            return req;
        }

        //bouton changer adresse ip serveur
        private void ModifierAdresseDuServeurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            serverAdress server = new serverAdress(con.Ip_dest);
            server.ShowDialog();
            SetIP(server.addrr);
        }

        //Méthode de changement de l'adresse ip
        public void SetIP(string addr)
        {
            con.Ip_dest = addr;
            Console.WriteLine("Addresse ip changé:" + addr);
        }
        //Méthode qui ouvre une fenetre avec les infos de l'utilisateur
        private void MesInfosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            infos infos = new infos(mydata);
            infos.ShowDialog();
        }

        //Bouton signalement équipement
        private void B_signaler_Click(object sender, EventArgs e)
        {
            string res;
            int index = GetStatutId();
            if (mydata.Trkp_statut=='o') {
            
            res = con.SignalEquip(equipements[index].Fk_statut,cmb_changeState.Text);
                
            }
            else
            {
                res = "Vous n'êtes pas autorisé à faire cette manipulation";

            }
            

            MessageBoxButtons button = MessageBoxButtons.OK;
            DialogResult dialog = MessageBox.Show(res, "Résultat commande",  button);

        }

        //Récupération de l'id statut dans la datagridview selectionné
        public int GetStatutId()
        {
            int fk;
           fk =  dgv_Equipements.CurrentCell.RowIndex;
            return fk;
        }



        //Méthode lors du clic récupere la liste des utilisateurs
        private void B_downloadUser_Click(object sender, EventArgs e)
        {
            utilisateurs = null;
            utilisateurs = new List<Utilisateur>();
            utilisateurs = con.AskUserList();
            RefreshUserLst(utilisateurs);

            
            
        }

        //Méthode de raffraichissement de la datagrid en lui passant une liste d'équipements
        public void RefreshUserLst(List<Utilisateur> utilisateurs)
        {
            dgv_GestUser.Rows.Clear();
            for (int i = 0; i < utilisateurs.Count; i++)
            {
                dgv_GestUser.Rows.Add(utilisateurs[i].Identifiant, utilisateurs[i].Nom, utilisateurs[i].Prenom, utilisateurs[i].Adresse_mail, utilisateurs[i].Fonction, utilisateurs[i].Trkp_consultation, utilisateurs[i].Trkp_statut, utilisateurs[i].Trkp_user);//ajout des données sur chaque lignes

            }
            dgv_GestUser.DefaultCellStyle.BackColor = Color.LightGray;
            SetUserAccessColors();

        }

        /*accès coloring*/
        private void SetUserAccessColors()
        {
            //dgv_Equipements.Rows[i].DefaultCellStyle.BackColor = Color.Green;
            for (int i=0;i<utilisateurs.Count;i++)
            {
                switch(utilisateurs[i].Trkp_consultation)//Consultation
                {
                    case 'o':
                        dgv_GestUser.Rows[i].Cells[5].Style.BackColor = Color.Green;
                        break;
                    case 'n':
                        dgv_GestUser.Rows[i].Cells[5].Style.BackColor = Color.Red;

                        break;


                }

                switch(utilisateurs[i].Trkp_statut)//Statut
                {
                    case 'o':
                        dgv_GestUser.Rows[i].Cells[6].Style.BackColor = Color.Green;
                         break;
                    case 'n':
                        dgv_GestUser.Rows[i].Cells[6].Style.BackColor = Color.Red;
                        break;
                }

                switch (utilisateurs[i].Trkp_user)//Gestion utilisateur
                {
                    case 'o':
                        dgv_GestUser.Rows[i].Cells[7].Style.BackColor = Color.Green;
                        break;
                    case 'n':
                        dgv_GestUser.Rows[i].Cells[7].Style.BackColor = Color.Red;
                        break;
                }
            }
        }
        //Bouton recherche utilisateur
        private void BSearchUser_Click(object sender, EventArgs e)
        {
            utilisateurs = null;
            utilisateurs = con.SearchUser(inp_searchUser.Text);
            RefreshUserLst(utilisateurs);
        }


        //bouton ajouter accès consultation 
        private void BAddCons_Click(object sender, EventArgs e)
        {
            int index = GetIndexUser(); //on stocke la cellule pointé
            Utilisateur userSelected = utilisateurs[index];//on stocke ensuite l'utilisateur

            //Vérifier qu'il ne possede pas déjà les acces
            if(userSelected.Trkp_consultation=='o')
            {
                error error = new error("L'utilisateur possède déjà des accès de consultation");
                error.ShowDialog();
            }
            else//si ce n'est pas le cas on peut demander au serveur de l'ajouter
            {
                string result = con.AddAccesUser(userSelected.IdAcess,"trkp_consultation");
                Message m = new Message(result);
                m.ShowDialog();
            }



            
        }
        private int GetIndexUser()
        {
            return dgv_GestUser.CurrentCell.RowIndex;

        }
        //Bouton ajouter statut accès 
        private void BAddStatut_Click(object sender, EventArgs e)
        {
            int index = GetIndexUser();
            Utilisateur userSelected = utilisateurs[index];

            if(userSelected.Trkp_statut=='o')
            {
                error error = new error("L'utilisateur possède déjà les accès de gestion des statuts d'équipements");
                error.ShowDialog();
            }
            else
            {
                string result = con.AddAccesUser(userSelected.IdAcess,"trkp_statut");
                Message m = new Message(result);
                m.ShowDialog();
            }
        }

        //Bouton ajout gest user
        private void BAddGestUser_Click(object sender, EventArgs e)
        {
            int index = GetIndexUser();
            Utilisateur userSelected = utilisateurs[index];

            if(userSelected.Trkp_user=='o')
            {
                error error = new error("L'utilisateur possède déjà les droits de gestion des utilisateurs");
                error.ShowDialog();

            }
            else
            {
                string result = con.AddAccesUser(userSelected.IdAcess,"trkp_user");
                Message m = new Message(result);
                m.ShowDialog();
            }
        }

        //Bouton retrait acces consultation
        private void BRemoveCons_Click(object sender, EventArgs e)
        {
            int index = GetIndexUser();
            Utilisateur userSelected = utilisateurs[index];

            if(userSelected.Trkp_consultation=='n')
            {
                error error = new error("L'utilisateur ne possède pas les droits de consultation");
                error.ShowDialog();
            }
            else
            {
                string result = con.DeleteAccesUser(userSelected.IdAcess,"trkp_consultation");
                Message m = new Message(result);
                m.ShowDialog();
            }

        }
        //Bouton retrait des droits de gestion des statuts équipements
        private void BRemoveStatut_Click(object sender, EventArgs e)
        {
            int index = GetIndexUser();
            Utilisateur userSelected = utilisateurs[index];

            if(userSelected.Trkp_statut=='n')
            {
                error error = new error("L'utilisateur ne possède pas du droit de gestion d'équipements");
                error.ShowDialog();
            }
            else
            {
                string result = con.DeleteAccesUser(userSelected.IdAcess, "trkp_statut");
                Message m = new Message(result);
                m.ShowDialog();
            }
        }

        //Bouton de retrait des accès : gestion des utilisateurs
        private void BRemoveGestUser_Click(object sender, EventArgs e)
        {
            int index = GetIndexUser();
            Utilisateur userSelected = utilisateurs[index];

            if(userSelected.IdAcess=='n')
            {
                error error = new error("L'utilisateur ne possède pas du droit de gestion des accès utilisateur");
                error.ShowDialog();
            }
            else
            {
                string result = con.DeleteAccesUser(userSelected.IdAcess,"trkp_user");
                Message m = new Message(result);
                m.ShowDialog();
            }
        }


    }

}
