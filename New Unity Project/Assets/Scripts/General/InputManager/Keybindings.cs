using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Keybindings", menuName = "Keybindings")]
public class Keybindings : ScriptableObject
{
    [System.Serializable]
    public class KeysArray
    {
        public KeyList KeyFor;

        public KeyCode keyBinding;
    }

    public KeysArray[] arrayOfKeys;
    public enum KeyList
    {
        Jump,
        Attack, 
        Shoot,
        ResetBullet,
        Skill1,
        Skill2,
        SkillsMenuHotKey,
        PauseMenu,
    }
    public KeyCode CheckKey(KeyList key)
    {
        KeysArray k = GetKeyPressed(key);
        return k.keyBinding;
    }

    private KeysArray GetKeyPressed(KeyList key)
    {
        foreach (KeysArray currentKey in arrayOfKeys)
        {
            if (currentKey.KeyFor == key)
            {
                return currentKey;
            }
        }
        return null;
    }

    public void ChangeKey(KeyList key, KeyCode newKey)
    {
        foreach (KeysArray currentKey in arrayOfKeys)
        {
            if (currentKey.KeyFor == key)
            {
                currentKey.keyBinding = newKey;
            }
        }
    }
}
