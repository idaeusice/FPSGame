using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private int spread = 12;
    [SerializeField]//
    private GameObject laserbeamPrefab;//
    private GameObject laserbeam;//
    public float fireRate = 0.5f;//
    private float nextFire = 0.0f;//
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

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !paused)
        {
            //added some randomness to simulate a weapon 'spread'
            Vector3 point = new Vector3((cam.pixelWidth / 2) - UnityEngine.Random.Range(spread * -1, spread), (cam.pixelHeight / 2) - UnityEngine.Random.Range(spread * -1, spread), 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;

            /*
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                laserbeam.transform.position = transform.TransformPoint(point);
                laserbeam.transform.rotation = transform.rotation;
            }
            */
            
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                // is this object our Enemy? 
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    // visually indicate where there was a hit 
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
            
        }
    }

    private IEnumerator SphereIndicator(Vector3 hitPosition)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = hitPosition;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

    void OnGUI()
    {
        /*
        GUIStyle style = new GUIStyle();
        style.fontSize = aimSize;

        //get center of camera and adjust for asterisk
        float posX = cam.pixelWidth / 2 - aimSize / 4;
        float posY = cam.pixelHeight / 2 - aimSize / 2;

        GUI.Label(new Rect(posX, posY, aimSize, aimSize), "*", style);
        */

    }
}
