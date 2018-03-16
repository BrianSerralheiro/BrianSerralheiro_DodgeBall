﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerThrow : MonoBehaviour
{
    [SerializeField]
    private Transform ball;

    [SerializeField]
    private float throwTimer;

    void Update()
    {
        if (throwTimer > 0)
        {
            throwTimer -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball.GetComponent<DodgeBall>().throwed = true;
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.parent = null;
            ball.GetComponent<DodgeBall>().addForce(transform.forward * 1300f + Vector3.up * 100);
            throwTimer = 2f;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        //pick up ball
        if (c.gameObject.CompareTag("ball") && throwTimer <= 0)
        {
            if (!c.gameObject.GetComponent<DodgeBall>().throwed)
            {
                c.gameObject.GetComponent<Collider>().isTrigger = true;
                c.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                ball = c.transform;
                ball.parent = transform;
                ball.localPosition = new Vector3(0.5f, 1, 0);
            }
            else
            {
                SceneManager.LoadScene("WhoWins");
            }
        }
    }


}
