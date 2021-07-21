using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    //PARAMETERS - for tunning, typically set in the editor
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem rocketJetParticle;
    [SerializeField] ParticleSystem leftThrustleParticle;
    [SerializeField] ParticleSystem rightThrustleParticle;


    //CACHE - e.g. references for readability or speed
    Vector3 myNewPosition;

    //STATE - private instance (member) variables
    Rigidbody rigidBody;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            TurnOnMainEngine();
        }
        else
        {
            TurnOffMainEngine();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        else
        {
            leftThrustleParticle.Stop();
            rightThrustleParticle.Stop();
        }

    }

    void TurnOnMainEngine()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!rocketJetParticle.isPlaying)
        {
            rocketJetParticle.Play();
        }
    }

    void TurnOffMainEngine()
    {
        rocketJetParticle.Stop();
        audioSource.Stop();
    }

    void TurnLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftThrustleParticle.isPlaying)
        {
            leftThrustleParticle.Play();
        }
    }

    void TurnRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightThrustleParticle.isPlaying)
        {
            rightThrustleParticle.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }



}