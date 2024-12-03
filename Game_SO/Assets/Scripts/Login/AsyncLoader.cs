using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    //Variables
    [Header("Screens")] 
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject errorScreen;
    [SerializeField] private GameObject loginScreen;

    [Header("UI")] 
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TMP_Text percentageText;
    
    //Auth
    [SerializeField] private AuthManager authManager;

    private static AsyncLoader instance;
    [SerializeField] public string levelToLoad;

    private void Start()
    {
        authManager = FindObjectOfType<AuthManager>();

        if (authManager.connected)
        {
            loadingScreen.SetActive(true);
            errorScreen.SetActive(false);
            loginScreen.SetActive(false);
        }
        else
        {
            loadingScreen.SetActive(false);
            errorScreen.SetActive(false);
            loginScreen.SetActive(true);
        }
    }

    private void Update()
    {
        if(authManager.connected)
            StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    public void LoadLevelBtn(string levelToLoad)
    {
        SceneManager.LoadScene("Load");
        StartCoroutine(LoadLevelAsync(levelToLoad));
    }

    public IEnumerator LoadLevelAsync(string levelToLoad)
    {
        if (authManager.connected)
        {
            loginScreen.SetActive(false);
            loadingScreen.SetActive(true);

            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

            while (!loadOperation.isDone)
            {
                float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
                float percentage = (loadOperation.progress * 100) + 10;

                loadingSlider.value = progressValue;
                percentageText.text = percentage.ToString() + "%";
                yield return null;
            }
        }
        else
        {
            loadingScreen.SetActive(false);
            errorScreen.SetActive(true);
        }
    }
}
