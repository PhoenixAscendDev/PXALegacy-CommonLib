<?php
 
namespace JBsquared\Common;

use Mysqli; 
 
class Repository {


	private static $basedb;
	private $db;
	public $format = 'json';
	private static $initialized = false;
	public $lastInsert_id;
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
		//echo "db connection destroyed";
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
	
	public function query($query)
	{
		$result = $this->db->query($query);
		if($result == TRUE)
			$this->lastInsert_id = $this->db->insert_id;
		return $result;
	}
	
	public function run($sql,$paramType)
	{
		$numargs = func_num_args();
		
		$arg_list = func_get_args();
		
		$stmt = $this->db->prepare($sql);
		$params = array();
		
		//var_dump($stmt);
		//$params[] = & $paramType;
		
		if ($numargs > 2) {
			$n = count($arg_list);
			for($i = 1; $i < $n; $i++) {
				$params[] = & $arg_list[$i];
			}	
		}
		
		if($stmt === false) {
			trigger_error('Wrong SQL: ' . $sql . ' Error: ' . $this->db->errno . ' ' . $this->db->error, E_USER_ERROR);
		}
		
		call_user_func_array(array($stmt, 'bind_param'), $params);
		
		$stmt->execute();
		
		return $this->returnObject($stmt->get_result());
		
	}
  	
	public function output($query)
	{
		//var_dump($query);
		$sqlResults = $this->db->query($query);
		$results = array();
		
		if($sqlResults != null)
		{
			$sqlResults->data_seek(0);
			while ($obj = $sqlResults->fetch_object()) {
				array_push($results,$obj);
			}

			if( count($results) == 1)
				return $results[0];
			else
			return $results;
		}
		else
			return null;
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
	
	private function returnObject($sqlResults)
	{
		$results = array();
		
		if($sqlResults != null)
		{

			$sqlResults->data_seek(0);
			while ($obj = $sqlResults->fetch_object()) {
				array_push($results,$obj);
			}

			if( count($results) == 1)
				return $results[0];
			else
			return $results;
		}
		return null;		
	}
	
	
	
	
	
	
		
	
}