using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float waitAndReload = 2f;
    [SerializeField] AudioClip crush;
    [SerializeField] AudioClip success;

    [SerializeField] ParticleSystem crushParticle;
    [SerializeField] ParticleSystem successParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabe = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabe = !collisionDisabe; // toggle collision
        }
    }

    void OnCollisionEnter(Collision other)
    {

        if (isTransitioning || collisionDisabe) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("you picked up fuel");
                break;
            default:
                StartCrashSequence();
                break;
        }
    }




    void StartSuccessSequence()
    {
        successParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        Invoke("LoadNextLevel", 3f);
        GetComponent<Movement>().enabled = false;
    }

    void StartCrashSequence()
    {
        crushParticle.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crush);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", 2f);   
    }


    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    
}
