using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public float speed = 6f;
    private float spread = 0.25f;

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = new Vector3(0 - UnityEngine.Random.Range(spread * -1, spread),
            0 - UnityEngine.Random.Range(spread * -1, spread), 1);
        transform.Translate(fwd * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if(player != null)
        {
            player.Hit();
        }
        Destroy(this.gameObject);
    }
}
