DROP TABLE IF EXISTS log;
DROP TRIGGER IF EXISTS log_primary_key;

CREATE TABLE log(date date not null, id int not null, text blob, PRIMARY KEY( date, id )) ENGINE = MyISAM;

DELIMITER $$
CREATE TRIGGER log_primary_key BEFORE INSERT ON log
FOR EACH ROW BEGIN
	DECLARE max_id int;
	SET NEW.date = IF( NEW.date IS NULL OR NEW.date = '0000-00-00', DATE(NOW()), NEW.date );
	SELECT MAX(id) FROM log WHERE date = NEW.date INTO max_id;
	SET NEW.id = IF( max_id IS NULL, 1, max_id + 1);
END$$

DELIMITER ;