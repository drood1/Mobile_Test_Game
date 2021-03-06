﻿using UnityEngine;
using System.Collections;

public class Player_Turning_Movement : MonoBehaviour {

    public float speed = 1;

    float run_speed;

    public float RotateSpeed = 1;

    public float jump_height = 8;

    public bool is_falling = false;
    public bool on_wall = false;

    Rigidbody this_rb;


    // Use this for initialization
    void Start () {
        this_rb = this.gameObject.GetComponent<Rigidbody>();
        run_speed = speed;
	}

    void OnCollisionEnter(Collision col)
    {
        if (is_falling == true)
        {
            if (col.gameObject.tag == "Floor" || col.gameObject.tag == "Moving_Platform" || col.gameObject.tag == "Wall")
                is_falling = false;
        }

        //parent player to the platform when landing on it in order to have player move with it
        if (col.gameObject.tag == "Moving_Platform")
        {
            transform.parent = col.gameObject.transform.parent;
        }
        if (col.gameObject.tag == "Wall")
        {
            on_wall = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        //when player jumps/falls off platform, unparent the player from it
        if (col.gameObject.tag == "Moving_Platform")
        {
            transform.parent = null;
        }
        //reset the falling boolean when the player jumps/falls off the object
        if (col.gameObject.tag == "Moving_Platform" || col.gameObject.tag == "Floor" || col.gameObject.tag == "Wall")
        {
            is_falling = true;
        }
        if (col.gameObject.tag == "Wall")
        {
            on_wall = false;
        }
    }




    //move right loop
    public IEnumerator TurnRight()
    {
        while(true)
        {
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }
    }

    //button calls this function to start moving right loop
    public void StartRight()
    {
        StartCoroutine("TurnRight");
    }
    
    public IEnumerator TurnLeft()
    {
        while (true)
        {
            transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }
    }

    public void StartLeft()
    {
        StartCoroutine("TurnLeft");
    }

    public IEnumerator MoveUp()
    {
        while(true)
        {
            transform.Translate(Vector3.forward * 0.1f * speed);
            yield return null;
        }
    }

    public void StartUp()
    {
        StartCoroutine("MoveUp");
    }

    public IEnumerator MoveDown()
    {
        while (true)
        {
            transform.Translate(Vector3.back * 0.1f * speed);
            yield return null;
        }
    }

    public void StartDown()
    {
        StartCoroutine("MoveDown");
    }

    public void Jump()
    {
        if (!(is_falling == true && on_wall == false))
        {
            on_wall = false;
            is_falling = true;
            this_rb.velocity = new Vector3(0, jump_height, 0);
        }
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(-Vector3.up * RotateSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up * RotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * 0.1f * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.forward * -0.1f * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && is_falling == false)
            Jump();

    }


}
