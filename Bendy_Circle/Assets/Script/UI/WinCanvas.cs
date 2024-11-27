using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinCanvas : UICanvas
{
    // Start is called before the first frame update
    public void NextBtn()
    {
        Time.timeScale = 1;
        UIManager.Instance.CloseUI<WinCanvas>(0.2f);
        LoadNextScene();
        SoundManager.Instance.PlayVFXSound(2);
    }
    public void LoadNextScene() 
    { 
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int nextSceneIndex = currentSceneIndex + 1; 
        // Kiểm tra xem scene tiếp theo có tồn tại không
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Debug.Log("Loading next scene");
        }
        else
        {
            SceneManager.LoadScene("Home");
            Debug.Log("Home scene");
        } 
    }

    IEnumerator NextSence()
    {
        yield return new WaitForSeconds(0.3f);
        LoadNextScene();
        Debug.Log("next");
    }
}
