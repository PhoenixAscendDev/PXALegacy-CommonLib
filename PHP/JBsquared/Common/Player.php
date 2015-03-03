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
	private $repo;
	   protected $dbstatements = Array(
	'player' => "select ID,SquareHeadID,Email,FirstName,LastName,Birthdate,City,Gender from Player as p"
   );


   public function __construct($repo) {
		$this->repo = $repo;
   }
   
   public static function withID($id,$repo)
   {
		$instance = new self($repo);
		$instance->loadById($id);		
		return $instance;
   }
   
   
   protected function loadById($id)
   {
        $sqlstatement = $this->dbstatements['player'].' where p.ID = "'.$id.'"';
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
		$this->id = $r->ID;
		$this->email = $r->Email;
		$this->firstname = $r->FirstName;
		$this->lastname = $r->LastName;
		$this->birthdate = $r->Birthdate;
		$this->city = $r->City;
		$this->gender = $r->Gender;
   }
}
?>