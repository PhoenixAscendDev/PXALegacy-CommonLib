<?php
 
namespace JBsquared\Common;

use JBsquared\Common\Interfaces\IBaseObject;

class Player implements IBaseObject  {

	public $id;
	public $email;
	public $firstname;
	public $lastname;
	public $birthdate;
	public $city;
	public $gender;
	public $apikey;
	protected $repo;
	protected $dbstatements = Array(
	'player' => "select ID,SquareHeadID,Email,FirstName,LastName,Birthdate,City,Gender,APIkey from Player as p"
   );


   public function __construct($repo) {
		$this->repo = $repo;
		$this->id = 0;
   }
   
   public static function withID($id,$repo)
   {
		$instance = new self($repo);
		$instance->loadById($id);		
		return $instance;
   }
   
   
   protected function loadById($id)
   {
		//echo 'Player:startloadById';
        $sqlstatement = $this->dbstatements['player'].' where p.ID = "'.$id.'"';
		try
		{
			$p = $this->repo->output($sqlstatement);
			self::fill($p);
		}
		catch(Exception $e)
		{
			var_dump($e);
		}
		//echo 'Player:endloadById';
   }  
   
   protected function fill($r)
   {
		//echo 'Player:startfill';
		if($r != null)
		{
			$this->id = $r->ID;
			$this->email = $r->Email;
			$this->firstname = $r->FirstName;
			$this->lastname = $r->LastName;
			$this->birthdate = $r->Birthdate;
			$this->city = $r->City;
			$this->gender = $r->Gender;
			$this->apikey = $r->APIkey;
		}
   }
   
   public function save()
   {
		//echo 'Player:beforesave';
		$result = FALSE;
		if($this->isNew())  //inserted a new record
		{
			$resultID = 0;
			$insertSQL = "INSERT INTO Player(Email,FirstName,LastName) VALUES('".$this->email."','".$this->firstname."','".$this->lastname."')";
			$result = $this->repo->query($insertSQL);
			
			//if we saved this lets reload the player object;
			if($result == TRUE)
			{
				$this->loadById($this->repo->lastInsert_id);
			}
		}
		//echo 'Player:aftersave';
		//var_dump($this);
		
			
	 return $result; 
   }
   
   public function isNew()
   {
		return $this->id == 0;
   }
   
   
}
?>