<?php
 
namespace JBsquared\Common;

use JBsquared\Common\Interfaces\IBaseObject;
use JBsquared\Common\Repository;

class  Application implements IBaseObject  {

   public $ProjectCode;
   public $Appkey;
   public $Id;
   public $Name;
   public $Repo;
   protected $dbstatements;
   
   public function __construct() {
		$this->Repo = new Repository();
		$this->dbstatements = array();	
		$this->dbstatements['application'] = "select ID,Name,ApplicationType from Application as application";
	}
	
   public function getByProjectCode($projectCode) {
        return $projectCode;
		//return $this->Repo->output($this->dbstatements['application'].' where ProjectCode = 456');
   }
   
   
   
   
   
   
   
   
}
?>
