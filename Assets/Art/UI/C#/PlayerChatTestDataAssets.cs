using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerChatTestDataAssets : ScriptableObject
{
    [System.Serializable]
    public struct ChatResponse
    {
        public string deviceNicname;
        public enum playerType { self=0,other,dm}
        public playerType player_Type;

        [System.Serializable]
        public  class MessageInfo
        {
            public enum chatType { pub = 0, prv }
            public chatType chat_Type;
            public int chatOrder;
            public string message;
        }

        public List<MessageInfo> messageInfos;
    };

    public List<ChatResponse> chatResponses = new List<ChatResponse>();
}
