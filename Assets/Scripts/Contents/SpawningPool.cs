using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    // 현재 몬스터 숫자
    [SerializeField] int _monsterCount = 0;
    
    int _reserveCount = 0;

    // 유지시켜아할 몬스터 숫자
    [SerializeField] int _keepMonsterCount = 0;

    // 소환 중심점
    [SerializeField] Vector3 _spawnPos;

    // 중심점으로부터 랜덤 반경
    [SerializeField] float _spawnRadius = 5.0f;

    // 스폰 시간 (랜덤~)
    [SerializeField] float _spawnTime = 7.0f;
    
    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    
    void Update()
    {
        while (_reserveCount + _monsterCount < _keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    // 딜레이를 줘서 생성하기 위해 Coroutine을 사용한다.
    IEnumerator ReserveSpawn()
    {
        _reserveCount++;

        // (0~ spawntime의 랜덤값을 기다린 후 실행)
        yield return new WaitForSeconds(Random.Range(4.0f, _spawnTime));

        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        // 랜덤 위치
        Vector3 randPos;

        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            
            randDir.y = 0;      // 땅 밑으로 생성되지 않게 하기 위해서 y값은 0으로 고정시킨다
            _spawnPos = new Vector3(2.85f, 0, -38.5f);
            randPos = _spawnPos + randDir;

            // 갈 수 있는 곳인지 체크
            NavMeshPath path = new NavMeshPath();
            if(nma.CalculatePath(randPos, path))
                break;

        }

        obj.transform.position = randPos;
        _reserveCount--;

    }
}
