-- =======================================================
-- This is a function to replace bad for good characters, this function works based in a table with the mapping
-- =======================================================
-- drop the table if it exists
DROP TABLE IF EXISTS urlcodemap;
-- =======================================================
-- Here the table required for the mapping
CREATE TABLE `urlcodemap` (
`id` INTEGER NOT NULL AUTO_INCREMENT,
`encoded` VARCHAR(100) NOT NULL,
`decoded` VARCHAR(100) NOT NULL,
`defEncode` BIT(1) NOT NULL DEFAULT b'0',
UNIQUE KEY urlcodemapUIdx1(encoded),
KEY urlcodemapUIdx2(decoded),
KEY urlcodemapUIdx3(defEncode),
PRIMARY KEY (`id`)  
) ENGINE=INNODB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- very important that % must be the first character to replace
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%25","%", b'1');

-- the next block include the flag defEncode which indicates is necesary to encode the character
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%20"," ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%21","!", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%22","""", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%23","#", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%24","$", b'1');

INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%26","&", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%27","'", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%28","(", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%29",")", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%2A","*", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%2B","+", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%2C",",", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%2D","-", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%2E",".", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%2F","/", b'1');


INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%3A",":", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%3B",";", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%3C","<", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%3D","=", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%3E",">", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%3F","?", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%40","@", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%5B","[", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%5C","\\", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%5D","]", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%5E","^", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%5F","_", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%60","`", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%7B","{", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%7C","|", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%7D","}", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%7E","~", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%80","`", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%82","‚", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%83","ƒ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%84","„", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%85","…", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%86","†", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%87","‡", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%88","ˆ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%89","‰", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%8A","Š", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%8B","‹", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%8C","Œ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%8E","Ž", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%91","‘", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%92","’", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%93","“", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%94","”", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%95","•", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%96","–", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%97","—", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%98","˜", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%99","™", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%9A","š", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%9B","›", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%9C","œ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%9E","ž", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%9F","Ÿ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A1","¡", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A2","¢", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A3","£", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A4","¤", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A5","¥", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A6","¦", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A7","§", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A8","¨", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%A9","©", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%AA","ª", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%AB","«", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%AC","¬", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%AE","®", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%AF","¯", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B0","°", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B1","±", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B2","²", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B3","³", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B4","´", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B5","µ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B6","¶", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B7","·", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B8","¸", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%B9","¹", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%BA","º", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%BB","»", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%BC","¼", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%BD","½", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%BE","¾", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%BF","¿", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C0","À", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C1","Á", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C2","Â", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C3","Ã", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C4","Ä", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C5","Å", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C6","Æ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C7","Ç", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C8","È", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%C9","É", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%CA","", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%CB","Ë", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%CC","Ì", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%CD","Í", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%CE","Î", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%CF","Ï", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D0","Ð", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D1","Ñ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D2","Ò", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D3","Ó", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D4","Ô", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D5","Õ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D6","Ö", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D7","×", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D8","Ø", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%D9","Ù", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%DA","Ú", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%DB","Û", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%DC","Ü", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%DD","Ý", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%DE","Þ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%DF","ß", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E0","à", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E1","á", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E2","â", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E3","ã", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E4","ä", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E5","å", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E6","æ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E7","ç", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E8","è", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%E9","é", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%EA","ê", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%EB","ë", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%EC","ì", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%ED","í", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%EE","î", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%EF","ï", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F0","ð", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F1","ñ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F2","ò", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F3","ó", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F4","ô", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F5","õ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F6","ö", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F7","÷", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F8","ø", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%F9","ù", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%FA","ú", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%FB","û", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%FC","ü", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%FD","ý", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%FE","þ", b'1');
INSERT INTO urlcodemap (encoded,decoded, defEncode) VALUES ("%FF","ÿ", b'1');


-- these characters are not required to be inserted in the table, but a URLDecode function can
-- be constructed with them

INSERT INTO urlcodemap (encoded,decoded) VALUES ("%30","0");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%31","1");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%32","2");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%33","3");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%34","4");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%35","5");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%36","6");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%37","7");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%38","8");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%39","9");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%41","A");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%42","B");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%43","C");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%44","D");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%45","E");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%46","F");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%47","G");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%48","H");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%49","I");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%4A","J");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%4B","K");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%4C","L");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%4D","M");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%4E","N");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%4F","O");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%50","P");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%51","Q");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%52","R");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%53","S");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%54","T");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%55","U");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%56","V");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%57","W");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%58","X");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%59","Y");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%5A","Z");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%61","a");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%62","b");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%63","c");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%64","d");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%65","e");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%66","f");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%67","g");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%68","h");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%69","i");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%6A","j");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%6B","k");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%6C","l");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%6D","m");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%6E","n");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%6F","o");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%70","p");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%71","q");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%72","r");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%73","s");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%74","t");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%75","u");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%76","v");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%77","w");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%78","x");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%79","y");
INSERT INTO urlcodemap (encoded,decoded) VALUES ("%7A","z");




-- =======================================================
-- NEXT: The function that does the magic

DELIMITER $$

DROP FUNCTION IF EXISTS `URLENCODERCUR`$$

CREATE FUNCTION `URLENCODERCUR`(str VARCHAR(4096) CHARSET utf8) RETURNS VARCHAR(4096) DETERMINISTIC
BEGIN
DECLARE chr VARCHAR(256);
DECLARE chrto VARCHAR(256);               
DECLARE done INT DEFAULT FALSE;
DECLARE cur1 CURSOR FOR SELECT decoded, encoded FROM urlcodemap WHERE defEncode = b'1' ORDER BY id ;
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
