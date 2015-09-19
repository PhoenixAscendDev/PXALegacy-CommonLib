using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace JB2.Login
{
    public class Player : JB2.Common.IPerson<string>
    {
        public string DisplayName { get;set;}
        public DateTime Birthdate { get; set; }
        public Common.Name NameInfo {get;set;}
        public string ID {get;set;}
        public string Name {get;set;}
        public string ProfileUrl { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
