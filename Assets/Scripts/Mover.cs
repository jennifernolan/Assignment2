using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = -transform.right * speed;//the speed at which the spiders move
	}

}
