using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public interface IPlayerRepo : IRepository<IPlayer,string>
    {

        void Insert(PlayerProfile profile);
        void Delete(PlayerProfile profile);
        PlayerProfile GetPlayerProfileByID(string id);
        IEnumerable<PlayerProfile> GetPlayerProfileByPlayerID(string playerid);


    }
}
