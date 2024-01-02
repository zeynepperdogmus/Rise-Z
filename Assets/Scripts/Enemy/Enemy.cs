using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IZombieHit
{
    public int Health;
    public int maxHealth = 100;
    public int damageAmount = 25; // Zombie Sald�r� Hasar�

    public Transform target; // Oyuncu (Player) objesinin transformu
    public float moveSpeed = 3f; // Zombi hareket h�z�
    public Animator animator; // Animator bile�eni

    public float attackRange = 1.5f; // Zombi'nin sald�r� menzili
    public float timeBetweenAttacks = 2f; // Sald�r�lar aras�ndaki zaman aral���

    private bool isAttacking = false; // Zombi'nin sald�r� durumunu kontrol etme
    bool isDead;
    void Start()
    {
        Health = maxHealth; // Ba�lang��ta mevcut can� maksimum cana e�itle
    }

    void Update()
    {
        if (target != null&& !isDead)
        {
            // Zombi'nin oyuncuyu takip etmesi
           
                Vector3 direction = (target.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
          
            

            // Zombi'nin y�n�n� belirleme
            transform.LookAt(target);

            // Sald�r� menziline gelindi�inde Player'a sald�r�
            if (Vector3.Distance(transform.position, target.position) <= attackRange && !isAttacking)
            {
                StartAttackAnimation();
            }

        }
    }

    void StartAttackAnimation()
    {
        // Zombi'nin sald�r� animasyonunu ba�latma
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        isAttacking = true;

        // Belirli bir s�re sonra hasar verme i�lemini ger�ekle�tirme
        Invoke("AttackPlayer", 0.5f);
    }
    void AttackPlayer()
    {
        // Player'a hasar verme
        target.GetComponent<PlayerController>().TakeDamage(damageAmount);

        // Sald�r�lar aras�ndaki zaman aral���n� g�ncelleme
        Invoke("ResetAttackState", timeBetweenAttacks);
    }

    void ResetAttackState()
    {
        // Zombi'nin sald�r� animasyonunu s�f�rlama
        animator.SetBool("isAttacking", false);
        isAttacking = false;

        // Zombi'nin ko�ma animasyonunu tekrar ba�latma
        animator.SetBool("isRunning", true);
    }

    void OnTriggerEnter(Collider other)// Zombi temas Sorgulama
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
        // E�er �arp��an obje Player ise
        if (other.CompareTag("Player"))
        {
            // Player'a hasar verme
            other.GetComponent<PlayerController>().TakeDamage(damageAmount);
            animator.SetBool("isAttacking", false);
        }
    }

    public void DealDamage(int damage)
    {
        Health -= damage;
        GameManager.instance.playerController.GetHealth(damage);
        Debug.Log(damage);
        if (Health <= 0&&!isDead)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead=true;
        animator.Play("Die");
        Destroy(gameObject,10f);
        GameManager.instance.SetEnemyCount();
    }
}
