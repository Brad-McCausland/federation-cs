using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Being_TaskManager : MonoBehaviour
{
    public Transform target;
    public GameObject SearchMarker;
    public GameObject TracebackMarker;
    public float WALK_SPEED;

    private UnityEngine.AI.NavMeshAgent navComponent;

    private List<Vector2> path;

    // Start is called before the first frame update
    void Start()
    {
        var map = Global_Object_Manager.toolbox.gameBoard.traversableMatrix;
        var being = GameObject.Find("Human");
        var target = GameObject.Find("spr_target");
        var start = new int[] {(int)being.transform.position.x, (int)being.transform.position.y};
        var end = new int[] {(int)target.transform.position.x, (int)target.transform.position.y};
        
        var search = new Astar(map, start, end, "Euclidean", false);
        this.path = search.result;
        
        // Render debug markers
        foreach (Vector2 marker in search.searchedMarkers)
        {
            GameObject _ = Instantiate(this.SearchMarker, marker, Quaternion.identity) as GameObject;
        }
        foreach (Vector2 marker in search.tracebackMarkers)
        {
            GameObject _ = Instantiate(this.TracebackMarker, marker, Quaternion.identity) as GameObject;
        }
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