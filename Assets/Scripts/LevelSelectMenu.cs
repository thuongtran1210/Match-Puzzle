using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public string mainMenu = "SceneMain";
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

}
