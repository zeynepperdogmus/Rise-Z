using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PlayerController playerController;

    private void Awake()
    {
        instance = this;
    }
    public void RestartGame(){
         SceneManager.LoadScene(0);
    }
    public void RestartGameAgain(){
         SceneManager.LoadScene(0);
    }
    public int EnemyCount;
    public EnemyController enemyController;
    public GameObject WinPanel;
    public GameObject GameOverPanel;
    public TextMeshProUGUI HpBar;
    public bool gameOver=false;
    private void Start()
    {
        EnemyCount = enemyController.Enemys.Length;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
          Cursor.lockState = CursorLockMode.None;     
            RestartGame();
        }

        if (EnemyCount <= 0)
        {
            WinPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void SetEnemyCount()
    {
        EnemyCount -= 1;
    }
}
