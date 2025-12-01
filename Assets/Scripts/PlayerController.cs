using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Threading;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject keyText;
    public GameObject unlockWall;
    public GameObject player;
    public GameObject nextButton;
    public GameObject retryButton;
    public GameObject menuButton;
    public GameObject topHat;
    public GameObject goldHat;
    public GameObject goldHatPickup;
    public AudioClip pickupSound;
    public AudioClip keyPickupSound;
    public AudioClip hatPickupSound;
    public Animator keyHide;
    private AudioSource audioSource;
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    static bool hasGoldHat = false;
    
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
        keyText.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        keyHide.enabled = false;

        if (hasGoldHat)
        {
            topHat.SetActive(false);
            goldHat.SetActive(true);
            goldHatPickup.SetActive(false);
        }
        else
        {
            topHat.SetActive(true);
            goldHat.SetActive(false);
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 8)
        {
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Win!";
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            nextButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            audioSource.PlayOneShot(pickupSound, .3f);
            audioSource.Play();
        }
        else if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            unlockWall.gameObject.SetActive(false);
            audioSource.PlayOneShot(keyPickupSound, .3f);
            keyText.SetActive(true);
        }
        else if (other.gameObject.CompareTag("Trigger"))
        {
            keyHide.enabled = true;
        }
        else if (other.gameObject.CompareTag("GoldHat"))
        {
            hasGoldHat = true;
            topHat.SetActive(false);
            goldHat.SetActive(true);
            keyText.SetActive(true);
            other.gameObject.SetActive(false);
            audioSource.PlayOneShot(hatPickupSound, .1f);
        }
        else if (other.gameObject.CompareTag("Portal"))
        {
            SceneManager.LoadScene(5);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            player.gameObject.SetActive(false);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            retryButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("KillZone"))
        {
            player.gameObject.SetActive(false);
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            retryButton.gameObject.SetActive(true);
            menuButton.gameObject.SetActive(true);
        }
        else if (collision.gameObject.CompareTag("PickUp"))
        {
            collision.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
            audioSource.PlayOneShot(pickupSound, .3f);
            audioSource.Play();
        }
    }
}
