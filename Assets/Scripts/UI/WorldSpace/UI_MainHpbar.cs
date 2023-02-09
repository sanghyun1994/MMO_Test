using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_MainHpbar : UI_Base
{
    enum GameObjects
    {
        UI_Hpbar,
        Bars,
        Healthbar,
        Expbar,
        LevelText,
        HpText,

    }



    PlayerStat _stat;


    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<PlayerStat>();

    }

    private void Update()
    {
        float ratio = _stat.Hp / (float)_stat.MaxHP;
        SetHpRatio(ratio);

        SetLevel();
        SetHpText();



    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.Healthbar).GetComponent<Image>().fillAmount = ratio;
    }

    public void SetLevel()
    {
        GetObject((int)GameObjects.LevelText).GetComponent<Text>().text = $"{_stat.Level}";
    }

    public void SetHpText()
    {
        GetObject((int)GameObjects.HpText).GetComponent<TMP_Text>().text = $" {_stat.Hp} / {_stat.MaxHP}";
    }






}
