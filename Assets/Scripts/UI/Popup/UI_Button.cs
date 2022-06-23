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

    // enum으로부터 받아와서 이름과 일치하는 것을 찾아서 저장
    // reflection 이용

    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons)); // enum Buttons 타입을 넘긴다.
        Bind<Text>(typeof(Texts)); // "" Texts 타입을 넘긴다
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //GetText((int)Texts.ScoreText).text = "바인드 테스트";

        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);


        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);

    }

    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }

    
}
