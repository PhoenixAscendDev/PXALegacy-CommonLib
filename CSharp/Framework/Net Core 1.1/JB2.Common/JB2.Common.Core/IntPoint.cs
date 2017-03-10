using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public class TinyPoint : Point<byte>
    {
        public TinyPoint(byte x, byte y)
        {
            _x = x;
            _y = y;
        }
        public override void Offset(byte x, byte y)
        {
            this._x = Convert.ToByte(this._x + x);
            this._y = Convert.ToByte(this._y + y);
        }
    }

    public class SmallPoint : Point<short>
    {
        public SmallPoint(short x, short y)
        {
            _x = x;
            _y = y;
        }

        public override void Offset(short x, short y)
        {
            this._x = Convert.ToInt16(this._x + x);
            this._y = Convert.ToInt16(this._y + y);
        }
    }

    public class Point : Point<int>
    {
        public Point(int x, int y)
        {
            _x = x;
            _y = y;
        }

        #region Methods
        public override void Offset(int x, int y)
        {
            this._x = this._x + x;
            this._y = this._y + y;
        }
        #endregion Methods
    }
}
