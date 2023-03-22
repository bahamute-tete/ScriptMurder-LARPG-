using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static CommonVar.CommonVariable;

public class ScripBrifItem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title, content;
    string titleText = null, contentText = null;
    [SerializeField] int typeInde;
    [SerializeField]PlayerStoryBreifUIManager manager;
    [SerializeField]ScriptInfo info;



    private void OnEnable()
    {
        manager.onScriptInfo += OnToggleChanged;
    }
    private void OnDisable()
    {
        manager.onScriptInfo -= OnToggleChanged;
    }



    public void OnInfoRecived()
    {
        info = ScriptMurderUIManager.Instance.info;
        CreateItemContent(typeInde);
        ScriptMurderUIManager.RebuildLayout(this.GetComponent<RectTransform>());
    
    }

    void OnToggleChanged(int type, int roleIndxe = 0)
    {
        ChangeInfo(type, roleIndxe);
        ScriptMurderUIManager.RebuildLayout(this.GetComponent<RectTransform>());
    }


    private void CreateItemContent(int type)
    {
        contentText = "";
        switch (type)
        {
            case 0:
                title.text = "友情提示";
                contentText = info.scriptHint;
                break;

            case 1:
                title.text = "故事剧情";
                contentText = info.scriptStory;
                break;

            case 2:
                title.text = "角色简介";
                foreach (var role in info.scriptRole)
                {
                    var name = role.name;
                    var content = role.intro;
                    contentText += $"<b>{name}</b>:\n{content}\n";
                }
               
                break;
        }

        content.text = contentText;
    }

    private void ChangeInfo(int type, int roleIndex)
    {
        contentText = "";
        switch (type)
        {
            case 0:
                switch (typeInde)
                {
                    case 0:
                        contentText = info.scriptHint;
                        break;

                    case 1:
                        contentText = info.scriptStory;
                        break;

                    case 2:
                        foreach (var role in info.scriptRole)
                        {
                            var name = role.name;
                            var content = role.intro;
                            contentText += $"<b>{name}</b>:\n{content}\n";
                        }
                        break;
                }

                break;


            case 1:
                switch (typeInde)
                {
                    case 0:
                        contentText = info.roleHint;
                        break;

                    case 1:
                        contentText = info.scriptRole[roleIndex].roleStory;
                        break;

                    case 2:
                        contentText = info.scriptRole[roleIndex].name + "\n" + info.scriptRole[roleIndex].details;
                        break;
                }
                break;

            case 2:
                switch (typeInde)
                {
                    case 0:
                        contentText = null;
                        break;

                    case 1:
                        contentText = null;
                        break;

                    case 2:
                        contentText = null;
                        break;
                }

                break;

        }

        content.text = contentText;
    }


}
