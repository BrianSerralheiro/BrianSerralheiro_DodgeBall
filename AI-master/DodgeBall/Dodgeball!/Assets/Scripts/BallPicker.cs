using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPicker : MonoBehaviour
{
    [SerializeField]
    private AIAgentController aiAgent;
    [SerializeField]
    private Transform ball;
    [SerializeField]
    private BasicVelocity playerTarget;

    private float throwTimer;

    [SerializeField]
    private AIStates currentState;

    enum AIStates
    {
        wander, attack, defend
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(currentState == AIStates.attack)
        {
            aiAgent.OnDestinationFound(playerTarget.transform.position);
            if((playerTarget.transform.position - transform.position).sqrMagnitude < 25f)
            {
                ball.GetComponent<Rigidbody>().isKinematic = false;
                ball.GetComponent<BallProjectile>().throwBall(playerTarget);
                throwTimer = 2f;
                ball.parent = null;
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
        
	}

    void OnCollisionEnter(Collision c)
    {
        //pick up ball
        if(c.gameObject.CompareTag("ball") && throwTimer <= 0)
        {
            c.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ball = c.transform;
            ball.parent = transform;
            ball.localPosition = new Vector3(0.5f, 1, 0);
            currentState = AIStates.attack;
        }
    }
}
