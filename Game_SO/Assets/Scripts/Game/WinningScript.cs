using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinningScript : MonoBehaviour
{
    //Variables
    [Header("Components")]
    [SerializeField] private GameObject winningCanvas;
    [SerializeField] private CameraMovement camMovement;

    private void Awake()
    {
        camMovement.enabled = false;
    }

    private void Start()
    {
        winningCanvas.SetActive(true);
    }

    private void Update()
    {
        //Checking if its touching screen
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Load");
        }
    }
}
