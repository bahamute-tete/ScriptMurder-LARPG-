using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.Linq;
using static ScriptMurderUIManager;

public class ScriptMurderUIManager : MonoBehaviour
{
    public PlayerInfoDatasAssets playerassets;
    public PlayerChatTestDataAssets chatAssets;

    public class PlayerData
    {
        public string uid;
        public string playerName;
        public string roalName;

        public enum NetType { local, net }
        public NetType netType;

        
        public enum prepareState { ready=0, notReady, prepare }
        public prepareState state;

        public bool enterRoom;
    }
    public class ChatData 
    {

        public string deviceNicname;
        public int playerType;
        public int chatType;
        public int messageOrder;
        public string message;
    }


    [HideInInspector]public int playerCount;
    public List<ChatData> chatDatas = new List<ChatData>();

    private void Awake()
    {

        playerCount = playerassets.playerInfos.Count;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public PlayerData GetPlayerInfo(int index)
    {
        PlayerData datas = new PlayerData();

        datas.playerName = playerassets.playerInfos[index].player.playerName;
        datas.uid = playerassets.playerInfos[index].player.uid;
        datas.netType = (PlayerData.NetType)playerassets.playerInfos[index].player.netType;


        datas.state = (PlayerData.prepareState)playerassets.playerInfos[index].charactor.state;
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

        //foreach (var o in res)
        //{
        //    Debug.Log(o.messageOrder);
        //}

        return res;
    }
}
