using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Population : MonoBehaviour {

	public Text generationText;
	public Button run;

	public GameObject rocket;

	public int populationSize = 25;

	private List<GameObject> rocketList = new List<GameObject>();
	private List<GameObject> matingPool;

	private bool running = false;

	private int frames = 0;
	private int generation = 1;
	private int lifeSpan = 250;
	private int acceleration = 15;
	private int topSpeed = 20;
	private float biggestFitness = 0;
	private float mutationRate = 0.03f;
	private float completion = 0f;


	// Use this for initialization
	void Start () {
	}

	public void runButton(){
		if (run.GetComponentInChildren<Text> ().text == "Run") {
			try {
				destroyCurrentRockets ();
			} catch {}
			GameObject temp;
			for (int i = 0; i < populationSize; i++) {
				temp = Instantiate (rocket);
				temp.GetComponent<Rockets> ().setGenes (lifeSpan, mutationRate, null);
				rocketList.Add (temp);
			}
			generationText.text = "Generation: " + generation.ToString ();
			running = true;
			run.GetComponentInChildren<Text> ().text = "Stop";
		} else {
			running = false;
			destroyCurrentRockets ();
			run.GetComponentInChildren<Text> ().text = "Run";
			biggestFitness = 0;
			mutationRate = 0.03f;
			completion = 0f;
			generation = 1;
			frames = 0;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (running) {
			if (frames < lifeSpan) {
				for (int i = 0; i < populationSize; i++) {
					rocketList [i].GetComponent<Rockets> ().update (frames, acceleration, topSpeed);
				}
			} else {
				evaluate ();
				List<List<Vector3>> newList = breed ();
				frames = -1;
				destroyCurrentRockets ();
				spawnNewRockets (newList);
				matingPool = new List<GameObject> ();
			}
			frames++;
		}
	}

	public void evaluate(){
		float maxFitness = 0;
		float completed = 0;
		GameObject fastest = null;
		float fastestFrame = Mathf.Infinity;
		for (int i = 0; i < populationSize; i++) {
			Rockets current = rocketList [i].GetComponent<Rockets> ();
			current.calculateFitness ();
			if (current.getFitness () > maxFitness) {
				maxFitness = current.getFitness ();
			}
			if (current.getWin() && current.getTimeToWin () < fastestFrame) {
				fastestFrame = current.getTimeToWin ();
				fastest = rocketList [i];
			}
			if (current.getWin ()) {
				completed++;
			}
		}

		completion = completed / populationSize;

		if (fastest != null) {
			float tempFit = fastest.GetComponent<Rockets> ().doubleFitness ();
			if (tempFit > maxFitness) {
				maxFitness = tempFit;
			}
		}
			
		biggestFitness = maxFitness;
			
		//Normalize the fitnesses
		for (int i = 0; i < populationSize; i++) {
			rocketList [i].GetComponent<Rockets> ().normalizeFitness(maxFitness);
		}

		matingPool = new List<GameObject> ();
		for (int i = 0; i < populationSize; i++) {
			float mateRating = rocketList [i].GetComponent<Rockets> ().getFitness () * 100;
			for (int j = 0; j < mateRating; j++) {
				matingPool.Add (rocketList [i]);
			}
		}
	}

	public List<List<Vector3>> breed(){
		List<List<Vector3>> newRockets = new List<List<Vector3>> ();
		for (int i = 0; i < populationSize; i++) {
			int index1 = Random.Range (0, matingPool.Count);
			int index2 = index1;
			Rockets parent1 = matingPool [index1].GetComponent<Rockets> ();
			while (index2 == index1) {
				index2 = Random.Range (0, matingPool.Count);
				if (matingPool [index2].GetComponent<Rockets> () == parent1) {
					index2 = index1;
				}
			}
			Rockets parent2 = matingPool [index2].GetComponent<Rockets> ();
			List<Vector3> newGenes = parent1.crossover (parent2);
			newRockets.Add (newGenes);
		}
		return newRockets;
	}

	public void destroyCurrentRockets(){
		for (int i = 0; i < populationSize; i++) {
			Destroy (rocketList [i]);
		}
		rocketList = new List<GameObject> ();
	}

	public void spawnNewRockets(List<List<Vector3>> rl){
		GameObject temp;
		for (int i = 0; i < populationSize; i++) {
			temp = Instantiate(rocket);
			temp.GetComponent<Rockets> ().setGenes (lifeSpan, mutationRate, rl [i]);
			rocketList.Add (temp);
		}
		generation++;
		generationText.text = "Generation: " + generation.ToString ();
	}

	public void freezeRockets(){
		for (int i = 0; i < populationSize; i++) {
			rocketList [i].GetComponent<Rockets> ().freeze ();
		}
	}

	public int getFrame (){
		return frames;
	}

	public int getLifeSpan(){
		return lifeSpan;
	}

	public int getTopSpeed(){
		return topSpeed;
	}

	public int getAcceleration(){
		return acceleration;
	}

	public int getPopulation(){
		return populationSize;
	}

	public float getMutationRate(){
		return mutationRate;
	}

	public int getGeneration(){
		return generation;
	}

	public float getMaxFitness(){
		return biggestFitness;
	}

	public float getCompletion(){
		return completion * 100;
	}

	public void changeLifeSpan(int n){
		if (lifeSpan == 200) {
			if (n > 0) {
				lifeSpan += n;
			}
		} else if (lifeSpan >= 500) {
			if (n < 0) {
				lifeSpan += n;
			}
		} else {
			lifeSpan += n;
		}
	}

	public void changeTopSpeed(int n){
		if (topSpeed == 10) {
			if (n > 0) {
				topSpeed += n;
			}
		} else if (topSpeed == 30) {
			if (n < 0) {
				topSpeed += n;
			}
		} else {
			topSpeed += n;
		}
	}

	public void changeAcceleration(int n){
		if(acceleration == 10){
			if (n > 0) {
				acceleration += n;
			}
		} else if (acceleration == 20) {
			if (n < 0) {
				acceleration += n;
			}
		} else {
			acceleration += n;
		}
	}

	public void changePopulation(int n){
		if(populationSize == 5){
			if (n > 0) {
				populationSize += n;
			}
		} else if (populationSize == 100) {
			if (n < 0) {
				populationSize += n;
			}
		} else {
			populationSize += n;
		}
	}

	public void changeMutation(float n){
		if (mutationRate == 0f) {
			if (n > 0) {
				mutationRate += n;
			}
		} else if (mutationRate == 1) {
			if (n < 0) {
				mutationRate += n;
			}
		} else {
			mutationRate += n;
		}
	}

	public void setRocket(GameObject r){
		rocket = r;
	}
}