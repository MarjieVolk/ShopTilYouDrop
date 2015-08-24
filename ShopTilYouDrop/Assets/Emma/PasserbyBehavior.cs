using UnityEngine;
using System.Collections;

public class PasserbyBehavior : MonoBehaviour {

	public Vector2 Step = new Vector2(5, 5);
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position += (Vector3)Step;
	}
}
