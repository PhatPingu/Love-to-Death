using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float speed;
    [SerializeField] private GameObject marker;
    [SerializeField] private GameObject killText;
    private bool endGameChoice;

    [SerializeField] private Camera myCamera;
    [SerializeField] private GameObject trail;
    [SerializeField] private GameObject endGameScreen;

    [SerializeField] private float unFeverValue;
    public Slider feverSlider;
    public bool feverMode;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;    
    [SerializeField] private TextMeshProUGUI maxHighScoreText;
    private int maxHighScore;
    private bool highScorePlayed;

    [SerializeField] private TextMeshProUGUI scoreTextGameOver;
    [SerializeField] private TextMeshProUGUI maxHighScoreTextGameOver;
    
    public bool goingRight;

    [Header ("Sound Clips")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip feverModeSound;
    [SerializeField] private AudioClip unFeverSound;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip[] highScoreSound;

    [Header ("Particle Systems")]
    [SerializeField] private ParticleSystem coinParticle;
    [SerializeField] private ParticleSystem enemyParticle;

    void Awake()
    {
        feverMode = false;
    }

    void Update()
    {
        UpdateHighScore();

        UpdateText();
        
        FeverController();
        OutOfBoundsCheck();
        NoLoveCheck();   

        if(endGameChoice && Input.GetKeyDown(KeyCode.Space))
        {
            endGameChoice = false;
            feverSlider.value =9f;
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(1);
        }
    }

    void UpdateText()
    {
        scoreText.text = score.ToString();
        maxHighScoreText.text = maxHighScore.ToString();
        scoreTextGameOver.text = score.ToString();
        maxHighScoreTextGameOver.text = maxHighScore.ToString();
    }
    
    void FixedUpdate()
    {
        Move();        
    }

    void FeverController()
    {
        feverSlider.value -= 1f * Time.deltaTime;

        if(feverSlider.value >= 9.90f && !feverMode)
        {
            FeverModeOn();
        }
        if(feverSlider.value <= unFeverValue && feverMode)
        {
            FeverModeOff();
        }

        marker.SetActive(feverMode);     
        trail.SetActive(feverMode);
        killText.SetActive(feverMode);
    }

    void FeverModeOn()
    {
        feverMode = true;
        audioSource.PlayOneShot(feverModeSound, 1.2f);
        audioSource.pitch = 1.2f;
        myCamera.GetComponent<Camera>().backgroundColor = new Color32(52,172,78,0);
    }

    void FeverModeOff()
    {
        feverMode = false;
        audioSource.PlayOneShot(unFeverSound, 1.3f);
        audioSource.pitch = 1f;
        myCamera.GetComponent<Camera>().backgroundColor = new Color32(52,172,139,0);
    }

    void OutOfBoundsCheck()
    {
        if(transform.position.x > 9.15 || transform.position.x < -9.15) 
        {
            trail.SetActive(false);
            transform.position = new Vector2(transform.position.x * -0.999f,transform.position.y);
            trail.SetActive(true);
        }

        if(transform.position.y > 5.15 || transform.position.y < -5.15) 
        {
            trail.SetActive(false);
            transform.position = new Vector2(transform.position.x, transform.position.y * -0.999f);
            trail.SetActive(true);
        }
    }

    void NoLoveCheck()
    {
        if(feverSlider.value <= 0)
        {
            GameOver();
        }     
    }

    void Move()
    {
      if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))   
        {
            transform.position += -Vector3.right * speed;
            goingRight = false;
            playerSprite.flipX = false;

        }     

        if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))   
        {
            transform.position += Vector3.right * speed;
            goingRight = true;
            playerSprite.flipX = true;
        }  

        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))   
        {
            transform.position += Vector3.up * speed;
        }  

        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))   
        {
            transform.position += -Vector3.up * speed;
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && feverMode)
        {
            score += 3;
            Destroy(other.transform.parent.gameObject);
            audioSource.PlayOneShot(enemyDeathSound, 1.1f);
            enemyParticle.Play();
        }
        
        if(other.tag == "Enemy" && !feverMode)
        {
            Debug.Log("Triggered" + other);
            GameOver();
        }

        if(other.tag == "Coin")
        {
            score += 1;
            feverSlider.value += 3;
            Destroy(other.gameObject);
            audioSource.PlayOneShot(coinSound, 1.1f);
            coinParticle.Play();
        }
    }

    void GameOver()
    {
        Debug.Log("GameOver_00");
        audioSource.PlayOneShot(playerDeathSound, 1.3f);
        Time.timeScale = 0f;
        endGameScreen.SetActive(true);
        endGameChoice = true;
        Debug.Log("GameOver_01");
    }

    void UpdateHighScore()
    {
        maxHighScore = PlayerPrefs.GetInt("HighScore");

        if (score > maxHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);

            if(!highScorePlayed)
            {
                int randomHighScoreSound = Random.Range(0,highScoreSound.Length);
                audioSource.PlayOneShot(highScoreSound[randomHighScoreSound]);
                highScorePlayed =true;
            }
        }
    }

}
