using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B3.Test.Logger
{
    public enum ESecureLevel
    {
        ALL = 0x11,

        SEE_ONLY = 0x01,
        SET_ONLY = 0x10,

        NONE = 0x00
    }
}
