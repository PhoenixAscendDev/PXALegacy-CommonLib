<?php
 
namespace JBsquared\Common;

use Mysqli; 
 
class Repository {


	private static $basedb;
	private $db;
	public $format = 'json';
	private static $initialized = false;
	private static $dbstatements = Array( 
	 'application' => "select ID,Name,ApplicationType,AuthU,AuthP from Application as application"	
	);
	
	public function __construct($appkey) {
		//$this->basedb = new mysqli("mysql.jbsquared.com", "jbsquared_api", "api1029JB2", "jbsquared_appdata");
		$this->format = 'json';
		if ($this->format == 'json') {
			if (!function_exists('json_decode')) {
				throw new Exception('The JSON library could not be loaded, and is needed for the JSON format.');
			}
		}
		
		
		if( Repository::isValidkey($appkey))
		{
			$app = Repository::get_appInfo($appkey);
			//var_dump($app);
			$this->db = new mysqli("mysql.jbsquared.com", $app->AuthU, $app->AuthP, "jbsquared_appdata");
		}
	}
	
	// Destructor - close DB connection
    public function __destruct() {
		$this->db->close();
    }
	
	
	
	private static function initialize()
    {
        if (self::$initialized)
            return;
			
        self::$initialized = true;
		self::$basedb = new mysqli("mysql.jbsquared.com", "jbsquared_api", "api1029JB2", "jbsquared_appdata");
    }
	
	private static function isValidkey($appkey)
	{
		self::initialize();

		$sqlResults = self::$basedb->query(self::$dbstatements['application'].' where AppKey = "'.$appkey.'"');

		
		return $sqlResults->num_rows == 1;
	}

	private static function get_appInfo($appkey)
	{
		self::initialize();
		$sqlstatement = self::$dbstatements['application'].' where AppKey = "'.$appkey.'"';
		$sqlResults = self::$basedb->query(self::$dbstatements['application'].' where AppKey = "'.$appkey.'"');
		
		$results = array();
		$sqlResults->data_seek(0);
		while ($obj = $sqlResults->fetch_object()) {
			array_push($results,$obj);
		}

		if( count($results) == 1)
			return $results[0];
		else
		return $results;
		
	}	
  	
	public function output($query)
	{
		//var_dump($query);
		$sqlResults = $this->db->query($query);
		$results = array();
		

		$sqlResults->data_seek(0);
		while ($obj = $sqlResults->fetch_object()) {
			array_push($results,$obj);
		}

		if( count($results) == 1)
			return $results[0];
		else
		return $results;
	}
	
	public function outputWithParam($query,$paramType,$param)
	{
		$sqlStmt = 	 $this->db->prepare($query);

		$sqlStmt->bind_param($paramType,$param);
		$sqlStmt->execute();

		$sqlResults = $sqlStmt->get_result();

		$results = array();

		$sqlResults->data_seek(0);
		while ($obj = $sqlResults->fetch_object()) {
			array_push($results,$obj);
		}

		if( count($results) == 1)
			return $results[0];
		else
		return $results;
	}
	
	
	
	
		
	
}