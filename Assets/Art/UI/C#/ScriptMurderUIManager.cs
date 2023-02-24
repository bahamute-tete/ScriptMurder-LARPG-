using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

public class ScriptMurderUIManager : MonoBehaviour
{

    public PlayerInfoDatasAssets assets;

    public class PlayerData
    {
        public string uid;
        public string playerName;
        public enum playerType { local, net }
        public playerType type;

        public string roalName;
        public enum prepareState { ready=0, notReady, prepare }
        public prepareState state;
        public bool enterRoom;


    }

    [HideInInspector]public int playerCount;


    private void Awake()
    {
        playerCount = assets.playerInfos.Count;
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

        datas.playerName = assets.playerInfos[index].player.playerName;
        datas.uid = assets.playerInfos[index].player.uid;
        datas.type = (PlayerData.playerType)assets.playerInfos[index].player.type;


        datas.state = (PlayerData.prepareState)assets.playerInfos[index].charactor.state;
        datas.roalName = assets.playerInfos[index].charactor.roalName;
        datas.enterRoom = assets.playerInfos[index].charactor.enterRoom;
       

        return datas;
    }
}
