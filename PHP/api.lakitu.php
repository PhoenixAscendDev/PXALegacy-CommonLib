<?php
/**
 * Lakitu API Wrapper
 * For use with the Lakitu Video Game API
 *
 * @author Joshua Bennett <http://about.me/jasonclemons>
 * @version 1.0
 * @license DBAD <http://philsturgeon.co.uk/code/dbad-license/
 * @todo null
 */
  
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
  
  

class LakituApi {

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
  

  
  public function fetch_game($id)
  {
    $query = $this->dbstatements['game']." where game.ID = '".$id."'";
    return $this->output($query);
  }
  
  public function fetch_hardware($id)
  {
    $query = $this->dbstatements['hardware']." where hardware.ID = '".$id."'";
	return $this->output($query);
	//var_dump($query);
	
  }
  
  
  public function fetch_pokemon($id)
  {
    //var_dump($this->dbstatements);
     $query = $this->dbstatements['pokemon']." where pokemon.Pokedex_Number = '".$id."'";
   //var_dump($query);
   return $this->output($query);
  }
  
  public function fetch_member($id)
  {
    $query = $this->dbstatements['member']."where member.ID = '".$id."'  group by fmg.member_id ";
    //var_dump($this->wpdbPrefix);
    //var_dump($query);
    return $this->output($query);
  }
  
  
  
  
  
  public function fetch_games($searchParameters)
  { 
    foreach($searchParameters as $sp)
    {
    }
  }
    
    public function fetch_miiversePosts($memberid)
    {
        $query = $this->dbstatements['miiversePost']." where member.ID = '".$memberid."'";
    return $this->output($query);
    }
    
    public function fetch_miiversePost($communityid,$memberid)
    {
      $query = $this->dbstatements['miiversePost']." where miiversePost.Community_ID = '".$communityid."' and miiversePost.Member_ID = '".$memberid."'";
    return $this->output($query);
    }
  
  public function fetch_memberGame($gameid,$memberid)
  {
    $query = $this->dbstatements['membergame']." where membergame.Game_ID = '".$gameid."' and membergame.Member_ID = '".$memberid."'";
    return $this->output($query);
  }
  
  public function fetch_memberPlatform($platformid,$memberid)
  {
    $query = $this->dbstatements['memberplatform']." where memberplatform.Platform_ID = '".$platformid."' and memberplatform.Member_ID = '".$memberid."'";
    return $this->output($query);
  }
  
  public function fetch_memberPlatformAll()
  {
    $query = $this->dbstatements['memberplatform']." order by memberplatform.Total_Games desc";
    return $this->output($query);
	}
	  
  public function fetch_platform($id)
  {
    $query = $this->dbstatements['platform']." where platform.ID = '".$id."'";
    return $this->output($query);
  }
  
  public function fetch_company($id)
  {
    $query = $this->dbstatements['company']." where company.ID = '".$id."'";
    return $this->output($query);
  }
  
  public function fetch_character($id)
  {
    $query = $this->dbstatements['character']." where character.ID = '".$id."'";
    return $this->output($query);
  }
    
   public function fetch_franchise($id)
  {
    $query = $this->dbstatements['franchise']." where franchise.ID = '".$id."'";
    return $this->output($query);
  }
    

  

    
  public function __construct() {
    $this->dbsql = new mysqli("mysql.jbsquared.com", "jbsquared_dbo", "webpass21", "lakitu");
    $this->dbstatements = array();
    $this->dbstatements['game'] = "select game.ID,game.Title,p.ID as Platform_ID,p.Title as Platform_Title,
                                               game.Image_Url,
                                               game.Game_Type,
                                               game.Image_Thumb_Url,game.Image_Tiny_Url,game.Image_Small_Url,game.Image_Icon_Url,
                                               game.Image_Icon_Url as Icon_Url,     
                                               game.GiantBomb_ID,game.Port_Game_ID,
                                               game.YearReleased as Release_Year,game.MonthReleased as Release_Month,
                                               game.Port_Platform_ID,
                                               p2.Title as Port_Platform_Title,
                                               f.Title as Format_Title,
                                               f.ID as Format_ID,
                                               (select sum(fmg.Total_Minutes)
                                                   from lakitu.Fact_MemberGame as fmg
                                                   where fmg.Game_ID = game.ID
                                                ) as Total_Minutes,
                                               (select count(fmg.Total_Entries)
                                                   from lakitu.Fact_MemberGame as fmg
                                                  where fmg.Game_ID = game.ID
                                               ) as Total_Entries,
                                               tags.Tags,
                                               game.Microsoft_TitleID,
                                               'game' as Object_Type,
                                               game.ID as ID,
                                               game.Parent_Game_ID,
                                               ifnull(gd.Company_ID,0) as Developer_Company_ID,
                                               ifnull(gp.Company_ID,0) as Publisher_Company_ID,
                                               ifnull(game.RemakeOf_Game_ID,0) as RemakeOf_Game_ID,
                                                                                              chars.Characters as Characters
                                          from lakitu.Game as game
                                               
                                       left join lakitu.Fact_GameTags as tags on (tags.Game_ID = game.ID)
                                       left join lakitu.Format as f on (f.ID = game.Format_ID)
                                       left join lakitu.Platform as p on (p.ID = game.Platform_ID)
                                       left join lakitu.Platform as p2 on (p2.ID = game.Port_Platform_ID)
                                       left join lakitu.Game_Developer as gd on (gd.Game_ID = game.ID and gd.Is_Main = 1)
                                                                              left join lakitu.vGameCharactersCSV as chars on (chars.Game_ID = game.ID)
                                       left join lakitu.Game_Publisher as gp on (gp.Game_ID = game.ID and gp.Is_Main =1)";
    $this->dbstatements["membergame"] = "select game.ID,game.Title,p.ID as Platform_ID,p.Title as Platform_Title,                       
                                   game.Image_Url,game.GiantBomb_ID,game.Port_Game_ID,
                                   game.Game_Type,
                                   game.YearReleased as Release_Year,game.MonthReleased as Release_Month,
                                   game.Port_Platform_ID,
                                   p2.Title as Port_Platform_Title,
                                   f.Title as Format_Title,
                                   f.ID as Format_ID,
                                   game.Parent_Game_ID as Parent_Game_ID,
                                   fmg.Total_Minutes,
                                   fmg.Total_Entries,
                                   membergame.Completed,
                                   membergame.DateCompleted,
                                   membergame.Cost,
                                   membergame.DatePurchased,
                                   membergame.Member_ID,
                                   membergame.Online_AccountID,
                                   membergame.Notes,
                                   (select 
                                         case 
                                             when membergame.Condition = 'L' then gv.Price_L
                                             when membergame.Condition = 'NIB' then gv.Price_NIB
                                             when membergame.Condition = 'CIB' then gv.Price_CIB
                                         end
                                      from Game_Value as gv
                                     where gv.Game_ID = game.ID) as 'MarketValue',
                                   membergame.Condition,                       
                                   membergame.Store,
                                   tags.Tags,
                                   'membergame' as Object_Type,
                                                                       ifnull(gd.Company_ID,0) as Developer_Company_ID,
                                               ifnull(gp.Company_ID,0) as Publisher_Company_ID,
                                                                      ifnull(game.RemakeOf_Game_ID,0) as RemakeOf_Game_ID
                              from lakitu.Game as game
                         left join lakitu.Fact_GameTags as tags on (tags.Game_ID = game.ID)
                         left join lakitu.Format as f on (f.ID = game.Format_ID)
                         left join lakitu.Platform as p on (p.ID = game.Platform_ID)
                         left join lakitu.Platform as p2 on (p2.ID = game.Port_Platform_ID)
                         left join lakitu.Member_Game as membergame on (membergame.Game_ID = game.ID)
                         left join lakitu.Fact_MemberGame as fmg on (fmg.Game_ID = game.ID and fmg.Member_ID = membergame.Member_ID)
                                       left join lakitu.Game_Developer as gd on (gd.Game_ID = game.ID and gd.Is_Main = 1)
                                       left join lakitu.Game_Publisher as gp on (gp.Game_ID = game.ID and gp.Is_Main =1)";
    $this->dbstatements["platform"] = "select platform.*,platform.Image_Url,'platform' as Object_Type,
                                    (select sum(fmg.Total_Minutes)
                                       from lakitu.Fact_MemberGame as fmg,
                                            lakitu.Game as g
                                      where fmg.Game_ID = g.ID
                                        and ( ((g.Platform_ID = platform.ID) and (g.Port_Platform_ID < 1))
                                             or
                                             (g.Port_Platform_ID = platform.ID)
                                            )
                                      ) as Total_Minutes,
                                    (select count(fmg.Total_Entries)
                                       from lakitu.Fact_MemberGame as fmg,
                                            lakitu.Game as g
                                      where fmg.Game_ID = g.ID
                                        and ( ((g.Platform_ID = platform.ID) and (g.Port_Platform_ID < 1))
                                             or
                                             (g.Port_Platform_ID = platform.ID)
                                            )
                                      ) as Total_Entries,
                                    (select count(mg.Game_ID)
                                       from lakitu.Member_Game as mg,
                                            lakitu.Game as g 
                                       where g.ID = mg.Game_ID
                                        and ( ((g.Platform_ID = platform.ID) and (g.Port_Platform_ID < 1))
                                             or
                                             (g.Port_Platform_ID = platform.ID)
                                            )
                                     ) as Total_Games
                                  from lakitu.Platform as platform ";
$this->dbstatements["pokemon"] = "select pokemon.*,'pokemon' as Object_Type, pokemon.Pokedex_Number as ID from lakitu.Pokedex as pokemon";
//$this->dbstatements["franchise"] = "select franchise.*,(select count(gf.Game_ID))";                                                                            from lakitu.Franchise as franchise ";
$this->dbstatements["tag"] = "select tag.*,'tag' as Object_Type from lakitu.vTags as tag ";
$this->dbstatements["hardware"] = "select hardware.*,'hardware' as Object_Type from lakitu.Hardware as hardware ";
$this->dbstatements["gameValue"] = "select 'gameValue' as Object_Type,
                                            gv.Prive_L as 'Price',
                                            gv.Source as 'Source,
                                            gv.Date_Entered as 'DateUpdated',
                                            g.ID as 'Game_ID'
                                      from  Game_Value as gamevalue,
                                            Game as g
                                      where g.ID = gv.Game_ID
                                     order by gv.Date_Entered ";                                                    
$this->dbstatements["member"] = "select member.*, 'member' as Object_Type, 
                                     sum(fmg.Total_Entries) as Total_Entries, 
                                     sum(fmg.Total_Minutes) as Total_Minutes, 
                                      sum(fmg.IsCompleted) as Total_Completed
                                 from lakitu.Member as member 
                                 left join lakitu.Fact_MemberGame as fmg on (fmg.Member_ID = member.ID)
                                 ";
$this->dbstatements["memberplatform"] = "select memberplatform.*,p.Title as Platform_Title from Fact_MemberPlatform as memberplatform join Platform as p on p.ID = memberplatform.Platform_ID";
$this->dbstatements["memberAchievement"] = "select memberAchievement.Member_ID,
                                                 memberAchievement.Game_ID,
                                                 memberAchievement.Achievement_ID,
                                                 memberAchievement.Achievement_ID as 'ID',
                                                 memberAchievement.Achievement_System,
                                                 memberAchievement.Date_Earned,
                                                 memberAchievement.Points,
                                                 ga.Image_URL as 'Image_Thumb_Url',
                                                 ga.Title,
                                                 ga.Description,
                                                 'memberAchievement' as Object_Type
                                            from lakitu.Member_Achievement as memberAchievement
                                            join lakitu.Game_Achievement as ga on ga.Achievement_ID = memberAchievement.Achievement_ID and ga.Game_ID = memberAchievement.Game_ID ";
$this->dbstatements["company"] = "select company.ID,
                                      company.Name as Title,
                                      company.GiantBomb_ID,
                                      company.Parent_Company_ID,
                                      ifnull(pc.Name,'') as Parent_Company_Title,
                                      company.Image_Url,
                                      company.Image_Url as Icon_Url,
                                      'company' as Object_Type
                                 from Company as company
                                 left join Company as pc on pc.ID = company.Parent_Company_ID";
$this->dbstatements["character"] = "select character.ID,
                                               character.Title,
                                               character.GiantBomb_ID,
                                               character.GiantBomb_Type,
                                               character.Icon_Url,
                                               (select count(Game_ID) from Game_Lookup where Lookup_ID = `character`.ID) as Total_Games,
                                               (select sum(fmg.Total_Minutes) from lakitu.Fact_MemberGame as fmg,Game_Lookup as l where fmg.Game_ID = l.Game_ID and l.Lookup_ID = `character`.ID) as Total_Minutes,
                                               'character' as Object_Type
                                         from  lakitu.vCharacters as `character`";
  $this->dbstatements["miiversePost"] = "select miiversePost.ID,";
  
    if ($this->format == 'json') {
      if (!function_exists('json_decode')) {
        throw new Exception('The JSON library could not be loaded, and is needed for the JSON format.');
      }
    }
  }
  
  
  
  
}
  
?>