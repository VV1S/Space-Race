using UnityEngine;

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
                Debug.Log("Katastrofa");
                break;
        }
    }
}
