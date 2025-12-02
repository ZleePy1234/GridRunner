using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
