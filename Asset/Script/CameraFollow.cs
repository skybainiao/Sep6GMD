using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; // 跟随的目标（角色）
    public float smoothSpeed = 0.125f; // 跟随的平滑速度
    public Vector3 offset; // 与目标之间的偏移量
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // 目标位置
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // 平滑过渡到目标位置
        transform.position = smoothedPosition; // 更新摄像机位置
    }
}
