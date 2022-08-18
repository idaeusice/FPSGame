using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SettingsPopup : BasePopup
{
    [SerializeField] private UIController uiController;
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI difficultyLabel;

    //private float difficulty;

    public override void Open()//open menu
    {
        base.Open();
        slider.value = PlayerPrefs.GetInt("difficulty", 1);
        UpdateDifficulty(slider.value);
        gameObject.SetActive(true);
    }

    public void UpdateDifficulty(float difficulty)
    {
        difficultyLabel.text = "Difficulty: " + ((int)difficulty).ToString();
    }

    public void OnDifficultyValueChanged(float difficulty)
    {
        UpdateDifficulty(difficulty);
    }

    public void OnOKButton()
    {
        //set difficulty level based on the slider.
        PlayerPrefs.SetInt("difficulty", (int)slider.value);
        Messenger<int>.Broadcast(GameEvent.DIFFICULTY_CHANGED, (int)slider.value);
        Close();
    }
    
    public void OnCancelButton()
    {
        Close();
    }
}
