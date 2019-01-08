using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient; //Permet la communication avec une base de donnée
using MySql.Data;
using SharedEquipement;
using SharedUtilisateur;

namespace HIA_Server
{
    class Dao
    {

        //paramettre de connexion bdd
        readonly String cs = @"server=localhost;userid=aforp;password=system;database=hia;port=3306;SslMode=none";
        //objet de connexion MySql : valeur à null car aucun parametre donné
        MySqlConnection conn = null;
        

        //Constructeur par défaut
        public Dao()
        {
                //Initialisation de l'objet en lui donnant les paramètres
                conn = new MySqlConnection(cs);       
        }

        //méthode retourne la liste d'équipements
        public List<Equipement> GetListe()
        {
            
            List<Equipement>Equipements = new List<Equipement>();
            string req = "SELECT * FROM v_all_equipements";
            
            MySqlCommand cmd = new MySqlCommand(req,conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            Equipements = FetchEquipements(reader,Equipements);



            conn.Close();
            return Equipements;
        }
        //Méthode utiliser qui prend en paramètre un reader contenant une requete puis retourne une liste d'équipements
        public List<Equipement> FetchEquipements(MySqlDataReader reader,List<Equipement> Equipements)
        {
            while (reader.Read())
            {
                Equipements.Add(new Equipement
                {
                    IdEquipement = Convert.ToInt32(reader["idEquipement"]),
                    Serial_number = Convert.ToString(reader["serial_number"]),
                    Nom = Convert.ToString(reader["nom"]),
                    Piece = Convert.ToInt32(reader["idPiece"]),
                    Etage = Convert.ToInt32(reader["idEtage"]),
                    DateTime = Convert.ToDateTime(reader["heure_date"]),
                    Description = Convert.ToString(reader["description"]),
                    Type = Convert.ToString(reader["type"]),
                    Statut = Convert.ToString(reader["statut"]),
                    Fk_statut = Convert.ToInt32(reader["fk_statut"]),


                });

               
            }
            return Equipements;
        }
        //Méthode de recherche d'équipement       
        public List<Equipement> SearchEquipement(string clause)
        {
            
            List<Equipement>Equipements = new List<Equipement>();
            string req = "SELECT * FROM v_all_equipements WHERE "+clause;
            MySqlCommand cmd = new MySqlCommand(req, conn);
            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();
            Equipements = FetchEquipements(reader,Equipements);



            conn.Close();
            return Equipements;
        }
        //Renvoi les infos  de l'utilisateur connecté 
        public Utilisateur GetUserInfo(string email)
        {
            Utilisateur user = new Utilisateur();
            string req = "SELECT * FROM user_access WHERE adresse_mail=\"" + email+"\"";
            conn = new MySqlConnection(cs);

            MySqlCommand cmd = new MySqlCommand(req, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while(reader.Read())
            {
                user.Identifiant = Convert.ToInt32(reader["identifiant"]);
                user.Nom = Convert.ToString(reader["nom"]);
                user.Prenom = Convert.ToString(reader["prenom"]);
                user.Adresse_mail = Convert.ToString(reader["adresse_mail"]);
                user.Fonction = Convert.ToString(reader["fonction"]);
                user.Trkp_consultation = Convert.ToChar(reader["trkp_consultation"]);
                user.Trkp_statut = Convert.ToChar(reader["trkp_statut"]);
                user.Trkp_user = Convert.ToChar(reader["trkp_user"]);
                user.IdAcess = Convert.ToInt32(reader["idAcess"]);
                user.statistique = Convert.ToChar(reader["statistique"]);

            }

            conn.Close();
            return user;
        }
        //méthode qui vérifie l'identifiant et le mot de passe
        public bool VerifUser(string id,string mdp)
        {
            string req = "SELECT * FROM utilisateur;";
            List<string> identifiants = new List<string>();
            List<string> mdps = new List<string>();

            conn = new MySqlConnection(cs);
            MySqlCommand cmd = new MySqlCommand(req, conn);
            //ouverture de la connexion de l'objet portant les attributs contenu dans la variable cs (string)
            conn.Open();
            //on stocke dans un objet MySqlReader les resultats de la requete
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //liste.Add(reader[0].ToString());//on affecte dynamiquement l'élément pointé dans la liste
                identifiants.Add(reader["adresse_mail"].ToString());
                mdps.Add(reader["password"].ToString());

            }

            for(int i=0; i<identifiants.Count;i++)
            {
                if(identifiants[i]==id && mdps[i] == mdp)
                {
                    conn.Clone();
                    return true;
                }
            }
            conn.Close();
            return false;
        }
        //Méthode d'ajout acces utilisateur
        public string AddAccess(int fk_access,string type)
        {
            char o = 'o';
            string req = string.Format("UPDATE acces SET {0}='{1}' WHERE idAcess={2}",type,o,fk_access);
            conn = new MySqlConnection(cs);
            MySqlCommand cmd = new MySqlCommand(req, conn);
            conn.Open();

            return ExecQuery(cmd);
        }
        //Méthode retrait access
        public string DeleteAccess(int fk_access, string type)
        {
            //string requete = $"UPDATE Equipement SET nom=\"{nom}\",description=\"{description}\", serial_number=\"{sn}\", type=\"{type}\" WHERE idEquipement={id};";
            char n = 'n';
            string req = string.Format("UPDATE acces SET {0}='{1}' WHERE idAcess={2}",type,n,fk_access);
            conn = new MySqlConnection(cs);
            MySqlCommand cmd = new MySqlCommand(req, conn);
            conn.Open();
            
            return ExecQuery(cmd);
        }
        //méthode de changement de statut
        public string ChangeStatusEquipement(int idstatut, string statut)
        {
            string req = $"UPDATE statut SET statut=\"{statut}\" WHERE idStatut="+idstatut;
            conn = new MySqlConnection(cs);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(req, conn);

            return ExecQuery(cmd);

        }
        //Méthode qui execture une commande sql et retourne un résultat ok / no ok
        public string ExecQuery(MySqlCommand cmd)
        {
            try
            {
                cmd.ExecuteNonQuery();


            }
            catch (MySqlException e)
            {
                conn.Close();
                return "Une erreur s'est produite :" + e.ToString();

            }
            conn.Close();
            return "La demande a bien été effectué";

        }
        //Méthode récupération des utilisateurs et leur accès
        public List<Utilisateur> GetUserLst()
        {
            List<Utilisateur> lst = new List<Utilisateur>();
            string req = "SELECT * FROM user_access";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(req,conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            lst = FetchUsers(reader, lst);

            conn.Close();
            return lst;
            
        }
        //Méthode réutilisable prenant un reader et renvoi une liste
        public List<Utilisateur> FetchUsers(MySqlDataReader reader,List<Utilisateur> lst)
        {
            while (reader.Read())
            {
                lst.Add(new Utilisateur
                {
                    Identifiant = Convert.ToInt32(reader["identifiant"]),
                    Nom = Convert.ToString(reader["nom"]),
                    Prenom = Convert.ToString(reader["prenom"]),
                    Adresse_mail = Convert.ToString(reader["adresse_mail"]),
                    Fonction = Convert.ToString(reader["fonction"]),
                    Trkp_consultation = Convert.ToChar(reader["trkp_consultation"]),
                    Trkp_statut = Convert.ToChar(reader["trkp_statut"]),
                    Trkp_user = Convert.ToChar(reader["trkp_user"]),
                    IdAcess = Convert.ToInt32(reader["idAcess"])
                });
            }
            return lst;
        }
        //Méthode pour rechercher un utilisateur en fonction de: nom prenom et mail
        public List<Utilisateur> SearchUser(string toSearch)
        {
            string req = string.Format("SELECT * FROM user_access WHERE nom LIKE '%{0}%' OR prenom LIKE '%{0}%' OR adresse_mail LIKE '%{0}%'",toSearch);
            List<Utilisateur> users = new List<Utilisateur>();
            MySqlCommand cmd = new MySqlCommand(req, conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            
            users = FetchUsers(reader,users);
            conn.Close();
            return users;
        }

    }
}
