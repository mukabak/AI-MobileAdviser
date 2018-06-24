-- phpMyAdmin SQL Dump
-- version 4.5.4.1deb2ubuntu2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Aug 17, 2017 at 01:54 AM
-- Server version: 5.7.19-0ubuntu0.16.04.1
-- PHP Version: 7.0.22-0ubuntu0.16.04.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `mobileadvisordb`
--

-- --------------------------------------------------------

--
-- Table structure for table `companies`
--

CREATE TABLE `companies` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `image` mediumtext NOT NULL,
  `description` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `companies`
--

INSERT INTO `companies` (`id`, `name`, `image`, `description`) VALUES
(1, 'Apple', 'DSAD', 'DASDAS'),
(2, 'HTC', '..', '..'),
(3, 'LG', '..', '..'),
(4, 'GOOGLE', '..', '..'),
(5, 'Sony', '..', '..'),
(6, 'Motorola', '..', '..'),
(7, 'Samsung', '..', '..'),
(8, 'OnePlus', '..', '..'),
(9, 'Microsoft', '..', '..'),
(10, 'Blackberry', '..', '..'),
(11, 'Huawei', '..', '..'),
(12, 'Lenovo', '..', '..');

-- --------------------------------------------------------

--
-- Table structure for table `questions`
--

CREATE TABLE `questions` (
  `id` int(11) NOT NULL,
  `question_title` text NOT NULL,
  `question_answer` text NOT NULL,
  `query` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `questions`
--

INSERT INTO `questions` (`id`, `question_title`, `question_answer`, `query`) VALUES
(1, 'What operating system you are familiar with the most?', '1-android\r\n2-ios\r\n3-windowsphone\r\n4-blackberry', 'os = [ANSWER]'),
(2, 'Do you feel better holding a metal or plastic chassis?', '1-Metal\r\n2-Plastic\r\n3-I Don\'t mind\r\n', 'chassis = [ANSWER]'),
(3, 'What screen size do you prefer?', '· Small (<4 Inches)\n· Medium(4~6)\n· Large(>6)\n1-10 Float Number\n[NUMBER] or less || [NUMBER] or bigger\n\nor less [OPERATOR] = <\nor bigger [OPERATOR] = >', 'screen_size [OPERATOR]= [ANSWER]'),
(4, 'What method of interaction do you prefer?', '· Touch screen\r\n· Physical Keyboard', 'physical_keyboard = [ANSWER] AND touchscreen = [REV_ANSWER]'),
(5, 'How much are you willing to spend on a mobile phone?\r\n', 'A number ', 'price <= [ANSWER]'),
(6, 'What storage capacity do you think you need?', '· 4GB\r\n· 8GB\r\n· 16GB\r\n· 32GB\r\n· 64GB\r\n· 128GB\r\n· 256GB', 'size >= [ANSWER]'),
(7, '7. What Storage do you prefer?\r\n', '· Internal storage only\r\n· Internal and external storage\r\n\r\n\r\nOnly Interal storage \r\nInternal and external storage', 'internal_storage = [ANSWER] OR external_storage = [ANSWER2]\r\n'),
(8, 'Do you want battery removable phone?', 'YES OR NO', 'battery_removable = [ANSWER]'),
(9, 'Do you do a lot gaming? ', 'YES OR NO', 'gpu = 1 AND cores >= 2'),
(10, 'Do you do phone photography?', 'YES or NO', 'back_camera = 1 AND back_camera_res >= 8 AND autofocus = 1 AND flash = 1 OR hdr = 1 or image_stablization = 1');

-- --------------------------------------------------------

--
-- Table structure for table `smartphones`
--

CREATE TABLE `smartphones` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `company_id` int(11) NOT NULL,
  `desc` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `smartphones`
--

INSERT INTO `smartphones` (`id`, `name`, `company_id`, `desc`) VALUES
(1, 'iPhone 6', 1, 'DASJDJLASJKD'),
(2, 'One M9', 2, ''),
(3, 'G6', 3, '..'),
(4, 'GOOGLE Pixel', 4, '..'),
(5, 'U11', 2, '--'),
(6, 'Sony Xperia XZ Premium', 5, '..'),
(7, 'Motorola Moto Z', 6, '..'),
(8, 'Galaxy S8', 7, '..'),
(9, 'Iphone 7', 1, '..'),
(10, 'OnePlus 5', 8, '..'),
(11, 'Iphone 7 Plus', 1, '..'),
(12, 'Lumia 930', 9, '..'),
(13, 'Curve 9360', 10, '..'),
(14, 'Huawei P10 Lite', 11, '..'),
(15, 'Lenovo A7000 Turbo', 12, '..'),
(16, 'Huawei P9', 11, '..'),
(17, 'Sony Xperia J1 Compact', 5, '..'),
(18, 'Samsung Galaxy J3', 7, '..'),
(19, 'IPhone Se', 1, ''),
(20, 'Sony Xperia Z5 Premium', 5, '..'),
(21, 'HTC U Ultra ', 2, '..'),
(22, 'Samsung Galaxy Folder 2', 7, '..'),
(23, 'Huawei Mate 9 Porsche Design ', 11, '..'),
(24, 'LG X Venture', 3, '..'),
(25, 'Lenovo ZUK Edge', 12, '..'),
(26, 'Motorola Moto X Style ', 6, '..'),
(27, 'Sony Xperia C5 Ultra', 5, '..'),
(28, 'Huawei Honor 9', 11, '..'),
(29, 'LG X mach', 3, '..');

-- --------------------------------------------------------

--
-- Table structure for table `smartphones_features`
--

CREATE TABLE `smartphones_features` (
  `id` int(11) NOT NULL,
  `smartphone_id` int(11) NOT NULL,
  `price` float NOT NULL,
  `chassis` tinyint(4) NOT NULL DEFAULT '1' COMMENT '1-Plastic 2-Metal',
  `os` tinyint(4) NOT NULL COMMENT '1-android 2-ios 3-windowsphone 4-blackberry',
  `accelerometer` tinyint(1) NOT NULL DEFAULT '1',
  `gyroscope` tinyint(1) NOT NULL DEFAULT '1',
  `digital_compass` tinyint(1) NOT NULL DEFAULT '1',
  `ambient_light_sensor` tinyint(1) NOT NULL DEFAULT '0',
  `proximity_sensor` tinyint(1) NOT NULL DEFAULT '0',
  `thickness` float NOT NULL COMMENT 'device < 8 -11 MM == Slim else are not slim',
  `weight` float NOT NULL COMMENT '<140 is lightweight else is heavy',
  `color` varchar(20) NOT NULL,
  `physical_keyboard` tinyint(1) NOT NULL DEFAULT '0',
  `touchscreen` tinyint(1) NOT NULL DEFAULT '1' COMMENT '1-capacitive(gestures_friendly) 2-resistive',
  `ruggedness` tinyint(1) NOT NULL DEFAULT '0' COMMENT 'waterproof/dust proof',
  `screen_size` float NOT NULL COMMENT 'by Inch',
  `resolution_h` smallint(6) NOT NULL COMMENT 'Ex 1080 ',
  `resolution_w` smallint(6) NOT NULL COMMENT 'Ex 1920',
  `pixel_density` tinyint(4) NOT NULL COMMENT 'pixel per inch',
  `screen_tech` tinyint(4) NOT NULL COMMENT '1-amoled 2-tft ',
  `protection_glass` tinyint(4) NOT NULL DEFAULT '2' COMMENT '1-gorila 2-other',
  `cores` tinyint(4) NOT NULL DEFAULT '1' COMMENT 'Number of processors cores',
  `clock_speed` float NOT NULL COMMENT 'by Mhz ',
  `gpu` tinyint(1) NOT NULL DEFAULT '0',
  `ram` smallint(6) NOT NULL COMMENT 'megabyte',
  `internal_storage` tinyint(1) NOT NULL DEFAULT '1',
  `external_storage` tinyint(1) NOT NULL DEFAULT '0',
  `internal_storage_size` int(11) NOT NULL COMMENT 'megabyte',
  `battery_capacity` mediumint(9) NOT NULL COMMENT 'mili Amper per Hour',
  `battery_removable` tinyint(1) NOT NULL,
  `battery_type` tinytext NOT NULL COMMENT 'Lithium or Anyother battery types ',
  `wireless_charging` tinyint(1) NOT NULL,
  `wifi` tinyint(1) NOT NULL,
  `bluetooth` tinyint(1) NOT NULL DEFAULT '1' COMMENT '1-old 2-new bluetooth 4',
  `cellular_connectivity` varchar(6) NOT NULL COMMENT '2G 3G 4G LTE GSM CDMA',
  `gps` tinyint(1) NOT NULL COMMENT '1-good no internet required 2-bad requires internet',
  `usb` tinyint(1) NOT NULL DEFAULT '1' COMMENT '1-common 2-usbc 3-apple_new 4-apple_old',
  `dual_simcard` tinyint(1) NOT NULL,
  `front_camera` tinyint(1) NOT NULL DEFAULT '1',
  `back_camera` tinyint(1) NOT NULL DEFAULT '1',
  `back_camera_res` float NOT NULL COMMENT '10 MP ',
  `flash` tinyint(1) NOT NULL,
  `video_record_fps` tinyint(4) NOT NULL COMMENT '120fps',
  `video_record_res` varchar(5) NOT NULL COMMENT '720,1080,4K',
  `zoom_type` tinyint(1) NOT NULL COMMENT '1-Optical 2-Digital 3-Both',
  `image_stablization` tinyint(1) NOT NULL,
  `autofocus` tinyint(1) NOT NULL DEFAULT '1',
  `manual_focus` tinyint(1) NOT NULL DEFAULT '0',
  `hdr` tinyint(1) NOT NULL DEFAULT '0',
  `panorama` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `smartphones_features`
--

INSERT INTO `smartphones_features` (`id`, `smartphone_id`, `price`, `chassis`, `os`, `accelerometer`, `gyroscope`, `digital_compass`, `ambient_light_sensor`, `proximity_sensor`, `thickness`, `weight`, `color`, `physical_keyboard`, `touchscreen`, `ruggedness`, `screen_size`, `resolution_h`, `resolution_w`, `pixel_density`, `screen_tech`, `protection_glass`, `cores`, `clock_speed`, `gpu`, `ram`, `internal_storage`, `external_storage`, `internal_storage_size`, `battery_capacity`, `battery_removable`, `battery_type`, `wireless_charging`, `wifi`, `bluetooth`, `cellular_connectivity`, `gps`, `usb`, `dual_simcard`, `front_camera`, `back_camera`, `back_camera_res`, `flash`, `video_record_fps`, `video_record_res`, `zoom_type`, `image_stablization`, `autofocus`, `manual_focus`, `hdr`, `panorama`) VALUES
(1, 1, 900, 2, 2, 1, 1, 1, 1, 1, 5, 100, 'white', 0, 1, 0, 4.8, 1920, 1080, 30, 2, 2, 2, 1600, 0, 1, 1, 0, 16, 6000, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 3, 0, 1, 1, 8, 1, 120, '1080', 2, 0, 1, 1, 1, 1),
(1, 9, 750, 2, 2, 1, 1, 1, 1, 1, 7.1, 138, 'White', 0, 1, 0, 4.7, 1334, 750, 30, 2, 2, 4, 2370, 1, 2000, 1, 0, 128000, 1960, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 3, 0, 1, 1, 12, 1, 120, '1080', 2, 1, 1, 1, 1, 1),
(1, 11, 900, 2, 2, 1, 1, 1, 1, 1, 7.3, 188, 'Gold', 0, 1, 1, 5.5, 1920, 1080, 30, 2, 2, 4, 2370, 1, 3000, 1, 0, 256000, 2900, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 3, 0, 1, 1, 12, 1, 120, '1080', 3, 1, 1, 1, 1, 1),
(1, 19, 420, 2, 2, 1, 1, 1, 1, 1, 7.6, 113, 'Silver', 0, 1, 0, 4, 1136, 640, 30, 1, 1, 2, 1840, 1, 2000, 1, 0, 64000, 1642, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 1, 0, 1, 1, 12, 1, 60, '1080', 2, 1, 1, 1, 1, 1),
(2, 2, 340, 2, 1, 1, 1, 1, 1, 1, 9.61, 157, 'Silver', 0, 1, 0, 5, 1920, 1080, 30, 2, 1, 8, 1500, 0, 3000, 1, 1, 32000, 2840, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 0, 1, 1, 20, 1, 120, '1080', 2, 1, 1, 1, 1, 1),
(2, 5, 750, 2, 1, 1, 1, 1, 1, 1, 7.9, 169, 'Blue', 0, 1, 1, 5.5, 2560, 1440, 30, 2, 1, 8, 2450, 1, 6000, 1, 1, 128000, 3000, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 12, 1, 120, '1440', 2, 1, 1, 1, 1, 1),
(2, 21, 700, 2, 1, 1, 1, 1, 1, 1, 7.99, 170, 'Blue', 0, 1, 0, 5.7, 2560, 1440, 30, 1, 1, 4, 2150, 1, 4000, 1, 1, 128000, 3000, 0, 'Lithium', 0, 1, 1, 'LTE', 1, 2, 1, 1, 1, 12, 1, 30, '1080', 2, 1, 1, 1, 1, 1),
(3, 3, 600, 2, 1, 1, 1, 1, 1, 1, 7.9, 163, 'Black', 0, 1, 1, 5.7, 2880, 1440, 1, 2, 1, 4, 2350, 1, 4, 1, 0, 32000, 3300, 0, 'Lithium', 0, 1, 2, 'LTE', 2, 2, 1, 1, 1, 13, 1, 60, '2880', 3, 1, 1, 1, 1, 0),
(3, 24, 330, 1, 1, 1, 1, 1, 1, 1, 9.29, 166.5, 'Black', 0, 1, 1, 5.2, 1080, 1920, 30, 1, 1, 8, 1400, 1, 2000, 1, 1, 32000, 4100, 1, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 0, 1, 1, 13, 1, 30, '1080', 2, 0, 1, 1, 1, 1),
(3, 29, 300, 1, 1, 1, 0, 1, 0, 1, 7.9, 139, 'Black', 0, 1, 0, 5.5, 2560, 1440, 30, 1, 2, 6, 1800, 1, 3000, 1, 1, 32000, 3000, 1, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 12, 1, 30, '2160', 2, 0, 1, 1, 1, 1),
(4, 4, 700, 2, 1, 1, 1, 1, 1, 1, 8.6, 143, 'white', 0, 1, 0, 5, 1920, 1080, 30, 1, 1, 4, 2150, 1, 4, 1, 0, 128000, 2770, 0, 'Lithium', 0, 1, 1, '', 1, 2, 0, 1, 1, 12.3, 1, 30, '1080', 2, 0, 1, 1, 1, 0),
(5, 6, 750, 2, 1, 1, 1, 1, 1, 1, 7.9, 191, 'Black', 0, 1, 1, 5.46, 3840, 2160, 30, 2, 1, 8, 2450, 1, 4000, 1, 1, 64000, 3230, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 19, 1, 30, '3840', 2, 1, 1, 1, 1, 1),
(5, 17, 440, 1, 1, 1, 1, 1, 1, 1, 9.2, 124, 'white', 0, 1, 0, 4, 854, 480, 30, 2, 1, 1, 2200, 1, 20000, 1, 1, 16000, 2300, 0, 'Lithium', 0, 1, 1, 'LTE', 1, 2, 0, 1, 1, 5, 1, 30, '4080', 2, 1, 1, 1, 1, 1),
(5, 20, 530, 2, 1, 1, 1, 1, 1, 1, 7.8, 180, 'Black', 0, 1, 1, 5.5, 3840, 2160, 30, 1, 2, 8, 2000, 1, 3000, 1, 1, 32000, 3430, 0, 'Lithium', 0, 1, 1, 'LTE', 1, 2, 1, 1, 1, 23, 1, 30, '1080', 2, 1, 1, 1, 1, 1),
(5, 27, 380, 2, 1, 1, 1, 0, 1, 1, 8.2, 187, 'white', 0, 1, 0, 6, 1080, 1920, 30, 1, 2, 8, 1700, 2, 2000, 1, 1, 32000, 2930, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 13, 1, 30, '1080', 2, 1, 1, 1, 1, 1),
(6, 7, 300, 2, 1, 1, 1, 1, 1, 1, 5.19, 136, 'Black', 0, 1, 0, 5.5, 2560, 1440, 30, 1, 1, 4, 2150, 1, 4000, 1, 1, 64000, 2600, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 13, 1, 30, '1080', 2, 1, 1, 1, 1, 1),
(6, 26, 430, 2, 1, 1, 1, 1, 1, 1, 11.06, 179, 'Black', 0, 1, 1, 5.7, 1440, 2560, 30, 1, 1, 6, 1800, 1, 3, 1, 1, 64000, 3000, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 21, 1, 30, '2160', 2, 1, 1, 1, 1, 1),
(7, 8, 800, 2, 1, 1, 1, 1, 1, 1, 8, 155, 'Silver', 0, 1, 1, 5.8, 2960, 1440, 30, 1, 1, 8, 2350, 1, 4000, 1, 1, 64000, 3000, 0, 'Lithium', 0, 1, 2, '4G', 1, 2, 1, 1, 1, 12, 1, 60, '1080', 2, 1, 1, 1, 1, 1),
(7, 18, 140, 1, 1, 1, 1, 0, 0, 1, 7.9, 138, 'Gold', 0, 1, 0, 5, 1280, 720, 30, 1, 2, 4, 400, 1, 15000, 1, 1, 8000, 2600, 1, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 8, 1, 30, '720', 2, 1, 1, 1, 1, 1),
(7, 22, 240, 2, 1, 1, 0, 0, 0, 1, 15.4, 160, 'Gold', 1, 0, 0, 3.8, 800, 480, 30, 2, 2, 4, 1400, 0, 2000, 1, 1, 16000, 1950, 1, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 8, 1, 30, '1080', 2, 0, 1, 1, 1, 0),
(8, 10, 500, 2, 1, 1, 1, 1, 1, 1, 7.25, 153, 'Black', 0, 1, 0, 5.5, 1920, 1080, 30, 1, 1, 8, 2450, 1, 8000, 1, 0, 128000, 3300, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 16, 1, 30, '1080', 1, 1, 1, 1, 1, 1),
(9, 12, 550, 2, 3, 1, 1, 1, 1, 1, 9.8, 167, 'Orange', 0, 1, 0, 5, 1920, 1080, 30, 1, 1, 4, 2200, 1, 2000, 1, 0, 32000, 2420, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 0, 1, 1, 20, 1, 30, '720', 2, 1, 1, 1, 0, 1),
(10, 13, 120, 1, 4, 0, 0, 0, 0, 0, 11, 99, 'Black', 1, 0, 0, 2.44, 480, 360, 30, 2, 2, 1, 800, 0, 512, 1, 1, 512, 1000, 1, 'Lithium', 0, 1, 1, '3G', 2, 2, 0, 0, 1, 5, 1, 0, '640', 2, 0, 0, 0, 0, 0),
(11, 14, 380, 2, 1, 1, 0, 0, 0, 0, 7.2, 142, 'Grey', 0, 1, 0, 5.2, 1920, 1080, 30, 2, 2, 8, 2100, 1, 4000, 1, 1, 32000, 3000, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 12, 1, 30, '1080', 2, 0, 1, 1, 1, 1),
(11, 16, 480, 2, 1, 1, 1, 1, 1, 1, 6.95, 144, 'Color', 0, 1, 0, 5.2, 1920, 1080, 30, 2, 1, 8, 2500, 1, 3000, 1, 1, 64000, 3000, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 0, 1, 1, 12, 1, 30, '1080', 2, 1, 1, 1, 1, 1),
(11, 23, 1400, 2, 1, 1, 1, 1, 0, 1, 7.5, 169, 'Black', 0, 1, 0, 5.5, 2560, 1440, 30, 1, 1, 1, 2360, 1, 8, 1, 1, 256000, 4000, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 12, 1, 30, '1080', 2, 1, 1, 1, 1, 1),
(11, 28, 420, 2, 1, 1, 1, 0, 1, 1, 7.45, 155, 'Blue', 0, 1, 0, 5.15, 1080, 1920, 30, 1, 2, 8, 2360, 1, 6000, 1, 1, 128000, 32000, 0, 'Lithium', 0, 1, 1, 'LTE', 1, 2, 1, 1, 1, 20, 1, 30, '2160', 2, 1, 1, 1, 1, 1),
(12, 15, 170, 1, 1, 1, 1, 1, 1, 1, 8, 150, 'Black', 0, 1, 0, 5.5, 1920, 1080, 30, 2, 2, 8, 1700, 1, 2000, 1, 1, 16000, 2900, 1, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 13, 1, 30, '1080', 2, 1, 1, 1, 1, 1),
(12, 25, 490, 2, 1, 1, 1, 1, 1, 1, 7.6, 160, 'White', 0, 1, 0, 5.5, 1080, 1920, 30, 1, 1, 4, 2350, 1, 6000, 1, 0, 64000, 3100, 0, 'Lithium', 0, 1, 2, 'LTE', 1, 2, 1, 1, 1, 13, 1, 30, '2160', 2, 1, 1, 1, 1, 1);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `companies`
--
ALTER TABLE `companies`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `questions`
--
ALTER TABLE `questions`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `smartphones`
--
ALTER TABLE `smartphones`
  ADD PRIMARY KEY (`id`),
  ADD KEY `company_id` (`company_id`);

--
-- Indexes for table `smartphones_features`
--
ALTER TABLE `smartphones_features`
  ADD PRIMARY KEY (`id`,`smartphone_id`),
  ADD KEY `smartphone_id` (`smartphone_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `companies`
--
ALTER TABLE `companies`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- AUTO_INCREMENT for table `questions`
--
ALTER TABLE `questions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;
--
-- AUTO_INCREMENT for table `smartphones`
--
ALTER TABLE `smartphones`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;
--
-- AUTO_INCREMENT for table `smartphones_features`
--
ALTER TABLE `smartphones_features`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `smartphones_features`
--
ALTER TABLE `smartphones_features`
  ADD CONSTRAINT `smartphones_features_ibfk_1` FOREIGN KEY (`smartphone_id`) REFERENCES `smartphones` (`id`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
