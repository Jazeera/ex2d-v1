// ======================================================================================
// File         : Geometry2D.cs
// Author       : Wu Jie 
// Last Change  : 06/10/2011 | 17:13:16 PM | Friday,June
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
// usings
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

///////////////////////////////////////////////////////////////////////////////
// exIntersection2D
///////////////////////////////////////////////////////////////////////////////

public class exIntersection2D {

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    public static bool RectRect ( Rect _a, Rect _b ) {
        if ( (_a.xMin <= _b.xMin && _a.xMax >= _b.xMin) ||
             (_b.xMin <= _a.xMin && _b.xMax >= _a.xMin ) ) 
        {
            if ( (_a.yMin <= _b.yMin && _a.yMax >= _b.yMin) ||
                 (_b.yMin <= _a.yMin && _b.yMax >= _a.yMin ) ) 
            {
                return true;
            }
        }
        return false;
    } 
}

///////////////////////////////////////////////////////////////////////////////
// exContains2D
///////////////////////////////////////////////////////////////////////////////

public class exContains2D {

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    public static int RectRect ( Rect _a, Rect _b ) {
        if ( _a.xMin <= _b.xMin &&
             _a.xMax >= _b.xMax &&
             _a.yMin <= _b.yMin &&
             _a.yMax >= _b.yMax )
        {
            // a contains b
            return 1;
        }
        if ( _b.xMin <= _a.xMin &&
             _b.xMax >= _a.xMax &&
             _b.yMin <= _a.yMin &&
             _b.yMax >= _a.yMax )
        {
            // b contains a
            return -1;
        }
        return 0;
    }
}
