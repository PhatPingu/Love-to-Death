using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveup : MonoBehaviour
{
    [SerializeField] float yspeed; 
    [SerializeField] private TrailRenderer[] trailRenderers;
    private Vector3 storedPosition;


    // Start is called before the first frame update
    void Awake()
    {
        storedPosition = transform.position;
        Debug.Log(storedPosition);
    }

    void OnEnable()
    {
        transform.position = storedPosition;

        foreach (var trail in trailRenderers)
        {
            trail.Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, Time.deltaTime * yspeed, 0);
    }
}
