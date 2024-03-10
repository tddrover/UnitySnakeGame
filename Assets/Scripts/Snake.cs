using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 1;
    public int score = 0;
    public TextMeshProUGUI scoreText;
    //public static bool GameIsPaused = false;
    //  public GameObject pauseMenuUI;
    [SerializeField] private AudioSource collectFood;
    [SerializeField] private AudioSource collectAntiFood;
    [SerializeField] private AudioSource death;

    private void Start()
    {
        ResetState();
    }

    // Update is called once per frame

    private void Update()
    {
        HandleInput(); // Handle movement and pause/unpause input

        // Rest of your game logic
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }
 
    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );

    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
    }

    private void DecreaseScore()
    {
        score--;
        
        if (score < 0)
        {
            score = 0;
            scoreText.text = score.ToString();
            PlayerPrefs.SetInt("score", score);
            ResetState();
            SceneManager.LoadScene("GameOver");
        }
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("score", score);
    }

    private void ResetScore()
    {
        score =0;
        scoreText.text = score.ToString();
    }

    private void Grow()
    {

        Transform segment = Instantiate(this.segmentPrefab);          //makes game object
        segment.position = _segments[_segments.Count - 1].position;   //places it behind snake

        _segments.Add(segment);                                       //adds it to list
        initialSize++;
    }
    private void Shrink()
    {
        Destroy(_segments[_segments.Count - 1].gameObject);
        _segments.RemoveAt(_segments.Count - 1);


    }

    public void ResetState()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);
        initialSize = 1;

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        //this.transform.position = Vector3.zero;
        this.transform.position = new Vector3(10, 10, 0.0f);

        ResetScore();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            IncreaseScore();
            collectFood.Play();
        }
        else if(other.tag == "AntiFood")
        {
            Shrink();
            DecreaseScore();
            collectAntiFood.Play();
        }
        else if(other.tag == "Obstacle")
        {
            death.Play();
            Invisible();
          
            Invoke("ResetAndLoadGameOver", 1.5f);

            
        }
    }

    private void ResetAndLoadGameOver()
    {
        ResetState();
        SceneManager.LoadScene("GameOver");
    }
    private void Invisible()
    {
        // Loop through all segments and disable their renderer component
        foreach (Transform segment in _segments)
        {
            Renderer renderer = segment.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }
        }
        _segments.Clear(); // Clear the list
    }


}
