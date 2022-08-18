using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { alive, dead };

public class WanderingAI : MonoBehaviour
{
    private float enemySpeed = 2f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;
    private EnemyStates state; // can only have states alive or dead, as defined in the enum. 
    [SerializeField]
    private GameObject laserbeamPrefab;
    private GameObject laserbeam;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;
    private float baseSpeed = 0.25f;
    float difficultySpeedDelta = 0.3f; // change in speed per level of difficulty.

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyStates.alive;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == EnemyStates.alive)
        {
            //move enemy
            transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
            //generate ray
            Ray ray = new Ray(transform.position, transform.forward);

            //spherecast and determine if the Enemy needs to turn
            //or if the Enemy is facing the player.
            RaycastHit hit;
            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    // Spherecast hit Player, fire laser! 
                    if (laserbeam == null && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        laserbeam = Instantiate(laserbeamPrefab) as GameObject;
                        laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                        laserbeam.transform.rotation = transform.rotation;
                    }
                }
                //if NOT 'looking' at the player, Enemy makes a rotation.
                else if (hit.distance < obstacleRange)
                {
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(Vector3.up * turnAngle);
                }
            }
        }
    }

    public void SetDifficulty(int difficulty)
    {
        Debug.Log("WanderingAi.setDifficulty(" + difficulty + ")");
        enemySpeed = baseSpeed + (difficulty * difficultySpeedDelta);
    }
    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;
        Debug.DrawLine(transform.position, rangeTest);
        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }
}
