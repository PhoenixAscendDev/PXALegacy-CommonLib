using System;
using System.Collections.Generic;
using System.Text;

using JB2.Common.Attributes;

using JB2.Helpers;

namespace JB2.Common
{
    public class USAStates : BaseCollection<StateProvince>
    {
        #region Fields

        Dictionary<Enum.USAStateType, StateProvince> _dictionay;

        #endregion Fields

        #region Constructors

        public USAStates() : base()
        {
            _dictionay = new Dictionary<Enum.USAStateType, StateProvince>();

            var states = System.Enum.GetValues(typeof(Enum.USAStateType));

            foreach(Enum.USAStateType state in states)
            {



                StateProvince sp = new StateProvince(state.ToString(), state.GetAttributeOfType<Description>().Value);
                _dictionay.Add(state,sp);
            }

            _list = _dictionay.Values;
        }

        #endregion Constructors

        #region Properties

        #endregion Properties

        #region Methods

        public StateProvince this[Enum.USAStateType state]
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
