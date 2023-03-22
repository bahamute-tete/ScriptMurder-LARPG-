using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Networking;
using static CommonVar.CommonVariable;


public class ScriptTestUIManager : MonoBehaviour
{
    [SerializeField] Transform contentBox,chatContent;
    [SerializeField] GameObject playerItemPrefab, chatItemPrefab;
    [SerializeField] TextMeshProUGUI waitingCounter;
    [SerializeField,Tooltip("Seconds")] float countDownTime = 30f*60f;
    float totalSeconds;

    ScriptMurderUIManager manager;
    UserData playerDatas;

    int playerCount,messageCount;

    public List<UserData> players = new List<UserData>();
    public List<ChatData> chatMessages = new List<ChatData>();
    public List<GameObject> playerItems = new List<GameObject>();
    public List<GameObject> chatItmes = new List<GameObject>();



    ///////////////////////////////UI/////////
    [SerializeField] Button chatViewBtn, chatPublicBtn, chatPrivateBtn, chatAudioBtn, storyInroBtn;
    [SerializeField] GameObject chatView, storyInfoView;
    [SerializeField] TextMeshProUGUI storyTextContent;
    [SerializeField]bool isFolded_storyView = true, isFolded_chatView=true;
   

    private void Awake()
    {

        manager = GameObject.Find("Canvas").GetComponent<ScriptMurderUIManager>();
       
        totalSeconds = countDownTime;
    }

    void Start()
    {
        ////Button callback
        chatViewBtn.onClick.AddListener(OnChatView);
        chatPublicBtn.onClick.AddListener(OnPublicChatView);
        chatPrivateBtn.onClick.AddListener(OnPrivateChatView);
        chatAudioBtn.onClick.AddListener(OnAudioChatView);
        storyInroBtn.onClick.AddListener(OnStoryInfo);
       

        playerCount = manager.playerCount;
        ///Create player and chat ui item  only for debug
        CreatePlayerItemList();
        CreateChatItemList();
    }

    private void OnStoryInfo()
    {
        StartCoroutine(GetStoryInfo());

        //chatView and storyView exchange
        isFolded_storyView = !isFolded_storyView;

        if (!isFolded_chatView)
        {
            chatView.GetComponent<LerpAnim>().Active();
            isFolded_chatView = true;
        }

        IconDirectionChange();
    }

    private void OnChatView()
    {




        //chatView and storyView exchange
        isFolded_chatView = !isFolded_chatView;

        if (!isFolded_storyView)
        {
            storyInfoView.GetComponent<LerpAnim>().Active();
            isFolded_storyView = true;
        }

        IconDirectionChange();

    }

    private void IconDirectionChange()
    {
        RectTransform iconRtc = chatViewBtn.GetComponent<RectTransform>();

        Vector3 scale = (isFolded_chatView) ? new Vector3(1, 1, 1) : new Vector3(1, -1, 1);

        iconRtc.localScale = scale;
    }

    private void OnAudioChatView()
    {
        
    }

    private void OnPrivateChatView()
    {
    }

    private void OnPublicChatView()
    {  
    }

   



    void Update()
    {
        PlayerWatingTime();
    }



    /////////////// Only for Debug///////////////
    void CreatePlayerItemList()
    {
     

        if (contentBox.childCount != 0)
        {
            for (int i = 0; i < contentBox.childCount; i++)
            {
                Destroy(contentBox.GetChild(i).gameObject);
            }
            playerItems.Clear();
        }

        for (int i = 0; i < playerCount; i++)
        {
            UserData datas = manager.GetPlayerInfo(i);
            players.Add(datas);

            GameObject item = Instantiate(playerItemPrefab, contentBox);
            playerItems.Add(item);
        }


    }
    void CreateChatItemList()
    {
        
        for (int i = 0; i < chatContent.childCount; i++)
        {
            Destroy(chatContent.GetChild(i).gameObject);
        }
        chatItmes.Clear();

        chatMessages = manager.CollectionChatData();

        for (int i = 0; i < chatMessages.Count; i++)
        {
            GameObject item = Instantiate(chatItemPrefab, chatContent);
            chatItmes.Add(item);
        }
    }
    /////////////// Only for Debug///////////////


    //Countdown
    void PlayerWatingTime()
    {
       

        if (playerCount == 0)
        {
            totalSeconds = countDownTime;
            return;
        }

        CountDowmTime();
    }
    private void CountDowmTime()
    {

        totalSeconds -= Time.deltaTime;

        int hours =(int) totalSeconds / 3600;
        string hh = hours < 10 ? "0" + hours : hours.ToString();
        int minutes =(int) (totalSeconds - hours * 3600) / 60;
        string mm = minutes < 10f ? "0" + minutes : minutes.ToString();
        int seconds =(int) totalSeconds - hours * 3600 - minutes * 60;
        string ss = seconds < 10 ? "0" + seconds : seconds.ToString();
        waitingCounter.text = string.Format("{0}:{1}:{2}", hh, mm, ss);

       
        if (totalSeconds <= 0)
        {
            GameStart();
        }
    }

    //MainProcess
    private void GameStart()
    {
        
    }

    //GET storyInfo on Server(python-flask)
    IEnumerator GetStoryInfo()
    {

        string url = "http://localhost:2020/script_murder";
        
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
                var res = request.downloadHandler.text;
                showResult($"{res}", storyTextContent);
            }
        }
    }

    private void showResult(string text,TextMeshProUGUI content)
    {
        content.alignment = TextAlignmentOptions.Left;
        content.text = text;

    }
}
