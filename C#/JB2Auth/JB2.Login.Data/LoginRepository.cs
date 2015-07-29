using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Login.Data
{
    public class LoginRepository :BaseRepository
    {
        protected LoginDataContext _dbcontext;

        public LoginRepository(LoginDataContext dbc)
        {
            this._dbcontext = dbc;
        }

        public LoginRepository() : this(new LoginDataContext())
        {

        }

        public PlayerClient GetPlayerClient(string playerID, string clientID)
        {
             var query = from i in _dbcontext.jb2login_Get_PlayerClient(playerID,clientID)
                        select (PlayerClient)getPlayerClient(i);
             return query.ToArray().FirstOrDefault();
        }

        public JB2.Login.Player GetPlayer(string playerID)
        {
            var query = from i in _dbcontext.jb2login_Profile_Get(playerID)
                        select (Player)getPlayer(i);
            return query.FirstOrDefault();
        }

        internal static JB2.Login.Player getPlayer<T>(T r) where T: class
        {
            Player result = new Player()
            {
                ID = getString(r,"ID"),
                DisplayName = getString(r,"DisplayName"),
                Birthdate = getDate(r,"Birthdate"),
                Username = getString(r,"Username"),
                Email = getString(r,"Email"),
                ProfileUrl = JB2.Gravatar.GetImageUrl(getString(r,"Email"),100,string.Empty)            
            };
            return result;
        }

        internal static PlayerClient getPlayerClient<T>(T r) where T : class
        {
            PlayerClient result = new PlayerClient()
            {
                ClientID = getString(r, "ClientID"),
                PlayerID = getString(r, "PlayerID")
            };

            string[] strscopes = getString(r, "Scope").Split(',');
            List<Enum.ClaimScope> scopes = new List<Enum.ClaimScope>();
            foreach(string scope in strscopes)
            {
                scopes.Add((Enum.ClaimScope)System.Enum.Parse(typeof(Enum.ClaimScope), scope));
            }
            result.Scope = scopes.ToArray();

            return result;
        }


    }
}
