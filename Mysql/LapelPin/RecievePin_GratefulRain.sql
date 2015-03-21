drop procedure if exists RecievePin_GratefulRain;
delimiter //
create procedure RecievePin_GratefulRain(IN playerid INT)
begin

declare playerpinid int default 0;

if playerid is null then
  set playerid = 0;
end if;

create temporary table tmpPins(select * from jbsquared_appdata.Player_Pin where 1 = 0);


#JBsquared Signin Pin
insert into tmpPins(Player_ID,Pin_ID,DateRecieved,Description) 
select l.Player_ID,3,l.CreateDate,'' 
  from fiveandtwo.Player_EntryLog as l where (lower(l.TextValue) like '% rain %' or lower(l.TextValue) like '% rain' or lower(l.TextValue) like 'rain %' or lower(l.TextValue) = 'rain')
  and ((playerid = 0) or (l.Player_ID = playerid));

#insert 
insert into jbsquared_appdata.Player_Pin(Player_ID,Pin_ID,DateRecieved,Description)
select t.Player_ID,t.Pin_ID,t.DateRecieved,''
  from tmpPins as t
where t.Player_ID not in (select Player_ID from jbsquared_appdata.Player_Pin as p where p.Player_ID = t.Player_ID and p.Pin_ID = t.Pin_ID);

set playerpinid = LAST_INSERT_ID();


drop table tmpPins;

end//

delimiter ;
