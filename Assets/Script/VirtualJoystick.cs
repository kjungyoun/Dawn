using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Image imageBackground;
    private Image imageController;
    private Vector2 touchPosition;

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
        imageController = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchPosition = Vector2.zero;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
        {
            touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
            touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

            touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

            touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

            imageController.rectTransform.anchoredPosition = new Vector2(
                touchPosition.x * imageBackground.rectTransform.sizeDelta.x / 4,
                touchPosition.y * imageBackground.rectTransform.sizeDelta.y / 4);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        imageController.rectTransform.anchoredPosition = Vector2.zero;
        touchPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        return touchPosition.x;
    }

    public float Vertical()
    {
        return touchPosition.y;
    }

    public int getDirection()
    {
        if (touchPosition == Vector2.zero)
        {
            return -1;
        }
        else
        {
            if (touchPosition.x <= 0.5 && touchPosition.x >= -0.5)
            {
                if (touchPosition.y >= 0)
                {
                    Debug.Log("up");
                    return 0;
                }
                else
                {
                    Debug.Log("Down");
                    return 1;
                }
            }

            if (touchPosition.y <= 0.5 && touchPosition.y >= -0.5)
            {
                if (touchPosition.x < 0)
                {
                    Debug.Log("Left");
                    return 2;
                }
                else
                {
                    Debug.Log("Right");
                    return 3;
                }
            }
        }
        return -1;
    }
}
