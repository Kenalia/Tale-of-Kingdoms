using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour {

    public static PlayerPrefsManager PPM;

    //PlayerPrefs Strings

    //Settings
    [ReadOnly] public string volume = "volume";

    //Keys
    [Header("InputKey Save Tags")]
    [ReadOnly] public string left_Key = "leftKey";
    [ReadOnly] public string right_Key = "rightKey";
    [ReadOnly] public string forward_Key = "forwardKey";
    [ReadOnly] public string backward_Key = "backwardKey";
    [ReadOnly] public string jump_Key = "jumpKey";
    [ReadOnly] public string sprint_Key = "sprintKey";
    [ReadOnly] public string crouch_Key = "crouchKey";
    [ReadOnly] public string interact_Key = "interactKey";
    [ReadOnly] public string attack_Key = "attackKey";
    [ReadOnly] public string interact_secondary_Key = "interactSecondaryKey";

    List<string> Keys;

    private void Awake()
    {
        //Singleton pattern
        if (PPM == null)
        {
            DontDestroyOnLoad(gameObject);
            PPM = this;
        }
        else if (PPM != this)
        {
            Destroy(gameObject);
        }

        Keys = new List<string>();

        Keys.Add(left_Key);
        Keys.Add(right_Key);
        Keys.Add(forward_Key);
        Keys.Add(backward_Key);
        Keys.Add(jump_Key);
        Keys.Add(sprint_Key);
        Keys.Add(crouch_Key);
        Keys.Add(interact_Key);
        Keys.Add(attack_Key);
        Keys.Add(interact_secondary_Key);

    }

    public float GetVolume()
    {
        return PlayerPrefs.GetFloat(volume);
    }

    public string GetKeyCode(string prefsKey, string defaultKeyCode)
    {
        return PlayerPrefs.GetString(prefsKey, defaultKeyCode);
    }

    public void SetKeyCode(string prefsKey, KeyCode newKeyCode)
    {
        if (Keys.Contains(prefsKey))
        {
            PlayerPrefs.SetString(prefsKey, newKeyCode.ToString());
        }
        else
        {
            Debug.LogError("PlayerPrefsManager.Keys does not contain a key for " + prefsKey);
        }
    }

    public void SetVolume(float newVolume)
    {
        PlayerPrefs.SetFloat(volume, newVolume);
    }


}
