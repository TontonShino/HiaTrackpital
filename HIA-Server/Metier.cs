using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEquipement;
using SharedUtilisateur;

namespace HIA_Server
{
    class Metier
    {
        static public Dao donnee;//Objet DAO
        //Constrcteur par défaut de Métier
        public Metier()
        {
            donnee = new Dao();//Instanciation de l'objet DAO  
        }
        //En fonction du mail donnée on récupère les infos utilisateur
        public Utilisateur GetUtilisateur(string mail)
        {
            return donnee.GetUserInfo(mail);
        }

        //connexion avec les accès d'identification
        public bool Connect(string id, string mdp)
        {
            return donnee.VerifUser(id,mdp);
        }
        //Récupération des équipements 
        public List<Equipement> GetEquipementLst()
        {
            return donnee.GetListe();
        }
        //Méthode de recherche d'équipements
        public List<Equipement> SearchEqu(string req)
        {
            return donnee.SearchEquipement(req);
        }
        //Méthode de changement de statut des équipements
        public string ChangeStatEquipement(int fk_stat,string statut)
        {
            return donnee.ChangeStatusEquipement(fk_stat,statut);
        }
        //Méthode qui récupere la lite des utilsiateurs
        public List<Utilisateur> GetUserLst()
        {
            return donnee.GetUserLst();
        }
        //Méthode d'ajout d'accès
        public string AddUserAccess(int fkaccess, string type)
        {
            return donnee.AddAccess(fkaccess,type);
        }
        //Méthode qui octroie d'accès
        public string DeleteUserAccess(int fkacess, string type)
        {
            return donnee.DeleteAccess(fkacess, type);
        }
        //Méthode de recherche d'utilisateur
        public List<Utilisateur> SearchUsr(string search)
        {
            return donnee.SearchUser(search);
        }

     
    }
}
