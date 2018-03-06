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
    private BasicVelocity playerTarget;
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
		if(currentState == AIStates.attack && ball.parent)
        {
            agent.destination = playerTarget.transform.position;
            if((playerTarget.transform.position - transform.position).sqrMagnitude < 80f)
            {
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.parent = null;
                ball.GetComponent<DodgeBall>().addForce((playerTarget.transform.position - ball.transform.position) * 100f);
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
        }


        
	}

    void OnCollisionEnter(Collision c)
    {
        //pick up ball
        if(c.gameObject.CompareTag("ball") && throwTimer <= 0)
        {
            c.gameObject.GetComponent<Collider>().isTrigger = true;
            c.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ball = c.transform;
            ball.parent = transform;
            ball.localPosition = new Vector3(0.5f, 1, 0);
            currentState = AIStates.attack;
        }
    }
}
