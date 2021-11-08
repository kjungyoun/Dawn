using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool check;
    [SerializeField]
    private PlayerController playerController;

    // 전역변수 선언

    public void OnPointerDown(PointerEventData eventData)
    {

        check = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        check = false;

    }

    void Update()
    {

        if (check)
        {
            playerController.UpdateMove(1);
        }
        else
        {
            playerController.UpdateMove(0);

        }

    }

}
