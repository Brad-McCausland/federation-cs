using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_TaskManager : MonoBehaviour
{
    public Transform target;

    public float WALK_SPEED;

    private UnityEngine.AI.NavMeshAgent navComponent;

    private List<Vector2> path;

    // Start is called before the first frame update
    void Start()
    {
        var map = Global_Object_Manager.toolbox.gameBoard.map;
        var being = GameObject.Find("Human");
        var target = GameObject.Find("spr_target");
        var start = new int[] {(int)being.transform.position.x, (int)being.transform.position.y};
        var end = new int[] {(int)target.transform.position.x, (int)target.transform.position.y};
        
        //var convertedMap = Toolbox.boardConverter(map);
        //this.path = new Astar(convertedMap, start, end, "Diagonal").result;
        //Debug.Log("FINAL COUNT: " + this.path.Count);
        
        //foreach (Vector2 node in this.path)
        //{
            //Debug.Log(node);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (this.path.Count > 0)
        {
            Vector2 currentPos = new Vector2(this.transform.position.x, this.transform.position.y);
            Vector2 currentTarget = new Vector2(path[0].x, path[0].y);
            // Pop coordinate when reached
            if (currentPos == currentTarget)
            {
                path.RemoveAt(0);
            }

            this.transform.position = Vector2.MoveTowards(this.transform.position, currentTarget, WALK_SPEED);
        }
    }
}