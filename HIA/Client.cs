using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;// Gère le multithread
using System.Threading.Tasks;// Gère les tâches multithread
using System.Net; // Gère le réseau
using System.Net.Sockets;// Gère les sockets réseau
using System.IO; // Gère le stream et le read
using SharedEquipement;//Bibliothèque contenant la classe Equipement
using SharedUtilisateur;//Biblothèque contenant la classe Utilisateur
using System.Runtime.Serialization.Formatters.Binary; //permet la serialization/deseralization

namespace HIA
{
    class Client
    {
        public int Port { get; set; } = 6500; // Port du serveur
        public string Ip_dest { get; set; } = "127.0.0.1";  // Adresse IP du serveur
       
        NetworkStream netstream;
        StreamReader lecteur;
        StreamWriter scribe;
        TcpClient client;

        private BinaryFormatter Bformatter { set; get; }


        public string Acces { get; set; }
        public String Recu { get; set; }

        public Client()
        {
            

        }
        //Méthode de connexion au serveur
        public string Connect(string id , string password)
        {
            client = new TcpClient(Ip_dest, Port);
            netstream = client.GetStream(); //clonage du tuyau de stream (communication)

            lecteur = new StreamReader(netstream);//lecteur du stream
            scribe = new StreamWriter(netstream)
            {
                AutoFlush = true// permet de ne pas attendre que le buffer soit plein (important!!)
            }; //pigeon voyageur qui envoi un message dans la console du client

            Authentification(id,password);

            return Recu;
     
        }
        //Méthode qui envoi un message au serveur
        public void Envoi(String p_message)
        {
            scribe.WriteLine(p_message);
            
        }

        //Méthode d'authentification 
        public void Authentification(string id, string mdp)
        {
            Recu = null;
            String tampon = String.Empty;
            Envoi(id);
            tampon = lecteur.ReadLine();
            if (tampon == "login recu") 
            {
                Envoi(mdp);
                tampon = lecteur.ReadLine();

                if (tampon == "mdp recu")
                {
                    tampon = lecteur.ReadLine();
                    Recu = tampon;

                }
            }


        }
        //Méthode de déconnexion
        public void Disconnect()
        {
            if (netstream != null) { 
            scribe.WriteLine("disconnect");
            //client.Close();
            //netstream.Close();

            }

        }
        //Méthode qui demande les données de l'utilisateur connecté
        public Utilisateur GetMyData()
        {
            Utilisateur user = new Utilisateur();
            Bformatter = null;
            Bformatter = new BinaryFormatter();

            scribe.WriteLine("my data");
            user = (Utilisateur)Bformatter.Deserialize(netstream);

            return user;
        }

        //Méthode de demande de la liste des 
        public List<Equipement> GetListEquipements()
        {
            List<Equipement> equipements = new List<Equipement>();
            Bformatter = null;
            Bformatter = new BinaryFormatter();
            scribe.WriteLine("liste");

            equipements = (List<Equipement>)Bformatter.Deserialize(netstream);
            return equipements;

        }

        //Méthode de requête (recherche équipements)
        public List<Equipement> SendReqEquipement(List<string> req)
        {
            List<Equipement> equipements = new List<Equipement>();

            scribe.WriteLine("recherche equipement");
            scribe.WriteLine(PrepareQuery(req));
            Bformatter = null;
            Bformatter = new BinaryFormatter();
            equipements = (List<Equipement>)Bformatter.Deserialize(netstream);
            return equipements;
        }

        //Méthode qui prépare la requete
        public string PrepareQuery(List<string> req)
        {
            string reqRes=null;

            for(int i=0; i<req.Count;i++)
            {
                if (i < 1)
                {
                    reqRes = req[i].ToString();
                }
                else
                {
                    reqRes = reqRes + " AND " + req[i];
                }          

            }

            return reqRes;
        }

        //Méthode qui envoi au serveur une demande d'ajout d'accès à un utilisateur
        public string AddAccesUser(int fk_acces,string type)
        {
            scribe.WriteLine("add access");
            scribe.WriteLine(fk_acces);
            scribe.WriteLine(type);
            string result = lecteur.ReadLine();
            return result; //retour de la réponse serveur
        }
        //Methode qui envoi au serveur d'enlever un retrait d'accès
        public string DeleteAccesUser(int fk_acces, string type)
        {
            scribe.WriteLine("delete access");
            scribe.WriteLine(fk_acces);
            scribe.WriteLine(type);
            string result = lecteur.ReadLine();
            return result; //retour de la réponse serveur
        }
        //Méthode qui signale un équipement
        public string SignalEquip(int fk_statut,string statut)
        {
            scribe.WriteLine("signal equipement");
            scribe.WriteLine(statut);
            scribe.WriteLine(fk_statut);
            
            string result;
            result = lecteur.ReadLine();
            return result;
        }

        //Méthode qui demande la liste des accès Utilisateurs
        public List<Utilisateur> AskUserList()
        {
            List<Utilisateur> lst = new List<Utilisateur>();
            scribe.WriteLine("lst users");
            Bformatter = null;
            Bformatter = new BinaryFormatter();
            lst = (List<Utilisateur>)Bformatter.Deserialize(netstream);

            return lst;
            
        }

        //Méthode qui demande au serveur de lui retrouver des utilisateurs
        public List<Utilisateur> SearchUser(string search)
        {
            List<Utilisateur> users = new List<Utilisateur>();
            Bformatter = null;
            Bformatter = new BinaryFormatter();
            scribe.WriteLine("search user");
            scribe.WriteLine(search);
            users = (List<Utilisateur>)Bformatter.Deserialize(netstream);
            return users;
            

        }





    }
}
