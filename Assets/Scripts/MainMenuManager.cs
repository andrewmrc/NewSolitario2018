﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public void LoadScene(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

}
