using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour {

	public int Modelnum = 0;
	int speed = 1;
	public Vector2 touchDeltaPosition;
	//public LayerMask touchInputMask;
	private Vector3 InitialPosition;
	private Vector3 endPos;

	public Button DogBtn;
	public Button CatBtn;
	public Text PetName;
	public Text Price;
	public Canvas PopUp;

	//private PetIconChanger ClassChangeImage;
	public List<GameObject> models = new List<GameObject>();

	// Use this for initialization
	void Start () {
		PopUp = PopUp.GetComponent<Canvas>();
		DogBtn = DogBtn.GetComponent<Button>();
		CatBtn = CatBtn.GetComponent<Button>();
		PetName = PetName.GetComponent<Text>();
		Price = Price.GetComponent<Text>();

		//ClassChangeImage = new PetIconChanger();
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update()
	{
		//InitialPosition = transform.position - Vector3.down;
		if(Input.touches.Length <= 0)
		{
			// if no touches on the screen
		}
		else // if there is a touch on the screen
		{
			if(Input.touchCount > 0)
			{
				if(Input.GetTouch(0).phase == TouchPhase.Began)
				{
					//transform.Rotate(Time.deltaTime * 90, 0 ,0);
					//transform.position += new Vector3(0.03f, 0, 0);
					//Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
					//RaycastHit hit = new RaycastHit();
				}
				if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
				{
					touchDeltaPosition = Input.GetTouch(0).deltaPosition;
					transform.RotateAround(this.transform.position, Vector3.down,
					                       touchDeltaPosition.x * speed);
				}
			}
		}
	}
	 
	public void ButtonDogClick () 
	{
		models[Modelnum].SetActive(false);
		Modelnum = 0;
		PetName.text = "Dog";
		Price.text = "FREE";
		models[Modelnum].SetActive(true);
		//ClassChangeImage.modelnum = Modelnum; 			// instead of this code, this line cannot change the variable in other scripts
		//ClassChangeImage.GetComponent<PetIconChanger>().modelnum = Modelnum; 	// thsi line will trigger any public variables in other scripts
		//DogBtn.onClick.AddListener(() => {ModelCalled(0);});
		PetIconChanger.modelnum = Modelnum;
	}

	public void ButtonCatClick () 
	{
		models[Modelnum].SetActive(false);
		Modelnum = 1;
		PetName.text = "Cat";
		Price.text = "FREE";
		models[Modelnum].SetActive(true);
		PetIconChanger.modelnum = Modelnum;
	}

	public void ModelCalled(int num)
	{
		models[num].SetActive(true);
		Modelnum = num;
		Debug.Log("clicked");
	}
	
}
