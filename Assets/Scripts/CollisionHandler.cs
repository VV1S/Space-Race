using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Startujemy!");
                break;

            case "Finish":
                Debug.Log("Dobra robota!");
                break;

            case "Fuel":
                Debug.Log("It's time to spread democracy!");
                break;

            default:
                ReloadLevel();
                break;
        }
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //It assures that current scene will be realoaded.
        SceneManager.LoadScene(currentSceneIndex);
    }
}
