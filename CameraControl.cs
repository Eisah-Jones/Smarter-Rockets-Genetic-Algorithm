using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {

	public Transform spot1;
	public Transform spot2;
	public Transform spot3;
	public Transform spot4;
	public Transform top;
	public Transform lookat;

	private Transform target;

	public Button left;
	public Button right;

	public Transform[] positions;

	private int index;
	private bool upTop;

	// Use this for initialization
	void Start () {
		index = 0;
		positions = new Transform[] {spot1, spot2, spot3, spot4};
		target = positions [index];
		upTop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position != target.position) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, 5f);
		}
		transform.LookAt (lookat);
	}


	public void moveLeft(){
		if (!upTop) {
			index--;
			if (index == -1) {
				index = 3;
			}
		}
		target = positions [index];
	}

	public void moveRight(){
		if (!upTop) {
			index++;
			if (index == 4) {
				index = 0;
			}
		}
		target = positions [index];
	}

	public void moveDown(){
		upTop = false;
		target = positions [index];
		left.gameObject.SetActive (true);
		right.gameObject.SetActive (true);
	}


}
