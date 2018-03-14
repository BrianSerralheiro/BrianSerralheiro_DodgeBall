﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBall : MonoBehaviour
{
    private Rigidbody _rb;

    public bool throwed;

	// Use this for initialization
	void Start ()
    {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(transform.position.y < -5)
        {
            //Respawning the ball
            transform.position = new Vector3(Random.Range(-4,4), 2, Random.Range(-4,4));
            //To reset the ball velocity
            _rb.velocity = Vector3.zero;
        }
	}

    public void addForce(Vector3 v)
    {
        _rb.AddForce(v);
    }

    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.CompareTag("Ground"))
        {
            throwed = false;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Agent"))
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
