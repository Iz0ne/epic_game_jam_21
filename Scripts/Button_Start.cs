using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Dodo_64");
    }
}
