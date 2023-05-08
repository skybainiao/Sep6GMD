using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target; // �����Ŀ�꣨��ɫ��
    public float smoothSpeed = 0.125f; // �����ƽ���ٶ�
    public Vector3 offset; // ��Ŀ��֮���ƫ����
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
        Vector3 desiredPosition = target.position + offset; // Ŀ��λ��
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // ƽ�����ɵ�Ŀ��λ��
        transform.position = smoothedPosition; // ���������λ��
    }
}
