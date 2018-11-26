using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Player.PlayerState direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Player.instance != null)
            Player.instance.ChangeState(direction);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Player.instance != null)
            Player.instance.ChangeState(Player.PlayerState.STOP);
    }
}
