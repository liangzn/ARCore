using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player.
/// Use state machine to move player.
/// </summary>
public class Player : MonoBehaviour {

    public static Player instance;

    private Vector3 direction = Vector3.zero;

    public enum PlayerState
    {
        STOP,
        MOVING_FORWARD,
        MOVING_BACKWARD,
        MOVING_LEFT,
        MOVING_RIGHT
    };

    private PlayerState state = PlayerState.STOP;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (state != PlayerState.STOP)
            transform.Translate(direction * Time.deltaTime);
    }

    public void ChangeState(PlayerState _state)
    {
        state = _state;
        switch (_state)
        {
            case PlayerState.MOVING_FORWARD:
                direction = Vector3.forward;
                break;
            case PlayerState.MOVING_BACKWARD:
                direction = Vector3.back;
                break;
            case PlayerState.MOVING_LEFT:
                direction = Vector3.left;
                break;
            case PlayerState.MOVING_RIGHT:
                direction = Vector3.right;
                break;
            default:
                break;
        }
    }

    public void Attack ()
    {
        BulletManager.Instance.CreateBullet(transform.position);
    }
}
