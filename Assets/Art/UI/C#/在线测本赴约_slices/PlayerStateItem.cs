using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerStateItem : MonoBehaviour
{
    [SerializeField] Sprite[] state_sprites;
    [SerializeField] GameObject[] hints = new GameObject[3];

    Image bgimage => GetComponent<Image>();
    Transform state_0=> transform.Find("State_0");
    Transform state_1=> transform.Find("State_1");

    public TextMeshProUGUI roalName;
    [SerializeField] GameObject blackMask;
    

    ScriptTestUIManager scriptTestManager;
    int index;
    bool enterRoom;


    ScriptMurderUIManager.PlayerData playerData;
    //////////////////////////////DEBUGINFO///////
    [Header("Debug")]
    [SerializeField]string defaultStrPlayer;
    [SerializeField]string defaultStrCharactor;
    
    private void Awake()
    {
        scriptTestManager = GameObject.Find("Canvas").transform.Find("UI_ScriptTest").GetComponent<ScriptTestUIManager>();
        blackMask.SetActive(false);
    }

    private void Start()
    {
        InitializedPlayerItemCard();
    }

    void InitializedPlayerItemCard()
    {
        index = transform.GetSiblingIndex();
        playerData = scriptTestManager.players[index];

        
        enterRoom = scriptTestManager.players[index].enterRoom;

        int type = (int)playerData.netType;

        if ( type==0)
        {
            ShowLocalPlayerUI();
        }
        else
        {
            ShowNetlPlayerUI();
        }

    }

    void ShowLocalPlayerUI()
    {
        bgimage.sprite = state_sprites[0];

        state_0.gameObject.SetActive(true);
        state_1.gameObject.SetActive(false);

        enterRoom = true;

        blackMask.SetActive(String.IsNullOrEmpty(playerData.roalName));

        if (String.IsNullOrEmpty(playerData.roalName))
        {
            roalName.text = defaultStrPlayer;
            roalName.color = new Color(0.598f, 0.598f, 0.598f, 1f);
            
        }
        else
        {
            roalName.text = playerData.roalName;
            roalName.color = Color.black;
           
        }

    }

    void ShowNetlPlayerUI()
    {
        bgimage.sprite = state_sprites[1];

        state_0.gameObject.SetActive(false);
        state_1.gameObject.SetActive(true);

        

        if (String.IsNullOrEmpty(playerData.roalName))
        {
            roalName.text = defaultStrCharactor;

            if (enterRoom)
            {
                blackMask.SetActive(!String.IsNullOrEmpty(playerData.roalName));
                roalName.color = Color.black;
                int index = (int)playerData.state == 1 ? 2 : (int)playerData.state;
                UpdateHintText(index);
            }
            else
            {
                blackMask.SetActive(String.IsNullOrEmpty(playerData.roalName));
                roalName.color = new Color(0.365f, 0.365f, 0.365f, 1f);
                UpdateHintText(1);
            }
           
        }
        else
        {
            blackMask.SetActive(String.IsNullOrEmpty(playerData.roalName));
            roalName.text = playerData.roalName;
            roalName.color = Color.black;
            int index = (int)playerData.state == 1 ? 2 : (int)playerData.state;
            UpdateHintText(index);
          
        }

    }

    private void UpdateHintText(int index)
    {
        foreach (var o in hints)
        {
            o.SetActive(false);
        }

        hints[index].SetActive(true);
    }

    public virtual void SelectedCharctor() { }




}
