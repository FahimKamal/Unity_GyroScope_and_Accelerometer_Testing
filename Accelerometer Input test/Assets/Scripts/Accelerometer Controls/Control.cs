using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Control : MonoBehaviour
{
    [SerializeField] private GameObject winFX;
    [SerializeField] private GameObject shineFX;
    [SerializeField] private GameObject loseFX;
    private Rigidbody2D _rigidbody2D;

    private float _dx;
    private float _dy;

    private float _width;
    private float _height;

    private int _click;

    private bool _gameRunning = true;

    [SerializeField] private float moveSpeed = 20f;
    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        var cam = Camera.main;
        _height = 2f * cam!.orthographicSize - 10;
        _width = _height * cam.aspect - 10;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_gameRunning)
        {
            _dx = Input.acceleration.x * moveSpeed;
            _dy = Input.acceleration.y * moveSpeed;
            var playerPosition = transform.position;
            playerPosition = new Vector2(Mathf.Clamp(playerPosition.x, -_width/2, _width/2), Mathf.Clamp(playerPosition.y, -_height/2, _height/2));
            transform.position = playerPosition;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _click++;
            StartCoroutine(ClickTime());
 
            if (_click > 1)
            {
                Application.Quit();
            }
        }
    }

    private IEnumerator ClickTime()
    {
        yield return new WaitForSeconds(0.5f);
        _click = 0;
    }

    private void FixedUpdate()
    {
        _rigidbody2D.velocity = _gameRunning ? new Vector2(_dx, _dy) : new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Obsticle")) return;
        
        _gameRunning = false;
        loseFX.SetActive(true);
        StartCoroutine(GameOver());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Goal")) return;
        
        _gameRunning = false;
        winFX.SetActive(true);
        shineFX.SetActive(true);
        StartCoroutine(GameWin());
    }

    private static IEnumerator GameWin()
    {
        yield return new WaitForSeconds(1.1f);
        PopupManager.Instance.ShowPopup("You Have Won!!", "Congratulations");
        CallBackManager.Instance.onPopupClosed.AddListener(Restart);
    }

    private static IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1.1f);
        PopupManager.Instance.ShowPopup("You Lose!!", "GAME OVER");
        CallBackManager.Instance.onPopupClosed.AddListener(Restart);
    }

    private static void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
