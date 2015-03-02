<?php
/**
 * JBsquared API Common
 * For use with JBsquared web applications
 *
 * @author Joshua Bennett <http://about.me/joshua_bennett>
 * @version 1.0
 * @license 
 * @todo null
 */
 
 abstract class AuthType
 {
    const JBsquared = 0;
	const Facebook = 1;
 }
  
class CompareType {
   const EqualTo = 0;
   const LessThan = 1;
   const LessThanEqual = 2;
   const GreaterThan = 3;
   const GreaterThanEqual = 4;
   const In = 5;
   const Contains = 6;
   const StartsWith = 7;
   const EndsWith = 8; 
}
    
  
class SearchParameter {
  public $name;
  public $value;
  public $compareType = CompareType::EqualTo; 
}
  
  

class JBsquaredApi {

  private $endpoint = 'http://api.giantbomb.com/';
  public $timeout = 8;
  public $format = 'json'; //Can be json, xml, or php
  private $giantbombApiKey = 'd633f9e8c7dbb47875e03b01de83617a2e88d26f';
  private $dbpsql;
  private $dbStatements;
  private $wpdbPrefix = 'lakituschronicle_net.dbo.wp_22tmub_';
  
  
  
  
  private function output($query)
  {
    //var_dump($query);
    $sqlResults = $this->dbsql->query($query);
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
  
   private function outputWithParam($query,$paramType,$param)
  {
    $sqlStmt = 	 $this->dbsql->prepare($query);
	
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
   
  public function get_application($id)
  {
    $query = $this->dbstatements['application']." where application.ID = ?";
    return $this->outputWithParam($query,"s",$id);
  } 


  

    
  public function __construct() {
    $this->dbsql = new mysqli("mysql.jbsquared.com", "jbsquared_api", "api1029JB2", "jbsquared_appdata");
    $this->dbstatements = array();
	
	$this->dbstatements['application'] = "select ID,Name,ApplicationType from Application as application";

  
    if ($this->format == 'json') {
      if (!function_exists('json_decode')) {
        throw new Exception('The JSON library could not be loaded, and is needed for the JSON format.');
      }
    }
  }
  
  
  
  
}
  
?>