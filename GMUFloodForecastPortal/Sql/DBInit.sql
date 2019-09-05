/*
CREATE SCHEMA jpssflood

CREATE TABLE `jpssflood`.`products` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Id`));

CREATE TABLE `jpssflood`.`kmlmetadata` (
  `Id` INT NOT NULL AUTO_INCREMENT,
  `ProductId` INT NULL,
  `RegionId` INT NULL,
  `DistrictId` INT NULL,
  `Date` DATETIME NULL,
  `FileName` VARCHAR(255) NULL,
  PRIMARY KEY (`Id`));
*/

INSERT INTO jpssflood.products (Name) VALUES ('Suomi-NPP');
INSERT INTO jpssflood.products (Name) VALUES ('NOAA-20');
INSERT INTO jpssflood.products (Name) VALUES ('GOES-16');

INSERT INTO jpssflood.kmlmetadata (ProductId, RegionId, DistrictId, Date, FileName) 
	VALUES (1, 1, 42, '20180815 01:00:00', 'cspp-viirs-flood-globally_20180815_010000_42');

INSERT INTO jpssflood.kmlmetadata (ProductId, RegionId, DistrictId, Date, FileName) 
	VALUES (1, 1, 53, '2018-08-15 01:00:00', 'cspp-viirs-flood-globally_20180815_010000_53');

INSERT INTO jpssflood.kmlmetadata (ProductId, RegionId, DistrictId, Date, FileName) 
	VALUES (1, 1, 54, '2018-08-15 01:00:00', 'cspp-viirs-flood-globally_20180815_010000_54');

INSERT INTO jpssflood.kmlmetadata (ProductId, RegionId, DistrictId, Date, FileName) 
	VALUES (1, 1, 55, '2018-08-15 01:00:00', 'cspp-viirs-flood-globally_20180815_010000_55');
