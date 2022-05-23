using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int time = 30;
    [SerializeField] int score = 0;
    //Range limita la variable para que tenga valores de 1 a 10. Aparece un slider en unity
    [Range(1, 10)][SerializeField] float spawnRate = 1;

    public AudioSource gameLoopAudio;
    public AudioSource gameOverAudio;
    public AudioSource buttonAudio;

    public bool gameOver = false;

    public int difficulty = 1;

    public int TimeLeft {
        get => time;
        set {
            time = value;
            UIManager.Instance.UpdateUITime(time);
        }
    }


    public int Score {
        get => score;
        set {
            score = value;

            //Actualiza el score en pantalla
            UIManager.Instance.UpdateUIScore(score);

            if(score % 1000 == 0){
                difficulty++;
            }
        }
    }

    private void Awake() {
        print("AWAKE gameMAnager");
        if(Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        print("Start GameManager");
        StartCoroutine(CountDown());
        UIManager.Instance.UpdateUITime(time);
        UIManager.Instance.UpdateUIScore(score);
    }

    IEnumerator CountDown()
    {
        while(TimeLeft > 0)
        {
            yield return new WaitForSeconds(1/spawnRate);
            TimeLeft--;
        }

        //Game Over
        gameOver = true;
        UIManager.Instance.ShowGameOverScreen();
    }

    public void PlayAgain()
    {
        buttonAudio.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void BackToTitleScreen()
    {
        buttonAudio.Play();
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }
}
