using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour {

    //player's particle System
    ParticleSystem particle;
    //player's rigidbody
    Rigidbody rigidbody;
    void Start()
    {
        //getComponent
        particle = GetComponent<ParticleSystem>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Default move
        rigidbody.AddForce(Vector3.forward * 100.0f * Time.deltaTime);

    }

    //Player's Collision
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Rigidbody>().isKinematic = true;
        Explode();
        Destroy(gameObject, particle.duration);
    }

    //play particle system
    void Explode()
    {

        particle.Play();

    }
}
