using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommonVar
{
    public class CommonVariable
    {

        public class UserData
        {
            public string uid;
            public string userName;
            public string roalName;

            public enum NetType { local, net }
            public NetType netType;


            public enum prepareState { ready = 0, notReady, prepare }
            public prepareState state;

            public bool enterRoom;
        }
        public  class ChatData
        {

            public string deviceNicname;
            public int playerType;
            public int chatType;
            public int messageOrder;
            public string message;
        }


        ///////////////ScriptBrief_Json///////////////
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
        ///////////////Json///////////////

    }

}

