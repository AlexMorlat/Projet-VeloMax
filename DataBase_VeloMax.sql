DROP database if exists VeloMax;
CREATE database if not exists VeloMax;
USE VeloMax;

drop table if exists user ;
drop table if exists modele_velo ;
drop table if exists fournisseur;
drop table if exists modele_piece;
drop table if exists liste_assemblage;
drop table if exists commande;
drop table if exists client_entreprise;
drop table if exists client_particulier;
drop table if exists adresse;
drop table if exists programme_fidelio;
drop table if exists catalogue;
drop table if exists liste_velo_commande;
drop table if exists liste_assemblage;
drop table if exists liste_piece_commande;


create table if not exists user(
fonction varchar(25),
id varchar(20) not null,
password varchar(20) not null,
primary key(id,password));
INSERT INTO user VALUES ('Admin','bozo','bozo');
INSERT INTO user VALUES ('Vendeur','bozo2','bozo2');


#Création des tables
CREATE TABLE if not exists modele_velo 
(numero_velo int NOT NULL, 
nom_velo varchar(40), 
grandeur_velo varchar(40), 
prix_velo int, 
ligne_produit_velo varchar(40), 
date_introduction_velo varchar(40), 
date_discontinuation_velo varchar(40), 
stock_velo int,
PRIMARY KEY(numero_velo));  


CREATE TABLE modele_piece
(numero_piece_catalogue varchar(40) NOT NULL, 
numero_piece varchar(40), 
description_piece varchar(40),
date_introduction_piece varchar(40), 
date_discontinuation_piece varchar(40), 
prix_piece int, 
delai_approvisionnement_piece int, 
stock_piece int,
PRIMARY KEY(numero_piece_catalogue,numero_piece));

CREATE TABLE adresse 
(id_adresse int, 
rue_adresse varchar(40), 
ville_adresse varchar(40), 
code_postal_adresse varchar(8), 
province_adresse varchar(40), 
PRIMARY KEY(ID_adresse));  

CREATE TABLE programme_fidelio
(numero_programme int NOT NULL, 
nom_programme varchar(40), 
prix_programme int, 
duree_programme int, 
rabais_programme float,
PRIMARY KEY (numero_programme));    

CREATE TABLE fournisseur
(siret_fournisseur varchar(40) NOT NULL, 
nom_fournisseur varchar(40),
qualite_fournisseur varchar(20), 
nom_contact_fournisseur varchar(40), 
ID_adresse_fournisseur int,
PRIMARY KEY(siret_fournisseur));
ALTER TABLE fournisseur ADD CONSTRAINT FK_fournisseur_id_adresse 
FOREIGN KEY (ID_adresse_fournisseur) REFERENCES adresse (id_adresse) ON DELETE CASCADE ON UPDATE NO ACTION;

CREATE TABLE client_entreprise
(ID_client_entreprise varchar(40) NOT NULL, 
nom_client_entreprise varchar(40),
remise_client_entreprise float, 
courriel_entreprise varchar(40), 
telephone_entreprise varchar(40), 
nom_contact_entreprise varchar(40), 
ID_adresse_client_entreprise int,
PRIMARY KEY(ID_client_entreprise));
ALTER TABLE client_entreprise ADD CONSTRAINT FK_client_entreprise_id_adresse 
FOREIGN KEY (ID_adresse_client_entreprise) REFERENCES adresse (id_adresse) ON DELETE CASCADE;

CREATE TABLE client_particulier 
(ID_client_particulier varchar(40) NOT NULL, 
nom_client_particulier varchar(40), 
prenom_client_particulier varchar(40), 
date_adhesion_programme varchar(40), 
courriel_particulier varchar(40), 
telephone_particulier varchar(40), 
numero_programme int, 
ID_adresse_client_particulier int,
PRIMARY KEY(ID_client_particulier));
ALTER TABLE client_particulier ADD CONSTRAINT FK_client_particulier_numero_programme 
FOREIGN KEY (numero_programme) REFERENCES programme_fidelio (numero_programme) ON DELETE CASCADE;
ALTER TABLE client_particulier ADD CONSTRAINT FK_client_particulier_id_adresse 
FOREIGN KEY (ID_adresse_client_particulier) REFERENCES adresse (id_adresse) ON DELETE CASCADE; 

CREATE TABLE commande
(numero_commande int NOT NULL, 
date_commande varchar(20), 
date_livraison_commande varchar(20),
ID_adresse_commande int,  
ID_client_entreprise varchar(40), 
ID_client_particulier varchar(40),
statut varchar(40),
PRIMARY KEY(numero_commande));
ALTER TABLE commande ADD CONSTRAINT FK_commande_id_client_entreprise 
FOREIGN KEY (ID_client_entreprise) REFERENCES client_entreprise (id_client_entreprise) ON DELETE CASCADE;
ALTER TABLE commande ADD CONSTRAINT FK_commande_id_client_particulier 
FOREIGN KEY (ID_client_particulier) REFERENCES client_particulier (id_client_particulier) ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE commande ADD CONSTRAINT FK_commande_id_adresse 
FOREIGN KEY (ID_adresse_commande) REFERENCES adresse (id_adresse) ON DELETE CASCADE ON UPDATE NO ACTION;


CREATE TABLE catalogue
(numero_piece_catalogue varchar(40) NOT NULL, 
siret_fournisseur varchar(40) NOT NULL, 
PRIMARY KEY (numero_piece_catalogue, siret_fournisseur));
ALTER TABLE catalogue ADD CONSTRAINT FK_catalogue_numero_piece_catalogue 
FOREIGN KEY (numero_piece_catalogue) REFERENCES modele_piece (numero_piece_catalogue) ON DELETE CASCADE;  
ALTER TABLE catalogue ADD CONSTRAINT FK_catalogue_siret_fournisseur 
FOREIGN KEY (siret_fournisseur) REFERENCES fournisseur (siret_fournisseur)ON DELETE CASCADE; 

CREATE TABLE liste_velo_commande
(numero_commande_velo int NOT NULL, 
numero_velo_commande int, 
quantite_velo_commande int,
PRIMARY KEY (numero_commande_velo, numero_velo_commande));
ALTER TABLE liste_velo_commande ADD CONSTRAINT FK_liste_velo_commande_numero_commande 
FOREIGN KEY (numero_commande_velo) REFERENCES commande (numero_commande) ON DELETE CASCADE;  
ALTER TABLE liste_velo_commande ADD CONSTRAINT FK_liste_velo_commande_numero_velo 
FOREIGN KEY (numero_velo_commande) REFERENCES modele_velo (numero_velo) ON DELETE CASCADE; 


CREATE TABLE IF NOT EXISTS liste_assemblage (numero_velo INT NOT NULL, 
numero_piece_catalogue VARCHAR(5)  NOT NULL,
PRIMARY KEY (numero_velo, numero_piece_catalogue));
ALTER TABLE liste_assemblage ADD CONSTRAINT FK_liste_assemblage_numero_velo FOREIGN KEY (numero_velo) REFERENCES modele_velo (numero_velo);  
ALTER TABLE liste_assemblage ADD CONSTRAINT FK_liste_assemblage_numero_piece_catalogue FOREIGN KEY (numero_piece_catalogue) REFERENCES modele_piece (numero_piece_catalogue);

CREATE TABLE liste_piece_commande 
(numero_piece_catalogue_commande varchar(255) NOT NULL, 
numero_commande_piece int NOT NULL, 
quantite_piece_commande int,
PRIMARY KEY (numero_piece_catalogue_commande, numero_commande_piece));
ALTER TABLE liste_piece_commande ADD CONSTRAINT FK_liste_piece_commande_numero_piece_catalogue 
FOREIGN KEY (numero_piece_catalogue_commande) REFERENCES modele_piece (numero_piece_catalogue) ON DELETE CASCADE;  
ALTER TABLE liste_piece_commande ADD CONSTRAINT FK_liste_piece_commande_numero_commande 
FOREIGN KEY (numero_commande_piece) REFERENCES commande (numero_commande) ON DELETE CASCADE;  



#Peuplement des tables
INSERT INTO modele_velo VALUES (101,'Kilimandjaro','Adultes',569,'VTT','2010-27-09','2018-12-01',22);
INSERT INTO modele_velo VALUES (102,'NorthPole','Adultes',329,'VTT','2013-01-01','2017-12-02',18);
INSERT INTO modele_velo VALUES (103,'MontBlanc','Jeunes',399,'VTT','2000-01-01','2015-12-01',10);
INSERT INTO modele_velo VALUES (104,'Hooligan','Jeunes',199,'VTT','2000-01-01','2015-12-01',08);
INSERT INTO modele_velo VALUES (105,'Orléans','Hommes',229,'Velo_de_course','2015-01-01','2019-12-02',33);
INSERT INTO modele_velo VALUES (106,'Orléans','Dames',229,'Velo_de_course','2015-01-01','2019-12-02',34);
INSERT INTO modele_velo VALUES (107,'BlueJay','Hommes',349,'Velo_de_course','2015-01-01','2019-12-02',84);
INSERT INTO modele_velo VALUES (108,'BlueJay','Dames',349,'Velo_de_course','2015-01-01','2019-12-02',157);
INSERT INTO modele_velo VALUES (109,'Trail_Explorer','Filles',129,'Classique','2016-01-01',null,233);
INSERT INTO modele_velo VALUES (110,'Trail_Explorer','Garçons',129,'Classique','2016-01-01',null,292);
INSERT INTO modele_velo VALUES (111,'Night_Hawk','Jeunes',189,'Classique','2018-01-01',null,157);
INSERT INTO modele_velo VALUES (112,'Tierra_Verde','Hommes',199,'Classique','2019-01-01',null,220);
INSERT INTO modele_velo VALUES (113,'Tierra_Verde','Dames',199,'Classique','2019-01-01',null,221);
INSERT INTO modele_velo VALUES (114,'Mud_Zinger_I','Jeune',279,'BMX','2021-01-01',null,101);
INSERT INTO modele_velo VALUES (115,'Mud_Zinger_II','Adultes',359,'BMX','2021-01-01',null,225);






#numero_piece_catalogue numero_piece description_piece date_introduction_piece date_discontinuation 
#prix_piece delai_approvisionnement_piece stock_piece int,
INSERT INTO modele_piece VALUES ('C32','C32a','Cadre','2014-01-01','2018-01-01',15,3,10); #Piece C32 Fournisseur A
INSERT INTO modele_piece VALUES ('C32','C32b','Cadre','2014-01-01','2018-01-01',15,3,10); #Piece C32 Fournisseur B
INSERT INTO modele_piece VALUES ('C34','C','Cadre','2014-01-01','2018-01-01',15,5,20);
INSERT INTO modele_piece VALUES ('C76','C','Cadre','2014-01-01','2018-01-01',15,1,30);
INSERT INTO modele_piece VALUES ('C43','C','Cadre','2014-01-01','2018-01-01',15,2,5);
INSERT INTO modele_piece VALUES ('C34f','C','Cadre','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('C44f','C','Cadre','2014-01-01','2018-01-01',15,4,20);
INSERT INTO modele_piece VALUES ('C01','C','Cadre','2014-01-01','2018-01-01',15,5,20);
INSERT INTO modele_piece VALUES ('C02','C','Cadre','2014-01-01','2018-01-01',15,6,20);
INSERT INTO modele_piece VALUES ('C15','C','Cadre','2014-01-01','2018-01-01',15,1,8);
INSERT INTO modele_piece VALUES ('C87','C','Cadre','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('C87f','C','Cadre','2014-01-01','2018-01-01',15,2,12);
INSERT INTO modele_piece VALUES ('C25','C','Cadre','2014-01-01','2018-01-01',15,2,13);
INSERT INTO modele_piece VALUES ('C26','C','Cadre','2014-01-01','2018-01-01',15,3,20);
INSERT INTO modele_piece VALUES ('G7','G','Guidon','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('G9','G','Guidon','2014-01-01','2018-01-01',15,4,8);
INSERT INTO modele_piece VALUES ('G12','G','Guidon','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('F3','F','Freins','2014-01-01','2018-01-01',15,3,9);
INSERT INTO modele_piece VALUES ('F9','F','Freins','2014-01-01','2018-01-01',15,5,20);
INSERT INTO modele_piece VALUES ('S88','S','Selle','2014-01-01','2018-01-01',15,3,9);
INSERT INTO modele_piece VALUES ('S37','S','Selle','2014-01-01','2018-01-01',15,2,12);
INSERT INTO modele_piece VALUES ('S35','S','Selle','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('S02','S','Selle','2014-01-01','2018-01-01',15,3,24);
INSERT INTO modele_piece VALUES ('S03','S','Selle','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('S36','S','Selle','2014-01-01','2018-01-01',15,2,22);
INSERT INTO modele_piece VALUES ('S34','S','Selle','2014-01-01','2018-01-01',15,3,16);
INSERT INTO modele_piece VALUES ('S87','S','Selle','2014-01-01','2018-01-01',15,2,24);
INSERT INTO modele_piece VALUES ('DV133','DV','Derrailleur_Avant','2014-01-01','2018-01-01',15,2,9);
INSERT INTO modele_piece VALUES ('DV17','DV','Derrailleur_Avant','2014-01-01','2018-01-01',15,2,14);
INSERT INTO modele_piece VALUES ('DV87','DV','Derrailleur_Avant','2014-01-01','2018-01-01',15,2,22);
INSERT INTO modele_piece VALUES ('DV57','DV','Derrailleur_Avant','2014-01-01','2018-01-01',15,2,18);
INSERT INTO modele_piece VALUES ('DV15','DV','Derrailleur_Avant','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('DV41','DV','Derrailleur_Avant','2014-01-01','2018-01-01',15,2,16);
INSERT INTO modele_piece VALUES ('DV132','DV','Derrailleur_Avant','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('DR56','DR','Derrailleur_Arriere','2014-01-01','2018-01-01',15,2,15);
INSERT INTO modele_piece VALUES ('DR87','DR','Derrailleur_Arriere','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('DR86','DR86a','Derrailleur_Arriere','2014-01-01','2018-01-01',15,2,4); #Founissuer A
INSERT INTO modele_piece VALUES ('DR86','DR86b','Derrailleur_Arriere','2014-01-01','2018-01-01',15,2,4); #Fournisseur B
INSERT INTO modele_piece VALUES ('DR23','DR','Derrailleur_Arriere','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('DR76','DR','Derrailleur_Arriere','2014-01-01','2018-01-01',15,2,3);
INSERT INTO modele_piece VALUES ('DR52','DR','Derrailleur_Arriere','2014-01-01','2018-01-01',15,2,3);

INSERT INTO modele_piece VALUES ('RV45','RV','Roue_Avant','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RV48','RV','Roue_Avant','2014-01-01','2018-01-01',15,2,7);
INSERT INTO modele_piece VALUES ('RV12','RV','Roue_Avant','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RV19','RV','Roue_Avant','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RV1','RV','Roue_Avant','2014-01-01','2018-01-01',15,2,16);
INSERT INTO modele_piece VALUES ('RV11','RV','Roue_Avant','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RV44','RV','Roue_Avant','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RR46','RR','Roue_Arriere','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RR47','RR','Roue_Arriere','2014-01-01','2018-01-01',15,2,9);
INSERT INTO modele_piece VALUES ('RR32','RR','Roue_Arriere','2014-01-01','2018-01-01',15,2,6);
INSERT INTO modele_piece VALUES ('RR18','RR','Roue_Arriere','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RR2','RR','Roue_Arriere','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('RR12','RR','Roue_Arriere','2014-01-01','2018-01-01',15,2,1);
INSERT INTO modele_piece VALUES ('R02','R','Reflecteur','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('R09','R','Reflecteur','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('R10','R','Reflecteur','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('O2','O','Ordinateur','2014-01-01','2018-01-01',15,2,15);
INSERT INTO modele_piece VALUES ('O4','O','Ordinateur','2014-01-01','2018-01-01',15,2,7);
INSERT INTO modele_piece VALUES ('S01','S','Panier','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('S05','S','Panier','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('S74','S','Panier','2014-01-01','2018-01-01',15,2,20);
INSERT INTO modele_piece VALUES ('S73','S','Panier','2014-01-01','2018-01-01',15,2,0);
INSERT INTO modele_piece VALUES ('P12','P','Pédalier','2014-01-01','2018-01-01',15,2,10);
INSERT INTO modele_piece VALUES ('P34','P','Pédalier','2014-01-01','2018-01-01',15,2,12);

INSERT INTO liste_assemblage VALUES 
(101,'C32'),('101','G7'),('101','F3'),('101','S88'),('101','DV133'),
('101','DR56'),('101','RV45'),('101','RR46'),('101','P12'),('101','O2');
INSERT INTO liste_assemblage VALUES 
(102,'C34'),('102','G7'),('102','F3'),('102','S88'),('102','DV17'),
('102','DR87'),('102','RV48'),('102','RR47'),('102','P12');
INSERT INTO liste_assemblage VALUES 
(103,'C76'),('103','G7'),('103','F3'),('103','S88'),('103','DV17'),
('103','DR87'),('103','RV48'),('103','RR47'),('103','P12'),('103','O2');
INSERT INTO liste_assemblage VALUES 
(104,'C76'),('104','G7'),('104','F3'),('104','S88'),('104','DV87'),
('104','DR86'),('104','RV48'),('104','RR47'),('104','P12');
INSERT INTO liste_assemblage VALUES 
(105,'C43'),('105','G9'),('105','F9'),('105','S37'),('105','DV57'),
('105','DR86'),('105','RV19'),('105','RR18'),(105,'R02'),('105','P34');
INSERT INTO liste_assemblage VALUES 
(106,'C44f'),('106','G9'),('106','F9'),('106','S35'),('106','DV57'),
('106','DR86'),('106','RV19'),('106','RR18'),(106,'R02'),('106','P34');
INSERT INTO liste_assemblage VALUES 
(107,'C43'),('107','G9'),('107','F9'),('107','S35'),('107','DV57'),
('107','DR86'),('107','RV19'),('107','RR18'),('107','R02'),('107','P34');

#numero_programme nom_programme prix_programme duree_programme rabais_programme
INSERT INTO programme_fidelio VALUES (1,'Fidélio',15,1,0.05);
INSERT INTO programme_fidelio VALUES (2,'Fidélio Or',25,2,0.08);
INSERT INTO programme_fidelio VALUES (3,'Fidélio Platine',60,2,0.10);
INSERT INTO programme_fidelio VALUES (4,'Fidélio Max',100,3,0.12);

INSERT INTO adresse VALUES('0','Unknown','Unknown','Unknown','Unknown');
INSERT INTO adresse VALUES('1','12 rue René Bourgeois','Reims','51100','Grand Est');
INSERT INTO adresse VALUES('2','15 rue diderot','Colombes','92700','Ile de France');
INSERT INTO adresse VALUES (3,'95 rue du dragon','Flic-en-Flac','78452','Caverne');
INSERT INTO adresse VALUES (4,'41 boulevard saint michel','Paris','75015','Ile de France');
INSERT INTO adresse VALUES (5,'64 allée du sentier','Oulan-Bator','39509','Ailleurs');
INSERT INTO adresse VALUES (6,'81 route du dr jekyll','Shenzhen','00150','Guangdong');
INSERT INTO adresse VALUES (7,'84 chemin des terres battues','Quigdao','29353','Guangdong');
INSERT INTO adresse VALUES (8,'89 sentier de la ivai','Taipei','05910','Taïwan');
INSERT INTO adresse VALUES (11,'90 sentier de la ivai','Taipei','05910','Taïwan');
INSERT INTO adresse VALUES (12,'72 impasse du Grouin','Liverpool','12916','Merseyside');
INSERT INTO adresse VALUES (9,'91 allée dorian','Strasbourg','47146','Alsace');
INSERT INTO adresse VALUES (14,'58 boulevard anthony','Paris','08942','Ile-de-France');
INSERT INTO adresse VALUES (10,'18 avenue leonard devinci','La Defense','92060','Ile de France');
INSERT INTO adresse VALUES (16,'78 sentier de lépine','Stuttgart','89898','Bade-Wurtemberg');
INSERT INTO adresse VALUES (17,'11 boulevard Malsherbe','Madrid','51614','Madrid');
INSERT INTO adresse VALUES (18,'30 avenue des champs elysées','Nairobie','51628','Nairobie');
INSERT INTO adresse VALUES (19,'92 rue tongra','Tokyo','13853','Kanto');
INSERT INTO adresse VALUES (20,'17 carrefour de la feuille','Genève','89910','Genève');
INSERT INTO adresse VALUES (21,'49 Boulevard Gouvion','Paris','75017','Ile de France');

#ID_client_particulier nom_client_particulier prenom_client_particulier date_adhesion_programme  
#courriel_particulier telephone_particulier numero_programme ID_adresse_client_particulier
INSERT INTO client_particulier VALUES ('cp_1','Morlat','Alexandre','2022-04-24','alexmorlat@gmail.com','0601720276',4,1);
INSERT INTO client_particulier VALUES ('cp_11','Morlat','Julien','2022-04-24','julienmorlat@gmail.com','0601720276',4,1);
INSERT INTO client_particulier VALUES ('cp_2','Bolzinger','Louis','2022-04-24','louis.bolzinger@outlook.com','0652967213',4,2);
INSERT INTO client_particulier VALUES ('cp_3','Autan','Camille','2012-10-05','CamilleAuhasard@gmail.com','0691301662',2,3);
INSERT INTO client_particulier VALUES ('cp_4','Decastelneau','Quentin','2020-12-06','QuentinDecastel@gmail.com','0630235253',null,4);
INSERT INTO client_particulier VALUES ('cp_5','Roulleaux','Clément','2010-05-26','ClémentRoulleaux@gmail.com','0664912967',3,5);
INSERT INTO client_particulier VALUES ('cp_6','Architas','Clémence','2011-11-23','ClémenceArtichaud@gmail.com','0685104115',4,6);
INSERT INTO client_particulier VALUES ('cp_7','Dumangin','Stanislas','2015-02-19','StanislasDubois@gmail.com','0624340925',4,6);
INSERT INTO client_particulier VALUES ('cp_8','Lelarge','Thomas','2019-02-18','ThomasLegrand@gmail.com','0619637880',3,7);
INSERT INTO client_particulier VALUES ('cp_9','Finot','Théodore','2011-12-09','ThéoFinot@gmail.com','0649082877',2,7);
INSERT INTO client_particulier VALUES ('cp_10','Vilmer','Yoan','2019-08-24','YoanViande@gmail.com','0666510043',null,7);

#ID_client_entreprise nom_client_entreprise remise_client_entreprise courriel_entreprise 
#telephone_entreprise nom_contact_entreprise ID_adresse_client_entreprise 
INSERT INTO client_entreprise VALUES ('ce_1','Decathlon',0.08,'Decathlon@gmail.com','0625698641','Charles_Levrier',16);
INSERT INTO client_entreprise VALUES ('ce_2','GoSport',0.04,'GoSport@gmail.com','0684612457','Henri_Course',19);
INSERT INTO client_entreprise VALUES ('ce_3','InterSport',0.04,'InterSport@gmail.com','0662404458','Quentin_Wolff',17);
INSERT INTO client_entreprise VALUES ('ce_4','Les_pros_du_vélo',0.05,'Les_pros_du_vélo@gmail.com','0600792418','Lance_Armstrong',18);
INSERT INTO client_entreprise VALUES ('ce_5','Google',0.06,'Google@gmail.com','0677690487','Forest_Gump',20);
INSERT INTO client_entreprise VALUES ('ce_6','Le_tour_de_France',0.06,'Le_tour_de_France@gmail.com','0643475269','Buzz_Aldrin',null);

#siret_fournisseur nom_fournisseur qualite_fournisseur nom_contact_fournisseur ID_adresse_fournisseur int,
INSERT INTO fournisseur VALUES('F01','Matos Velo','excellent','Jean Marc Bidot',10);
INSERT INTO fournisseur VALUES('F02','P2RP','bon','Ziang Han',8);
INSERT INTO fournisseur VALUES('F03','BikeMax','moyen','Ziang Lee',11);
INSERT INTO fournisseur VALUES('F04','Mon Velo','mauvais','Eric Judor',9);
INSERT INTO fournisseur VALUES('F07','Montain Bike','bon','Andre Henry',21);

#numero_piece_catalogue siret_fournisseur 
INSERT INTO catalogue VALUES ('C01','F01');
INSERT INTO catalogue VALUES ('C01','F02');
INSERT INTO catalogue VALUES ('C02','F01');
INSERT INTO catalogue VALUES ('C02','F03');
INSERT INTO catalogue VALUES ('C02','F04');
INSERT INTO catalogue VALUES ('C32','F03');
INSERT INTO catalogue VALUES ('C34','F03');
INSERT INTO catalogue VALUES ('C25','F02');
INSERT INTO catalogue VALUES ('C26','F02');
INSERT INTO catalogue VALUES ('C43','F02');
INSERT INTO catalogue VALUES ('C43','F07');
INSERT INTO catalogue VALUES ('RR12','F07');
INSERT INTO catalogue VALUES ('G7','F07');
INSERT INTO catalogue VALUES ('G9','F01');
INSERT INTO catalogue VALUES ('G12','F01');
INSERT INTO catalogue VALUES ('S37','F03');
INSERT INTO catalogue VALUES ('S73','F03');
INSERT INTO catalogue VALUES ('DR76','F03');
INSERT INTO catalogue VALUES ('S35','F03');
INSERT INTO catalogue VALUES ('RR46','F04');
INSERT INTO catalogue VALUES ('RR47','F04');
INSERT INTO catalogue VALUES ('RV48','F01');
INSERT INTO catalogue VALUES ('RV12','F01');
INSERT INTO catalogue VALUES ('RV19','F01');
INSERT INTO catalogue VALUES ('RV1','F01');
INSERT INTO catalogue VALUES ('RV11','F01');
INSERT INTO catalogue VALUES ('RR46','F01');
INSERT INTO catalogue VALUES ('RR47','F01');
INSERT INTO catalogue VALUES ('RR32','F01');
INSERT INTO catalogue VALUES ('RR18','F01');
INSERT INTO catalogue VALUES ('RR2','F01');
INSERT INTO catalogue VALUES ('RR12','F01');
INSERT INTO catalogue VALUES ('R02','F01');
INSERT INTO catalogue VALUES ('R09','F01');
INSERT INTO catalogue VALUES ('R10','F01');
INSERT INTO catalogue VALUES ('O2','F01');
INSERT INTO catalogue VALUES ('O4','F01');
INSERT INTO catalogue VALUES ('S01','F01');
INSERT INTO catalogue VALUES ('S05','F01');
INSERT INTO catalogue VALUES ('S74','F01');
INSERT INTO catalogue VALUES ('S73','F01');
INSERT INTO catalogue VALUES ('P12','F01');
INSERT INTO catalogue VALUES ('P34','F01');



#numero_commande date_commande date_livraison_commande ID_adresse_commande ID_client_entreprise ID_client_particulier 
INSERT INTO commande VALUES (1,'2017-06-12','2017-07-01',14,'ce_1',null,'Livré');
INSERT INTO commande VALUES (2,'2011-07-03','2011-07-31',1,'ce_2',null,'Livré');
INSERT INTO commande VALUES (3,'2010-06-07','2010-06-14',20,'ce_3',null,'Livré');
INSERT INTO commande VALUES (4,'2015-08-04','2015-08-09',12,'ce_4',null,'Livré');
INSERT INTO commande VALUES (5,'2010-04-02','2010-04-25',8,'ce_5',null,'Livré');
INSERT INTO commande VALUES (55,'2010-04-02','2010-04-25',8,'ce_5',null,'Livré');
INSERT INTO commande VALUES (6,'2013-04-21','2013-05-14',8,null,'cp_3','Livré');
INSERT INTO commande VALUES (7,'2016-07-27','2016-08-09',20,'ce_6',null,'Livré');
INSERT INTO commande VALUES (19,'2011-06-03','2011-06-25',2,null,'cp_2','Livré');
INSERT INTO commande VALUES (17,'2020-09-19','2020-10-04',8,null,'cp_1','Livré');
INSERT INTO commande VALUES (18,'2017-11-18','2017-12-05',19,null,'cp_4','Livré');
INSERT INTO commande VALUES (20,'2022-05-10','2022-05-20',19,null,'cp_4','Livré');

DROP TRIGGER IF EXISTS Livraison_statut ;
CREATE  TRIGGER   Livraison_statut BEFORE INSERT ON commande FOR EACH ROW SET NEW.statut =  "En Livraison";
INSERT INTO commande VALUES (69,'2022-05-10','2022-05-20',19,null,'cp_4',null);

       
#Numero Piece / Numero Commande / Quantite
INSERT INTO liste_piece_commande VALUES ('R10',1,1);
INSERT INTO liste_piece_commande VALUES ('RR32',1,1);
INSERT INTO liste_piece_commande VALUES ('DV15',1,2);
INSERT INTO liste_piece_commande VALUES ('RV1',2,2);
INSERT INTO liste_piece_commande VALUES ('F9',3,1);
INSERT INTO liste_piece_commande VALUES ('R10',3,1);
INSERT INTO liste_piece_commande VALUES ('C15',5,6);
INSERT INTO liste_piece_commande VALUES ('R09',19,4);
INSERT INTO liste_piece_commande VALUES ('DR87',17,10);
INSERT INTO liste_piece_commande VALUES ('DR56',17,10);
INSERT INTO liste_piece_commande VALUES ('O4',18,15);
INSERT INTO liste_piece_commande VALUES ('C34',55,1);
INSERT INTO liste_piece_commande VALUES ('C34',1,10);


#numero_commande_velo, numero_velo_commande, quantite_velo_commande
INSERT INTO liste_velo_commande VALUES (4,101,3);
INSERT INTO liste_velo_commande VALUES (5,102,2);
INSERT INTO liste_velo_commande VALUES (18,103,6);
INSERT INTO liste_velo_commande VALUES (18,104,5);
INSERT INTO liste_velo_commande VALUES (6,105,6);
INSERT INTO liste_velo_commande VALUES (19,109,1);
INSERT INTO liste_velo_commande VALUES (17,114,3);
INSERT INTO liste_velo_commande VALUES (17,115,3);



#Module Statiqtique
-- 1. Quantités vendues de chaque item qui se trouve dans l’inventaire de VéloMax.
SELECT numero_piece_catalogue,description_piece,sum(quantite_piece_commande)
FROM modele_piece JOIN liste_piece_commande 
ON modele_piece.numero_piece_catalogue = liste_piece_commande.numero_piece_catalogue_commande 
GROUP BY numero_piece_catalogue;
-- 2. Produire la liste des membres pour chaque programme d’adhésion.
SELECT * FROM client_particulier NATURAL JOIN programme_fidelio ;
-- 3. Affichez également la date d’expiration des adhésions
SELECT numero_programme,nom_client_particulier,prenom_client_particulier,date_adhesion_programme FROM client_particulier NATURAL JOIN programme_Fidelio;
-- 4. Retrouvez-le (ou les) meilleur client en fonction des quantités vendues en nombre de
-- pièces vendues ou en cumul en euros

-- 5. Faîtes une analyse des commandes : moyenne des montants des commandes, moyenne du
-- nombre de pièces ou de vélos par commande.


SELECT * 
FROM commande 
JOIN client_entreprise USING(ID_client_entreprise)
JOIN liste_piece_commande WHERE commande.numero_commande = liste_piece_commande.numero_commande_piece ;


SELECT *
FROM commande 
JOIN client_entreprise USING(ID_client_entreprise)
JOIN liste_piece_commande ON commande.numero_commande = liste_piece_commande.numero_commande_piece
JOIN modele_piece ON modele_piece.numero_piece_catalogue = liste_piece_commande.numero_piece_catalogue_commande;




#Pour les pièces uniquement
#Client ayant commandé le plus de pièces
#A)Client Entreprise
SELECT ID_client_entreprise,sum(quantite_piece_commande) 
FROM commande 
JOIN client_entreprise USING(ID_client_entreprise)
JOIN liste_piece_commande WHERE commande.numero_commande = liste_piece_commande.numero_commande_piece
GROUP BY ID_client_entreprise ORDER BY sum(quantite_piece_commande) DESC LIMIT 1;
#B)Client Particulier
SELECT ID_client_particulier,sum(quantite_piece_commande) 
FROM commande 
JOIN client_particulier USING(ID_client_particulier)
JOIN liste_piece_commande WHERE commande.numero_commande = liste_piece_commande.numero_commande_piece
GROUP BY ID_client_entreprise ORDER BY sum(quantite_piece_commande) DESC LIMIT 1;

#Prix total de chaque ligne de commande de pièce
SELECT numero_commande_piece,quantite_piece_commande,prix_piece,(quantite_piece_commande*prix_piece)as prixtotligne
FROM liste_piece_commande JOIN modele_piece 
ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue;

#Prix total de chaque ligne de commande de pièce Reduction Particulier
#La bonne
SELECT nom_client_particulier, prenom_client_particulier,((quantite_piece_commande*prix_piece*(1-programme_fidelio.rabais_programme)))as prixtotavecreduc
FROM liste_piece_commande JOIN modele_piece 
ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
JOIN commande ON liste_piece_commande.numero_commande_piece = commande.numero_commande
JOIN client_particulier ON commande.ID_client_particulier=client_particulier.ID_client_particulier
JOIN programme_fidelio ON client_particulier.numero_programme=programme_fidelio.numero_programme
GROUP BY nom_client_particulier ;

#Prix total de chaque ligne de commande de pièce Reduction Entreprise
#premiere
SELECT numero_commande_piece,quantite_piece_commande,prix_piece,(quantite_piece_commande*prix_piece)as prixtotligne,
(quantite_piece_commande*prix_piece*(1-remise_client_entreprise)) as prixavecremise
FROM liste_piece_commande JOIN modele_piece 
ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
JOIN commande ON liste_piece_commande.numero_commande_piece = commande.numero_commande
JOIN client_entreprise ON client_entreprise.ID_client_entreprise = commande.ID_client_entreprise;
#La bonne
SELECT nom_client_entreprise,
sum((quantite_piece_commande*prix_piece*(1-remise_client_entreprise))) as prixavecremise
FROM liste_piece_commande JOIN modele_piece 
ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
JOIN commande ON liste_piece_commande.numero_commande_piece = commande.numero_commande
JOIN client_entreprise ON client_entreprise.ID_client_entreprise = commande.ID_client_entreprise
GROUP BY nom_client_entreprise ORDER BY prixavecremise ;

#Prix Total de chaque commande de pièce
SELECT numero_commande_piece,SUM((quantite_piece_commande*prix_piece))as prixtotcommande
FROM liste_piece_commande JOIN modele_piece 
ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
GROUP BY numero_commande_piece;
#Prix Total dépensé pour chaque client 
#A) Client Entreprise
SELECT ID_client_entreprise,sum(prixtotcommande)as somme_total
FROM commande 
JOIN client_entreprise USING(ID_client_entreprise)
JOIN(SELECT numero_commande_piece,SUM((quantite_piece_commande*prix_piece))as prixtotcommande
	FROM liste_piece_commande JOIN modele_piece 
	ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
	GROUP BY numero_commande_piece) my_req
ON commande.numero_commande = my_req.numero_commande_piece
GROUP BY ID_client_entreprise
ORDER BY somme_total DESC;
#B) Client Particulier
SELECT ID_client_particulier,sum(prixtotcommande)as somme_total
FROM commande 
JOIN client_particulier USING(ID_client_particulier)
JOIN(SELECT numero_commande_piece,SUM((quantite_piece_commande*prix_piece))as prixtotcommande
	FROM liste_piece_commande JOIN modele_piece 
	ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
	GROUP BY numero_commande_piece) my_req
ON commande.numero_commande = my_req.numero_commande_piece
GROUP BY ID_client_particulier
ORDER BY somme_total DESC;

#C) Client Entreprise et particulier 
SELECT ID_client_entreprise,ID_client_particulier,prixtotcommande 
FROM commande 
LEFT JOIN client_entreprise USING(ID_client_entreprise)
LEFT JOIN client_particulier USING(ID_client_particulier)
JOIN(SELECT numero_commande_piece,SUM((quantite_piece_commande*prix_piece))as prixtotcommande
	FROM liste_piece_commande JOIN modele_piece 
	ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
	GROUP BY numero_commande_piece) my_req
ON commande.numero_commande = my_req.numero_commande_piece;



#Moyenne TOTAL de chaque commandes : Sans réduction
SELECT numero_commande,sum(prix_commande_piece) as prix_commande FROM
((SELECT numero_commande,prix_commande_piece
FROM commande
JOIN(SELECT numero_commande_piece,SUM((quantite_piece_commande*prix_piece))as prix_commande_piece
	FROM liste_piece_commande JOIN modele_piece 
	ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
	GROUP BY numero_commande_piece) my_req
ON commande.numero_commande = my_req.numero_commande_piece)
UNION ALL
(SELECT numero_commande,prix_commande_velo
FROM commande
JOIN(SELECT numero_commande_velo,sum(quantite_velo_commande*prix_velo) as prix_commande_velo
	FROM liste_velo_commande JOIN modele_velo 
	ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo
    GROUP BY numero_commande_velo) my_req
ON commande.numero_commande = my_req.numero_commande_velo)) req2
GROUP BY numero_commande;

#MOYENNE DU PRIX DE CHAQUE COMMANDE : WRITE ONE !!!!!!!
SELECT avg(prix_commande) as prix_moyen FROM (SELECT numero_commande,sum(prix_commande_piece) as prix_commande FROM
((SELECT numero_commande,prix_commande_piece FROM commande JOIN(SELECT numero_commande_piece,SUM((quantite_piece_commande*prix_piece))as prix_commande_piece FROM liste_piece_commande JOIN modele_piece 
ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue GROUP BY numero_commande_piece) my_req
ON commande.numero_commande = my_req.numero_commande_piece) UNION ALL
(SELECT numero_commande,prix_commande_velo FROM commande JOIN(SELECT numero_commande_velo,sum(quantite_velo_commande*prix_velo) as prix_commande_velo FROM liste_velo_commande JOIN modele_velo  ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo GROUP BY numero_commande_velo) my_req ON commande.numero_commande = my_req.numero_commande_velo)) req2
GROUP BY numero_commande)alias ;


#MONTANT TOTAL DEPENSE PAR CLIENT Particulier
SELECT nom_client_particulier,sum(prix_commande_piece) FROM client_particulier NATURAL JOIN Commande NATURAL JOIN (SELECT numero_commande,prix_commande_piece FROM
((SELECT numero_commande,prix_commande_piece
FROM commande
JOIN(SELECT numero_commande_piece,SUM((quantite_piece_commande*prix_piece))as prix_commande_piece
	FROM liste_piece_commande JOIN modele_piece 
	ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
	GROUP BY numero_commande_piece) my_req
ON commande.numero_commande = my_req.numero_commande_piece)
UNION ALL
(SELECT numero_commande,prix_commande_velo
FROM commande
JOIN(SELECT numero_commande_velo,sum(quantite_velo_commande*prix_velo) as prix_commande_velo
	FROM liste_velo_commande JOIN modele_velo 
	ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo
    GROUP BY numero_commande_velo) my_req
ON commande.numero_commande = my_req.numero_commande_velo)) req2)req3 group by nom_client_particulier;



#TEST1 Pour chaque commande Velo : MOYENNE Nombre Velo
		SELECT avg(nombre_velo) FROM(
        SELECT sum(quantite_velo_commande) as nombre_velo
			FROM liste_velo_commande JOIN modele_velo 
			ON liste_velo_commande.numero_velo_commande = modele_velo.numero_velo
			GROUP BY numero_commande_velo) nb;
	
#TEST1 Pour chaque commande Piece : MOYENNE Nombre Piece
		SELECT avg(nombre_piece) FROM(
        SELECT sum(quantite_piece_commande) as nombre_piece
			FROM liste_piece_commande JOIN modele_piece 
			ON liste_piece_commande.numero_piece_catalogue_commande = modele_piece.numero_piece_catalogue
			GROUP BY numero_commande_piece) nb;


#Menu_client
INSERT INTO client_particulier VALUES ('cp_25','Morlat','Alexandre','2022-04-24','alexmorlat@gmail.com','0601720276',null,1);
SELECT * FROM client_particulier WHERE ID_client_particulier='cp_25';
DELETE FROM client_particulier WHERE ID_client_particulier='cp_25';

SELECT * FROM commande;
SELECT * FROM liste_velo_commande;
SELECT * FROM liste_piece_commande;
SELECT * FROM liste_assemblage NATURAL JOIN modele_velo NATURAL JOIN modele_piece ;



#GESTION DES STOCKS
#Par piece
SELECT numero_piece_catalogue,sum(stock_piece) FROM modele_piece GROUP BY numero_piece_catalogue ;
#Par founisseur
SELECT nom_fournisseur,sum(stock_piece) 
FROM Fournisseur NATURAL JOIN Catalogue NATURAL JOIN modele_piece
GROUP BY siret_fournisseur;
#Par Velo
SELECT nom_velo,min(stock_piece) 
FROM modele_velo NATURAL JOIN liste_assemblage NATURAL JOIN modele_piece
GROUP BY numero_velo;
#Par catégorie de velo
SELECT ligne_produit_velo,min(stock_piece) 
FROM modele_velo NATURAL JOIN liste_assemblage NATURAL JOIN modele_piece
GROUP BY ligne_produit_velo;

# SELECTION STOCK FAIBLE
SELECT numero_piece_catalogue,stock_piece FROM modele_piece WHERE stock_piece <=5 ;
