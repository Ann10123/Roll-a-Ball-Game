using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int count = 0;
    private float timer = 0f;
    private bool isGameActive = true;
    private float finalTime;

    [Header("UI Elements")]
    public TextMeshProUGUI countText;
    public GameObject gameUI;
    public GameObject winMenu;
    public GameObject loseMenu;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI loseTimeText;

    [Header("Stars & Effects")]
    public GameObject finalStar1;
    public GameObject finalStar2;
    public GameObject finalStar3;
    public ParticleSystem winEffect;
    public AudioClip winSound;
    public AudioClip loseSound;

    private AudioSource audioSource;

    void Start()
    {
        timer = 0;
        count = 0;
        isGameActive = true;

        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        finalStar1.SetActive(false);
        finalStar2.SetActive(false);
        finalStar3.SetActive(false);
        gameUI.SetActive(false);

        audioSource = gameObject.AddComponent<AudioSource>();
        UpdateCountText();
    }

    void Update()
    {
        if (isGameActive)
        {
            timer += Time.deltaTime;
        }
    }

    public void AddGem()
    {
        if (!isGameActive) return;
        count++;
        UpdateCountText();
    }

    void UpdateCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 22)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        isGameActive = false;
        finalTime = timer;

        winEffect.Play();
        audioSource.PlayOneShot(winSound);

        gameUI.SetActive(false);
        winMenu.SetActive(true);

        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);
        winTimeText.text = string.Format("Your Time: \n{0:00}:{1:00}", minutes, seconds);

        if (finalTime <= 60f)
        {
            if (finalStar1 != null) finalStar1.SetActive(true);
            if (finalStar2 != null) finalStar2.SetActive(true);
            if (finalStar3 != null) finalStar3.SetActive(true);
        }
        else if (finalTime <= 120f)
        {
            if (finalStar1 != null) finalStar1.SetActive(true);
            if (finalStar2 != null) finalStar2.SetActive(true);
        }
        else if (finalTime <= 180f)
        {
            if (finalStar1 != null) finalStar1.SetActive(true);
        }

        Destroy(GameObject.FindGameObjectWithTag("Enemy"));
    }

    public void TriggerGameOver()
    {
        if (!isGameActive) return;

        isGameActive = false;
        finalTime = timer;

        gameUI.SetActive(false);
        loseMenu.SetActive(true);

        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);
        loseTimeText.text = string.Format("Your Time: \n{0:00}:{1:00}", minutes, seconds);

        AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position);
    }
}
