using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManger : MonoBehaviour
{
    [SerializeField] private Transform[] spawnsPosition;
    [SerializeField] private GameObject[] balls;
    [SerializeField] private GameObject bomb;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI pointGreenText;
    [SerializeField] private TextMeshProUGUI pointRedText;
    [SerializeField] private TextMeshProUGUI pointBlueText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject panelBestPoints;
    [SerializeField] private TextMeshProUGUI bestnameText;
    [SerializeField] private TextMeshProUGUI bestPointGreenText;
    [SerializeField] private TextMeshProUGUI bestPointRedText;
    [SerializeField] private TextMeshProUGUI bestPointBlueText;
    [SerializeField] private GameObject panelGameOver;
    private Animator animPointGreenText;
    private Animator animPointRedText;
    private Animator animPointBlueText;
    
    [SerializeField] private float time;
    [SerializeField] private bool isCountdown;

    private bool isEnableForSpawn;
    [SerializeField] private int pointGreen = 0;
    [SerializeField] private int pointBlue = 0;
    [SerializeField] private int pointRed = 0;

    private void Start()
    {
        animPointGreenText = pointGreenText.gameObject.GetComponent<Animator>();
        animPointRedText = pointRedText.gameObject.GetComponent<Animator>();
        animPointBlueText = pointBlueText.gameObject.GetComponent<Animator>();
        
        panelGameOver.SetActive(false);
        panelBestPoints.SetActive(false);
        isCountdown = true;
        InvokeRepeating(nameof(CountDown),1,1);
        if (MainManager.Instance != null)
        {
            nameText.SetText(MainManager.Instance.name);
        }
        
        isEnableForSpawn = true;
    }

    private void Update()
    {
        if (isEnableForSpawn)
        {
            DeleteBombs();
            SpawnBall();
        }

        ShowPoints();
    }

    private void SpawnBall()
    {
        int indexSpawnPosition = Random.Range(0, spawnsPosition.Length);
        int indexBall = Random.Range(0, balls.Length);
        Instantiate(balls[indexBall], spawnsPosition[indexSpawnPosition].position, Quaternion.identity);
        isEnableForSpawn = false;
        InvokeRepeating(nameof(SpawnBomb), 2, 5);
    }

    private void SpawnBomb()
    {
        int indexSpawnPosition = Random.Range(0, spawnsPosition.Length);
        Instantiate(bomb, spawnsPosition[indexSpawnPosition].position, Quaternion.identity);
    }

    private void DeleteBombs()
    {
        foreach (var bomb in GameObject.FindGameObjectsWithTag("Bomb"))
        {
            Destroy(bomb);
        }
    }
    
    private void DeleteBall()
    {
        foreach (var ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            Destroy(ball);
        }
    }

    private void ShowPoints()
    {
        pointGreenText.SetText($"Green: {pointGreen.ToString()}");
        pointRedText.SetText($"Red: {pointRed.ToString()}");
        pointBlueText.SetText($"Blue: {pointBlue.ToString()}");
    }

    private void ShowTime()
    {
        float min = Mathf.FloorToInt(time / 60);
        float sec = Mathf.FloorToInt(time % 60);
        string temp = string.Format("{0:00}:{1:00}", min, sec);
        timeText.SetText(temp);
    }

    private void CountDown()
    {
        if (isCountdown)
        {
            if (time > 0)
            {
                time -= 1;
                ShowTime();
            }
            else
            {
                isCountdown = false;
            }
        }
        else
        {
            EndGame();
            CancelInvoke(nameof(CountDown));
        }
    }

    public void CancelInvokeBombs()
    {
        isEnableForSpawn = true;
        CancelInvoke(nameof(SpawnBomb));
    }

    public void AddPoint(string color)
    {
        CancelInvokeBombs();
        switch (color)
        {
            case "Verde":
                pointGreen += 1;
                animPointGreenText.SetTrigger("plus_point");
                break;
            case "Rojo":
                pointRed += 1;
                animPointRedText.SetTrigger("plus_point");
                break;
            case "Azul":
                pointBlue += 1;
                animPointBlueText.SetTrigger("plus_point");
                break;
        }
    }

    public void RestPoint(string color)
    {
        CancelInvokeBombs();
        switch (color)
        {
            case "Verde":
                pointGreen -= 1;
                break;
            case "Rojo":
                pointRed -= 1;
                break;
            case "Azul":
                pointBlue -= 1;
                break;
        }
    }

    
    private void EndGame()
    {
        FinishInvokes();

        float localPoints = pointRed + pointBlue + pointRed;
        float recordPoints = MainManager.Instance.redPoint + MainManager.Instance.bluePoint +
                             MainManager.Instance.greenPoint;
        if (localPoints > recordPoints)
        {
            MainManager.Instance.name = nameText.text;
            MainManager.Instance.redPoint = pointRed;
            MainManager.Instance.bluePoint = pointBlue;
            MainManager.Instance.greenPoint = pointGreen;
            MainManager.Instance.SavePoints();
            EnablePanelBestPoints();
        }
        else
        {
            EnablePanelGameOver();
        }
    }

    private void EnablePanelBestPoints()
    {
        panelBestPoints.SetActive(true);
        bestnameText.SetText(nameText.text);
        bestPointBlueText.SetText(pointBlue.ToString());
        bestPointGreenText.SetText(pointGreen.ToString());
        bestPointRedText.SetText(pointRed.ToString());
    }

    private void EnablePanelGameOver()
    {
        panelGameOver.SetActive(true);
    }

    private void FinishInvokes()
    {
        isEnableForSpawn = false;
        DeleteBombs();
        DeleteBall();
        CancelInvoke(nameof(SpawnBomb));
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}