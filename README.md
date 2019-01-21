# Introduction
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FTontonShino%2FHiaTrackpital.svg?type=shield)](https://app.fossa.io/projects/git%2Bgithub.com%2FTontonShino%2FHiaTrackpital?ref=badge_shield)

Ce projet est basé sur un contexte hospitalier qui concerne la localisation des équipements possedant un tag (RFID).
Il a été livré dans le cadre de l'examen du BTS SIO option SLAM 2018
# Fonctionnalités/Modules

### Trackpital - Client
####Fonctionnalités:
- Authentification/Connexion
- Listage des équipements en fonction de leur position / heure de scan 
- Recherche des équipements par critères
- Gestion des accès utilisateurs
- Recherche des Utilisateurs
- Deserialization des Equipements/Utilisateurs
- Horodatage des données 
- Paramètrage de l'adresse IP du serveur
- Infos utilisateurs avec accès
Bonus: 
Systeme de colorisation
Etat statut équipement rouge/vert/jaune
### Trackpital - Server
- Gestion des multiconnexion (Thread)
- Communication avec les clients
- Prompt des requêtes des différents clients
- Serialisation des Equipements/Utilisateurs à travers le réseau
- Transaction avec Base de donnée (select,update)

1. Pré-requis
- Mysql Server minimum 5.7.2(2) avec version BDD hia 2.0
- .NET Framework 4.6.1 
- Windows Server 2012 R2


## License
[![FOSSA Status](https://app.fossa.io/api/projects/git%2Bgithub.com%2FTontonShino%2FHiaTrackpital.svg?type=large)](https://app.fossa.io/projects/git%2Bgithub.com%2FTontonShino%2FHiaTrackpital?ref=badge_large)