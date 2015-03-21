<?php
namespace JBsquared\Common;

use JBsquared\Common\Interfaces\IBaseObject;
use JBsquared\Common\JB2Object;


class JB2Pin extends JB2Object implements IBaseObject  {
	
	public $key;
	public $title;
	public $description;
	public $appid;
	public $onlyOne;
	public $images = Array(
		"thumbnail" => "",
		"normal" => "",
		"large" => ""
	);
	private static $sqlstatements = Array(
		"search" => "select ID,Pin_Key,Title,Description,Application_ID,Completion_fx,Unique from jbsquared_appdata.JBsquaredPin as p "
	);
	
	public function __construct($repo) {
	  parent::__construct($repo);
	  $this->dbstatements['search'] =  "select ID,Pin_Key,Title,Description,Application_ID,Completion_fx,Unique from jbsquared_appdata.JBsquaredPin as p ";
	  //$this->dbstatements['save-insert'] = "INSERT into fiveandtwo.Player_EntryLog(Player_ID,EntryCode,CreateDate,TextValue,PrivacyLevel) VALUES (?,?,now(),?,?)";
	  //$this->dbstatements['save-update'] = "UPDATE fiveandtwo.Player_EntryLog set TextValue = ?, PrivacyLevel = ? where ID = ?";
   }
   
   public static function withID($id,$repo)
   {
		$instance = new self($repo);
		$instance->loadById($id);		
		return $instance;
   }
   
   public static function withKey($key,$repo)
   {
		$instance = new self($repo);
		$instance->loadByKey($key);		
		return $instance;
   }
   
   public static function getPlayerPins($playerid,$applicationID,$repo)
   {
		$sqlstatement = "select p.ID,p.Pin_Key,p.Title,p.Description,p.Application_ID,p.Completion_fx,p.Unique,pp.DateRecieved 
						  from jbsquared_appdata.JBsquaredPin as p 
						  join Player_Pin as pp on pp.Pin_ID = p.ID and pp.Player_ID =  ? and p.Application_ID = ?";	
		
		$dbresults = $repo->run($sqlstatement,'ii',$playerid,$applicationID);
		$outputResult = array();
		foreach($dbresults as $r)
		{
			//only return the requested application pins
			if( ($applicationID == null) or ($applicationID == $r-> Application_ID))
			{
				
					
				$pin = new self($repo);
				$pin->fill($r);
				
				$record = array(
					"playerid" => $playerid,
					"daterecieved" => $r->DateRecieved,
					"pin" => $pin
				);
				array_push($outputResult,(object)$record);
				
			}
		}		
		return $outputResult;
   }
   
   
   
   protected function loadById($id)
   {
        $sqlstatement = $this->dbstatements['search'].' where ID = ? LIMIT 1;';
		
		try
		{
			$o = $this->repo->run($sqlstatement,'i',$id);
		return $result != null;
			self::fill($o);
		}
		catch(Exception $e)
		{
			//var_dump($e);
		}
   }
   
   protected function loadByKey($key)
   {
        $sqlstatement = $this->dbstatements['search'].' where Pin_Key = ? LIMIT 1;';
		
		try
		{
			$o = $this->repo->run($sqlstatement,'s',$key);
		return $result != null;
			self::fill($o);
		}
		catch(Exception $e)
		{
			//var_dump($e);
		}
   } 
	
	
	
	
	
	
	protected function fill($o)
   {	
		if($o != null)
		{
			parent::fill($o);
			$this->key = $o->Pin_Key;
			$this->title = $o->Title;
			$this->description = $o->Description;
			$this->appid = $o->Application_ID;
			$this->onlyOne = $o->Unique;
			$this->images["thumbnail"] = "http://jbsquared.com/lapelpin/200/".$o->Pin_Key."/";
			$this->images["normal"] = "http://jbsquared.com/lapelpin/200/".$o->Pin_Key."/";
			$this->images["large"] = "http://jbsquared.com/lapelpin/200/".$o->Pin_Key."/";
		}
   }


}
	
?>