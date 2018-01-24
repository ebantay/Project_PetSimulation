using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PetIconChanger : MonoBehaviour {

	public static int modelnum = 0;
	public Image newImage;
	private Sprite IconHolder;
	public Canvas PopUp;
	public List<Sprite> Sprites = new List<Sprite>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			if(PopUp.enabled == false)
			{
				Application.LoadLevel("StartMenu");
			}

			if(PopUp.enabled == true)
			{
				CancelPopUp();
			}
		}
	}

	public void EnablePopUp()
	{
		PopUp.enabled = true;
		newImage.sprite = Sprites[modelnum];
		//Sprites[num] = IconHolder;
		//newSprite = Resources.Load("slice05_05") as Sprite;
		//sprRend = (SpriteRenderer)Renderer;
		//newImage = Resources.Load<Sprite>("slice05_05");
		//newImage.sprite = Resources.Load("slice05_05") as Sprite;
	}

	public void CancelPopUp()
	{
		PopUp.enabled = false;
	}

	public void InputDetails()
	{
		TouchScreenKeyboard.Open("", TouchScreenKeyboardType.NamePhonePad);
	}
}
