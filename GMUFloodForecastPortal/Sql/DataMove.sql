USE `jpssflood`;
DROP procedure IF EXISTS `sp_MoveKmlMetadataByYear`;

DELIMITER $$
USE `jpssflood`$$
CREATE PROCEDURE `sp_MoveKmlMetadataByYear` ()
BEGIN
	declare batchsize int unsigned default 5000;
    declare insertedRows int unsigned default 5000;
	while insertedRows >= batchsize do
		insert into tableName
		select * from jpssflood.kmlmetadata
		where Date >= startDate and Date < endDate
        limit batchsize;
		set insertedRows = ROW_COUNT();
	end while;
END$$

DELIMITER ;