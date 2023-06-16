using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    Dictionary<string, int> scenes = new Dictionary<string, int>()
    {
        { "menu" , 0 },
        { "main" , 1 }
    };

    Button startButton;
    Button quitButton;
    public TMP_InputField nameInputField;

    

    // Start is called before the first frame update
    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        startButton.onClick.AddListener(StartNew);
        quitButton = GameObject.Find("QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(Exit);
        nameInputField = GameObject.Find("NameInputField").GetComponent<TMP_InputField>();
        nameInputField.onEndEdit.AddListener(LockInput);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  Start new game
    void StartNew()
    {
        if (GeneralManager.instance.currentPlayername == "")
        {
            Debug.Log("Required field: player name");
        }
        else
        {
            SceneManager.LoadScene(scenes["main"]);
        }
        
    }

    // Exit game
    void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    void LockInput(string input)
    {
        if(input.Length == 0)
        {
            Debug.Log("Main input empty");
        }
        else
        {
            Debug.Log("Hello, " + input);
        }

        GeneralManager.instance.currentPlayername = input;
        GeneralManager.instance.hiscore = 0;
    }
}
