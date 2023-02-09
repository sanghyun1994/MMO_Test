using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected int _level;
    [SerializeField] protected int _hp;
    [SerializeField] protected int _maxHp;

    [SerializeField] protected int _attack;
    [SerializeField] protected int _defense;

    [SerializeField] protected float _movSpeed;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHP { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MovSpeed { get { return _movSpeed; } set { _movSpeed = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 10;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _movSpeed = 4.0f;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        // 데미지는 음수가 되면 안되므로 0보다는 무조건 크게 강제로 설정한다.
        int damage = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= damage; 
        if(Hp <= 0)
        {
            Hp = 0;
            Ondead(attacker);
        }

    }

    public virtual void Ondead(Stat attacker)
    {
        // attacker가 플레이어일경우 exp 증가
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 5;
        }

        Managers.Game.Despawn(gameObject);
    }



}
