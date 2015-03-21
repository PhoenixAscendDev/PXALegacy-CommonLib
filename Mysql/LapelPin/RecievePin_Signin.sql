drop procedure RecievePin_Signin;
delimiter //
create procedure RecievePin_Signin(IN playerid INT)
begin

declare playerpinid int default 0;

if playerid is null then
  set playerid = 0;
end if;

create temporary table tmpPins(select * from jbsquared_appdata.Player_Pin where 1 = 0);


#JBsquared Signin Pin
insert into tmpPins(Player_ID,Pin_ID,DateRecieved,Description) 
select su.Player_ID,2,min(Signin_Date) as DateRecieved, ''
  from jbsquared_appdata.Player_Signin_Audit as su
  where ((playerid = 0) or (su.Player_ID = playerid))
  group by Player_ID;

#insert 
insert into jbsquared_appdata.Player_Pin(Player_ID,Pin_ID,DateRecieved,Description)
select t.Player_ID,t.Pin_ID,t.DateRecieved,''
  from tmpPins as t
where t.Player_ID not in (select Player_ID from jbsquared_appdata.Player_Pin as p where p.Player_ID = t.Player_ID and p.Pin_ID and t.Pin_ID);

set playerpinid = LAST_INSERT_ID();

select ID,Player_ID,Pin_ID,DateRecieved,Description,ActionAudit_ID
  from Player_Pin
 where Pin_ID = playerpinid;


end//

delimiter ;
