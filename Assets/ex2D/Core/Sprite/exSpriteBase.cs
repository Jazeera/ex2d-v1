// ======================================================================================
// File         : exSpriteBase.cs
// Author       : Wu Jie 
// Last Change  : 08/06/2011 | 21:18:47 PM | Saturday,August
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
// usings
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;

///////////////////////////////////////////////////////////////////////////////
// defines
///////////////////////////////////////////////////////////////////////////////

[ExecuteInEditMode]
public class exSpriteBase : exPlane {

    ///////////////////////////////////////////////////////////////////////////////
    // properties
    ///////////////////////////////////////////////////////////////////////////////

    [SerializeField] protected Vector2 scale_ = Vector2.one;
    public Vector2 scale {
        get { return scale_; }
        set { 
            if ( scale_ != value ) {
                scale_ = value;
                updateFlags |= UpdateFlags.Vertex;
            }
        }
    }

    [SerializeField] protected Vector2 shear_ = Vector2.zero;
    public Vector2 shear {
        get { return shear_; }
        set { 
            if ( shear_ != value ) {
                shear_ = value;
                updateFlags |= UpdateFlags.Vertex;
            }
        }
    }

    [SerializeField] protected bool autoResizeCollision_ = true;
    public bool autoResizeCollision {
        get { return autoResizeCollision_; }
        set {
            if ( autoResizeCollision_ != value ) {
                autoResizeCollision_ = value;

                BoxCollider boxCol = gameObject.GetComponent<BoxCollider>();
                MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
                if ( meshFilter != null ) {
                    UpdateBoxCollider ( boxCol, meshFilter.sharedMesh );
                }
            }
        }
    }

    ///////////////////////////////////////////////////////////////////////////////
    // functions
    ///////////////////////////////////////////////////////////////////////////////

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    override protected void OnEnable () {
        base.OnEnable();
        exPixelPerfect ppf = GetComponent<exPixelPerfect>();
        if ( ppf ) {
            ppf.enabled = true;
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    override protected void OnDisable () {
        base.OnDisable();
        exPixelPerfect ppf = GetComponent<exPixelPerfect>();
        if ( ppf ) {
            ppf.enabled = false;
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    void Reset() {
        if ( GetComponent<exLayer2D>() == null ) {
            gameObject.AddComponent<exLayer2D>();
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    public void AddMeshCollider () {
        if ( collider == null ) {
            MeshCollider meshCol = gameObject.AddComponent<MeshCollider>();
            if ( meshCol && meshFilter ) {
                meshCol.sharedMesh = meshFilter.sharedMesh;
            }
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    public void AddBoxCollider () {
        if ( collider == null ) {
            BoxCollider boxCol = gameObject.AddComponent<BoxCollider>();
            UpdateBoxCollider ( boxCol, meshFilter.sharedMesh );
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    public void UpdateBoxCollider ( BoxCollider _boxCol, Mesh _mesh ) {
        if ( _boxCol == null || _mesh == null || autoResizeCollision == false )
            return;

        _boxCol.center = _mesh.bounds.center;
        _boxCol.size = _mesh.bounds.size;
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    public Bounds CalculateBounds ( float _offsetX, float _offsetY,
                                    float _halfWidth, float _halfHeight )
    {
        switch ( plane ) {
        case exSprite.Plane.XY:
            return new Bounds (  new Vector3( -_offsetX, _offsetY, 0.0f ), 
                                 new Vector3( _halfWidth * 2.0f, _halfHeight * 2.0f, 0.2f ) );
        case exSprite.Plane.XZ:
            return new Bounds (  new Vector3( -_offsetX, 0.0f, _offsetY ), 
                                 new Vector3( _halfWidth * 2.0f, 0.2f, _halfHeight * 2.0f ) );
        case exSprite.Plane.ZY:
            return new Bounds (  new Vector3( 0.0f, _offsetY, -_offsetX ), 
                                 new Vector3( 0.2f, _halfHeight * 2.0f, _halfWidth * 2.0f ) );
        default:
            return new Bounds (  new Vector3( -_offsetX, _offsetY, 0.0f ), 
                                 new Vector3( _halfWidth * 2.0f, _halfHeight * 2.0f, 0.2f ) );
        }
    } 
}

