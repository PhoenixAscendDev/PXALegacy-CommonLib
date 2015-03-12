DELIMITER $$

DROP FUNCTION IF EXISTS `JB2_Htmlencode`$$

CREATE FUNCTION `JB2_Htmlencode`(str VARCHAR(4096) CHARSET utf8) RETURNS VARCHAR(4096) DETERMINISTIC
BEGIN
DECLARE chr VARCHAR(256);
DECLARE chrto VARCHAR(256);               
DECLARE done INT DEFAULT FALSE;
DECLARE cur1 CURSOR FOR SELECT Character_Key as decoded, Html_Code as encoded FROM Dim_Character;
DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;


OPEN cur1;

-- iterate through the replacements
read_loop: LOOP
    FETCH cur1 INTO chr, chrto;

    IF done THEN
    LEAVE read_loop;
    END IF;

    SET str = REPLACE(str,chr,chrto);

END LOOP;

-- required to erase the flag of NOT FOUND DATA ( mysql bug ? )
SELECT '', '' INTO chr, chrto;

CLOSE cur1;

RETURN str;
END$$


DELIMITER ;  
-- =======================================================
