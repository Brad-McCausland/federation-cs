using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine;

/*
 * Objectifies game board. Stores map data in a 2D array plus various functions.
 */
public class GameBoard
{
    public Block[,] map;

    // Initialize with pre-made list of blocks
    GameBoard(Block[,] blocks)
    {
        this.map = blocks;
    }

    // Initialize an empty array with width and height parameters
    GameBoard(int width, int height)
    {
        this.map = new Block[width,height];

        //initialize to empty
        for (int i = 0; i < this.map.GetLength(0); i ++)
        {
            for (int j = 0; j < this.map.GetLength(1); j++)
            {
                this.map[i, j] = Block.empty;
            }
        }
    }

    public GameBoard(StreamReader inputFile)
    {
        int x = 0;
        int y = 0;
        int oldX = -1;
        char cell;

        List<List<Block>> list2D = new List<List<Block>>();
        list2D.Add(new List<Block>());

        // Create 2d linked list during initial file read
        while (!inputFile.EndOfStream)
        {
            cell = (char)inputFile.Read();

            //detect newline
            if (Convert.ToInt32(cell) == 10)
            {
                // Check for jagged array
                if (oldX >= 0 && oldX != x)
                {
                    throw new ArgumentException("GameBoard construction failed: jagged array detected", "text");
                }
                oldX = x;

                list2D.Add(new List<Block>());

                x = 0;
                y ++;
            }
            else
            {
                x ++;
                list2D[y].Add((Block)(int)cell);
            }
        }

        this.map = new Block[x+1,y+1];

        x = 0;
        y = 0;
        foreach(List<Block> row in list2D)
        {
            foreach(Block block in row)
            {
                this.map[x, y] = block;
                x++;
            }
            x = 0;
            y++;
        }
    }

    public string printableString
    {
        get
        {
            var returnString = "";

            for (int y = 0; y < this.height; y ++)
            {
                for (int x = 0; x < this.width; x++)
                {
                    returnString += (char)this.map[x, y];
                }
                returnString += "\n";
            }

            return returnString;
        }
    }

    public int width
    {
        get
        {
            return this.map.GetLength(0);
        }
    }

    public int height
    {
        get
        {
            return this.map.GetLength(1);
        }
    }
}
