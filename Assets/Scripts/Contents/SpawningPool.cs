using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    // ���� ���� ����
    [SerializeField] int _monsterCount = 0;
    
    int _reserveCount = 0;

    // �������Ѿ��� ���� ����
    [SerializeField] int _keepMonsterCount = 0;

    // ��ȯ �߽���
    [SerializeField] Vector3 _spawnPos;

    // �߽������κ��� ���� �ݰ�
    [SerializeField] float _spawnRadius = 5.0f;

    // ���� �ð� (����~)
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

    // �����̸� �༭ �����ϱ� ���� Coroutine�� ����Ѵ�.
    IEnumerator ReserveSpawn()
    {
        _reserveCount++;

        // (0~ spawntime�� �������� ��ٸ� �� ����)
        yield return new WaitForSeconds(Random.Range(4.0f, _spawnTime));

        GameObject obj = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

        // ���� ��ġ
        Vector3 randPos;

        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
            
            randDir.y = 0;      // �� ������ �������� �ʰ� �ϱ� ���ؼ� y���� 0���� ������Ų��
            _spawnPos = new Vector3(2.85f, 0, -38.5f);
            randPos = _spawnPos + randDir;

            // �� �� �ִ� ������ üũ
            NavMeshPath path = new NavMeshPath();
            if(nma.CalculatePath(randPos, path))
                break;

        }

        obj.transform.position = randPos;
        _reserveCount--;

    }
}
