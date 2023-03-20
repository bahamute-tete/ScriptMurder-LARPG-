using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;
using UnityEngine.Events;

public class PlayerStoryBreifUIManager : MonoBehaviour
{
    [SerializeField] string TestServerUrl;
    [SerializeField] int roleIndex=0;
    [SerializeField] Header header;
    [SerializeField] Image subTitle, subBG;
    [SerializeField] Sprite[] subTitleSprite, subBGSprite;
    [SerializeField] GameObject storyIntro, relationShip;//UI


    ///////////////Json///////////////
    [System.Serializable]
    public class ScriptInfo
    {
        public string scriptName;
        public string scriptHint;
        public string roleHint;
        public string scriptStory;
        public List<scriptRoal> scriptRole = new List<scriptRoal>();
    }

    [System.Serializable]
    public class scriptRoal
    {
        public string name;
        public string intro;
        public string details;
        public string avatarTexture;
        public string roleStory;
    }
    public ScriptInfo info;

    ///////////////UI///////////////
    [SerializeField] Toggle tog_Story, tog_Relationship, tog_MyRole;


    public UnityEvent onInfoRecived;
    //typeIndex:0=hintUIItem,1=storyUIItem,2=roleUIItem  roalIndex=index of role
    public delegate void ScripInfoDelegate(int typeIndex,int roalIndex);
    public event ScripInfoDelegate onScriptInfo;
 

    IEnumerator Start()
    {
        //get data from server
        StartCoroutine(GetStoryBriefUIInfo(TestServerUrl));
        yield return new WaitUntil(() => info != null);
        onInfoRecived.Invoke();

        Initialize();
    }

    private void Initialize()
    {
        storyIntro.SetActive(true);
        relationShip.SetActive(false);

        tog_Story.isOn = true;

        header.title.text = info.scriptName;
        header.gamePlayeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "结束当前环节";
        header.btnHint.transform.parent.gameObject.SetActive(false);

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

    IEnumerator GetStoryBriefUIInfo( string url)
    {
       
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(request.error);
            }
            else
            {
                var scriptDatas = request.downloadHandler.text;
                info = JsonConvert.DeserializeObject<ScriptInfo>(scriptDatas);
            }
        }
    }



}
