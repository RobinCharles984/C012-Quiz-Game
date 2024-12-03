using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.instance.ToggleSFX();
    }
}
