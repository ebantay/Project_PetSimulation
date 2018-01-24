using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {

	private Image BGImage;
	private Image joystickImage;
	public Vector3 InputDirection{ set; get; }

	void Start()
	{
		BGImage = GetComponent<Image>();
		joystickImage = transform.GetChild(0).GetComponent<Image>();
		InputDirection	= Vector3.zero;
	}

	public virtual void OnDrag(PointerEventData Ped)
	{
		Vector2 pos = Vector2.zero;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
		    (BGImage.rectTransform,
		 		Ped.position,
		 		Ped.pressEventCamera,
		 		out pos))
		{
			pos.x = (pos.x / BGImage.rectTransform.sizeDelta.x);
			pos.y = (pos.y / BGImage.rectTransform.sizeDelta.y);

			float x = (BGImage.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			float y = (BGImage.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

			InputDirection = new Vector3(x, 0, y);
			InputDirection = (InputDirection.magnitude > 1) ?InputDirection.normalized : InputDirection;
			
			joystickImage.rectTransform.anchoredPosition = 
				new Vector3(InputDirection.x * (BGImage.rectTransform.sizeDelta.x / 3)
				            , InputDirection.z * (BGImage.rectTransform.sizeDelta.y / 3));
		}
	}
	public virtual void OnPointerDown(PointerEventData Ped)
	{
		OnDrag(Ped)	;
	}
	public virtual void OnPointerUp(PointerEventData Ped)
	{
		InputDirection = Vector3.zero;
		joystickImage.rectTransform.anchoredPosition = Vector3.zero;
	}
}
