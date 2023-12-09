using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private float respawnDelay = 4f;
    private bool isGameEnding = false;
    private int score;
    public Text scoreText;
    public Text WinText;
    public GameObject WinnerUI;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

    }

    public void RespawnPlayer()
    {
        if (isGameEnding==false)
        {
            isGameEnding = true;
            StartCoroutine("RespawnCoroutine");
        }
    }

    private IEnumerator RespawnCoroutine()
    {
        playerController.gameObject.SetActive(false);
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //playerController.transform.position = playerController.respawnPoint;
        //playerController.gameObject.SetActive(true);
        isGameEnding = false;
    }

    public void AddScore(int numberOfScore)
    {
        score += numberOfScore;
        scoreText.text = score.ToString();
    }

    public void LevelUp()
    {
        WinnerUI.SetActive(true);
        WinText.text = "Seviye Tamamlandý Toplam Puan" + score;
        Invoke("NextLevel", respawnDelay);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

