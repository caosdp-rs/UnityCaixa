using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager gamemanager;

    [SerializeField] 
    private RectTransform scoreRectTransform;
    private void Start()
    {
        scoreRectTransform.anchoredPosition = new Vector2(scoreRectTransform.anchoredPosition.x, 54);
        GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject.LeanScale(new Vector3(1.2f,1.2f),0.5f).setLoopPingPong();
    }
    public void Play()
    {
        GetComponent<CanvasGroup>().LeanAlpha(0, 0.2f).setOnComplete(onComplete);

       
    }
    private void onComplete()
    {
        scoreRectTransform.LeanMoveY(0, 0.75f).setEaseOutBounce();
        gamemanager.Enable();

        Destroy(gameObject);
    }
}
