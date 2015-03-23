CREATE DEFINER=`jbsquared_dbo`@`69.163.128.0/255.255.128.0` PROCEDURE `RecievePin_IronManTen`(IN playerid INT)
begin

declare auditid int default 0;
declare cursor_finished INT default 0;
declare c_playerid INT default 0;
declare c_datekey VARCHAR(255);
declare dayindex INT default 0;
declare pin_cursor CURSOR for select Player_ID,Date_Key from tmpDates;
declare continue handler for not found set cursor_finished = 1;

if playerid is null then
 set playerid = 0;
end if;


#get the list of dates
create temporary table tmpDates(select f.Player_ID, f.Date_Key, 1 as DaysInRow 
                                 from fiveandtwo.Fact_Player_Date as f
                                where ((playerid = 0) or (f.Player_ID = playerid)));







#now loop through all the Dates


open pin_cursor;

get_date: LOOP

fetch pin_cursor INTO c_playerid,c_datekey;

if cursor_finished = 1 then 
 LEAVE get_date;
end if;

set dayindex = c_datekey;


label1: LOOP
   SET dayindex = dayindex + 1;

   IF dayindex > (c_datekey + 10) THEN
     LEAVE label1;
   END IF;

  if  exists (select Date_Key 
                    from tmpDates  as t 
                   where t.Player_ID = c_playerid
                     and t.Date_Key = dayindex) THEN
  update tmpDates as f
     set DaysInRow = DaysInRow + 1
   where f.Player_ID = c_playerid
     and f.Date_Key = c_datekey;

  ELSE 
     LEAVE label1;
  END IF;
   
 END LOOP label1;


end loop get_date;


close pin_cursor;

#now that we calculated that lets send pins out the folks
insert into jbsquared_appdata.Player_Pin(Player_ID,Pin_ID,DateRecieved,Description)
select t.Player_ID,4,now(),''
 from tmpDates as t
where t.DaysInRow >= 10
group by t.Player_ID;

drop table tmpDates;





end
