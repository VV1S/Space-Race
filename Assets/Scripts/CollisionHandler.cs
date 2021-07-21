using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //PARAMETERS - for tunning, typically set in the editor
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip soundOfSuccess;
    [SerializeField] AudioClip soundOfMisery;

    [SerializeField] ParticleSystem explosionOfSuccess;
    [SerializeField] ParticleSystem explosionOfMisery;

    //CACHE - e.g. references for readability or speed

    //STATE - private instance (member) variables
    AudioSource audioSource;
    BoxCollider boxCollider;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<BoxCollider>();
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            CollisionSwitch();
        }
    }


    void CollisionSwitch()
    {
        collisionDisabled = !collisionDisabled;
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }

            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Startujemy!");
                    break;

                case "Finish":
                    LoadNextLevelSequence();
                    break;

                case "Fuel":
                    Debug.Log("It's time to spread democracy!");
                    break;

                default:
                    StartCrashSequence();
                    break;
        }
    }
    void StartCrashSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundOfMisery);
        explosionOfMisery.Play();
        GetComponent<Move>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        isTransitioning = true;
    }
    void LoadNextLevelSequence()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundOfSuccess);
        explosionOfSuccess.Play();
        GetComponent<Move>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
        isTransitioning = true;
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //It assures that current scene will be realoaded.
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings)//It prevents from counting too far.
        {
            nextSceneIndex = 0;
        }
            SceneManager.LoadScene(nextSceneIndex);
    }
}
