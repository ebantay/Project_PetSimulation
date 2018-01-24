using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveInfo : MonoBehaviour {
	public Text savePetName;
	private string PetSelectName;
	public int statusSave;

	public DogFunction _DogFunction;

	// Update is called once per frame

	public void SaveName()
	{
		if (savePetName.text == "")
		{
			Debug.Log ("You must input before you proceed");
		}
		else
		{
		PlayerPrefs.SetString("SelectedName", savePetName.text);
		Application.LoadLevel("InGame");
			Debug.Log (PlayerPrefs.GetString("SelectedName"));
		}

	}

	public void LoadSave()
	{
		PetSelectName = PlayerPrefs.GetString("SelectedName");
		_DogFunction.PetName.text = PetSelectName;
	}

	public void LoadSlot1()
	{
		PlayerPrefs.SetString("SelectedName", PetSelectName);
		PetSelectName = PlayerPrefs.GetString("SelectedName");
	}
	
}
