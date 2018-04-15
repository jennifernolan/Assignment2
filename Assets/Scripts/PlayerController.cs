using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    AudioSource audio1;
    public float playerSpeed;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotspawn;
    public float fireRate;

    private float nextFire;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio1 = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotspawn.position, shotspawn.rotation);
            audio1.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.Translate(playerSpeed * Time.deltaTime * moveHorizontal, 0 , 0);

        float moveVertical = Input.GetAxis("Vertical");
        transform.Translate(0, playerSpeed * Time.deltaTime * moveVertical, 0);

        rb.position = new Vector3
        (
             Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
             0.0f,
             Mathf.Clamp(rb.position.z, boundary.zMax, boundary.zMin)
        );
    }
}
