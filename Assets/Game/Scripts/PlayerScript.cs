
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{   
    public GameObject car;
    public ParticleSystem deathEffect;
    public bool died = false;

    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

    private float moveDelay = 0.1f;
    private float lastMoveTime = 0f;
    private bool isMoving = false;
    private AudioSource audioSource;
    public enum CurrentLane
    {
        leftmost,
        left,
        middle,
        right,
        rightmost
    }
    
    public CurrentLane currentLane;
    public List<Transform> lanes = new();
    private Transform startLane;
    private Transform endLane;
    void Awake()
    {
        currentLane = CurrentLane.middle;
        audioSource = GetComponent<AudioSource>();
    }

    public void KillPlayer()
    {
        died = true;
        Debug.Log("Player Died");
        car.SetActive(false);
        deathEffect.Play();
        PlayerScript playerScript = GetComponent<PlayerScript>();
        audioSource.PlayOneShot(audioSource.clip);
        playerScript.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(died) return;
        if(Input.GetKeyDown(leftKey) && !isMoving)
        {
            MoveLeft();
        }
        else if(Input.GetKeyDown(rightKey) && !isMoving)
        {
            MoveRight();
        }
    }
    void MoveLeft()
    {
        if(currentLane > CurrentLane.leftmost)
        {
            startLane = lanes[(int)currentLane];
            currentLane--;
            endLane = lanes[(int)currentLane];
            isMoving = true;
            lastMoveTime = Time.time;
            StartCoroutine(MoveOverTime(startLane.position, endLane.position, moveDelay));
        }
    }
    void MoveRight()
    {
        if(currentLane < CurrentLane.rightmost)
        {
            startLane = lanes[(int)currentLane];
            currentLane++;
            endLane = lanes[(int)currentLane];
            isMoving = true;
            lastMoveTime = Time.time;
            StartCoroutine(MoveOverTime(startLane.position, endLane.position, moveDelay));
        }
    }
    private IEnumerator MoveOverTime(Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
        isMoving = false;
    }
}
