// ======================================================================================
// File         : exUIScrollViewEditor_.cs
// Author       : Wu Jie 
// Last Change  : 06/23/2012 | 17:36:22 PM | Saturday,June
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
// usings
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;
using System.Collections;

///////////////////////////////////////////////////////////////////////////////
// public
///////////////////////////////////////////////////////////////////////////////

[CustomEditor(typeof(exUIScrollView_))]
public class exUIScrollViewEditor_ : exUIElementEditor {

    ///////////////////////////////////////////////////////////////////////////////
    // properties
    ///////////////////////////////////////////////////////////////////////////////

    private exUIScrollView_ editScrollView;

    ///////////////////////////////////////////////////////////////////////////////
    // functions
    ///////////////////////////////////////////////////////////////////////////////

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    protected new void OnEnable () {
        base.OnEnable();
        if ( target != editScrollView ) {
            editScrollView = target as exUIScrollView_;
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

	public override void OnInspectorGUI () {

        // ======================================================== 
        // Base GUI 
        // ======================================================== 

        base.OnInspectorGUI();
        GUILayout.Space(20);

        // ======================================================== 
        // width
        // ======================================================== 

        GUI.enabled = !inAnimMode;
        editScrollView.contentWidth = EditorGUILayout.FloatField( "Content Width", editScrollView.contentWidth );
        GUI.enabled = true;

        // ======================================================== 
        // height
        // ======================================================== 

        GUI.enabled = !inAnimMode;
        editScrollView.contentHeight = EditorGUILayout.FloatField( "Content Height", editScrollView.contentHeight );
        GUI.enabled = true;

        // ======================================================== 
        // bounce
        // ======================================================== 

        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
            GUI.enabled = !inAnimMode;
            editScrollView.bounce = GUILayout.Toggle( editScrollView.bounce, "Bounce" );
            GUI.enabled = true;
        GUILayout.EndHorizontal();

        // ======================================================== 
        // bounce duration 
        // ======================================================== 

        GUI.enabled = !inAnimMode && editScrollView.bounce;
        editScrollView.bounceDuration = EditorGUILayout.FloatField( "Bounce Duration", editScrollView.bounceDuration );
        GUI.enabled = true;

        // ======================================================== 
        // scroll direction
        // ======================================================== 

        GUI.enabled = !inAnimMode;
        EditorGUIUtility.LookLikeControls ();
        editScrollView.scrollDirection = (exUIScrollView_.ScrollDirection)EditorGUILayout.EnumPopup( "Scroll Direction", editScrollView.scrollDirection, GUILayout.Width(165) );
        EditorGUIUtility.LookLikeInspector ();
        GUI.enabled = true;

        // ======================================================== 
        // damping 
        // ======================================================== 

        GUI.enabled = !inAnimMode;
        editScrollView.damping = EditorGUILayout.FloatField( "Damping", editScrollView.damping );
        GUI.enabled = true;

        // ======================================================== 
        // elasticity 
        // ======================================================== 

        GUI.enabled = !inAnimMode;
        editScrollView.elasticity = EditorGUILayout.FloatField( "Elasticity", editScrollView.elasticity );
        GUI.enabled = true;

        // ======================================================== 
        // check dirty 
        // ======================================================== 

        // DELME { 
        // if ( EditorApplication.isPlaying == false )
        //     editScrollView.Sync();
        // } DELME end 

        if ( GUI.changed )
            EditorUtility.SetDirty (editScrollView);
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    protected override void OnSceneGUI () {
        base.OnSceneGUI();

        // ======================================================== 
        // check dirty 
        // ======================================================== 

        // DELME { 
        // if ( EditorApplication.isPlaying == false )
        //     editScrollView.Sync();
        // } DELME end 

        if ( GUI.changed )
            EditorUtility.SetDirty (editScrollView);
    }
}