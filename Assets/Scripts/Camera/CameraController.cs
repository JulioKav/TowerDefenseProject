using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 4f;
    public float panBorderThickness = 10f;

    bool cameraMovableByPlayer = false;

    private bool doMovement = true;

    public bool movementWithMouse = false;

    void Update()
    {
        if (!cameraMovableByPlayer) return;

        if (Input.GetKey(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement) { return; }

        if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow) || movementWithMouse && Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime / Time.timeScale);
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || movementWithMouse && Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime / Time.timeScale);
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || movementWithMouse && Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime / Time.timeScale);
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || movementWithMouse && Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime / Time.timeScale);
        }
    }

    // This scripts subscribes the attached game object to the GameEndEvent, and disables it on game end
    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
        UIStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
        UIStateManager.OnStateChange += StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.PATH_GENERATION:
            case GameState.PRE_GAME:
            case GameState.WIN:
            case GameState.LOSE:
                cameraMovableByPlayer = false;
                break;
            case GameState.ROUND_ONGOING:
                cameraMovableByPlayer = true;
                break;
            default:
                break;
        }
    }

    void StateChangeHandler(UIState newState)
    {
        switch (newState)
        {
            case UIState.BOONS:
            case UIState.SKILL_TREE:
            case UIState.DIALOGUE:
                cameraMovableByPlayer = false;
                break;
            case UIState.IDLE:
                cameraMovableByPlayer = true;
                break;
            default: break;
        }
    }
}
