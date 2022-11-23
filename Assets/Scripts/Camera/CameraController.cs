using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    private bool doMovement = true;

    private bool movementWithMouse = false;

    void Update()
    {
        if (panSpeed == 0) return;

        if (Input.GetKey(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement) { return; }

        if (Input.GetKey("w") || movementWithMouse && Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("s") || movementWithMouse && Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("d") || movementWithMouse && Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a") || movementWithMouse && Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }
    }
    // This scripts subscribes the attached game object to the GameEndEvent, and disables it on game end
    void OnEnable()
    {
        GameStateManager.OnStateChange += StateChangeHandler;
    }

    void OnDisable()
    {
        GameStateManager.OnStateChange -= StateChangeHandler;
    }

    void StateChangeHandler(GameState newState)
    {
        switch (newState)
        {
            case GameState.WIN:
            case GameState.LOSE:
                panSpeed = 0f;
                break;
            default:
                break;
        }
    }
}
