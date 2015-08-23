using UnityEngine;
using System.Collections;

public class ConstantMovement : MonoBehaviour {
    public Vector2 Step;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position += (Vector3)Step;
	}
}
