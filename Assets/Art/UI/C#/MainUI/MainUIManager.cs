using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class MainUIManager : MonoBehaviour
{
    [SerializeField] Button sideNavigatorBtn;
    [SerializeField] GameObject blackMask;
    bool isActiveMask = false;

    // Start is called before the first frame update
    void Start()
    {
        sideNavigatorBtn.onClick.AddListener(OnSideNavigatorClick);
    }

    private void OnSideNavigatorClick()
    {
        blackMask.SetActive(isActiveMask = !isActiveMask);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        blackMask.SetActive(isActiveMask);
    }
}
