using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
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
        //todo add SFX upon crash
        //todo add particle effect upon crash
        GetComponent<Move>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void LoadNextLevelSequence()
    {
        //todo add SFX upon success
        //todo add particle effect upon success
        GetComponent<Move>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
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
