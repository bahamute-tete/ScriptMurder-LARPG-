using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static CommonVar.CommonVariable;

public class Header : MonoBehaviour
{
    public TextMeshProUGUI title,btnHint;
    public Button gamePlayeBtn, backBtn;


    
    public void OnInforRecived()
    {
        ScriptInfo info = ScriptMurderUIManager.Instance.info;
        title.text = info.scriptName;
        backBtn.gameObject.SetActive(false);
        ScriptMurderUIManager.RebuildLayout(this.GetComponent<RectTransform>());

        
    }

}


