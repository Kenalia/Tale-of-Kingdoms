using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    //Used for singleton
    public static InputManager IM;

    public KeyCode KEY_left { get; set; }
    public KeyCode KEY_right { get; set; }
    public KeyCode KEY_forward { get; set; }
    public KeyCode KEY_backward { get; set; }
    public KeyCode KEY_jump { get; set; }
    public KeyCode KEY_sprint { get; set; }
    public KeyCode KEY_crouch { get; set; }
    public KeyCode KEY_interact { get; set; }
    public KeyCode KEY_attack { get; set; }
    public KeyCode KEY_interact_secondary { get; set; }

    private void Awake()
    {
        //Singleton pattern
        if(IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }
        else if(IM != this)
        {
            Destroy(gameObject);
        }

        /*Assign each keycode when the game starts.
        * Loads data from PlayerPrefs so if a user quits the game,
        * their bindings are loaded next time. Default values
        * are assigned to each Keycode via the second parameter
        * of the GetString() function
        */

        KEY_left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.left_Key, "A"));
        KEY_right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.right_Key, "D"));
        KEY_forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.forward_Key, "W"));
        KEY_backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.backward_Key, "S"));
        KEY_jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.jump_Key, "Space"));
        KEY_sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.sprint_Key, "LeftShift"));
        KEY_crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.crouch_Key, "LeftControl"));
        KEY_interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.interact_Key, "E"));
        KEY_attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.attack_Key, "Mouse0"));
        KEY_interact_secondary = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefsManager.PPM.GetKeyCode(PlayerPrefsManager.PPM.interact_secondary_Key, "F"));
    }
}
