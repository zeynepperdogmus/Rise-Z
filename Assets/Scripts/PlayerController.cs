using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum can
    public int currentHealth; // Mevcut can

    void Start()
    {
        currentHealth = maxHealth; // Başlangıçta mevcut canı maksimum cana eşitle
        GameManager.instance.HpBar.text= currentHealth.ToString();
        GameManager.instance.HpBar.gameObject.SetActive(true);
    }

    void Update()
    {
        // Player'ın can durumunu kontrol etme (isteğe bağlı)
        if (currentHealth <= 0)
        {
            Die();
            GameManager.instance.gameOver = true;
            GameManager.instance.HpBar.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int amount)
    {
        // Player'ın canını azaltma
        Debug.Log("damage yedim"+amount);
        currentHealth -= amount;
        GameManager.instance.HpBar.color=Color.red;
        GameManager.instance.HpBar.text= currentHealth.ToString();

        // Hasar aldığında gerekli işlemleri yapma (isteğe bağlı)
        // Örneğin, can çubuğunu güncelleme veya görsel efekt oynatma gibi işlemler eklenebilir.

        // Player'ın ölüm durumunu kontrol etme
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void GetHealth(int amount)
    {
        
        currentHealth +=amount;
        GameManager.instance.HpBar.color=Color.green;
        GameManager.instance.HpBar.text= currentHealth.ToString();
    }

    void Die()
    {
        Cursor.lockState = CursorLockMode.None;   
        Time.timeScale = 0;
        GameManager.instance.GameOverPanel.SetActive(true);

        //SceneManager.LoadScene("GameOverScene"); // Game Over sahnesine geçiş yapma
        
        // Player'ın ölümü durumunda gerekli işlemleri yapma (isteğe bağlı)
        // Örneğin, ölüm animasyonu oynatma, oyunu yeniden başlatma gibi işlemler eklenebilir.
    }
}
