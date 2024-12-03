using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameInputField : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.instance.nameInputBoolean)
        {
            gameObject.SetActive(false);
        }
    }
}