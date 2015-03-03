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
   private $oAuthconfig;
   protected $dbstatements = Array(
	'application' => "select ID,Name,ApplicationType,ProjectCode,AppKey from Application as application",
	'app_providers' => "select ID,Application_ID,Provider,Provider_Key,Provider_Secret from Application_OAuth as oAuth";
   );
   
   public function __construct($appkey) {
		$this->repo = new Repository($appkey);
		$this->loadbyAppKey($appkey);				
   }
   
   protected function loadbyAppKey($appkey) {  
		try
		{		
			$app = $this->repo->output($this->dbstatements['application'].' where AppKey = "'.$appkey.'"');
			$this->fill($app);
		}
		catch(Exception $e)
		{
		}
		
   }
   
   protected function fill($row)
   {
		try
		{
			$this->id = $app->ID;
			$this->name = $app->Name;
			$this->appkey = $app->AppKey;
			$this->projectCode = $app->ProjectCode;
			$this->oAuthconfig = $this->get_oAuthconfig($app->ID);
		}
		catch(Exception $e)
		{
			
		}		
   }
   
   private function get_oAuthconfig($appid) {
   
		$result = array(
					"base_url" => "http://localhost/fivetwo/library/oAuth/"),
					"providers" => array(),
					"debug_mode" => false,
					"debug_file" => ""
					);
		try
		{
			$sql = $this->dbstatements['app_providers']." where Application_ID = '".$appid."' ";
			$providers = $this->repo->output($sql);
			foreach( $providers as $p)
			{
				switch ($p->Provider) {
					case "Facebook":
						$setting =  array ( 
							"enabled" => true,
							"keys"    => array ( "id" => $p->Provider_Key, "secret" => $p->Provider_Secret ),
							"trustForwarded" => false
						);
						break;
					case "Twitter":
						$setting = array (						
							"enabled" => true,
							"keys"    => array ( "key" => $p->Provider_Key, "secret" => $p->Provider_Secret ) 
						);
						break;
					case "Google":
						$setting = array (
							"enabled" => true,
							"keys"    => array ( "id" => $p->Provider_Key, "secret" => $p->Provider_Secret ), 
						);
						break;
				};
				$result["providers"][$p->Provider] = $setting;
			}
		}
		catch(Exception $e)
		{
			
		}		
		return $result; 
   }
   
   
   
   
   
   
   
   
}
?>
