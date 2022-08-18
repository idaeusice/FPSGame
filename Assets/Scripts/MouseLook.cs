using System.Collections;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHoriz = 8.0f;
    public float sensitivityVert = 8.0f;

    public float minVert = -45.0f;
    public float maxVert = 45.0f;

    private float rotationX = 0.0f;
    private bool paused = false;

    private void Awake()
    {
        Messenger.AddListener("GAME_ACTIVE", OnGameActive);
        Messenger.AddListener("GAME_INACTIVE", OnGameInactive);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener("GAME_ACTIVE", OnGameActive);
        Messenger.RemoveListener("GAME_INACTIVE", OnGameInactive);
    }

    void OnGameActive()
    {
        paused = false;
    }

    void OnGameInactive()
    {
        paused = true;
    }

    void Update()
    {
        if (!paused)
        {
            if (axes == RotationAxes.MouseX)
            {
                //horiz
                transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * sensitivityHoriz);
            }
            else if (axes == RotationAxes.MouseY)
            {
                //vert
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
                transform.localEulerAngles = new Vector3(rotationX, 0, 0);
            }
            else
            {
                //both
                rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
                float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
                float rotationY = transform.localEulerAngles.y + deltaHoriz;

                transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
            }
        }
    }
}
