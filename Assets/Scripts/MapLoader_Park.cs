﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapLoader_Park : MonoBehaviour {

    public GameObject loadingScreen;
    public Slider slider;


    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsync(sceneIndex));
    }
    IEnumerator LoadAsync(int sceneIndex)
    {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

            loadingScreen.SetActive(true);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);

                slider.value = progress;
                Debug.Log(progress);
                yield return null;
            }
        }
    }
