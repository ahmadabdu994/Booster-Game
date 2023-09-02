using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 100f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip rocketAudio;
    [SerializeField] ParticleSystem RightSideRocketParticel;
    [SerializeField] ParticleSystem LeftSideRocketParticel;
    [SerializeField] ParticleSystem MainRocketParticel;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (Input.GetKey(KeyCode.W))
        {
            StartThursting();
        }
        else
        {
            StopThursting();
        }
    }

    void StopThursting()
    {
        audioSource.Stop();
        MainRocketParticel.Stop();
    }

    void StartThursting()
    {
        rb.AddRelativeForce(Vector3.up * moveSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(rocketAudio);
            MainRocketParticel.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();

        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();

        }
        else
        {
            StopRotation();
        }
    }

    void StopRotation()
    {
        LeftSideRocketParticel.Stop();
        RightSideRocketParticel.Stop();
    }

    void RotateRight()
    {
        applayRotation(-rotationSpeed);
        if (!RightSideRocketParticel.isPlaying)
        {
            RightSideRocketParticel.Play();
        }
    }

    void RotateLeft()
    {
        applayRotation(rotationSpeed);
        if (!LeftSideRocketParticel.isPlaying)
        {
            LeftSideRocketParticel.Play();
        }
    }

    void applayRotation(float rotationDirection)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationDirection * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
