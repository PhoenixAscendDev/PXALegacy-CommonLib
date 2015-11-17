using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common.Attributes;

namespace JB2.Common
{
    public class USAStates : BaseCollection<StateProvidence>
    {
        #region Fields

        Dictionary<Enum.USAStateType, StateProvidence> _dictionay;

        #endregion Fields

        #region Constructors

        public USAStates() : base()
        {
            _dictionay = new Dictionary<Enum.USAStateType, StateProvidence>();

            var states = System.Enum.GetValues(typeof(Enum.USAStateType));

            foreach(Enum.USAStateType state in states)
            {
                StateProvidence sp = new StateProvidence(state.ToString(), state.GetAttributeOfType<Description>().Value);
                _dictionay.Add(state,sp);
            }

            _list = _dictionay.Values;
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Methods

        public StateProvidence this[Enum.USAStateType state]
        {
            get
            {
                return _dictionay[state];
            }

            set
            {
                _dictionay[state] = value;
                _list = _dictionay.Values;
            }
        }

        #endregion Methods
    }
}
