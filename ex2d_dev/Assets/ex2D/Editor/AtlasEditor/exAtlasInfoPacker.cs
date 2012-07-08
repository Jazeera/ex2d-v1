// ======================================================================================
// File         : exAtlasInfoPacker.cs
// Author       : Wu Jie 
// Last Change  : 08/27/2011 | 10:35:11 AM | Saturday,August
// Description  : 
// ======================================================================================

///////////////////////////////////////////////////////////////////////////////
// usings
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

///////////////////////////////////////////////////////////////////////////////
// AtlasPacker
///////////////////////////////////////////////////////////////////////////////

partial class exAtlasInfo {

    ///////////////////////////////////////////////////////////////////////////////
    // class Node
    // 
    // Purpose: 
    // 
    ///////////////////////////////////////////////////////////////////////////////

    class Node {
        public Rect rect;
        public Node[] child;

        // ------------------------------------------------------------------ 
        // Desc: 
        // ------------------------------------------------------------------ 

        public Node ( Rect _rect ) {
            rect = _rect;
        }

        // ------------------------------------------------------------------ 
        // Desc: 
        // ------------------------------------------------------------------ 

        public Node Insert ( exAtlasInfo.Element _el ) {
            Node node = null;

            //
            if ( child != null ) {
                node = child[0].Insert(_el);
                if ( node == null ) {
                    return child[1].Insert(_el);
                }
                else {
                    return node;
                }
            }

            // determine trimmed and padded sizes
            float trimmedWidth = _el.trimRect.width;
            float trimmedHeight = _el.trimRect.height;
            float paddedWidth = trimmedWidth + _el.atlasInfo.actualPadding;
            float paddedHeight = trimmedHeight + _el.atlasInfo.actualPadding;

            // trimmed element size must fit current node rect
            if (trimmedWidth <= rect.width && trimmedHeight <= rect.height)
            {
                child = new Node[2];

                // create first child node in remaining space to the right, using trimmedHeight
                // so that only other elements with the same height or less can be added there
                // (we do not use paddedHeight, because the padding area is reserved and should
                // not be occupied)
                child[0] = new Node( new Rect ( rect.x + paddedWidth, 
                                                rect.y,
                                                rect.width - paddedWidth, 
                                                trimmedHeight ) );

                // create second child node in remaining space at the bottom, occupying the entire width
                child[1] = new Node( new Rect ( rect.x,
                                                rect.y + paddedHeight,
                                                rect.width, 
                                                rect.height - paddedHeight ) );

                node = new Node( new Rect ( rect.x, 
                                            rect.y, 
                                            paddedWidth,
                                            paddedHeight ) );
            }
            return node;
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    void TreePack () {
        int i = 0; 
        Node root = new Node( new Rect( 0,
                                        0,
                                        width,
                                        height ) );
        foreach ( exAtlasInfo.Element el in elements ) {
            Node n = root.Insert (el);
            if ( n == null ) {
                Debug.LogError( "Failed to layout element " + el.texture.name );
                break;
            }
            el.coord[0] = (int)n.rect.x;
            el.coord[1] = (int)n.rect.y;
            ++i;
        }
    }

    // ------------------------------------------------------------------ 
    // Desc: 
    // ------------------------------------------------------------------ 

    void BasicPack () {
        int curX = 0;
        int curY = 0;
        int maxY = 0; 
        int i = 0; 

        foreach ( Element el in elements ) {
            if ( (curX + el.Width()) > width ) {
                curX = 0;
                curY = curY + maxY + actualPadding;
                maxY = 0;
            }
            if ( (curY + el.Height()) > height ) {
                Debug.LogError( "Failed to layout element " + el.texture.name );
                break;
            }
            el.coord[0] = curX;
            el.coord[1] = curY;

            curX = curX + el.Width() + actualPadding;
            if (el.Height() > maxY) {
                maxY = el.Height();
            }
            ++i;
        }
    }

    // ------------------------------------------------------------------ 
    /// Layout elements by the exAtlasInfo.algorithm
    // ------------------------------------------------------------------ 

    public void LayoutElements () {
        ResetElements();
        SortElements();

        // this is very basic algorithm
        if ( algorithm == exAtlasInfo.Algorithm.Basic ) {
            BasicPack ();
        }
        else if ( algorithm == exAtlasInfo.Algorithm.Tree ) {
            TreePack ();
        }
        EditorUtility.SetDirty(this);

        //
        foreach ( exAtlasInfo.Element el in elements ) {
            AddSpriteAnimClipForRebuilding(el);
        }

        needLayout = false;
    }
}


