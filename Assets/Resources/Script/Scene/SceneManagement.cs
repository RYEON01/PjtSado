using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public string sceneToLoad;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Birth2" && Input.GetKeyDown(KeyCode.Space))
        {
            LoadScene();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadScene();
        }
    }

    public void LoadSceneOnClick()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
 
 