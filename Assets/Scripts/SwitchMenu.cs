using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenu : MonoBehaviour
{
    /// <summary>
    /// Кнопка, которая будет скрываться/появляться
    /// </summary>
    public GameObject ButtonStart;

    private bool currState = true;

    public void Switch()
    {
        if (currState)
            currState = false;
        else
            currState = true;

            this.ButtonStart.SetActive(currState);
    }
}
