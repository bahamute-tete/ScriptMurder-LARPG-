using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInfoDatasAssets : ScriptableObject
{
    [System.Serializable]
    public struct player
    {
        public string uid;
        public string playerName;

        public enum playerType {local=0,net }
        public playerType type;
       
    };

    [System.Serializable]
    public struct charactor
    {
        public string roalName;

        public enum prepareState {ready=0,notReady,prepare }
        public prepareState state;
        public bool enterRoom;
       
    };

    [System.Serializable]
    public class PlayerInfo
    {
        public player player;
        public charactor charactor;
    }

    public List<PlayerInfo> playerInfos = new List<PlayerInfo>();
}


