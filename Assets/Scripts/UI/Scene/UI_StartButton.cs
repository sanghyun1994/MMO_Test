using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_StartButton : UI_Scene
{
    [SerializeField]
    Text _text;

    enum Texts
    {
        text,
    }

    enum Buttons
    {
        Button,
    }


    // enum���κ��� �޾ƿͼ� �̸��� ��ġ�ϴ� ���� ã�Ƽ� ����
    // reflection �̿�

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); // enum Buttons Ÿ���� �ѱ��.
        Bind<Text>(typeof(Texts)); // "" Texts Ÿ���� �ѱ��

        GetButton((int)Buttons.Button).gameObject.BindEvent(OnButtonClicked);


        //GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    }



    public void OnButtonClicked(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }


}
