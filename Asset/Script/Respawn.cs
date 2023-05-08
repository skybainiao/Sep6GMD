using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Vector3 spawnPoint; // 角色出生点
    public float deathThreshold = -10f; // 虚空阈值，低于此值时角色将重生

    private void Start()
    {
        spawnPoint = transform.position; // 设置出生点为角色的初始位置
    }

    private void Update()
    {
        // 检查角色是否低于虚空阈值
        if (transform.position.y < deathThreshold)
        {
            transform.position = spawnPoint; // 将角色位置重置为出生点
        }
    }
}
