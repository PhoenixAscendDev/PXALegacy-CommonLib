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
