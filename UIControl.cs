using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {

	public Text lifeSpan;
	public Button life1;
	public Button life2;

	public Text topSpeed;
	public Button speed1;
	public Button speed2;

	public Text acceleration;
	public Button accel1;
	public Button accel2;

	public Text population;
	public Button pop1;
	public Button pop2;

	public Text mutationRate;
	public Button mutate1;
	public Button mutate2;

	public Text generation;

	public Text frames;

	public Text maxFitness;

	public Text completionPercentage;

	public Button rock1;
	public Button rock2;
	public Button level1;
	public Button level2;

	public Button run;

	public Slider timeSlider;

	public Text levelText;

	public GameObject L1;
	public GameObject L2;
	public GameObject L3;
	public GameObject L4;
	public GameObject L5;
	public GameObject L6;

	public GameObject[] levelArray;

	public RawImage rocketPrev;

	public Texture rocket1;
	public Texture rocket2;
	public Texture rocket3;
	public Texture rocket4;
	public Texture rocket5;
	public Texture rocket6;
	public Texture rocket7;
	public Texture rocket8;
	public Texture rocket9;

	public GameObject rocket01;
	public GameObject rocket02;
	public GameObject rocket03;
	public GameObject rocket04;
	public GameObject rocket05;
	public GameObject rocket06;
	public GameObject rocket07;
	public GameObject rocket08;
	public GameObject rocket09;

	private Population populationControl;

	private int levelIndex;

	private int rocketIndex;

	private Texture[] rocketTextures;
	private GameObject[] rocketObjects;

	// Use this for initialization
	void Start () {
		populationControl = GameObject.FindGameObjectWithTag ("Floor").GetComponent<Population> ();
		setLifeSpan ();
		setTopSpeed ();
		setAcceleration ();
		setPopulation ();
		setMutation ();
		setGeneration ();
		setFrames ();
		setMaxFitness ();
		levelIndex = 0;
		rocketIndex = 0;
		levelArray = new GameObject[] {L1, L2, L3, L4, L5, L6};
		rocketTextures = new Texture[] {rocket1, rocket2, rocket3, rocket4, rocket5, rocket6, rocket7, rocket8, rocket9};
		rocketObjects = new GameObject[] {rocket01, rocket02, rocket03, rocket04, rocket05, rocket06, rocket07, rocket08, rocket09};
		populationControl.setRocket (rocketObjects[rocketIndex]);
		rocketPrev.texture = rocketTextures [rocketIndex];
	}
	
	// Update is called once per frame
	void Update () {
		setLifeSpan ();
		setTopSpeed ();
		setAcceleration ();
		setPopulation ();
		setMutation ();
		setGeneration ();
		setFrames ();
		setMaxFitness ();
		setCompletionPercentage ();
		Time.timeScale = timeSlider.value;
	}

	public void setAllButtons(){
		bool b;
		if (run.GetComponentInChildren<Text> ().text == "Run") {
			b = true;
		} else {
			b = false;
		}
		life1.gameObject.SetActive (b);
		life2.gameObject.SetActive (b);
		speed1.gameObject.SetActive (b);
		speed2.gameObject.SetActive (b);
		accel1.gameObject.SetActive (b);
		accel2.gameObject.SetActive (b);
		pop1.gameObject.SetActive (b);
		pop2.gameObject.SetActive (b);
		mutate1.gameObject.SetActive (b);
		mutate2.gameObject.SetActive (b);
		rock1.gameObject.SetActive (b);
		rock2.gameObject.SetActive (b);
		level1.gameObject.SetActive (b);
		level2.gameObject.SetActive (b);
	}

	public void setLifeSpan(){
		lifeSpan.text = "Life Span: " + populationControl.getLifeSpan();
	}

	public void upLifeSpan(){
		populationControl.changeLifeSpan (50);
	}

	public void downLifeSpan(){
		populationControl.changeLifeSpan (-50);
	}



	public void setTopSpeed(){
		topSpeed.text = "Top Speed: " + populationControl.getTopSpeed ();
	}

	public void upTopSpeed(){
		populationControl.changeTopSpeed (1);
	}

	public void downTopSpeed(){
		populationControl.changeTopSpeed (-1);
	}



	public void setAcceleration(){
		acceleration.text = "Acceleration: " + populationControl.getAcceleration ();
	}

	public void upAcceleration(){
		populationControl.changeAcceleration (1);
	}

	public void downAcceleration(){
		populationControl.changeAcceleration (-1);
	}



	public void setPopulation(){
		population.text = "Population: " + populationControl.getPopulation ();
	}

	public void upPopulation(){
		populationControl.changePopulation (5);
	}

	public void downPopulation(){
		populationControl.changePopulation (-5);
	}



	public void setMutation(){
		mutationRate.text = "Mutation Rate: " + (populationControl.getMutationRate() * 100).ToString("F0") + "%";
	}

	public void upMutation(){
		populationControl.changeMutation (0.01f);
	}

	public void downMutation(){
		populationControl.changeMutation (-0.01f);
	}



	public void setGeneration(){
		generation.text = "Generation: " + populationControl.getGeneration ();
	}

	public void setFrames(){
		frames.text = "Frames: " + populationControl.getFrame ();
	}

	public void setMaxFitness(){
		maxFitness.text = "Max Fitness: " + (populationControl.getMaxFitness () * 10).ToString("F2");
	}

	public void setCompletionPercentage(){
		completionPercentage.text = "Completion: " + populationControl.getCompletion ().ToString("F0") + "%";
	}

	public void levelRight(){
		levelIndex++;
		if (levelIndex == 6) {
			levelIndex = 0;
		}
		deactivateAllLevels ();
		levelArray [levelIndex].SetActive (true);
		levelText.text = levelArray [levelIndex].name;
	}

	public void levelLeft(){
		levelIndex--;
		if (levelIndex == 0) {
			levelIndex = 6;
		}
		deactivateAllLevels ();
		levelArray [levelIndex].SetActive (true);
		levelText.text = levelArray [levelIndex].name;
	}

	public void rocketRight(){
		rocketIndex++;
		if (rocketIndex == 8) {
			rocketIndex = 0;
		}
		populationControl.setRocket (rocketObjects[rocketIndex]);
		rocketPrev.texture = rocketTextures [rocketIndex];
	}

	public void rocketLeft(){
		rocketIndex--;
		if (rocketIndex == 0) {
			rocketIndex = 8;
		}
		populationControl.setRocket (rocketObjects[rocketIndex]);
		rocketPrev.texture = rocketTextures [rocketIndex];
	}

	private void deactivateAllLevels(){
		foreach (GameObject g in levelArray) {
			g.SetActive (false);
		}
	}
}
