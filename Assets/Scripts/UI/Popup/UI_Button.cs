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
        EscButton
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

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); // enum Buttons Ÿ���� �ѱ��.
        Bind<Text>(typeof(Texts)); // "" Texts Ÿ���� �ѱ��
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.EscButton).gameObject.BindEvent(OnButtonClicked);


        //GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    }

    public void OnButtonClicked(PointerEventData data)
    {
        Managers.UI.ClosePopupUI();
    }

    
}
