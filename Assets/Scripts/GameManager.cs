using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject hazardPrefeb;
    public int maxHazardToSpawn = 3;
    public TMPro.TextMeshPro scoreText;

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

}
