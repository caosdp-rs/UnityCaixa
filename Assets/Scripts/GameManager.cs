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
    private int score;
    private float timer;
    private static  bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnHazards());
        //InvokeRepeating("SpawnHazards", 0, 1f);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                StartCoroutine(ScaleTime(0, 1,0.5f));
                backgroundMenu.gameObject.SetActive(false);
            }
            if (Time.timeScale == 1)
            {
                StartCoroutine(ScaleTime(1, 0, 0.5f));
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

    public static void GamerOver()
    {
        gameOver = true;
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
        Time.timeScale = end;
        Time.fixedDeltaTime = 0.02f * end;
    }
}
