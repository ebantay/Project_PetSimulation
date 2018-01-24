using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class AudioScript : MonoBehaviour {

	public Text newText;
	public Image newImageAudio;
	public List<Sprite> Sprites2 = new List<Sprite>();

	// Use this for initialization
	void Start () {
		AudioListener.pause = false;
		newText = newText.GetComponent<Text>();
	}

	public void ClickInteraction()
	{
		if (AudioListener.pause)
		{
			AudioListener.pause = false;
			newText.text = "Audio : \n ON";
			newImageAudio.sprite = Sprites2[0];
		}
		else
		{
			AudioListener.pause = true;
			newText.text = "Audio : \n OFF";
			newImageAudio.sprite = Sprites2[1];
		}
	}
}
