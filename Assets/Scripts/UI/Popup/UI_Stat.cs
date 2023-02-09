using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Stat : UI_Popup
{


    enum GameObjects
    {
        NameText,
        HpText,
        AttackText,
        SpeedText,
        LevelText,
    }

    [SerializeField] PlayerStat _stat;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
        _stat = GameObject.Find("UnityChan").GetComponent<PlayerStat>();
        SetInfo();
        
    }

    public void SetInfo()
    {
        GetObject((int)GameObjects.NameText).GetComponent<TMP_Text>().text = "UnityChan";
        GetObject((int)GameObjects.LevelText).GetComponent<TMP_Text>().text = $"Lv : {_stat.Level}";
        GetObject((int)GameObjects.HpText).GetComponent<TMP_Text>().text = $"HP : {_stat.MaxHP}";
        GetObject((int)GameObjects.AttackText).GetComponent<TMP_Text>().text = $"ATK : {_stat.Attack}";
        GetObject((int)GameObjects.SpeedText).GetComponent<TMP_Text>().text = $"SPEED : {_stat.MovSpeed}";

    }


}
