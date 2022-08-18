using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPopup : BasePopup
{
    [SerializeField] private SettingsPopup settingsPopup;

    public void OnSettingsButton()//called when settings is pressed 
    {
        if (!settingsPopup.IsActive())
        {
            settingsPopup.Open();
        } 
        else
        {
            settingsPopup.Close();
        }
        Debug.Log("settings clicked");
    }

    public void OnExitGameButton() //exit the game
    {
        Debug.Log("exit game"); 
        Application.Quit();
    }

    public void OnReturnToGameButton()
    {
        if (settingsPopup.IsActive())
        {
            settingsPopup.Close();
        }
        Debug.Log("return to game");
        Close();
    }
}
