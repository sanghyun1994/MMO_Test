using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        Managers.UI.ShowSceneUI<UI_StartButton>();

        Managers.Sound.Play("Bgm/reminiscence", Define.Sound.Bgm);


    }

    private void Update()
    {
       
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!!");
    }
}
