drop procedure if exists AddPlayerAction;
delimiter //
create procedure AddPlayerAction(IN playerid INT,actionkey VARCHAR(20),applicationid INT,data VARCHAR(2000))
begin

declare auditid int default 0;
declare cursor_finished INT default 0;
declare pin_id INT default 0;
declare pin_fx VARCHAR(255);
declare pin_cursor CURSOR for select ID,Completion_fx from tmpTriggers;
declare continue handler for not found set cursor_finished = 1;


#insert new record
insert into jbsquared_appdata.Player_Action_Audit(Player_ID,Action_Key,Application_ID,DateOccured,Action_Data)
values(playerid,actionkey,applicationid,now(),data);

set auditid = LAST_INSERT_ID();

#get the JBSquared Pin triggers
create temporary table tmpTriggers(select p.ID,p.Pin_Key,p.Title,p.Description,p.Application_ID,p.Completion_fx,p.Unique from jbsquared_appdata.JBsquaredPin_ActionTrigger as t 
join jbsquared_appdata.JBsquaredPin as p on p.ID = t.Pin_ID where t.Action_Key = actionkey and t.Application_ID = applicationid);

#now loop through all the triggers


open pin_cursor;

get_pin: LOOP

fetch pin_cursor INTO pin_id,pin_fx;

if cursor_finished = 1 then 
  LEAVE get_pin;
end if;


#set the sp call
set @s = concat('call ',pin_fx,';');
set @id = playerid;

insert into jbsquared_appdata.JBsquaredPin_ActionTrigger_Audit(Pin_ID,Action_ID,Statement,Parameter,DateRun)
values(pin_id,auditid,pin_fx,@id,now());

prepare stmt from @s;

execute stmt using @id;



end loop get_pin;
DROP PREPARE stmt;

close pin_cursor;





 

end//

delimiter ;
