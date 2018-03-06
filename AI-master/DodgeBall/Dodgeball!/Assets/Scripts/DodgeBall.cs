using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeBall : MonoBehaviour
{
    private Rigidbody _rb;

	// Use this for initialization
	void Start ()
    {
        _rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void addForce(Vector3 v)
    {
        _rb.AddForce(v);
    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Agent"))
        {
            GetComponent<Collider>().isTrigger = false;
        }
    }
}
