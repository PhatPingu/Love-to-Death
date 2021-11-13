using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    bool colorFull = true;
    float fadeTimer = 0.2f;
    
    [SerializeField] private int xRange;
    [SerializeField] private int yRange;
    [SerializeField] private int pivotPosition;
    private Vector3 pivotLocation;

    [SerializeField] Sprite[] enemySprite;
    [SerializeField] GameObject whiteCollider_Obj;
    [SerializeField] GameObject redCollider_Obj;

    [SerializeField] private float rotateSpeed;
    [SerializeField] private float maxSpeedRange;


    private GameObject player;
    private PlayerMovement _playerMovement;

    private float direction = 1f;
    private bool enemyDirectionRight = true;    


    void Start()
    {
        player = GameObject.Find("Player");
        _playerMovement = player.GetComponent<PlayerMovement>();

        DefinePosition();
        DefinePivot();    
        ChooseDirection();
        ChooseSprite();
        Invoke("MakeTrigger", 1f);
        
        rotateSpeed = Random.Range(10f, maxSpeedRange);
    }

    void DefinePosition()
    {
        //Define a range that EXCLUDES -1 to 1
        float xPosition = Random.Range(1f, xRange);
        float yPosition = Random.Range(1f, yRange);
        
        float makeNegativeX = 1;
        float makeNegativeY = 1;
        
        if(Random.Range(0,1f) > 0.5f) {makeNegativeX = -1f;}
        if(Random.Range(0,1f) > 0.5f) {makeNegativeY = -1f;}
        
        transform.position = new Vector2(xPosition*makeNegativeX, yPosition*makeNegativeY);
    }

    void DefinePivot()
    {
        float posX = Random.Range(-pivotPosition, pivotPosition);
        float posY = Random.Range(-pivotPosition, pivotPosition);
        pivotLocation = new Vector3(posX, posY,0);
    }

    void ChooseDirection()
    {
        whiteCollider_Obj.SetActive(false);
        redCollider_Obj.SetActive(false);

        if(Random.Range(0,1f) > 0.5f)
        {
            direction = -1f;
            enemyDirectionRight = false;
            
            Color thisColor = sprite.color;
            thisColor.a = 0.3f;
            sprite.color = thisColor;
        }
        else
        {
            direction = 1f;
            enemyDirectionRight = true;
            
            Color thisColor = sprite.color;
            thisColor.a = 0.3f;
            sprite.color = thisColor;
        }
    }

    void ChooseSprite()
    {
        if(enemyDirectionRight)
        {
            sprite.sprite = enemySprite[0];
        }

        if(enemyDirectionRight && _playerMovement.feverMode)
        {
            sprite.sprite = enemySprite[1];
        }

        if(!enemyDirectionRight)
        {
            sprite.sprite = enemySprite[2];
        }

        if(!enemyDirectionRight && _playerMovement.feverMode)
        {
            sprite.sprite = enemySprite[3];
        }
    }

    void MakeTrigger()
    {
        if(enemyDirectionRight)
        {
            whiteCollider_Obj.SetActive(true);
        }
        else if(!enemyDirectionRight)
        {
            redCollider_Obj.SetActive(true);
        }
        
        Color thisColor = GetComponent<SpriteRenderer>().color;
        thisColor.a = 1f;
        sprite.color = thisColor;
    }

    void Update()
    {
        ChooseSprite();
        StopOrMove();
        transform.RotateAround(pivotLocation, Vector3.forward, direction*rotateSpeed*Time.deltaTime);

        if(_playerMovement.feverMode && _playerMovement.feverSlider.value < 8.5f)
        {
            FadeEnemy();
            MakeTrigger();
        }
        else if(_playerMovement.feverMode && _playerMovement.feverSlider.value >= 8.5f)
        {
            Color thisColor = sprite.color;
            thisColor.a = 1f;
            sprite.color = thisColor;         
            MakeTrigger();
        }
    }

    void StopOrMove()
    {
        bool player_enemyDirectionRight = player.GetComponent<PlayerMovement>().goingRight;
        
        if(enemyDirectionRight && player_enemyDirectionRight)
        {
            direction = 1f;
        }
        else if(enemyDirectionRight && !player_enemyDirectionRight)
        {
            direction = 0f;
        }

        if(!enemyDirectionRight && player_enemyDirectionRight)
        {
            direction = 0f;
        }
        else if(!enemyDirectionRight && !player_enemyDirectionRight)
        {
            direction = -1f;
        }
    }

     void FadeEnemy()
    {
        fadeTimer -= 1f * Time.deltaTime;

        if(fadeTimer <=0 && colorFull)
        {
            Color thisColor = sprite.color;
            thisColor.a = 0.1f;
            sprite.color = thisColor;
            
            colorFull = false;
            fadeTimer = 0.2f;
        }
        
        if(fadeTimer <=0 && !colorFull)
        {
            Color thisColor = sprite.color;
            thisColor.a = 0.5f;
            sprite.color = thisColor;
            
            colorFull = true;
            fadeTimer = 0.2f;
        }
    }
}
