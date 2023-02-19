using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    public float scrollSpeed = 1f;
    public float posValue = 0f;

    private Vector3 startPosition;
    private float newPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        newPosition = Mathf.Repeat(Time.time * scrollSpeed , posValue);
        transform.position = startPosition + Vector3.up * newPosition;
    }
}
