using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class SmallNumberRange : IRange<byte>
    {

        #region Fields

        private byte _min;
        private byte _max;

        #endregion Fields

        #region Properties
        public byte Max
        {
            get
            {
                return _max;
            }

            set
            {
                _max = value;
            }
        }

        public byte Min
        {
            get
            {
                return _min;
            }

            set
            {
                _min = value;
            }
        }

        #endregion Properties

        #region Methods

        public bool IsWithinRange(byte value)
        {
            return (Enumerable.Range(_min, _max-_min).Contains(value));
        }

        #endregion Methods
    }
}
