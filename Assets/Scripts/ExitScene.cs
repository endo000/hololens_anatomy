using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    public void GoToBodyAnatomyScene()
    {
        SceneManager.LoadSceneAsync("BodyAnatomy");
    }
}