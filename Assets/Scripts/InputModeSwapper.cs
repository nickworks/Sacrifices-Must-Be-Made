using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputMode
{
    Gamepad,
    MouseKeyboard
}
public static class PlayerInput
{
    public static InputMode mode = InputMode.MouseKeyboard;
}
public class InputModeSwapper : MonoBehaviour {

	void Update () {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");
        //if (mx > 1 || my > 1 || mx < -1 || my < -1) PlayerController.input = PlayerController.InputConfigState.MouseKeyboard;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //if (Mathf.Abs(h) > .2f || Mathf.Abs(v) > .2f) PlayerController.input = PlayerController.InputConfigState.Gamepad;
    }
}
