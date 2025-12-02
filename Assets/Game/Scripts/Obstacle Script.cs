using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleScript : MonoBehaviour
{
    public float speed = 10f;
    private PlayerScript playerScript;
    public List<Transform> ObstacleSpawns = new();

    void Awake()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(playerScript.died) return;
            playerScript.KillPlayer();
        }
    }
}
