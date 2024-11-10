using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneButton : MonoBehaviour
{
    public void SceneCheongi()
    {
        SceneManager.LoadScene("Cheongi");
    }
    
    public void SceneJuon()
    {
        SceneManager.LoadScene("Juon");
    }
    
    public void SceneBaeka()
    {
        SceneManager.LoadScene("Baeka");
    }
    
    public void SceneMuksa()
    {
        SceneManager.LoadScene("Muksa");
    }
}
