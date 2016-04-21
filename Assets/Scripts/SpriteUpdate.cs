﻿// --------------------------- SpriteUpdate.cs --------------------------------
// Author - Robert Griswold CSS 385
// Created - Apr 19, 2016
// Modified - April 21, 2016
// ----------------------------------------------------------------------------
// Purpose - Implementation for a player sprite animation behavior script. 
// Keeps track of facing, and reads input to determine primary direction.
// ----------------------------------------------------------------------------
// Notes - Commented out code is for full directional functionality, rather 
// than having the player rotate.
// ----------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class SpriteUpdate : MonoBehaviour
{
    #region Sprite variables
    enum Facing
    {
        Unknown = 0,
        South = 1,
        West = 2,
        East = 3,
        North = 4
    }

    private Animator animateComp = null;
    private Facing currentFacing;
    //private Vector3 oldPos;
    #endregion

    // Use this for initialization
    void Start ()
    {
        animateComp = GetComponent<Animator>();
        if (animateComp == null)
            Debug.LogError("Animator not found.");

        currentFacing = Facing.South;
        //oldPos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //determine delta
        //float xAxis = transform.position.x - oldPos.x;
        //float yAxis = transform.position.y - oldPos.y;
        //oldPos = transform.position;
        //float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //animate the sprite
        //updateSprite(xAxis, yAxis);
        updateSprite(0, yAxis);
    }

    #region Sprite functions
    //update the sprite facing, and animation speed
    private void updateSprite(float x, float y)
    {
        //determine direction from rotation
        //Vector3 direction = transform.right;
        //Facing newFacing = Facing.Unknown;

        //if (direction.x > direction.y) //more vertical than horizontal
        //{
        //    if (direction.x > 0)
        //        newFacing = Facing.North;
        //    else if (direction.x < 0)
        //        newFacing = Facing.South;
        //}
        //else //more horizontal than vertical
        //{
        //    if (direction.y > 0)
        //        newFacing = Facing.West;
        //    else if (direction.y < 0)
        //        newFacing = Facing.East;
        //}

        //determine strongest directional from input
        Facing newFacing = Facing.Unknown;
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
                newFacing = Facing.East;
            else
                newFacing = Facing.West;
        }
        else if (Mathf.Abs(y) > Mathf.Abs(x))
        {
            if (y > 0)
                newFacing = Facing.North;
            //else
                //newFacing = Facing.South;
        }

        //set new facing
        if (newFacing != currentFacing && newFacing != Facing.Unknown)
        {
            if (newFacing == Facing.North || newFacing == Facing.South)
                animateComp.SetBool("VerticalFacing", true);
            else
                animateComp.SetBool("VerticalFacing", false);

            animateComp.SetTrigger("NewFacing");
            currentFacing = newFacing;
        }

        //set animation speed
        animateComp.SetFloat("Horizontal", x);
        animateComp.SetFloat("Vertical", y);
    }
    #endregion
}
