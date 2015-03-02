<?php
 
namespace JBsquared\Common;
 
 
public class Repository {

	public function __construct() {
		$this->db = new mysqli("mysql.jbsquared.com", "jbsquared_api", "api1029JB2", "jbsquared_appdata");
		$this->dbstatements['application'] = "select ID,Name,ApplicationType from Application as application";
		if ($this->format == 'json') {
			if (!function_exists('json_decode')) {
				throw new Exception('The JSON library could not be loaded, and is needed for the JSON format.');
			}
		}
	}
	
	// Destructor - close DB connection
    public function __destruct() {
		$this->db->close();
    }
  
	private $db;
	
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