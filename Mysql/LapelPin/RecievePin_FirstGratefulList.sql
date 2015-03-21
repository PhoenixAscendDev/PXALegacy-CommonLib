drop procedure RecievePin_FirstGratefulList;
delimiter //
create procedure RecievePin_FirstGratefulList(IN playerid INT)
begin

declare playerpinid int default 0;


create temporary table tmpPins(select * from jbsquared_appdata.Player_Pin where 1 = 0);


#1st FiveAndTwo List
insert into tmpPins(Player_ID,Pin_ID,DateRecieved,Description) 
select f.Player_ID,1,dd.full_date,min(f.Date_Key)
  from fiveandtwo.Fact_Player_Date as f
  join fiveandtwo.Dim_Date as dd on dd.date_key = f.Date_Key
where ( (playerid is null) or (f.Player_ID = playerid))
group by f.Player_ID;

#insert 
insert into jbsquared_appdata.Player_Pin(Player_ID,Pin_ID,DateRecieved,Description)
select t.Player_ID,t.Pin_ID,t.DateRecieved,''
  from tmpPins as t
where t.Player_ID not in (select Player_ID from jbsquared_appdata.Player_Pin as p where p.Player_ID = t.Player_ID and p.Pin_ID = t.Pin_ID);

drop table tmpPins;

end//

delimiter ;
