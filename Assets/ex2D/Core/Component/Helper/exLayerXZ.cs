// ======================================================================================
// File         : exLayerXZ.cs
// Author       : Wu Jie 
// Last Change  : 09/01/2011 | 15:53:13 PM | Thursday,September
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
// usings
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

///////////////////////////////////////////////////////////////////////////////
// defines
// NOTE: without ExecuteInEditMode, we can't not drag and create mesh in the scene 
///////////////////////////////////////////////////////////////////////////////

[ExecuteInEditMode]
[AddComponentMenu("ex2D Helper/2D Layer XZ")]
public class exLayerXZ : exLayer2D {

    ///////////////////////////////////////////////////////////////////////////////
    // functions
    ///////////////////////////////////////////////////////////////////////////////

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    override public float CalculateDepth ( Camera _cam ) {
        if ( _cam == null )
            return 0.0f;
        float dist = _cam.farClipPlane - _cam.nearClipPlane;
        float unitLayer = dist/MAX_LAYER;
        return -(((float)layer_ + bias_) * unitLayer + _cam.transform.position.y + _cam.nearClipPlane);
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    override public void UpdateTransformDepth () { 
        if ( Mathf.Approximately(depth_, transform.position.y) == false ) {
            transform.position = new Vector3( transform.position.x,
                                              depth_,
                                              transform.position.z );
        }
    }

}