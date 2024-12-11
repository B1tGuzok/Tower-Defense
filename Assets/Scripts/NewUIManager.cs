using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewUIManager : MonoBehaviour
{
    public static NewUIManager main;

    private bool isHoveringUI;

    private void Awake()
    {
        main = this;
    }

    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        return isHoveringUI;
    }

}
