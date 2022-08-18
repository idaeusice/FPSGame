using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image crossHair;
    [SerializeField] private OptionsPopup optionsPopup;
    private int popupsOpen = 0;

    void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_CHANGED, OnHealthChanged);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
    }

    void OnPopupOpened()
    {
        popupsOpen++;
    }

    void OnPopupClosed()
    {
        popupsOpen--;
        if(popupsOpen == 0)
        {
            SetGameActive(true);
        }
    }

    void OnHealthChanged()
    {
        UpdateHealth(-0.2f);
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crossHair.gameObject.SetActive(true);
            Messenger.Broadcast("GAME_ACTIVE");
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crossHair.gameObject.SetActive(false);
            Messenger.Broadcast("GAME_INACTIVE");
        }
    }

    public void UpdateHealth(float healthPercentage)
    {
        healthBar.fillAmount += healthPercentage;
        if (healthBar.fillAmount > 0.8f)
        {
            healthBar.color = Color.Lerp(Color.red, Color.green, healthBar.fillAmount);
        }
        else if (healthBar.fillAmount > 0.5f)
        {
            healthBar.color = Color.Lerp(Color.red, Color.green, healthBar.fillAmount);
        }
        else if (healthBar.fillAmount > 0.0f)
        {
            healthBar.color = Color.Lerp(Color.red, Color.green, healthBar.fillAmount);
        }
    }

    public void UpdateScore(int newScore)
    {
        scoreValue.text = newScore.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateHealth(1.0f);
        SetGameActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && popupsOpen == 0)
        {
            SetGameActive(false);
            optionsPopup.Open();
        }
    }
}
