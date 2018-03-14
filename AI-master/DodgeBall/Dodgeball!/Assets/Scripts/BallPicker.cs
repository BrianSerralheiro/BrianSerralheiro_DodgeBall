using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BallPicker : MonoBehaviour
{
    private AIAgentController aiAgent;
    [SerializeField]
    private Transform ball;
    [SerializeField]
    private Transform[] enemyTeam;
    [SerializeField]
    private float throwTimer;

    [SerializeField]
    private AIStates currentState;

    //AI Movement
    [SerializeField]
    private NavMeshAgent agent;

    enum AIStates
    {
        wander, attack, defend
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(currentState == AIStates.attack && ball.parent == transform)
        {
            agent.destination = enemyTeam[Random.Range(0, enemyTeam.Length)].position;
            if(Mathf.Abs(transform.position.z) < 3)
            {
                ball.GetComponent<DodgeBall>().throwed = true;
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.parent = null;
                ball.GetComponent<DodgeBall>().addForce((agent.destination - ball.transform.position) * 110f);
                throwTimer = 0.5f;
            }
        }
        if(throwTimer > 0)
        {
            throwTimer -= Time.deltaTime;
            if(throwTimer <= 0)
            {
                currentState = AIStates.wander;
            }
        }

        if(currentState == AIStates.wander)
        {
            agent.destination = ball.position;
            if(ball.parent)
            {
                currentState = AIStates.defend;
            }
        }

        if(currentState == AIStates.defend)
        {
            agent.destination = new Vector3(0, 0, Mathf.Abs(transform.position.z) / transform.position.z * 30) ;
            if(!ball.parent)
            {
                currentState = AIStates.wander;
            }
        }


        
	}

    void OnCollisionEnter(Collision c)
    {
        //pick up ball
        if(c.gameObject.CompareTag("ball") && throwTimer <= 0)
        {
            if(!c.gameObject.GetComponent<DodgeBall>().throwed)
            {
                c.gameObject.GetComponent<Collider>().isTrigger = true;
                c.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                ball = c.transform;
                ball.parent = transform;
                ball.localPosition = new Vector3(0.5f, 1, 0);
                currentState = AIStates.attack;
            }
            else
            {
                enabled = false;
                agent.destination = new Vector3(100, 0, 0);
            }
        }
    }
}
