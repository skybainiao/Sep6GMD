using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Transform target;
    public int maxHealth = 100;
    private GameObject healthBar;
    public float knockbackForce = 5f;
    private int currentHealth;

    public float jumpForce = 5f;
    public LayerMask groundLayer;
    public Transform obstacleCheck;
    public float obstacleCheckDistance = 0.5f;
    public GameObject healthBarPrefab; // 血条预制件

    private Animator animator;
    private Rigidbody2D rb;
    private GameObject healthBarInstance; // 血条实例
    private Slider healthSlider; // 血条滑块
    private float health = 100f; // 敌人血量

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar = healthBarPrefab.transform.GetChild(0).gameObject;
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        // 创建血条实例并设置为敌人的子对象
        healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        //healthBarInstance.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform);
        healthSlider = healthBarInstance.GetComponent<Slider>();
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    void Update()
    {
        if (target != null)
        {
            // 计算敌人和目标之间的距离
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            // 检查敌人是否在移动范围内
            if (distanceToTarget > 0.1f)
            {
                // 计算敌人应该向哪个方向移动
                Vector2 direction = (target.position - transform.position).normalized;

                // 在 Rigidbody2D 上应用移动速度
                rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

                 

                // 更新敌人的朝向
                if (direction.x > 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else if (direction.x < 0)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                // 检测前方是否有障碍物
                RaycastHit2D hit = Physics2D.Raycast(obstacleCheck.position, transform.right, obstacleCheckDistance, groundLayer);

                if (hit.collider != null)
                {
                    // 如果有障碍物，执行跳跃
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                // 停止移动
                rb.velocity = new Vector2(0, rb.velocity.y);

                // 设置动画参数，停止移动动画
                animator.SetBool("isMoving", false);
            }
        }

        // 更新血条位置
        //healthBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2);
    }
    private void UpdateHealthBar()
{
    healthBar.transform.localScale = new Vector3((float)currentHealth / maxHealth, 1f, 1f);
}

     void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            // 减少敌人血量并更新血条
            health -= 10;
            healthSlider.value = health;

            // 如果血量为0，则销毁敌人和血条
            if (health <= 0)
            {
                Destroy(healthBarInstance);
                Destroy(gameObject);
            }
            else
            {
                // 添加击退效果
                Vector2 knockbackDirection = (transform.position - collider.transform.position).normalized;
                float knockbackForce = 5f;
                rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }

            // 销毁子弹
            Destroy(collider.gameObject);
        }
    }
    public void TakeDamage(int damageAmount)
{
    currentHealth -= damageAmount;
    if (currentHealth <= 0)
    {
        Destroy(gameObject);
    }
    UpdateHealthBar();
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (obstacleCheck != null)
        {
            Gizmos.DrawLine(obstacleCheck.position, (Vector2)obstacleCheck.position + (Vector2)transform.right * obstacleCheckDistance);
        }
    }

    // 当敌人被销毁时，同时销毁血条
    private void OnDestroy()
    {
        if (healthBarInstance != null)
        {
            Destroy(healthBarInstance);
        }
    }
}
