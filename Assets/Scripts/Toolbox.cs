using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public enum GridObjects
{
    empty = 48,
    floor = 49,
    wall = 50
}
 
public class Toolbox : MonoBehaviour //Singleton<Toolbox>
{
    public GameObject floor;
    public GameObject wall;

    // Used to track any global components added at runtime.
    private Dictionary<string, Component> m_Components = new Dictionary<string, Component>();

    private GridObjects[,] GameBoard;
 
 
    // Prevent constructor use.
    protected Toolbox() { }
 
 
    private void Awake()
    {
        this.initGameBoard();
        this.loadGameObjectsFromGrid();
    }
 
    private void initGameBoard()
    {
        //TODO: Load data from save file instead of creating from scratch
        string path = "Assets/Saves/test_map.xml";

        this.GameBoard = new GridObjects[100, 100];

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        int x = 0;
        int y = 0;
        char cell;
        while (!reader.EndOfStream)
        {
            cell = (char)reader.Read();

            Debug.Log(Convert.ToInt32(cell));

            //detect newline
            if (Convert.ToInt32(cell) == 10)
            {
                Debug.Log("new line");
                if (x != 100)
                {
                    Debug.LogError("Error: Attempted to load malformed map file! Line " + y + "is too wide");
                    Application.Quit();
                }
                x = 0;
                y ++;
            }
            else
            {
                //Debug.Log("(" + x + ", " + y + ")");
                this.GameBoard[x, y] = (GridObjects)(int)cell;
                x ++;
            }
            if (x == 101)
            {
                Debug.Log("Overrun");
                break;
            }
        }
/*
        if (x != 100 || y != 100)
        {
            Debug.LogError("Error: Attempted to load malformed map file! File is of dimensions: (" + x + ", " + y);
            Application.Quit();
        }

*/
        for (int i = 0; i < this.GameBoard.GetLength(0); i++)
        {
            for (int j = 0; j < this.GameBoard.GetLength(1); j++)
            {
                Debug.Log(this.GameBoard[i,j] + "\t");
            }
            Debug.Log("");
        }

        reader.Close();
    }

    private void loadGameObjectsFromGrid()
    {
        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                switch (GameBoard[x, y])
                {
                    case GridObjects.floor:
                        Instantiate(this.floor, new Vector2(x, y), Quaternion.identity);
                        break;
                    case GridObjects.wall:
                        Instantiate(this.wall, new Vector2(x, y), Quaternion.identity);
                        break;
                    case GridObjects.empty:
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

    public GridObjects[,] getGameBoard()
    {
        return this.GameBoard;
    }

    // Function for updating the gameboard from outside the toolbox
    public void updateGameBoard(int x, int y, GridObjects newObject)
    {
        this.GameBoard[x, y] = newObject;
    }
}