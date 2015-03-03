<?php
 
namespace JBsquared\Common;

use JBsquared\Common\Interfaces\IBaseObject;
use JBsquared\Common\Player;


class AuthPlayer extends Player  {

	private $provider;
	private $uid;

	
	
	public function __construct($repo) {
		$this->dbstatements['playerProvider'] = "select p.ID,p.SquareHeadID,p.Email,p.FirstName,p.LastName,p.Birthdate,p.City,p.Gender, auth.Provider,auth.Provider_UID from Player as p join  Player_OAuth as auth on auth.Player_ID = p.ID";		
		parent::__construct($repo);
	}
	
   public static function withProvider($provider,$uid,$repo)
   {
		$instance = new self($repo);
		$instance->loadByProvider($provider,$uid);		
		return $instance;
   }
   
   public static function signIn($provider,$uid,$app)
   {
		$instance = new self($app->repo);
		$resultID = 0;
		$insertSQL = "INSERT INTO Player_Signin_Audit(Player_ID,Provider,Signin_Date,Application_ID) SELECT a.Player_ID,'".$provider."',now(),".$app->id." from Player_OAuth as a where a.Provider = '".$provider."' and a.Provider_UID = '".$uid."'";
	 
		if($instance->repo->query($insertSQL) == TRUE)
		{
		   $instance->loadByProvider($provider,$uid);	
		}
		return $instance;
   }
   
   
   
   protected function loadByProvider($provider,$uid)
   {
		$sqlstatement = $this->dbstatements['playerProvider']." and auth.Provider = '".$provider."' and auth.Provider_UID = '".$uid."'";
		try
		{
			$p = $this->repo->output($sqlstatement);
			$this->fill($p);
		}
		catch(Exception $e)
		{
		
		}
   }
   
   
   
   
	
	
   protected function fill($r)
   {
		if($r != null)
		{
			parent::fill($r);
			$this->provider = $r->Provider;
			$this->uid = $r->Provider_UID;
		}
   }
   
   public function save()
   {
		$isNew = $this->isNew();
		
		if(parent::save() and $isNew == TRUE)
		{
			$result = FALSE;
				$insertSQL = "INSERT INTO Player_OAuth(Player_ID,Provider,Provider_UID) VALUES('".$this->id."','".$this->provider."','".$this->uid."')";
				$result = $this->repo->query($insertSQL);
				
				var_dump($this->repo);
				
				//if we saved this lets reload the player object complete with the provider;
				if($result == TRUE)
				{
					$this->loadByProvider($this->provider,$this->uid);
				}
		}
		
   }
   

}
?>