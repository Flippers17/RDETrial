using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathfinderDebugger : MonoBehaviour
{
    [SerializeField]
    private Transform p0;
    [SerializeField]
    private Transform p1;


    [SerializeField]
    private Pathfinder pathfinder;

    private Vector2[] path = new Vector2[0];

    // Update is called once per frame
    void Update()
    {
        path = pathfinder.GetPath(p0.position, p1.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(p0.position, .3f);
        Gizmos.DrawWireSphere(p1.position, .3f);

        Gizmos.color = Color.red;

        for (int i = 0; i < path.Length - 1; i++)
        {
            Gizmos.DrawLine(path[i], path[i + 1]);
        }

        if(path.Length > 0)
            Gizmos.DrawWireSphere(path[0], .5f);
    }
}
