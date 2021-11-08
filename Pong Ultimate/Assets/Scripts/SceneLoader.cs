using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  public void LoadNextScene()
  {
    int currentScene = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentScene + 1);
  }

  public void LoadGameMenu()
  {
    SceneManager.LoadScene(0);
  }

  public void LoadScene(int id)
  {
    SceneManager.LoadScene(id);
  }

  public void QuitGame()
  {
    Application.Quit();
  }
}
