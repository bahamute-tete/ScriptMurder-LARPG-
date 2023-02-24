using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScriptTestUIManager : MonoBehaviour
{
    [SerializeField] Transform contentBox;
    [SerializeField] GameObject playerItemPrefab;
    [SerializeField] TextMeshProUGUI waitingCounter;
    [SerializeField,Tooltip("Seconds")] float countDownTime = 30f*60f;
    float totalSeconds;

    ScriptMurderUIManager manager;
    ScriptMurderUIManager.PlayerData playerDatas;

    int playerCount;

    public List<ScriptMurderUIManager.PlayerData> players = new List<ScriptMurderUIManager.PlayerData>();
    public List<GameObject> playerItems = new List<GameObject>();

    void Awake()
    {
        manager = GameObject.Find("Canvas").GetComponent<ScriptMurderUIManager>();
        totalSeconds = countDownTime;
       
       
        CreatePlayerItemList();
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        PlayerWatingTime();
    }

    void CreatePlayerItemList()
    {
        playerCount = manager.assets.playerInfos.Count;

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
            ScriptMurderUIManager.PlayerData datas = manager.GetPlayerInfo(i);
            players.Add(datas);

            GameObject item = Instantiate(playerItemPrefab, contentBox);
            playerItems.Add(item);
        }


    }

    void PlayerWatingTime()
    {
        int playerCount = manager.playerCount;

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

    private void GameStart()
    {
        
    }
}
