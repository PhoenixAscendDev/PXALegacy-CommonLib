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
	'application' => "select ID,Name,ApplicationType,ProjectCode,AppKey from Application as application"
   );
   
   public function __construct($appkey) {
		$this->repo = new Repository($appkey);
		$this->get($appkey);
		//$this->dbstatements = array();	
		//$this->dbstatements['application'] = "select ID,Name,ApplicationType from Application as application";		
   }
   
   private function get($appkey)
   {
		try
		{
		    $app = $this->repo->output($this->dbstatements['application'].' where AppKey = "'.$appkey.'"');
			$this->id = $app->ID;
			$this->name = $app->Name;
			$this->appkey = $app->AppKey;
			$this->projectCode = $app->ProjectCode;
		}
		catch(Exception $e)
		{
			var_dump($e);
		}
		//var_dump($this->repo->output($this->dbstatements['application']));
   }
   
   
   
   
   
   
   
   
}
?>
