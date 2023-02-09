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


    // enum으로부터 받아와서 이름과 일치하는 것을 찾아서 저장
    // reflection 이용

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); // enum Buttons 타입을 넘긴다.
        Bind<Text>(typeof(Texts)); // "" Texts 타입을 넘긴다

        GetButton((int)Buttons.Button).gameObject.BindEvent(OnButtonClicked);


        //GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        //BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    }



    public void OnButtonClicked(PointerEventData data)
    {
        Managers.Scene.LoadScene(Define.Scene.Game);
    }


}
