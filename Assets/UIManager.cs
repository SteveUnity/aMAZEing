using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour {

    public Canvas canvas;
    [Space(5)]
    public Slider mazeSize;
    public InputField mazeSizeText;
    [Space(5)]
    public Slider mazeScale;
    public InputField mazeScaleText;
    [Space(5)]
    public Slider mazeHeight;
    public InputField mazeHeightText;
    [Space(5)]
    public InputField seedInput;
    public Button generateMaze;
    public Button endGame;
    [Space(15)]
    public GameObject player;


    private MazeGenerator mazeGen;
    private bool inGame = false;

    // Use this for initialization
    void Start () {
        mazeGen = GetComponent<MazeGenerator>();
        mazeSize.onValueChanged.AddListener(SetMazeSize);
        mazeScale.onValueChanged.AddListener(SetMazeScale);
        mazeHeight.onValueChanged.AddListener(SetMazeHeight);
        seedInput.onEndEdit.AddListener(SetSeed);
        generateMaze.onClick.AddListener(CreateMaze);
        endGame.onClick.AddListener(EndGame);

        SetMazeSize(mazeSize.value);
        SetMazeScale(mazeScale.value);
        SetMazeHeight(mazeHeight.value);
        seedInput.text = mazeGen.Seed.ToString();

    }
	
	// Update is called once per frame
	void Update () {

       
        if (inGame)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                canvas.enabled = !canvas.enabled;
            }
            if(endGame.interactable == false)
            endGame.interactable = true;
        }
        else
        {
            if (canvas.enabled == false)
                canvas.enabled = true;
            if (endGame.interactable == true)
                endGame.interactable = false;
        }
        if (canvas.enabled && !seedInput.isFocused)
        {
            seedInput.text = mazeGen.Seed.ToString();
        }

    }
    void SetMazeSize(float value)
    {
        mazeGen.mazeSize = (int)value;
        mazeSizeText.text = ((int)value).ToString();
    }
    void SetMazeScale(float value)
    {
        mazeGen.mazeScale = value;
        mazeScaleText.text = value.ToString();
    }
    void SetMazeHeight(float value)
    {
        mazeGen.mazeHeight = value;
        mazeHeightText.text = value.ToString();
    }
    void SetSeed(string value)
    {
        mazeGen.Seed = int.Parse(value);
    }
    void CreateMaze()
    {
        inGame = true;
        mazeGen.GenerateMaze();
        player.SetActive(true);
    }
    void EndGame()
    {
        inGame = false;
        player.SetActive(false);
        mazeGen.DestroyMaze();
    }

}
