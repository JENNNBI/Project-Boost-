using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    
    [SerializeField] float forcePower = 5f;
    [SerializeField] float rotatePower = 5f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;
    [SerializeField] ParticleSystem leftBoosterParticle;


    AudioSource audioSource;

    Rigidbody rb;


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
        ProcessRotaiton();
    }

    void ProcessThrust()
    {
        StartThrusting();
    }

    void ProcessRotaiton()
    {
        StartRotation();
    }


    void StartThrusting()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * forcePower * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                mainBoosterParticle.Play();
                audioSource.PlayOneShot(mainEngine);
            }

        }
        else
        {
            audioSource.Stop();
            mainBoosterParticle.Stop();
        }
    }

    

    void StartRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {

            ApplyRotation(rotatePower);
            if (!rightBoosterParticle.isPlaying)
            {
                rightBoosterParticle.Play();
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {

            ApplyRotation(-rotatePower);
            if (!leftBoosterParticle.isPlaying)
            {
                leftBoosterParticle.Play();
            }
        }
        else
        {
            leftBoosterParticle.Stop();
            rightBoosterParticle.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //unfreezing rotation so the physics system can take over
    }
}
