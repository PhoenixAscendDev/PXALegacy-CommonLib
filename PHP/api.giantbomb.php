<?php
/**
 * GiantBomb API Wrapper
 * For use with the GiantBomb.com Video Game API
 *
 * @author Joshua Bennett <http://about.me/jasonclemons>
 * @version 1.0
 * @license DBAD <http://philsturgeon.co.uk/code/dbad-license/
 * @todo null
 */

class GiantBombApi {

  private $endpoint = 'http://api.giantbomb.com/';
  public $timeout = 8;
  public $format = 'json'; //Can be json, xml, or php
  private $apikey = 'd633f9e8c7dbb47875e03b01de83617a2e88d26f';
  

  
  public function __construct() {
    if ($this->format == 'json') {
      if (!function_exists('json_decode')) {
        throw new Exception('The JSON library could not be loaded, and is needed for the JSON format.');
      }
    }
  }
  
  
  public function fetch($id,$objecttype,$fields = '')
  {
    switch (strtolower($objecttype))
    {
        case "game":
         return $this->fetch_game($id,$fields);
        break;
      case "concept":
        return $this->fetch_concept($id,$fields);
        break;
      case "character":
        return $this->fetch_character($id,$fields);
        break;
      case "object":
        return $this->fetch_object($id,$fields);
        break;     
    }
  }
  
 
  public function fetch_game($id,$fields)
  {
    $parameters = array('format' => $this->format);
    $parameters['api_key'] = $this->apikey;
    if($fields != '')
      $parameters['field_list'] = $fields;
      //array_push($parameters,'field_list' => $fields);
    
      return $this->http('game',$id,$parameters);
  }
  
  public function fetch_concept($id,$fields)
  {
    $parameters = array('format' => $this->format);
    $parameters['api_key'] = $this->apikey;
    if($fields != '')
      $parameters['field_list'] = $fields;
      //array_push($parameters,'field_list' => $fields);
    
      return $this->http('concept',$id,$parameters);    
  }
  
  public function fetch_object($id,$fields)
  {
        $parameters = array('format' => $this->format);
    $parameters['api_key'] = $this->apikey;
    if($fields != '')
      $parameters['field_list'] = $fields;
      //array_push($parameters,'field_list' => $fields);
    
      return $this->http('object',$id,$parameters);    
    
    
  }
  
  public function fetch_character($id,$fields)
  {
    $parameters = array('format' => $this->format);
    $parameters['api_key'] = $this->apikey;
    if($fields != '')
      $parameters['field_list'] = $fields;
      //array_push($parameters,'field_list' => $fields);
    
      return $this->http('character',$id,$parameters);    
  }
  
  
    protected function output($data) {
    switch ($this->format) {
      case 'xml':
        return simplexml_load_string($data);
        break;
      case 'json':
        return json_decode($data);
        break;
      case 'php':
        return unserialize($data);
        break;
    }

    return false;
  }

  protected function http($objectType,$id,$parameters = array()) {
    $url = $this->endpoint.'/'.$objectType.'/'.$id.'/';
    $url .= (!empty($parameters)) ? '?' . http_build_query($parameters, null, '&') : '';
    $result = file_get_contents($url);
    return $this->output($result);
  }
  
  
 
  
}



?>