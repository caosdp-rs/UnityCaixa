using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefeb;
    public int maxHazardToSpawn = 3;

  

    public TMPro.TextMeshProUGUI scoreText;
    public Image backgroundMenu;
    public GameObject mainVcam;
    public GameObject zoomVcam;
    public GameObject player;
    public GameObject gameOverMenu;
    private int score;
    private float timer;
    private Coroutine hazardsCoroutine;

    private bool gameOver;
    private int highScore;
    private static GameManager instance;
    public static GameManager Instance => instance;
    public int HighScore => highScore;
    private void OnEnable()
    {
        gameOver = false;
        score = 0;
        scoreText.text = "0";
        player.SetActive(true);
        mainVcam.SetActive(true);
        zoomVcam.SetActive(false);
        //StartCoroutine(SpawnHazards());
        hazardsCoroutine = StartCoroutine(SpawnHazards());
    }
    // Start is called before the first frame update

    void Start()
    {
        instance = this;
        highScore = PlayerPrefs.GetInt("highScore");
        //InvokeRepeating("SpawnHazards", 0, 1f);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                LeanTween.value(0, 1, 0.75f).setOnUpdate(SetTimeScale).setIgnoreTimeScale(true) ;
                //StartCoroutine(ScaleTime(0, 1,0.5f));
                backgroundMenu.gameObject.SetActive(false);
            }
            if (Time.timeScale == 1)
            {
                LeanTween.value(1,0,0.75f).setOnUpdate(SetTimeScale).setIgnoreTimeScale(true);
                //StartCoroutine(ScaleTime(1, 0, 0.5f));
                backgroundMenu.gameObject.SetActive(true);
            }
        }
        if (gameOver)
            return;
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            scoreText.text = score.ToString();
            timer = 0;
            score += 1;
        }
        
    }

    private void SetTimeScale(float obj)
    {
        Time.timeScale = obj;
        Time.fixedDeltaTime = 0.02f * obj;

    }

    //private void SpawnHazards()
    private IEnumerator SpawnHazards()
    {
        var hazardToSpawn = Random.Range(1, maxHazardToSpawn);
        for (int i=0; i < hazardToSpawn; i++)
        {
            var x = Random.Range(-7, 7);
            var drag = Random.Range(0f, 2f);
            var hazard = Instantiate(hazardPrefeb, new Vector3(x, 11, 0), Quaternion.identity);
            hazard.GetComponent<Rigidbody>().drag = drag;
            
        }
        var timeToawait = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(timeToawait);
        yield return SpawnHazards();

    }

    public void GameOver()
    {
        gameOver = true;
        //teste para saber se o jogo não estava entrando em modo pause
        if (Time.timeScale<1)
        {
            LeanTween.value(Time.time, 1, 0.75f).setOnUpdate(SetTimeScale).setIgnoreTimeScale(true);
            backgroundMenu.gameObject.SetActive(false);
        }
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        StopCoroutine(hazardsCoroutine);
        mainVcam.SetActive(false);
        zoomVcam.SetActive(true);
        gameObject.SetActive(false);
        gameOverMenu.SetActive(true);
    }
    IEnumerator ScaleTime(float start, float end, float duration)
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;
        while (timer < duration)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / duration);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;

        }

    }  
    
    public  void Enable()
    {

        gameObject.SetActive(true);
    }
}
