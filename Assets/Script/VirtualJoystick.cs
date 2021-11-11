using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour
{
    private Image imageBackground;
    private Vector2 touchPosition;
    private Button upBtn;
    private Button downBtn;
    private Button leftBtn;
    private Button rightBtn;

    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;

    private void Awake()
    {
        imageBackground = GetComponent<Image>();
        upBtn = transform.GetChild(0).GetComponent<Button>();
        downBtn = transform.GetChild(1).GetComponent<Button>();
        leftBtn = transform.GetChild(2).GetComponent<Button>();
        rightBtn = transform.GetChild(3).GetComponent<Button>();
        
    }

    public void GoUp()
    {
        up = true;
        down = false;
        left = false;
        right = false;
    }

    public void GoDown()
    {
        up = false;
        down = true;
        left = false;
        right = false;
    }

    public void goLeft()
    {
        up = false;
        down = false;
        left = true;
        right = false;
    }

    public void goRight()
    {
        up = false;
        down = false;
        left = false;
        right = true;
    }

    public int getDirection()
    {
        if (up)
        {
            return 0;
        }
        else if (down)
        {
            return 1;
        }
        else if (left)
        {
            return 2;
        }
        else if (right)
        {
            return 3;
        }
        return -1;
    }
}
