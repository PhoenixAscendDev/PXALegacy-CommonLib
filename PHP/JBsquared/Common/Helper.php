<?php
 
namespace JBsquared\Common;
 
 
class Helper {
	private function __construct() {}
    private static $greeting = 'Hello';
    private static $initialized = false;
    private static $httpcodes = Array(
        100 => 'Continue',
        101 => 'Switching Protocols',
        200 => 'OK',
        201 => 'Created',
        202 => 'Accepted',
        203 => 'Non-Authoritative Information',
        204 => 'No Content',
        205 => 'Reset Content',
        206 => 'Partial Content',
        300 => 'Multiple Choices',
        301 => 'Moved Permanently',
        302 => 'Found',
        303 => 'See Other',
        304 => 'Not Modified',
        305 => 'Use Proxy',
        306 => '(Unused)',
        307 => 'Temporary Redirect',
        400 => 'Bad Request',
        401 => 'Unauthorized',
        402 => 'Payment Required',
        403 => 'Forbidden',
        404 => 'Not Found',
        405 => 'Method Not Allowed',
        406 => 'Not Acceptable',
        407 => 'Proxy Authentication Required',
        408 => 'Request Timeout',
        409 => 'Conflict',
        410 => 'Gone',
        411 => 'Length Required',
        412 => 'Precondition Failed',
        413 => 'Request Entity Too Large',
        414 => 'Request-URI Too Long',
        415 => 'Unsupported Media Type',
        416 => 'Requested Range Not Satisfiable',
        417 => 'Expectation Failed',
        500 => 'Internal Server Error',
        501 => 'Not Implemented',
        502 => 'Bad Gateway',
        503 => 'Service Unavailable',
        504 => 'Gateway Timeout',
        505 => 'HTTP Version Not Supported'
    );
	
	private static function initialize()
    {
        if (self::$initialized)
            return;
			
        self::$initialized = true;
    }
	
	// Helper method to get a string description for an HTTP status code
	// From http://www.gen-x-design.com/archives/create-a-rest-api-with-php/ 
	public static function getHttpStatusCodeMessage($status)
	{
		self::initialize();
		return (isset($httpcodes[$status])) ? $httpcodes[$status] : '';
	   
	}
	
	// Helper method to send a HTTP response code/message
	public static function sendHttpResponse($status = 200, $body = '', $content_type = 'text/html')
	{
		self::initialize();
		$status_header = 'HTTP/1.1 ' . $status . ' ' . self::getHttpStatusCodeMessage($status);
		header($status_header);
		header('Content-type: ' . $content_type);
		echo $body;
	}
	
	public static function transform_HTML($string, $length = null) 
	{
		// Helps prevent XSS attacks
		// Remove dead space.
		$string = trim($string);
		// Prevent potential Unicode codec problems.
		$string = utf8_decode($string);
		// HTMLize HTML-specific characters.
		$string = htmlentities($string, ENT_NOQUOTES);
		$string = str_replace("#", "&#35;", $string);
		$string = str_replace("%", "&#37;", $string);
		$length = intval($length);
		if ($length > 0) {
			$string = substr($string, 0, $length);
		}
		return $string;
	} 
}
?>