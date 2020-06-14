using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeybindsSetter : MonoBehaviour
{
    public Transform menuPanel;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;
    private Keybindings keys;
    private EventSystem evt;

    bool waitingForKey;

    void Start()
    {
        waitingForKey = false;
        keys = InputManager.instance.keybindings;
        evt = EventSystem.current;
        SetStartKeys();
    }

    private void OnGUI()
    {
        keyEvent = Event.current;

        if(keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }

     //   if(InputManager.instance.KeyDown(Keybindings.KeyList.PauseMenu))

    }

    public void StartAssignment()
    {
        GameObject button = evt.currentSelectedGameObject;
        string name = button.transform.parent.name;
        buttonText = button.GetComponentInChildren<Text>();
        if (!waitingForKey)
            StartCoroutine(AssignKey(name));
    }


    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    public IEnumerator AssignKey(string keyName)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        waitingForKey = true;


        yield return WaitForKey();

        switch(keyName)
        {
            case "JumpKey":
                keys.ChangeKey(Keybindings.KeyList.Jump, newKey);
                buttonText.text = newKey.ToString(); 
                break;
            case "AttackKey":
                keys.ChangeKey(Keybindings.KeyList.Attack, newKey);
                buttonText.text = newKey.ToString();
                break;
            case "Shoot":
                keys.ChangeKey(Keybindings.KeyList.Shoot, newKey);
                buttonText.text = newKey.ToString();
                break;
            case "Skill1":
                keys.ChangeKey(Keybindings.KeyList.Skill1, newKey);
                buttonText.text = newKey.ToString();
                break;
            case "Skill2":
                keys.ChangeKey(Keybindings.KeyList.Skill2, newKey);
                buttonText.text = newKey.ToString();
                break;
            case "SkillsMenu":
                keys.ChangeKey(Keybindings.KeyList.SkillsMenuHotKey, newKey);
                buttonText.text = newKey.ToString();
                break;
            case "PauseMenu":
                keys.ChangeKey(Keybindings.KeyList.PauseMenu, newKey);
                buttonText.text = newKey.ToString();
                break;
            case "DestroyProjectile":
                keys.ChangeKey(Keybindings.KeyList.ResetBullet, newKey);
                buttonText.text = newKey.ToString();
                break;
        }  //assignKey

        yield return null;
    }

    private void SetStartKeys()
    {
        for (int i = 0; i < menuPanel.childCount; i++)
        {
            switch(menuPanel.GetChild(i).name)
            {
                case "JumpKey":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.Jump).ToString();
                    break;
                case "AttackKey":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.Attack).ToString();
                    break;
                case "Shoot":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.Shoot).ToString();
                    break;
                case "Skill1":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.Skill1).ToString();
                    break;
                case "Skill2":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.Skill2).ToString();
                    break;
                case "SkillsMenu":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.SkillsMenuHotKey).ToString();
                    break;
                case "PauseMenu":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.PauseMenu).ToString();
                    break;
                case "DestroyProjectile":
                    menuPanel.GetChild(i).GetComponentInChildren<Text>().text = keys.CheckKey(Keybindings.KeyList.ResetBullet).ToString();
                    break;
            }
        }
    }
}
