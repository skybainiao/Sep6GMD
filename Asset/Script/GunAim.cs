using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public Transform firePoint;
    public AudioClip shootSound;
    private AudioSource audioSource;

    public float fireRate = 0.25f; // 新增：控制子弹发射速率的变量
    private float nextFireTime; // 新增：记录下一次发射子弹的时间

    private Camera mainCamera;
    private Vector2 mousePosition;
    private Vector2 direction;
    private float angle;
    private bool facingRight = true;

    private void Start()
    {
        mainCamera = Camera.main;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - (Vector2)transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (direction.x > 0 && !facingRight || direction.x < 0 && facingRight)
        {
            Flip();
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, facingRight ? angle : angle + 180));

        // 修改：使用 Input.GetMouseButton 实现连续射击
        if (Input.GetMouseButton(0) && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // 更新下一次发射子弹的时间
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
    }

    private void Shoot()
    {
        Vector2 bulletVelocity = direction.normalized * bulletSpeed;
        GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, facingRight ? angle : angle + 180));
        bulletInstance.GetComponent<Rigidbody2D>().velocity = bulletVelocity;

        // 播放射击音效
        audioSource.PlayOneShot(shootSound);
    }
}
