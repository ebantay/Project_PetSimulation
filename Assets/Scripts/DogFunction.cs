using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


public class DogFunction : MonoBehaviour{
	public Image newImageNotify;
	public Canvas NotifyPanel;
	public Text LblMessage;
	public Text NotificationLabel;
	public Text NotifySave;

	public Image WaterLeftIMG;
	public Image FoodLeftIMG;
	public Text WaterLeft;
	public Text FoodLeft;

	public static int IconNumber = 0;
	public List<Sprite> Sprites1 = new List<Sprite>();

	private bool objTrigger = false;
	private bool objEnable = false;
	private bool objPlayTime = false;
	private bool objBeginTime = true;
	private int NumPlayTime;
	
	public Canvas PausePanel;
	public Canvas MessagePanel;

	public Button btnFood;
	public Button btnWater;
	public Button btnPlay;

	public  int ValWater = 0;
	public  int ValFood = 0;

	public Animator anim; 
	Vector3 tempMove;

	//this variables is for the information of current user
	public Text PetName;
	public Text Money;
	public Text lvl;
	public Text lvlIndicator;

	private int baseEXP = 50;
	private int currentEXP = 0;
	private int moneyVAL = 0;
	private int lvlUP = 1;
	private int tempMONEY = 0;
	
	//this variables is for the energy and hunger
	public Image BarEnergy;
	public Image BarHunger;
	public Image Recharge;
	public Text RemainTime;

	public float AmountEnergy;
	public float AmountHunger;


	//this variables is for the time functionality of the game	
	private float tempNumber = 20.0f;
	private float tempTimer = 60.0f;

	private int OldTimeHRS;
	private int OldTimeMIN;
	private int NewTimeHRS;
	private int NewTimeMIN;

	private int tempHRS;
	private int tempHRS1;
	private int tempMIN;
	private float RemainMIN;

	public Canvas ShopPanel;
	
	public Text ValWater1;
	public Text ValFood1;
	public Text	ValEnergy1;
	
	public Button btn_Water;
	public Button btn_Food;
	public Button btn_Energy;
	void Start()
	{
		//check if the player was newbie or not
		SaveSystemRetrieve();

		if (int.Parse(lvl.text) == 0)
		{
			MessagePanel.enabled = true;
			ValFood += 50;
			ValWater += 50;
			AmountEnergy = 1;
			AmountHunger = 1;
			lvl.text = "1";
		}					
		else
		{
			objPlayTime = true;

			print("Last time: " + OldTimeHRS + ":" + OldTimeMIN);
			print("New time: " + NewTimeHRS + ":" + NewTimeMIN);


		// this code will detect the time in previously and today's time
			if (NewTimeHRS < OldTimeHRS)
			{
				NewTimeHRS += 24;
			}

			tempHRS = OldTimeHRS * 60;
			tempHRS1 = NewTimeHRS * 60;
			OldTimeMIN = OldTimeMIN + tempHRS;
			NewTimeMIN = NewTimeMIN + tempHRS1;
			tempMIN = NewTimeMIN - OldTimeMIN;

		// this code will normalize the range of previouly log and the current log time of user
			if (tempMIN >= 20)
			{
				for(int a = 0; tempMIN >= 20; a++)
				{
					tempMIN = tempMIN - 20;
					AmountEnergy = AmountEnergy + 0.2f;
					AmountHunger = AmountHunger - 0.05f;
					StatusBar();
				}
			}
			tempMIN = 20 - tempMIN;
		}

		SaveSystemSet();
		RemainTime.text = "Recharging in " + tempMIN.ToString() + " Minutes";
		RemainMIN = tempMIN;


		anim = GetComponent<Animator>();

		BarEnergy = BarEnergy.GetComponent<Image>();
		BarHunger = BarHunger.GetComponent<Image>();
		Recharge = Recharge.GetComponent<Image>();

		btnFood = btnFood.GetComponent<Button>();
		btnWater = btnWater.GetComponent<Button>();
		btnPlay = btnPlay.GetComponent<Button>();

		ShopPanel = ShopPanel.GetComponent<Canvas>();
		PausePanel = PausePanel.GetComponent<Canvas>();
		NotifyPanel = NotifyPanel.GetComponent<Canvas>();
		LblMessage = LblMessage.GetComponent<Text>();

		print("Amount of Energy: " + AmountEnergy + " \nAmount of Hunger: " + AmountHunger);
	}

	// this update have smoothly process the in game time
	void FixedUpdate()
	{
		if (objPlayTime)
		{
			tempNumber -= Time.deltaTime;

			if(tempNumber  <= 0.0f)								//will exexute only if the temporary minute meets zero value
			{
				tempNumber = 20.0f;

				AmountEnergy = AmountEnergy - 0.02f;
				AmountHunger = AmountHunger - 0.01f;

				
				if (BarHunger.fillAmount <= 0.0f)
				{
					AmountHunger = 0.0f;
				}
				//print("Amount of Energy: " + AmountEnergy + " \nAmount of Hunger: " + AmountHunger);
				StatusBar();
			}
		}														//this will execute if the player is in game


		//will execute when the player is on game
		if(objBeginTime)
		{
			tempTimer -= Time.deltaTime;

			if (BarEnergy.fillAmount >= 1.0f)					//check if the energy bar is full or not
			{
				objBeginTime = false;
				Recharge.enabled = false;
				RemainTime.enabled = false;
				AmountEnergy = 1.0f;
			}
			else
			{
				if (tempTimer <= 0.0f)
				{
					tempTimer = 60.0f;
					RemainMIN -= 1.0f;
					RemainTime.text = "Recharging in " + Mathf.Round(RemainMIN).ToString() + " Minutes";
				}
		
				if (RemainMIN <= 0.0f)
				{
					objBeginTime = false;
					RemainMIN = 60.0f;
					Recharge.enabled = false;
					RemainTime.enabled = false;
					AmountEnergy = AmountEnergy + 0.2f;
					StatusBar();

					NotificationLabel.text = "Recharging +20 to your energy";
					StartCoroutine(AnimationLbl());
				}
			}
		}

	}

	// this update will trigger on every action done by the user
	void Update()
	{

		//testIMG.rectTransform.sizeDelta = new Vector2(tempNumber, 0f);
		if (objTrigger)
		{
			if(currentEXP >= baseEXP)
			{
				objTrigger = false;
				IconNumber = 1;
				StartCoroutine(LevelUp());
			}
			else
			{
				objTrigger = false;
				IconNumber = 0;
				StartCoroutine(Example());
			}
		}

		if ((Input.GetKey(KeyCode.Escape))) 
		{
			PausePanel.enabled = false;
			ShopPanel.enabled = false;
		}
	}


	public void PlayAnimationDrinking()	
	{
		if(BarHunger.fillAmount == 1.0f)
		{
			NotificationLabel.text = "Your pet is not hungry anymore";
			StartCoroutine(AnimationLbl());
		}
		else if(ValFood <= 0)
		{
			NotificationLabel.text = "Insufficient Food";
			StartCoroutine(AnimationLbl());
		}
		else
		{
			transform.position = new Vector3(2.78f,0f,-18.84f);
			anim.Play("Drink");
			objTrigger = true;

			ValFood -= 1;
			currentEXP += 6;
			moneyVAL = int.Parse(Money.text) + 10;
			tempMONEY = 10;
			AmountHunger += 0.2f;
		}
	}

	public void PlayAnimationEating()	
	{
		if(BarHunger.fillAmount == 1.0f)
		{
			NotificationLabel.text = "Your pet is not hungry anymore";
			StartCoroutine(AnimationLbl());
		}
		else if(ValWater <= 0)
		{
			NotificationLabel.text = "Insufficient Water";
			StartCoroutine(AnimationLbl());
		}
		else
		{
			transform.position = new Vector3(1.58f,0f,-18.84f);
			anim.Play("Eating");
			objTrigger = true;

			ValWater -= 1;
			currentEXP += 3;
			moneyVAL = int.Parse(Money.text) + 5;
			tempMONEY = 5;
			AmountHunger += 0.1f;
		}

	}

	public void PlayAnimationPlay()
	{
		int n = Random.Range(0, 2);
		if (n == 0)
		{
			anim.Play("Walk");
		}
		else
		{
			anim.Play("Run");
		}
		objTrigger = true;
	}


	IEnumerator Example() 
	{
		objEnable = false;
		EnableButton();
		print(AmountHunger);
		yield return new WaitForSeconds(5);
		transform.position= new Vector3(2.77f,0f,-20.52f);
		objEnable = true;
		NotifyPanel.enabled = true;
		newImageNotify.sprite = Sprites1[IconNumber];
		LblMessage.text = "+ " + tempMONEY.ToString();
		StatusBar();

		yield return new WaitForSeconds(3);
		EnableButton();
		NotifyPanel.enabled = false;
		lvlIndicator.text = currentEXP.ToString() + "/" + baseEXP.ToString() + " EXP";
		Money.text = moneyVAL.ToString();
	}

	IEnumerator LevelUp() 
	{
		print(AmountHunger);
		yield return new WaitForSeconds(5);
		transform.position= new Vector3(2.77f,0f,-20.52f);
		newImageNotify.sprite = Sprites1[IconNumber];
		lvlUP += 1;
		currentEXP = 0;
		objEnable = true;
		NotifyPanel.enabled = true;
		LblMessage.text = "You are now " + "\nlevel " + lvlUP.ToString() + " !";
		StatusBar();

		yield return new WaitForSeconds(3);
		EnableButton();
		NotifyPanel.enabled = false;
		lvl.text = lvlUP.ToString();
		baseEXP = baseEXP * int.Parse(lvl.text);
		lvlIndicator.text = currentEXP.ToString() + "/" + baseEXP.ToString() + " EXP";
		Money.text = moneyVAL.ToString();
	}

	IEnumerator AnimationLbl()
	{
		NotificationLabel.enabled = true;

		yield return new WaitForSeconds(3);
		NotificationLabel.enabled = false;
		NotificationLabel.text = "";
	}

	IEnumerator AnimationSave()
	{
		yield return new WaitForSeconds(3);
		NotifySave.enabled = false;
		NotifySave.text = "";
	}


	public void EnableButton()
	{
		btnFood.enabled = objEnable;
		btnWater.enabled = objEnable;
		btnPlay.enabled = objEnable;
	}

	public void GameBathEnable()
	{
		Application.LoadLevel("GameBath");
	}

	public void StartPanel()
	{
		objPlayTime = true;
		MessagePanel.enabled = false;
	}


	public void enablePanel()
	{
		if(PausePanel.enabled)
		{
			PausePanel.enabled = false;
		}
		else
		{
			PausePanel.enabled = true;
		}
	}

	public void enablePanel1()
	{
		if(ShopPanel.enabled)
		{
			ShopPanel.enabled = false;
		}
		else
		{
			ShopPanel.enabled = true;
		}
	}

	public void ExitGameYes()
	{
		SaveSystem();
		Application.Quit();

	}

	public void StatusBar()
	{
		BarEnergy.fillAmount = AmountEnergy;
		BarHunger.fillAmount = AmountHunger;
		WaterLeft.text = ValWater.ToString() + " LEFT";
		FoodLeft.text = ValFood.ToString() + " LEFT";
	}

	public void ShowRemainValue1()
	{
		if(WaterLeft.enabled)
		{
			WaterLeft.enabled = false;
			WaterLeftIMG.enabled = false;
		}
		else
		{
			WaterLeft.enabled = true;
			WaterLeftIMG.enabled = true;
		}
	}

	public void ShowRemainValue2()
	{
		if(FoodLeft.enabled)
		{
			FoodLeft.enabled = false;
			FoodLeftIMG.enabled = false;
		}
		else
		{
			FoodLeft.enabled = true;
			FoodLeftIMG.enabled = true;
		}
	}
	

	public void SaveSystem()
	{
		//print("save");
		string tempNAME = PlayerPrefs.GetString("SelectedName");

		int tempLVL = int.Parse(lvl.text);
		int tempEXP = currentEXP;
		int tempMONEY = int.Parse(Money.text);

		int tempHRS1 = int.Parse(System.DateTime.Now.ToString("H "));
		int tempMIN1 = int.Parse(System.DateTime.Now.ToString("m "));
		float tempBarEnergy = BarEnergy.fillAmount;
		float tempBarHunger = BarHunger.fillAmount;

		PlayerPrefs.SetString("SelectedName", tempNAME);

		PlayerPrefs.SetInt("SaveLVL", tempLVL);
		PlayerPrefs.SetInt("SaveEXP", tempEXP);
		PlayerPrefs.SetInt("SaveMONEY", tempMONEY);

		PlayerPrefs.SetInt("SaveHRS", tempHRS1);
		PlayerPrefs.SetInt("SaveMIN", tempMIN1);
		PlayerPrefs.SetFloat("SaveBarEnergy", tempBarEnergy);
		PlayerPrefs.SetFloat("SaveBarHunger", tempBarHunger);

		PlayerPrefs.SetInt("SaveActionWATER", ValWater);
		PlayerPrefs.SetInt("SaveActionFOOD", ValFood);
		NotifySave.enabled = true;
		StartCoroutine(AnimationSave());
	}

	public void SaveSystemRetrieve()
	{
		PetName.text = PlayerPrefs.GetString("SelectedName");

		currentEXP = PlayerPrefs.GetInt("SaveEXP");
		Money.text = PlayerPrefs.GetInt("SaveMONEY").ToString();
		lvl.text = PlayerPrefs.GetInt("SaveLVL").ToString();

		AmountEnergy = PlayerPrefs.GetFloat("SaveBarEnergy");
		AmountHunger = PlayerPrefs.GetFloat("SaveBarHunger");
		OldTimeHRS = PlayerPrefs.GetInt("SaveHRS"); 
		OldTimeMIN = PlayerPrefs.GetInt("SaveMIN");
		NewTimeHRS = int.Parse(System.DateTime.Now.ToString("H "));
		NewTimeMIN = int.Parse(System.DateTime.Now.ToString("m "));

		ValFood = PlayerPrefs.GetInt("SaveActionFOOD"); 
		ValWater = PlayerPrefs.GetInt("SaveActionWATER");
	}

	public void SaveSystemSet()
	{
		if (BarEnergy.fillAmount >= 1.0f)
		{
			AmountEnergy = 1.0f;
		}
		
		
		if (BarHunger.fillAmount <= 0.0f)
		{
			AmountHunger = 0.0f;
		}


		baseEXP = baseEXP * int.Parse(lvl.text);
		lvlIndicator.text = currentEXP.ToString() + "/" + baseEXP.ToString() + " EXP";
		WaterLeft.text = ValWater.ToString() + " LEFT";
		FoodLeft.text = ValFood.ToString() + " LEFT";

		BarEnergy.fillAmount = AmountEnergy;
		BarHunger.fillAmount = AmountHunger;

		ValWater1.text = "YOU HAVE:  " + ValWater.ToString();
		ValFood1.text = "YOU HAVE:  " + ValFood.ToString();
		ValEnergy1.text = "YOU HAVE:  " + (AmountEnergy * 100).ToString();
	}

	public void SaveSystemClear()
	{
		int tempINT = 0;
		string tempSTRING = "";
		float tempFLOAT = 0.0f;

		PlayerPrefs.SetString("SelectedName", tempSTRING);

		PlayerPrefs.SetInt("SaveLVL", tempINT);
		PlayerPrefs.SetInt("SaveEXP", tempINT);
		PlayerPrefs.SetInt("SaveMONEY", tempINT);
		
		PlayerPrefs.SetInt("SaveHRS", tempINT);
		PlayerPrefs.SetInt("SaveMIN", tempINT);
		PlayerPrefs.SetFloat("SaveBarEnergy", tempFLOAT);
		PlayerPrefs.SetFloat("SaveBarHunger", tempFLOAT);

		PlayerPrefs.SetInt("SaveActionWATER", tempINT);
		PlayerPrefs.SetInt("SaveActionFOOD", tempINT);
	}

	public void ComputeVal1()
	{
		if(int.Parse(Money.text) <= 3)
		{
			NotificationLabel.text = "Insufficient money! Cannot Buy!";
			StartCoroutine(AnimationLbl());
		}
		else
		{
			ValWater += 2;
			moneyVAL = int.Parse(Money.text) - 3;
			Money.text = moneyVAL.ToString();
			ValWater1.text = "YOU HAVE:  " + ValWater.ToString();
			StatusBar();
		}
	}
	
	public void ComputeVal2()
	{
		if(int.Parse(Money.text) <= 7)
		{
			NotificationLabel.text = "Insufficient money! Cannot Buy!";
			StartCoroutine(AnimationLbl());
		}
		else
		{
			ValFood += 1;
			moneyVAL = int.Parse(Money.text) - 7;
			Money.text = moneyVAL.ToString();
			ValFood1.text = "YOU HAVE:  " + ValFood.ToString();
			StatusBar();
		}
	}
	
	public void ComputeVal3()
	{
		if(int.Parse(Money.text) <= 10)
		{
			NotificationLabel.text = "Insufficient money! Cannot Buy!";
			StartCoroutine(AnimationLbl());
		}
		else if(AmountEnergy >= 1.0f)
		{
			NotificationLabel.text = "Your Energy is full!";
			StartCoroutine(AnimationLbl());
		}
		else
		{
			AmountEnergy += 0.1f;
			moneyVAL = int.Parse(Money.text) - 10;
			Money.text = moneyVAL.ToString();
			ValEnergy1.text = "YOU HAVE:  " + (AmountEnergy * 100).ToString();
			StatusBar();
		}
	}
}
