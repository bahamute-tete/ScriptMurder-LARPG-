using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerChatMessageItem : MonoBehaviour
{

    [SerializeField] Sprite[] identitySprites;
    string deviceNickname;
    string message;
    int chatType = 0;
    int playerType = 0;



    ScriptTestUIManager scriptTestManager;
    [SerializeField]int index;


    ScriptMurderUIManager.ChatData chatmessage;


    Image identifierIcon;
    TextMeshProUGUI deviceNicknameGUI, messageGUI;
    

    private void Awake()
    {
        scriptTestManager = GameObject.Find("Canvas").transform.Find("UI_ScriptTest").GetComponent<ScriptTestUIManager>();
        identifierIcon = transform.GetChild(0).GetComponent<Image>();
        deviceNicknameGUI = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        messageGUI = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        InitializedChatItem();
    }


    void InitializedChatItem()
    {
        index = transform.GetSiblingIndex();
        chatmessage = scriptTestManager.chatMessages[index];

       
        chatType = chatmessage.chatType;
        playerType = chatmessage.playerType;
        message = chatmessage.message;

        string whisper = (chatType == 1) ? " Whisper " : null;
        string tag = (playerType == 0) ? "( Me )" : null;
        deviceNickname = chatmessage.deviceNicname + whisper + tag + ":";

        string strColor = (playerType == 0) ? "<color=#F3C62F>" : (playerType == 1) ? "<color=#8DC3C1>" : "<color=#FFFFFF>";
        identifierIcon.sprite = (playerType == 0) ? identitySprites[1] : (playerType == 1) ? identitySprites[0] : identitySprites[2];

       

        deviceNicknameGUI.text = strColor + deviceNickname;
        messageGUI.text = strColor + message;


    }
}
