DROP FUNCTION IF EXISTS `stripSpeciaChars`;
DELIMITER ;;
CREATE FUNCTION `stripSpeciaChars`(`dirty_string` varchar(2048),allow_space TINYINT,allow_number TINYINT,allow_alphabets TINYINT,no_trim TINYINT) RETURNS varchar(2048) CHARSET utf8
BEGIN
/**
 * MySQL function to remove Special characters, Non-ASCII,hidden characters leads to spaces, accents etc
 * Downloaded from http://DevZone.co.in
 * @param VARCHAR dirty_string : dirty string as input
 * @param VARCHAR allow_space : allow spaces between string in output; takes 0-1 as parameter
 * @param VARCHAR allow_number : allow numbers in output; takes 0-1 as parameter
 * @param VARCHAR allow_alphabets : allow alphabets in output; takes 0-1 as parameter
 * @param VARCHAR no_trim : don't trim the output; takes 0-1 as parameter
 * @return VARCHAR clean_string : clean string as output
 * 
 * Usage Syntax: stripSpeciaChars(string,allow_space,allow_number,allow_alphabets,no_trim);
 * Usage SQL> SELECT stripSpeciaChars("sdfa7987*&^&*ÂÃ ÄÅÆÇÈÉÊ sd sdfgËÌÍÎÏÑ ÒÓÔÕÖØÙÚàáâã sdkarkhru",0,0,1,0);
 * result : sdfasdsdfgsdkarkhru
 */
      DECLARE clean_string VARCHAR(2048) DEFAULT '';
      DECLARE c VARCHAR(2048) DEFAULT '';
      DECLARE counter INT DEFAULT 1;
	  
	  DECLARE has_space TINYINT DEFAULT 0; -- let spaces in result string
	  DECLARE chk_cse TINYINT DEFAULT 0; 
	  DECLARE adv_trim TINYINT DEFAULT 1; -- trim extra spaces along with hidden characters, new line characters etc.	  
	
	     if allow_number=0 and allow_alphabets=0 then
	    RETURN NULL;
	  elseif allow_number=1 and allow_alphabets=0 then
	  set chk_cse =1;
	 elseif allow_number=0 and allow_alphabets=1 then
	  set chk_cse =2;
	  end if;	  
	  
	  if allow_space=1 then
	  set has_space =1;
	  end if;
	  
	   if no_trim=1 then
	  set adv_trim =0;
	  end if;
 
      IF ISNULL(dirty_string) THEN
            RETURN NULL;
      ELSE
	  
	  CASE chk_cse
      WHEN 1 THEN 
	  -- return only Numbers in result
	  WHILE counter <= LENGTH(dirty_string) DO
                   
                  SET c = MID(dirty_string, counter, 1);
 
                  IF ASCII(c) = 32 OR ASCII(c) >= 48 AND ASCII(c) <= 57  THEN
                        SET clean_string = CONCAT(clean_string, c);
                  END IF;
 
                  SET counter = counter + 1;
            END WHILE;
      WHEN 2 THEN 
	  -- return only Alphabets in result
	  WHILE counter <= LENGTH(dirty_string) DO
                   
                  SET c = MID(dirty_string, counter, 1);
 
                  IF ASCII(c) = 32 OR ASCII(c) >= 65 AND ASCII(c) <= 90  OR ASCII(c) >= 97 AND ASCII(c) <= 122 THEN
                        SET clean_string = CONCAT(clean_string, c);
                  END IF;
 
                  SET counter = counter + 1;
            END WHILE;
      ELSE
	   -- return numbers and Alphabets in result
        WHILE counter <= LENGTH(dirty_string) DO
                   
                  SET c = MID(dirty_string, counter, 1);
 
                  IF ASCII(c) = 32 OR ASCII(c) >= 48 AND ASCII(c) <= 57 OR ASCII(c) >= 65 AND ASCII(c) <= 90  OR ASCII(c) >= 97 AND ASCII(c) <= 122 THEN
                        SET clean_string = CONCAT(clean_string, c);
                  END IF;
 
                  SET counter = counter + 1;
            END WHILE;		
    END CASE;            
      END IF;
 
	 
	  -- remove spaces from result
	  if has_space=0 then
	  SET clean_string =REPLACE(clean_string,' ','');
	  end if;
	 
	   -- remove extra spaces, newline,tabs. from result
	 if adv_trim=1 then
	  SET clean_string =TRIM(Replace(Replace(Replace(clean_string,'\t',''),'\n',''),'\r',''));
	  end if;	 
	  
      RETURN clean_string;
END
;;
DELIMITER ;
