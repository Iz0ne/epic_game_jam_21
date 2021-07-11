using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_Start : MonoBehaviour
{
    private void OnMouseDown()
    {
        SceneManager.LoadScene("Dodo_64");
    }
}
