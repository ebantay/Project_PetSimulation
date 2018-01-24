using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BathScript : MonoBehaviour {
	
	public List<Sprite> SpritesBath = new List<Sprite>();
	public Canvas PanelImage;

	public Image Picture1;
	public Image Picture2;
	public Image Picture3;
	public Image Picture4;
	public Image Picture5;
	public Image Picture6;
	public Image Picture7;
	public Image Picture8;
	public Image Picture9;
	public Image Picture10;
	
	public Button btn1;
	public Image btn2;

	public Text Message1;
	public Text Message2;
	// Use this for initialization
	void Start () {
		PanelImage = PanelImage.GetComponent<Canvas>();
		Picture1 = Picture1.GetComponent<Image>();
		Picture2 = Picture2.GetComponent<Image>();
		Picture3 = Picture3.GetComponent<Image>();
		Picture4 = Picture4.GetComponent<Image>();
		Picture5 = Picture5.GetComponent<Image>();
		Picture6 = Picture6.GetComponent<Image>();
		Picture7 = Picture7.GetComponent<Image>();
		Picture8 = Picture8.GetComponent<Image>();
		Picture9 = Picture9.GetComponent<Image>();
		Picture10 = Picture10.GetComponent<Image>();
		
		btn1 = btn1.GetComponent<Button>();
		btn2 = btn2.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Picture1.enabled == false && Picture2.enabled == false &&
		   Picture3.enabled == false && Picture4.enabled == false &&
		   Picture5.enabled == false && Picture6.enabled == false &&
		   Picture7.enabled == false && Picture8.enabled == false &&
		   Picture9.enabled == false && Picture10.enabled == false)
		{
			Message1.text = "Good Job! You've done well :)";
			btn2.enabled = true;
			Message2.enabled = true;	
		}
	}

	public void exitBtn()
	{
		Application.LoadLevel("InGame");
	}

	public void BtnFunction1()
	{
		PanelImage.enabled = true;	
		Message1.text = "Pop the bubbles to rinse the the whole body of your pet to finish washing";
	}
	
	public void checkWash1()
	{
		if(Picture1.enabled)
		{
			Picture1.enabled = false;
		}
		else
		{
			Picture1.enabled = true;
		}
	}
	
	public void checkWash2()
	{
		if(Picture2.enabled)
		{
			Picture2.enabled = false;
		}
		else
		{
			Picture2.enabled = true;
		}
	}
	
	public void checkWash3()
	{
		if(Picture3.enabled)
		{
			Picture3.enabled = false;
		}
		else
		{
			Picture3.enabled = true;
		}
	}
	
	public void checkWash4()
	{
		if(Picture4.enabled)
		{
			Picture4.enabled = false;
		}
		else
		{
			Picture4.enabled = true;
		}
	}
	
	public void checkWash5()
	{
		if(Picture5.enabled)
		{
			Picture5.enabled = false;
		}
		else
		{
			Picture5.enabled = true;
		}
	}
	
	public void checkWash6()
	{
		if(Picture6.enabled)
		{
			Picture6.enabled = false;
		}
		else
		{
			Picture6.enabled = true;
		}
	}
	
	public void checkWash7()
	{
		if(Picture7.enabled)
		{
			Picture7.enabled = false;
		}
		else
		{
			Picture7.enabled = true;
		}
	}
	
	public void checkWash8()
	{
		if(Picture8.enabled)
		{
			Picture8.enabled = false;
		}
		else
		{
			Picture8.enabled = true;
		}
	}
	
	public void checkWash9()
	{
		if(Picture9.enabled)
		{
			Picture9.enabled = false;
		}
		else
		{
			Picture9.enabled = true;
		}
	}
	
	public void checkWash10()
	{
		if(Picture10.enabled)
		{
			Picture10.enabled = false;
		}
		else
		{
			Picture10.enabled = true;
		}
	}
}
