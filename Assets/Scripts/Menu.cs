using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {


	public Canvas QuitPanel;
	public Canvas SavePanel;
	public Canvas AboutPanel;
	public Canvas HowPanel;
	public Canvas NotifyPanel;

	public Button StartBtn;
	public Button HowBtn;
	public Button AboutBtn;
	public Button EscapeBtn;

	public Text MessageText;
	public SaveInfo _SaveInfo;
	public DogFunction _dog;

	// Use this for initialization
	void Start () {
		//Camera.main.aspect = 800f / 480f;
		MessageText = MessageText.GetComponent<Text>();
		_SaveInfo = _SaveInfo.GetComponent<SaveInfo>();
		_dog = _dog.GetComponent<DogFunction>();

		NotifyPanel = NotifyPanel.GetComponent<Canvas>();
		QuitPanel = QuitPanel.GetComponent<Canvas>();
		SavePanel = SavePanel.GetComponent<Canvas>();
		AboutPanel = AboutPanel.GetComponent<Canvas>();
		HowPanel = HowPanel.GetComponent<Canvas>();

		StartBtn = StartBtn.GetComponent<Button>();
		EscapeBtn = EscapeBtn.GetComponent<Button>();
		HowBtn = HowBtn.GetComponent<Button>();

		AboutBtn = AboutBtn.GetComponent<Button>();
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
		{
			EnabledPopUpQuit();
			EnabledPopUpSave();
			CanceledPopUpQuit();
			CanceledPopUpSave();
			CanceledPopUpHow();
			CanceledPopUpAbout();
		}
	}

	public void EnabledPopUpQuit()
	{
		QuitPanel.enabled = true;
		StartBtn.enabled = false;
		HowBtn.enabled = false;
		AboutBtn.enabled = false;
	}

	public void CanceledPopUpQuit()
	{
		QuitPanel.enabled = false;
		StartBtn.enabled = true;
		HowBtn.enabled = true;
		AboutBtn.enabled = true;
	}

	public void EnabledPopUpSave()
	{
		SavePanel.enabled = true;
		StartBtn.enabled = false;
		HowBtn.enabled = false;
		AboutBtn.enabled = false;
	}
	
	public void CanceledPopUpSave()
	{
		SavePanel.enabled = false;
		StartBtn.enabled = true;
		HowBtn.enabled = true;
		AboutBtn.enabled = true;
	}

	public void EnabledPopUpHow()
	{
		HowPanel.enabled = true;
		StartBtn.enabled = false;
		HowBtn.enabled = false;
		AboutBtn.enabled = false;
	}
	
	public void CanceledPopUpHow()
	{
		HowPanel.enabled = false;
		StartBtn.enabled = true;
		HowBtn.enabled = true;
		AboutBtn.enabled = true;
	}

	public void EnabledPopUpAbout()
	{
		AboutPanel.enabled = true;
		StartBtn.enabled = false;
		HowBtn.enabled = false;
		AboutBtn.enabled = false;
	}
	
	public void CanceledPopUpAbout()
	{
		AboutPanel.enabled = false;
		StartBtn.enabled = true;
		HowBtn.enabled = true;
		AboutBtn.enabled = true;
	}

	public void ExitGame()
	{
		print ("exit");
		Application.Quit();
	}

	public void StartGame()
	{
		string newSaves = PlayerPrefs.GetString("SelectedName");
		if (newSaves == "")
		{
		Application.LoadLevel("SelectionMenu");
		}
		else
		{
			NotifyPanel.enabled = true;
		}
	}

	public void SaveGameYES()
	{
		Application.LoadLevel("InGame");
	}

	public void SaveGameNO()
	{
		NotifyPanel.enabled = false;
		Application.LoadLevel("SelectionMenu");
		_dog.SaveSystemClear();

	}
}
