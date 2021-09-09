using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Grid_Tick_Tack_Toe : MonoBehaviour
{
    [SerializeField] private Sprite Cross;
    [SerializeField] private Sprite Circle;
    [SerializeField] private Grid_ScoreData scoreData;

    private bool crossTurn;
    private bool gameOver;
    private int amountOfMovesLeft;

    private TMP_Text scoreText;

    private void OnEnable()
    {
        Grid_Test.OnSpawnedTile += Grid_Test_OnSpawnedTile;
        Grid_Test.OnSpawnGrid += Grid_Test_OnSpawnGrid;
    }   

    private void OnDisable()
    {
        Grid_Test.OnSpawnedTile -= Grid_Test_OnSpawnedTile;
        Grid_Test.OnSpawnGrid -= Grid_Test_OnSpawnGrid;
    }

    private void Start()
    {
        scoreText = transform.Find("CanvasPlayers").transform.Find("ScoreText").GetComponent<TMP_Text>();
        UpdateScoreText();

        transform.Find("CanvasPlayers").Find("Image").GetComponent<mButton>().AddListener(() =>
        {
            Debug.Log("kupa");
        });
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
        if (!gameOver)
        {
            Image image = templateTransform.transform.Find("Canvas").transform.Find("Image").GetComponent<Image>();
            image.gameObject.SetActive(true);

            if (grid[x, y] != 1 && grid[x, y] != -1)
            {
                crossTurn = !crossTurn;

                grid[x, y] = crossTurn ? 1 : -1;

                image.sprite = crossTurn ? Cross : Circle;

                ShowActivePlayer();

                amountOfMovesLeft--;
            }
        }        
    }

    private void CheckWinCondition(int x, int y, int[,] grid)
    {
        //Checking Y
        if ((grid[x, 1 - 1] + grid[x,1] + grid[x, 1 + 1]) == 3 || (grid[x, 1 - 1] + grid[x, 1] + grid[x, 1 + 1]) == -3)
        {
            gameOver = true;
            GameOver();
        }
        //Checking X
        else if ((grid[1 - 1, y] + grid[1, y] + grid[1 + 1, y]) == 3 || (grid[1 - 1, y] + grid[1, y] + grid[1 + 1, y]) == -3)
        {
            gameOver = true;
            GameOver();
        }
        //Checking Cross from bottom to top
        else if ((grid[1 - 1, 1 - 1] + grid[1, 1] + grid[1 + 1, 1 + 1]) == 3 || (grid[1 - 1, 1 - 1] + grid[1, 1] + grid[1 + 1, 1 + 1]) == -3)
        {
            gameOver = true;
            GameOver();
        }
        //Checking Cross from top to bottom
        else if ((grid[1 - 1, 1 + 1] + grid[1, 1] + grid[1 + 1, 1 - 1]) == 3 || (grid[1 - 1, 1 + 1] + grid[1, 1] + grid[1 + 1, 1 - 1]) == -3)
        {
            gameOver = true;
            GameOver();
        }
        else if (amountOfMovesLeft <= 0)
        {
            gameOver = true;
            GameOver("Draw!", true);
        }
    }

    private void GameOver(string textToShow = "You Win!", bool draw = false)
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
        ShowActivePlayer();
    }

    private void ShowActivePlayer()
    {
        Image image = transform.Find("CanvasPlayers").Find("ActivePlayerImage").GetComponent<Image>();

        int positionModifier = -1;
        image.rectTransform.position = new Vector2(image.rectTransform.position.x * positionModifier, image.rectTransform.position.y);
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
}
