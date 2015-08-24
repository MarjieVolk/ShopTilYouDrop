using UnityEngine;
using System.Collections;

public class ThrowingMovement : MonoBehaviour {
	public Vector2 Step;
	public float Gravity;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += (Vector3)Step * Time.deltaTime;
		Step.y -= Gravity;
	}
}
