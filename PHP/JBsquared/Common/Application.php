<?php
 
namespace JBsquared\Common;

use JBsquared\Common\Interfaces\IBaseObject;
use JBsquared\Common\Repository;

class  Application implements IBaseObject  {

   public $projectCode;
   public $id;
   public $name;
   public $repo;
   private $appkey;
   protected $dbstatements = Array(
	'application' => "select ID,Name,ApplicationType from Application as application"
   );
   
   public function __construct($appkey) {
		$this->repo = new Repository($appkey);
		$this->appKey = $appkey;
		$this->get();
		//$this->dbstatements = array();	
		//$this->dbstatements['application'] = "select ID,Name,ApplicationType from Application as application";		
   }
   
   private function get()
   {
		var_dump($this->repo->output($this->dbstatements['application']));
   }
   
   
   
   
   
   
   
   
}
?>
