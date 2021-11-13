using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private float timer;
    private float resetTimer;

    [SerializeField] private SpriteRenderer sprite;
    bool colorFull = true;
    float fadeTimer = 0.5f;
    
    [Header ("Random Reset Timer Range:")]
    [SerializeField] float minRangeTimer;
    [SerializeField] float maxRangeTimer;
    void Start()
    {
        resetTimer = Random.Range(minRangeTimer, maxRangeTimer);
        timer = resetTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1f * Time.deltaTime;

        if(timer < resetTimer*0.3f)
        {
            FadeCoin();
        }

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void FadeCoin()
    {
        fadeTimer -= 1f * Time.deltaTime;

        if(fadeTimer <=0 && colorFull)
        {
            Color thisColor = sprite.color;
            thisColor.a = 0.3f;
            sprite.color = thisColor;
            
            colorFull = false;
            fadeTimer = 0.5f;
        }
        
        if(fadeTimer <=0 && !colorFull)
        {
            Color thisColor = sprite.color;
            thisColor.a = 1f;
            sprite.color = thisColor;
            
            colorFull = true;
            fadeTimer = 0.5f;
        }
    }
}

