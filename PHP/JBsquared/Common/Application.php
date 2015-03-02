<?php
 
namespace JBsquared\Common;

use JBsquared\Common\Interfaces;
use JBsquared\Common\Repository;


public class  Application implements IBaseObject  {

   public $ProjectCode;
   public $Appkey;
   public $Id;
   public $Name;
   protected $Repo
   protected $dbstatements;
   
   public function __construct() {
		$this->Repo = new JBsquared\Common\Repository();
		$this->dbstatements = array();	
		$this->dbstatements['application'] = "select ID,Name,ApplicationType from Application as application";
	}
	
   public function GetByProjectKey($projectCode) {
		$this->Repo->outputWithParam($this->dbstatements['application'] + ' where ProjectCode = '
   
   
   }
   
   
   
   
   
   
}
?>
