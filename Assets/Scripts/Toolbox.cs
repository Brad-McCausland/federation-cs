using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Block
{
    empty = '0', //char 0
    floor = '1', //char 1
    wall = '2'   //char 2
}
 
public class Toolbox : MonoBehaviour //Singleton<Toolbox>
{

    public GameObject floor;
    public GameObject wall;
    
    public GameBoard gameBoard;

    // Used to track any global components added at runtime.
    private Dictionary<string, Component> m_Components = new Dictionary<string, Component>();

 
 
    // Prevent constructor use.
    protected Toolbox() { }
 
 
    private void Awake()
    {
        Global_Object_Manager.toolbox = this;
        this.initGameBoard();
        this.loadGameObjectsFromGrid();
    }
 
    private void initGameBoard()
    {
        //TODO: remove hardcoded save file
        string path = "Assets/Saves/test_map.xml";

        //Read the text from directly from the test.txt file
        StreamReader mapFile = new StreamReader(path);

        this.gameBoard = new GameBoard(mapFile);

        mapFile.Close();

        Debug.Log(this.gameBoard.printableString);
        Debug.Log("Width: " + this.gameBoard.width);
        Debug.Log("Height: " + this.gameBoard.height);
    }

    private void loadGameObjectsFromGrid()
    {
        for (int x = 0; x < this.gameBoard.width; x++)
        {
            for (int y = 0; y < this.gameBoard.height; y++)
            {
                var gameGrid = GameObject.Find("GameGrid");
                GameObject block;
                switch (this.gameBoard.map[x, y])
                {
                    case Block.floor:
                        Debug.Log("Floor at (" + x + ", " + y + ")");
                        block = Instantiate(this.floor, new Vector2(x, y), Quaternion.identity, gameGrid.transform) as GameObject;
                        break;
                    case Block.wall:
                        block = Instantiate(this.wall, new Vector2(x, y), Quaternion.identity, gameGrid.transform) as GameObject;
                        break;
                    case Block.empty:
                    default:
                        break;
                }

            }
        }
    }
 
    // Define all required global components here. These are hard-codded components
    // that will always be added. Unlike the optional components added at runtime.
 
 
    // The methods below allow us to add global components at runtime.
    // TODO: Convert from string IDs to component types.
    public Component AddGlobalComponent(string componentID, Type component)
    {
        if (m_Components.ContainsKey(componentID))
        {
            Debug.LogWarning("[Toolbox] Global component ID \""
                +componentID + "\" already exist! Returning that." );
            return GetGlobalComponent(componentID);
        }
 
        var newComponent = gameObject.AddComponent(component);
        m_Components.Add(componentID, newComponent);
        return newComponent;
    }
 
 
    public void RemoveGlobalComponent(string componentID)
    {
        Component component;
 
        if (m_Components.TryGetValue(componentID, out component))
        {
            Destroy(component);
            m_Components.Remove(componentID);
        }
        else
        {
            Debug.LogWarning("[Toolbox] Trying to remove nonexistent component ID \""
                + componentID + "\"! Typo?");
        }
    }
 
 
    public Component GetGlobalComponent(string componentID)
    {
        Component component;
 
        if (m_Components.TryGetValue(componentID, out component))
        {
            return component;
        }
 
        Debug.LogWarning("[Toolbox] Global component ID \""
    + componentID + "\" doesn't exist! Typo?");
        return null;
    }

    public GameBoard getGameBoard()
    {
        return this.gameBoard;
    }

    // Function for updating the gameboard from outside the toolbox
    public void updateGameBoard(int x, int y, Block newObject)
    {
        //TODO: Add method to GemBoardClass to allow editing
        //this.gameBoard[x, y] = newObject;
    }

/*
    public static int[][] boardConverter(Block[,] input)
    {
        int[][] output = new int[100][];
        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                Debug.Log("huehue: " + input[i, j]);
                output[i][j] = input[i, j] == Block.floor ? 1 : 0;
            }
        }
        return output;
    }*/
}