using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject sqaure;
    public GameObject endPanel;
    public Text timeTxt;
    public Text thisScoreTxt;
    public Text maxScoreTxt;
    public Animator animator;
    float alive = 0f;
    bool isRunning = true;
    public static GameManager I;

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        InvokeRepeating("makeSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            alive += Time.deltaTime;
            timeTxt.text = alive.ToString("N2");
            outBestScore();
            maxScoreTxt.text = PlayerPrefs.GetFloat("bestScore").ToString("N2");
        }
    }

    void makeSquare()
    {
        Instantiate(sqaure);
    }

    public void gameOver()
    {
        isRunning = false;
        animator.SetBool("isDie", true);
        Invoke("timeStop", 0.5f);
        endPanel.SetActive(true);
        thisScoreTxt.text = alive.ToString("N2");
    }

    public void retry()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void outBestScore()
    {
        if (!PlayerPrefs.HasKey("bestScore"))
        {
            PlayerPrefs.SetFloat("bestScore", alive);
        }
        else
        {
            if (alive > PlayerPrefs.GetFloat("bestScore"))
            {
                PlayerPrefs.SetFloat("bestScore", alive);
            }
        }
    }

    void timeStop()
    {
        Time.timeScale = 0f;
    }
}
