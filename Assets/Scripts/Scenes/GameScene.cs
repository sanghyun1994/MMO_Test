using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{


    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        Managers.Input.KeyAction -= OnInvenEvent;
        Managers.Input.KeyAction += OnInvenEvent;


        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;

        // GameScene �ε�� cursor�߰�
        gameObject.GetOrAddComponent<CursorController>();

        // Bgm ����
        Managers.Sound.Play("Bgm/Steppers On Line", Define.Sound.Bgm);

        // player spawn(prefab��ǥ�� ����), maincarema setting
        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "UnityChan");
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
 
        GameObject go = new GameObject { name = "SpawningPool" };
        go.transform.position = new Vector3(2.85f, 0, -38.5f);
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.SetKeepMonsterCount(4);

    }

    public override void Clear() // ����� ����� �κ�
    {
        
    }

    public void OnInvenEvent()
    {
        Managers.UI.OnOffInvenUI();
        Managers.UI.OnOffStatUI();

        Managers.UI.CloseAllPopupUI();
    }



}
