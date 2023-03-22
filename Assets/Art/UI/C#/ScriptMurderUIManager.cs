using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Linq;
using UnityEngine.Events;
using UnityEngine.Networking;
using static CommonVar.CommonVariable;
 

public class ScriptMurderUIManager : Singleton<ScriptMurderUIManager>
{

    string url;
    public string port;

    public PlayerInfoDatasAssets playerassets;
    public PlayerChatTestDataAssets chatAssets;

    public List<GameObject> uiPerfabs = new List<GameObject>();

    public GameObject currentPage = null;

    [HideInInspector]public ScriptInfo info;
    public UnityEvent OnScriptsInforRecived;


    [HideInInspector]public int playerCount;
    public List<ChatData> chatDatas = new List<ChatData>();

    override protected void Awake()
    {
        base.Awake();
        playerCount = playerassets.playerInfos.Count;
        url = $"http://localhost:{port}/script_murder";
    }


    IEnumerator Start()
    {
        StartCoroutine(ServerGet(url));
        yield return new WaitUntil(() => info != null);
        OnScriptsInforRecived.Invoke();
        Debug.Log(info.scriptName);
    }



    public UserData GetPlayerInfo(int index)
    {
        UserData datas = new UserData();

        datas.userName = playerassets.playerInfos[index].player.playerName;
        datas.uid = playerassets.playerInfos[index].player.uid;
        datas.netType = (UserData.NetType)playerassets.playerInfos[index].player.netType;


        datas.state = (UserData.prepareState)playerassets.playerInfos[index].charactor.state;
        datas.roalName = playerassets.playerInfos[index].charactor.roalName;
        datas.enterRoom = playerassets.playerInfos[index].charactor.enterRoom;
       

        return datas;
    }

    ChatData GetChatInfo(int playerIndex,int messageIndex)
    {
        ChatData datas = new ChatData();

        datas.deviceNicname = chatAssets.chatResponses[playerIndex].deviceNicname;
        datas.playerType = (int)chatAssets.chatResponses[playerIndex].player_Type;
        datas.messageOrder = chatAssets.chatResponses[playerIndex].messageInfos[messageIndex].chatOrder;
        datas.chatType = (int)chatAssets.chatResponses[playerIndex].messageInfos[messageIndex].chat_Type;
        datas.message = chatAssets.chatResponses[playerIndex].messageInfos[messageIndex].message;
        return datas;
    }

    public List<ChatData> CollectionChatData()
    {
        List<ChatData> chatDatas = new List<ChatData>();

        int M = chatAssets.chatResponses.Count;
        for (int i = 0; i < M; i++)
        {
            var chatResponse = chatAssets.chatResponses[i];
            for (int j = 0; j < chatResponse.messageInfos.Count; j++)
            {
                var datas = GetChatInfo(i, j);
                chatDatas.Add(datas);
            } 
        }

        var res = chatDatas.OrderBy(m => m.messageOrder).ToList();
        return res;
    }

    public static void RebuildLayout(RectTransform rt)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
        Canvas.ForceUpdateCanvases();
    }

    IEnumerator ServerGet(string url)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.error);

            }
            else
            {
                var json = request.downloadHandler.text;
                info = JsonConvert.DeserializeObject<ScriptInfo>(json);
            }
        }
    }
}
