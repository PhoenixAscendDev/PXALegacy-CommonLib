<?php
 
namespace JBsquared\Common;

use JBsquared\Common\Interfaces\IBaseObject;

class JB2Object implements IBaseObject  {

	protected $repo;
	public $id;
	protected $dbstatements = Array();
	
	
    public function __construct($repo) {
	  $this->repo = $repo;
	  $this->id = 0;
   }
   
	protected function fill($o) {
		$this->id = $o->ID;
	}
	
	
	public function isNew() {
		return $this->id == 0;
	}
}


?>