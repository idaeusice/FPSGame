using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopup : MonoBehaviour
{
    public virtual void Open() //open menu 
    {
        if (!IsActive())
        {
            Messenger.Broadcast("POPUP_OPENED");
            gameObject.SetActive(true);
        }else
        {
            Debug.LogError(this + ".Open() – trying to open a popup that is active!");
        }
    }
    public virtual void Close()
    {
        if (IsActive())
        {
            Messenger.Broadcast("POPUP_CLOSED");
            gameObject.SetActive(false);
        }else
        {
            Debug.LogError(this + ".Close() – trying to close a popup that is not active!");
        }
    }
    public virtual bool IsActive() //checks if options menu is open
    {
        return gameObject.activeSelf;
    }
}
