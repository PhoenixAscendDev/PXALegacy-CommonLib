using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class ProfileCollection : BaseCollection<PlayerProfile>
    {
        #region Fields

        private Dictionary<string, PlayerProfile> _dictionary;

        #endregion Fields

        #region Constructors
   
        public ProfileCollection(PlayerProfile packet) : this(new PlayerProfile[1] { packet })
        {

        }

        public ProfileCollection(IEnumerable<PlayerProfile> packets) : base(packets)
        {
            _dictionary = packets.ToDictionary(x => x.ID, x => x);

        }

        #endregion Constructors



        #region Properties

        #endregion Properties

        #region Methods

        public ServiceResult Add(PlayerProfile packet)
        {
            ServiceResult result = true;

            if (!_dictionary.ContainsKey(packet.ID))
            {
                _dictionary.Add(packet.ID, packet);
                _list = _dictionary.Values.ToList();
            }
            else
                result = false;

            return result;



        }

        public PlayerProfile FindByPacketID(string packetID)
        {
            return _dictionary[packetID];
        }

        #endregion Methods

        #region Implicit Operators

        public static implicit operator ProfileCollection(List<PlayerProfile> l)
        {
            return new ProfileCollection(l);
        }

        #endregion Implicit Operators

        #region Private


        private static Dictionary<string, PlayerProfile> toDic(IEnumerable<PlayerProfile> list)
        {
            Dictionary<string, PlayerProfile> dic = new Dictionary<string, PlayerProfile>();
            foreach (PlayerProfile packet in list)
            {
                if (!dic.ContainsKey(packet.ID))
                    dic.Add(packet.ID, packet);
            }

            return dic;
        }



        #endregion Private

    }
}
