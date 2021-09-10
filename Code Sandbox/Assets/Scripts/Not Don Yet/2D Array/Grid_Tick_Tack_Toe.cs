using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Grid_Tick_Tack_Toe : MonoBehaviour
{
    public static event EventHandler OnGameEnded;

    [SerializeField] private Sprite Cross;
    [SerializeField] private Sprite Circle;
    [SerializeField] private Grid_ScoreData scoreData;

    private bool crossTurn;
    private bool gameEnded;
    private int amountOfMovesLeft;

    private TMP_Text scoreText;

    private void OnEnable()
    {
        Grid_Test.OnSpawnedTile += Grid_Test_OnSpawnedTile;
        Grid_Test.OnSpawnedGrid += Grid_Test_OnSpawnGrid;
    }   

    private void OnDisable()
    {
        Grid_Test.OnSpawnedTile -= Grid_Test_OnSpawnedTile;
        Grid_Test.OnSpawnedGrid -= Grid_Test_OnSpawnGrid;
    }

    private void Start()
    {
        scoreText = transform.Find("CanvasPlayers").transform.Find("ScoreText").GetComponent<TMP_Text>();
        UpdateScoreText();

        StartingPlayerSetup();
    }

    private void Grid_Test_OnSpawnedTile(object sender, Grid_Test.OnSpawnedTileEveryArgs e)
    {
        SetGridPieceButtonFunction(e.x, e.y, e.grid, e.templateTransform);
    }

    private void Grid_Test_OnSpawnGrid(object sender, Grid_Test.OnSpawnedGirdEventArgs e)
    {
        amountOfMovesLeft = e.grid.Length;        
    }

    private void SetGridPieceButtonFunction(int x, int y, int[,] grid, Transform templateTransform)
    {
        Button button = templateTransform.transform.Find("Canvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            PlaceGridPieceSigns(x, y, grid, templateTransform);
            CheckWinCondition(x, y, grid);
        });

    }

    private void PlaceGridPieceSigns(int x, int y, int[,] grid, Transform templateTransform)
    {     
        if (!gameEnded)
        {
            if (grid[x, y] != 1 && grid[x, y] != -1)
            {
                Image image = templateTransform.transform.Find("Canvas").Find("Button").Find("Image").GetComponent<Image>();

                crossTurn = !crossTurn;

                grid[x, y] = crossTurn ? 1 : -1;

                SetSprites(image);

                ShowActivePlayerLine();

                amountOfMovesLeft--;
            }
        }        
    }

    private void CheckWinCondition(int x, int y, int[,] grid)
    {
        if (!gameEnded)
        {
            //Jak zrobić to for loop
            
            /*int totalX = 0;
            for (int i = 0; i < 3; i++)
            {
                totalX += grid[x, i];
                if (totalX >= 3 || totalX <= -3)
                {
                    gameEnded = true;
                    GameEnded();
                }
            }

            int totalY = 0;
            for (int i = 0; i < 3; i++)
            {
                totalY += grid[i, y];
                if (totalY >= 3 || totalY <= -3)
                {
                    gameEnded = true;
                    GameEnded();
                }
            }

            int totalCrossUp = 0;
            for (int i = 0; i < 3; i++)
            {
                totalCrossUp += grid[i, i];
                if (totalCrossUp >= 3 || totalCrossUp <= -3)
                {
                    gameEnded = true;
                    GameEnded();
                }
            }

            int totalCrossDown = 0;
            int n = 0;
            int m = 2;

            for (int i = 0; i < 3; i++)
            {
                //Skips this part in first iteration, so that line starts properly from 0,2
                if (i != 0)
                {
                    n++;
                    m--;
                }

                totalCrossDown += grid[n, m]; 
                if (totalCrossDown >= 3 || totalCrossDown <= -3)
                {
                    gameEnded = true;
                    GameEnded();
                }
            }*/

            
            //Checking Y
            if ((grid[x, 0] + grid[x, 1] + grid[x, 2]) == 3 || (grid[x, 0] + grid[x, 1] + grid[x, 2]) == -3)
            {
                //Setting Line
                for (int i = 0; i < 3; i++)
                {
                    Grid_Test.templatesDictionary[new Vector2(x, i)].Find("Canvas").Find("Line").gameObject.SetActive(true);
                }

                gameEnded = true;
                GameEnded();
            }
            //Checking X
            else if ((grid[0, y] + grid[1, y] + grid[2, y]) == 3 || (grid[0, y] + grid[1, y] + grid[2, y]) == -3)
            {
                //Setting Line
                for (int i = 0; i < 3; i++)
                {
                    Transform template = Grid_Test.templatesDictionary[new Vector2(i, y)].Find("Canvas").Find("Line");
                    template.gameObject.SetActive(true);
                    template.rotation = Quaternion.Euler(new Vector3(0,0,90));
                    //Setting Line to be on top of grid
                    template.position = new Vector3(template.position.x, template.position.y, template.position.z - 1);
                }

                gameEnded = true;
                GameEnded();
            }
            //Checking Cross from bottom to top
            else if ((grid[0, 0] + grid[1, 1] + grid[2, 2]) == 3 || (grid[0, 0] + grid[1, 1] + grid[2, 2]) == -3)
            {
                //Setting Line
                for (int i = 0; i < 3; i++)
                {
                    Transform template = Grid_Test.templatesDictionary[new Vector2(i, i)].Find("Canvas").Find("Line");
                    template.gameObject.SetActive(true);
                    template.rotation = Quaternion.Euler(new Vector3(0, 0, -45));
                    //Setting Line to be on top of grid
                    template.position = new Vector3(template.position.x, template.position.y, template.position.z - 1);
                }

                gameEnded = true;
                GameEnded();
            }
            //Checking Cross from top to bottom
            else if ((grid[0, 2] + grid[1, 1] + grid[2, 0]) == 3 || (grid[0, 2] + grid[1, 1] + grid[2, 0]) == -3)
            {
                int n = 0;
                int m = 2;

                for (int i = 0; i < 3; i++)
                {
                    //Skips this part in first iteration, so that line starts properly from 0,2
                    if (i != 0)
                    {
                        n++;
                        m--;
                    }
                    
                    Transform template = Grid_Test.templatesDictionary[new Vector2(n, m)].Find("Canvas").Find("Line");
                    template.gameObject.SetActive(true);
                    template.rotation = Quaternion.Euler(new Vector3(0, 0, 45));
                    //Setting Line to be on top of grid
                    template.position = new Vector3(template.position.x, template.position.y, template.position.z - 1);
                }

                gameEnded = true;
                GameEnded();
            }
            else if (amountOfMovesLeft <= 0)
            {
                gameEnded = true;
                GameEnded("Draw!", true);
            }
        }        
    }

    private void GameEnded(string textToShow = "You Win!", bool draw = false)
    {
        transform.Find("Canvas").gameObject.SetActive(true);
        TMP_Text text = transform.Find("Canvas").transform.Find("Text").GetComponent<TMP_Text>();

        Button button = transform.Find("Canvas").transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            Grid_GameManager.ResetScene();
        });

        if (draw)
        {
            text.text = textToShow;
        }
        else
        {
            string winner = crossTurn ? "Cross" : "Circle";
            text.text = "Player " + "<color=#FFA34C>" + "<u>" + winner + "</u>" + "</color>" + " Won!";

            AddPointsToWinner();
        }

        UpdateScoreText();
        ShowActivePlayerLine();

        //Swaping starting player
        scoreData.NotCrossPlayerStarts = !scoreData.NotCrossPlayerStarts;
        OnGameEnded?.Invoke(this, EventArgs.Empty);
    }

    private void ShowActivePlayerLine(bool changeStartPosition = false)
    {
        Image image = transform.Find("CanvasPlayers").Find("ActivePlayerImage").GetComponent<Image>();

        if (changeStartPosition)
        {
            image.rectTransform.position = new Vector2(scoreData.NotCrossPlayerStarts ? -image.rectTransform.position.x : image.rectTransform.position.x, image.rectTransform.position.y);
        }
        else
        {
            int positionModifier = -1;
            image.rectTransform.position = new Vector2(image.rectTransform.position.x * positionModifier, image.rectTransform.position.y);
        }
        
    }

    private void AddPointsToWinner()
    {
        if (crossTurn)
        {
            scoreData.CrossPlayerScore++;
        }
        else
        {
            scoreData.CirclePlayerScore++;
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "<color=#FFA34C>"+ scoreData.CrossPlayerScore.ToString() + "</color>" + ":" + "<color=#FFA34C>" + scoreData.CirclePlayerScore.ToString() + "</color>";
    }

    private void SetSprites(Image image = null, Sprite gridButtonSprite = null)
    {
        if (image != null)
        {
            image.sprite = crossTurn ? Cross : Circle;
        }

        if (gridButtonSprite != null)
        {
            Grid_Button.sprite = gridButtonSprite;
        }
        else
        {
            Grid_Button.sprite = crossTurn ? Circle : Cross;
        }
    }

    private void StartingPlayerSetup()
    {
        crossTurn = scoreData.NotCrossPlayerStarts;
        SetSprites(null, crossTurn ? Circle : Cross);
        ShowActivePlayerLine(true);
    }
}
