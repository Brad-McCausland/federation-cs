using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_TaskManager : MonoBehaviour
{
    public Transform target;

    public float WALK_SPEED;

    private UnityEngine.AI.NavMeshAgent navComponent;

    private List<Vector2> path;

    int[][] map = new int[][] 
    {
        /* 
        new int[] {0, 1, 0, 0, 0},
        new int[] {0, 1, 0, 0, 0},	
        new int[] {0, 1, 0, 0, 0},
        new int[] {0, 1, 1, 1, 0},
        new int[] {0, 0, 0, 0, 0}
        */

        new int[] {0, 0, 0, 0, 0},
        new int[] {1, 1, 1, 1, 0},	
        new int[] {1, 0, 0, 0, 0},
        new int[] {1, 0, 0, 0, 0},
        new int[] {0, 0, 0, 0, 0}
    };

    // Start is called before the first frame update
    void Start()
    {
        var start = new int[] {2, 2};
        var end = new int[] {0, 0};
        this.path = new Astar(map, start, end, null).result;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.path.Count > 0)
        {
            Vector2 currentPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 currentTarget = new Vector2(path[0].x, -path[0].y);
            // Pop coordinate when reached
            if (currentPos == currentTarget)
            {
                //Debug.Log(currentTarget);
                path.RemoveAt(0);
            }

            this.transform.position = Vector2.MoveTowards(this.transform.position, currentTarget, WALK_SPEED);
        }
    }
}