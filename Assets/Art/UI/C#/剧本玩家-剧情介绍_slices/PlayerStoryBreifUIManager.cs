using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using UnityEngine.Events;
using static CommonVar.CommonVariable;

public class PlayerStoryBreifUIManager : MonoBehaviour
{
    [SerializeField] Sprite[] subTitleSprite, subBGSprite;
    [SerializeField] int roleIndex=0;
    [SerializeField] Image subTitle, subBG;
    [SerializeField] GameObject storyIntro, relationShip;//UI

    ScriptInfo info;

    ///////////////UI///////////////
    [SerializeField] Toggle tog_Story, tog_Relationship, tog_MyRole;


    public UnityEvent OnInfoRecived;
    //typeIndex:0=hintUIItem,1=storyUIItem,2=roleUIItem  roalIndex=index of role
    public delegate void ScripInfoDelegate(int typeIndex,int roleIndex);
    public event ScripInfoDelegate onScriptInfo;


    IEnumerator Start()
    {
        Initialize();

        yield return new WaitUntil(() => info != null);
        OnInfoRecived.Invoke();

        
    }

    private void Initialize()
    {
        storyIntro.SetActive(true);
        relationShip.SetActive(false);
        tog_Story.isOn = true;

       

        tog_Story.onValueChanged.AddListener(OnStotyInfo);
        tog_Relationship.onValueChanged.AddListener(OnRelationship);
        tog_MyRole.onValueChanged.AddListener(OnMyRoal);
    }

    private void OnStotyInfo(bool arg0)
    {
        if (tog_Story.isOn)
        {
            subBG.sprite = subBGSprite[0];
            subTitle.sprite = subTitleSprite[0];
            storyIntro.SetActive(true);
            relationShip.SetActive(false);

            if (onScriptInfo != null)
                onScriptInfo.Invoke(0, 0);
        }
    }

    private void OnMyRoal(bool arg0)
    {
        
        if (tog_MyRole.isOn)
        {
            subBG.sprite = subBGSprite[1];
            subTitle.sprite = subTitleSprite[1];
            storyIntro.SetActive(true);
            relationShip.SetActive(false);

            if (onScriptInfo != null)
                onScriptInfo.Invoke(1, roleIndex);
        }

    }

    private void OnRelationship(bool arg0)
    {
        if (tog_Relationship.isOn)
        {
            subTitle.sprite = subTitleSprite[2];
            storyIntro.SetActive(false);
            relationShip.SetActive(true);

            if (onScriptInfo != null)
                onScriptInfo.Invoke(2,0);
        }

    }





    public void OnInforRecived()
    {
        info = ScriptMurderUIManager.Instance.info;
    } 




}
