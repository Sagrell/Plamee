using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    public float speed = 5f;

    Vector2 currPosition;
    Transform _transform;

	// Use this for initialization
	void Start () {
        _transform = transform;
        currPosition = _transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        currPosition.x -= speed * Time.deltaTime;
        _transform.position = currPosition;
    }
}
