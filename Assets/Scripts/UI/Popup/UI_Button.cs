using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Popup
{
    

    [SerializeField]
    Text _text;

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum Buttons
    {
        PointButton,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    // enum���κ��� �޾ƿͼ� �̸��� ��ġ�ϴ� ���� ã�Ƽ� ����
    // reflection �̿�

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); // enum Buttons Ÿ���� �ѱ��.
        Bind<Text>(typeof(Texts)); // "" Texts Ÿ���� �ѱ��
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //GetText((int)Texts.ScoreText).text = "���ε� �׽�Ʈ";

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);


        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    }

    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"���� : {_score}";
    }

    
}
