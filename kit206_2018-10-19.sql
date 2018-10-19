# ************************************************************
# Sequel Pro SQL dump
# Version 4541
#
# http://www.sequelpro.com/
# https://github.com/sequelpro/sequelpro
#
# Host: alacritas.cis.utas.edu.au (MySQL 5.5.60-MariaDB)
# Database: kit206
# Generation Time: 2018-10-19 04:06:25 +0000
# ************************************************************


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


# Dump of table class
# ------------------------------------------------------------

DROP TABLE IF EXISTS `class`;

CREATE TABLE `class` (
  `unit_code` varchar(6) NOT NULL,
  `campus` enum('Hobart','Launceston') NOT NULL,
  `day` enum('Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sunday') NOT NULL,
  `start` time NOT NULL,
  `end` time NOT NULL,
  `type` enum('Lecture','Tutorial','Practical','Workshop') DEFAULT NULL,
  `room` varchar(20) NOT NULL,
  `staff` int(11) NOT NULL,
  PRIMARY KEY (`unit_code`,`day`,`start`,`campus`),
  KEY `fk_class_unit1_idx` (`unit_code`),
  KEY `fk_class_staff1_idx` (`staff`),
  CONSTRAINT `fk_class_staff1` FOREIGN KEY (`staff`) REFERENCES `staff` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fk_class_unit1` FOREIGN KEY (`unit_code`) REFERENCES `unit` (`code`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

LOCK TABLES `class` WRITE;
/*!40000 ALTER TABLE `class` DISABLE KEYS */;

INSERT INTO `class` (`unit_code`, `campus`, `day`, `start`, `end`, `type`, `room`, `staff`)
VALUES
	('KIT102','Hobart','Friday','14:00:00','16:00:00','Tutorial','C360',123465),
	('KIT104','Launceston','Wednesday','13:00:00','14:00:00','Tutorial','V191',123461),
	('KIT104','Hobart','Thursday','15:00:00','16:00:00','Tutorial','C359',123463),
	('KIT106','Hobart','Monday','12:00:00','14:00:00','Tutorial','C300',123461),
	('KIT106','Hobart','Wednesday','12:00:00','13:00:00','Tutorial','C311',123464),
	('KIT106','Hobart','Thursday','15:00:00','17:00:00','Tutorial','C269',123460),
	('KIT107','Hobart','Thursday','10:00:00','12:00:00','Tutorial','C326',123464),
	('KIT201','Hobart','Friday','12:00:00','14:00:00','Tutorial','C375',123464),
	('KIT206','Hobart','Wednesday','11:00:00','12:00:00','Tutorial','C254',123460),
	('KIT302','Launceston','Monday','12:00:00','14:00:00','Tutorial','V198',123460),
	('KIT302','Hobart','Wednesday','09:00:00','10:00:00','Tutorial','C238',123465),
	('KIT306','Launceston','Wednesday','09:00:00','11:00:00','Tutorial','V188',123460),
	('KIT306','Hobart','Thursday','13:00:00','15:00:00','Tutorial','C353',123460),
	('KIT501','Hobart','Monday','12:00:00','14:00:00','Lecture','C301',123460),
	('KIT501','Hobart','Tuesday','15:00:00','16:00:00','Lecture','C257',123462),
	('KIT501','Hobart','Wednesday','09:00:00','10:00:00','Tutorial','C314',123461),
	('KIT501','Hobart','Thursday','12:00:00','14:00:00','Tutorial','C300',123464),
	('KIT501','Launceston','Thursday','12:00:00','13:00:00','Tutorial','V197',123462),
	('KIT501','Hobart','Friday','12:00:00','14:00:00','Tutorial','C251',123465);

/*!40000 ALTER TABLE `class` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table consultation
# ------------------------------------------------------------

DROP TABLE IF EXISTS `consultation`;

CREATE TABLE `consultation` (
  `staff_id` int(11) NOT NULL,
  `day` enum('Monday','Tuesday','Wednesday','Thursday','Friday','Saturday','Sunday') NOT NULL,
  `start` time NOT NULL,
  `end` time NOT NULL,
  PRIMARY KEY (`staff_id`,`day`,`start`),
  KEY `fk_consultation_staff_idx` (`staff_id`),
  CONSTRAINT `fk_consultation_staff` FOREIGN KEY (`staff_id`) REFERENCES `staff` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

LOCK TABLES `consultation` WRITE;
/*!40000 ALTER TABLE `consultation` DISABLE KEYS */;

INSERT INTO `consultation` (`staff_id`, `day`, `start`, `end`)
VALUES
	(123460,'Monday','15:00:00','17:00:00'),
	(123460,'Tuesday','12:00:00','14:00:00'),
	(123461,'Tuesday','14:00:00','15:00:00'),
	(123461,'Thursday','14:00:00','15:00:00'),
	(123462,'Wednesday','10:00:00','11:00:00'),
	(123462,'Thursday','10:00:00','11:00:00'),
	(123463,'Wednesday','09:00:00','11:00:00'),
	(123463,'Thursday','09:00:00','11:00:00'),
	(123464,'Tuesday','11:00:00','12:00:00'),
	(123464,'Wednesday','15:00:00','17:00:00'),
	(123464,'Thursday','14:00:00','15:00:00'),
	(123465,'Monday','15:00:00','17:00:00'),
	(123465,'Wednesday','12:00:00','14:00:00'),
	(123465,'Friday','13:00:00','15:00:00');

/*!40000 ALTER TABLE `consultation` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table staff
# ------------------------------------------------------------

DROP TABLE IF EXISTS `staff`;

CREATE TABLE `staff` (
  `id` int(11) NOT NULL,
  `given_name` varchar(20) NOT NULL,
  `family_name` varchar(20) NOT NULL,
  `title` varchar(10) DEFAULT NULL,
  `campus` enum('Hobart','Launceston') NOT NULL,
  `phone` varchar(15) DEFAULT NULL,
  `room` varchar(20) DEFAULT NULL,
  `email` varchar(50) DEFAULT NULL,
  `photo` varchar(512) DEFAULT NULL,
  `category` enum('Academic','Technical','Admin','Casual') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;

INSERT INTO `staff` (`id`, `given_name`, `family_name`, `title`, `campus`, `phone`, `room`, `email`, `photo`, `category`)
VALUES
	(123460,'John','Beckett','Dr','Hobart','(03) 6226 0000','C489','no.such.email@example.org','http://www.hyndburnleisure.co.uk/wp-content/uploads/2013/08/male_face2.png','Academic'),
	(123461,'Gemma','Stanford','Dr','Launceston','(03) 6226 0000','V124','no.such.email@example.org','http://rotarymeansbusiness.com/wp-content/uploads/avatar-female.png','Academic'),
	(123462,'Edward','Vogt','Dr','Hobart','(03) 6226 0000','C467','no.such.email@example.org','http://www.hyndburnleisure.co.uk/wp-content/uploads/2013/08/male_face2.png','Academic'),
	(123463,'Mary','Lister','Dr','Hobart','(03) 6226 0000','C412','no.such.email@example.org','http://rotarymeansbusiness.com/wp-content/uploads/avatar-female.png','Academic'),
	(123464,'Caitlyn','Pemulwuy','Ms','Hobart','(03) 6226 0000','C432','no.such.email@example.org','http://rotarymeansbusiness.com/wp-content/uploads/avatar-female.png','Casual'),
	(123465,'Indiana','Whitta','Mr','Hobart','(03) 6226 0000','C448','no.such.email@example.org','http://www.hyndburnleisure.co.uk/wp-content/uploads/2013/08/male_face2.png','Casual'),
	(123466,'Alexandra','Halloran','Ms','Hobart','(03) 6226 0000','C435','no.such.email@example.org','http://rotarymeansbusiness.com/wp-content/uploads/avatar-female.png','Admin'),
	(123467,'Charlie','Byrnes','Mr','Hobart','(03) 6226 0000','C439','no.such.email@example.org','http://www.hyndburnleisure.co.uk/wp-content/uploads/2013/08/male_face2.png','Admin'),
	(123468,'Holly','Pell','Ms','Launceston','(03) 6226 0000','V177','no.such.email@example.org','http://rotarymeansbusiness.com/wp-content/uploads/avatar-female.png','Admin'),
	(123469,'Ben','Bramston','Mr','Hobart','(03) 6226 0000','C449','no.such.email@example.org','http://www.hyndburnleisure.co.uk/wp-content/uploads/2013/08/male_face2.png','Technical');

/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;


# Dump of table unit
# ------------------------------------------------------------

DROP TABLE IF EXISTS `unit`;

CREATE TABLE `unit` (
  `code` varchar(6) NOT NULL,
  `title` varchar(50) NOT NULL,
  `coordinator` int(11) NOT NULL,
  PRIMARY KEY (`code`),
  KEY `fk_unit_staff1_idx` (`coordinator`),
  CONSTRAINT `fk_unit_staff1` FOREIGN KEY (`coordinator`) REFERENCES `staff` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

LOCK TABLES `unit` WRITE;
/*!40000 ALTER TABLE `unit` DISABLE KEYS */;

INSERT INTO `unit` (`code`, `title`, `coordinator`)
VALUES
	('KIT102','Data Organisation and Visualisation',123461),
	('KIT103','Computational Science',123463),
	('KIT104','ICT Architecture & Operating Systems',123460),
	('KIT106','ICT Impact & Emerging Technology',123460),
	('KIT107','Programming',123463),
	('KIT201','Data Networks & Security',123460),
	('KIT204','ICT Solutions Analysis for Business',123462),
	('KIT206','Software Design & Development',123462),
	('KIT302','ICT Project B',123462),
	('KIT306','Data Analytics',123462),
	('KIT501','ICT Systems Administration Fundamentals',123462),
	('KIT506','Software Application Design and Implementation',123461);

/*!40000 ALTER TABLE `unit` ENABLE KEYS */;
UNLOCK TABLES;



/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
