using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {

    public float speed = 5f;

    Vector2 currPosition;
    Transform _transform;

    void Start()
    {
        _transform = transform;
        currPosition = _transform.position;
    }

    void Update()
    {
        currPosition.x -= speed * Time.deltaTime;
        _transform.position = currPosition;
    }
}
