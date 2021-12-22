using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMgr : SingletonAuto<InputMgr>
{
    //[NonSerialized]
    public float mouseX;
    //[NonSerialized]
    public float mouseY;
    //[NonSerialized]
    public float mouseScroll;
    // [NonSerialized]
    public float horizontal;
    // [NonSerialized]
    public float vertical;
    
    void FixedUpdate()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        
    }
}