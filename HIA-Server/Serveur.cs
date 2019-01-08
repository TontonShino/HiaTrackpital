using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;// Gère le multithread
using System.Net; // Gère le réseau
using System.Net.Sockets;//Gère aussi le réseau :p
using System.IO; // Gère le stream et le read
using SharedEquipement;
using SharedUtilisateur;
using System.Runtime.Serialization.Formatters.Binary;

namespace HIA_Server
{
    class Serveur
    {
        private readonly int port = 6500; //Port du serveur
        private TcpListener listener = null; //objet d'écoute
        TcpClient client = null; //objet TcpClient
        static int compteurClient = 0;
        static public Metier traitement;
        
        

        public Serveur()
        {
            Launch();  
        }// fin du constructeur par défaut

        public void Launch()
        {
            
            traitement = new Metier(); //Instanciation de l'objet 
            
            Console.WriteLine("Serveur HIA version 1.3");

            listener = new TcpListener(IPAddress.Any, port); //Initialisation des paramètres du serveur avec port et provenance de toutes les adresses ip du réseau
            listener.Start(); //Lancemement de l'écoute
           
            Console.WriteLine("Serveur à l'écoute ...");

            //Tant que c'est vrai 
            while (true)
            {
                Console.WriteLine("Serveur en attente d'un client ...");
                client = listener.AcceptTcpClient(); //le listener accepte un client
                ThreadPool.QueueUserWorkItem(ThreadClient, client); // le client est ajouté dans la pile de thread en donnant les paramètres à ce thread
            }

        }

        //Thread qui gère le CLIENT au cas par cas
        public static void ThreadClient(object parametres)
        {
            String login, mdp;
            String requete;
            TcpClient clone = parametres as TcpClient; //on clone les paramètres contenu dans Object parametres
            NetworkStream tuyau = clone.GetStream(); //Création du tuyau
            StreamReader lecteur = new StreamReader(tuyau); //lecteur du tuyau
            StreamWriter scribe = new StreamWriter(tuyau)
            {
                AutoFlush = true // permet de ne pas attendre que le buffer soit plein (important!!)
            }; // pigeon voyageur qui envoi un message dans la console du client
            bool connect = false;
            BinaryFormatter bformatter = new BinaryFormatter();



            compteurClient++; //on compte les clients

                Console.WriteLine(compteurClient + " client connecté ");



                Console.WriteLine("En attente de login");
                login = lecteur.ReadLine();
                Console.WriteLine("Login recu:" + login);
                scribe.WriteLine("login recu");
                mdp = lecteur.ReadLine();
                scribe.WriteLine("mdp recu");
                connect = traitement.Connect(login, mdp);
                
            try
            {

                if (connect != true)
                {
                    scribe.WriteLine("identifiant incorrect");
                    scribe.Close();
                    compteurClient--;
                    Console.WriteLine("Fermeture d'une connexion cliente: non identifié");
                    return; //fermeture de la connexion 
                }
                Console.WriteLine("Utilisateur:" + login + " connecté");
                scribe.WriteLine("authentified");

                Utilisateur userData = new Utilisateur();
                //boucle des qui prend en charge les requetes
                do
                {
                    Console.WriteLine("En attente de requete du client:" + login);
                    requete = lecteur.ReadLine();

                    switch (requete)
                    {
                        case "liste":
                            Console.WriteLine("Demande de liste pour: " + login);
                            bformatter = null;
                            bformatter = new BinaryFormatter();
                            List<Equipement> equipements = new List<Equipement>();
                            equipements = traitement.GetEquipementLst();
                            bformatter.Serialize(tuyau, equipements);
                            Console.WriteLine("Liste des équipements envoyé au client:" + login);
                            
                            break;

                        case "my data":
                            Console.WriteLine("Demande d'envoi infos pour:"+login);
                            bformatter = null;
                            bformatter = new BinaryFormatter();
                            userData = traitement.GetUtilisateur(login);
                            bformatter.Serialize(tuyau, userData);
                            Console.WriteLine("Infos client envoyé pour:"+login);
                            break;

                        case "recherche equipement":
                            Console.WriteLine("Recherche équipement pour le client:" + login);
                            string req = lecteur.ReadLine();
                            bformatter = null;
                            bformatter = new BinaryFormatter();
                            List<Equipement> searchedEquipements = new List<Equipement>();
                            searchedEquipements = traitement.SearchEqu(req);
                            bformatter.Serialize(tuyau, searchedEquipements);
                            Console.WriteLine("Resultat recherche envoyé pour:"+login);
                            break;

                        case "add access":
                            int fk_access;
                            string type = null;
                            Console.WriteLine("Demande d'ajout d'accès utilisateurpar"+login);
                            fk_access = Convert.ToInt32(lecteur.ReadLine());
                            type = lecteur.ReadLine();
                            string resaddAccess = traitement.AddUserAccess(fk_access, type);
                            scribe.WriteLine(resaddAccess);
                            //
                            break;
                            

                        case "delete access":
                            int access;
                            string typeofacess;

                            Console.WriteLine("Demande de retrait d'accès par "+login);

                            access = Convert.ToInt32(lecteur.ReadLine());
                            typeofacess = lecteur.ReadLine();
                            var res = traitement.DeleteUserAccess(access,typeofacess);
                            scribe.WriteLine(res);

                            break;

                        case "signal equipement":
                            Console.WriteLine("Demande de signalement equipement par "+login);
                            string statut, result;
                            int idstatut;
                            statut = lecteur.ReadLine();
                            idstatut = Convert.ToInt32(lecteur.ReadLine());
                            
                            result = traitement.ChangeStatEquipement(idstatut, statut);
                            Console.WriteLine("Changement de statut d'equipement :"+statut+" identifiant statut: "+idstatut);
                            scribe.WriteLine(result);
                            Console.WriteLine("Resultat changement statut envoyé pour "+login);
                            break;

                        case "lst users":
                            Console.WriteLine("Demande information utilisateurs & accès pour "+login);
                            bformatter = null;
                            bformatter = new BinaryFormatter();
                            List<Utilisateur> users = new List<Utilisateur>();
                            users = traitement.GetUserLst();
                            bformatter.Serialize(tuyau, users);
                            Console.WriteLine("Information utilisateurs & accès envoyé pour "+login);
                            
                            break;

                        case "search user":
                            string tosearch;
                            Console.WriteLine("Demande de recherche utilisateur pour "+login);
                            bformatter = null;
                            bformatter = new BinaryFormatter();

                            tosearch = lecteur.ReadLine();
                            Console.WriteLine("recu:"+tosearch);
                            List<Utilisateur> usersToFind = new List<Utilisateur>();
                            usersToFind = traitement.SearchUsr(tosearch);
                            Console.WriteLine("user to find récupéré");
                            bformatter.Serialize(tuyau, usersToFind);
                            Console.WriteLine("Information de recherche envoyé pour " + login);
                            break;
                        default:
                            Console.WriteLine("Aucune correspondance pour la requete: " + requete +", client:"+ login);
                            
                            break;
                        case "disconnect":
                            Console.WriteLine("Demande de déconnexion du client: " + login);

                            break;
                    }


                } while (requete != "disconnect");

                compteurClient--;
                tuyau.Close();
                return;
            }
            catch (Exception e)
            {
                compteurClient--;
                Console.WriteLine("Une erreur s'est produite pour le client:" + login + " erreur: "+e.ToString());
            } 
            }


    }


}
