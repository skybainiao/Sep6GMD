using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 spawnPoint; // ��ɫ������
    public float deathThreshold = -10f; // �����ֵ�����ڴ�ֵʱ��ɫ������

    private void Start()
    {
        spawnPoint = transform.position; // ���ó�����Ϊ��ɫ�ĳ�ʼλ��
    }

    private void Update()
    {
        // ����ɫ�Ƿ���������ֵ
        if (transform.position.y < deathThreshold)
        {
            transform.position = spawnPoint; // ����ɫλ������Ϊ������
        }
    }
}
