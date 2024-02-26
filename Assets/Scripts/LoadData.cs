using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI recordUI;

    private void OnGUI()
    {
        recordUI.text = PlayerPrefs.GetInt("Record").ToString();
    }
}
