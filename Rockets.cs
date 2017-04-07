using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rockets : MonoBehaviour {

	private GameObject target;

	public List<Vector3> genes = new List<Vector3> ();

	private Rigidbody rb;

	private Population populationControl;

	private float fitness;

	private bool win;
	private bool crashed;

	private float timeToWin;

	public float mutationRate;


	// Use this for initialization
	void Start () {
		timeToWin = Mathf.Infinity;
		rb = GetComponent<Rigidbody>();
		target = GameObject.FindGameObjectWithTag ("Finish");
	}

	public void setGenes(int lifeSpan, float mutate, List<Vector3> g = null){
		mutationRate = mutate;
		if (g == null) {
			for (int i = 0; i < lifeSpan; i++) {
				genes.Add (new Vector3(Random.Range(-360, 360), Random.Range(-360, 360), Random.Range(-360, 360)));
			}
		} else {
			genes = g;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void update(int frame, int force, int topSpeed){
		try{
			if (!crashed && !win) {
				rb.AddTorque (genes [frame]);
				rb.AddForce (transform.up * force);
				rb.velocity = Vector3.ClampMagnitude (rb.velocity, topSpeed);
			} else {
				freeze();
			}
		} catch{}

		if (win && frame == Mathf.Infinity) {
			timeToWin = frame;
		}
	}

	public void calculateFitness(){
		float distance = Vector3.Distance (transform.position, target.transform.position);

		fitness = 1 / distance;

		if (win) {
			fitness *= 10;
		}

		if (crashed) {
			fitness /= 10;
		}

	}

	public List<Vector3> crossover(Rockets other){
		List<Vector3> newGenes = new List<Vector3> ();
		for (int i = 0; i < genes.Count; i++) {
			int chooseParent = Random.Range (0, 2);
			if (chooseParent == 0) {
				newGenes.Add (genes [i]);
			} else {
				newGenes.Add (other.getGenes () [i]);
			}
		}
		return newGenes;
	}

	public void mutation(){
		for (int i = 0; i < genes.Count; i++) {
			if (Random.Range (0.0f, 1.0f) <= mutationRate) {
				genes [i] = new Vector3 (Random.Range (-360, 360), Random.Range (-360, 360), Random.Range (-360, 360));
			}
		}
	}
		

	public void normalizeFitness(float n){
		fitness /= n;
	}

	public float doubleFitness(){
		fitness *= 2;
		return fitness;
	}

	public float getFitness(){
		return fitness;
	}

	public List<Vector3> getGenes(){
		return genes;
	}

	public float getTimeToWin(){
		return timeToWin;
	}

	public bool getWin(){
		return win;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Finish") {
			win = true;
		} else if (other.tag == "Wall" || other.tag == "Floor"){
			crashed = true;
		}
	}

	public void freeze(){
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
	}
}
