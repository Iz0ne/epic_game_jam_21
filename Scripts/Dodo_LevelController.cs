using UnityEngine;
using UnityEngine.SceneManagement;

public class Dodo_LevelController : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
