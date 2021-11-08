using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPath : MonoBehaviour
{
    //Path type cycle movement
    public enum PathTypes
    {
        direct,
        loop
    }

    [SerializeField] public PathTypes   PathType;   //Type
    [SerializeField] public int         NowPoint;   //End point for object
    [SerializeField] public bool        Direction;   //End point for object
    [SerializeField] public Transform[] PathPoints; //Al points to move

    void Start()
    {

    }

    //Drawing gizmos in unity editor
    void OnDrawGizmos()
    {
        //Don't draw lines in gizmos
        if (PathPoints == null || PathPoints.Length < 2) return;

        //Drawing line between points
        for (int i = 1; i < PathPoints.Length; i++) Gizmos.DrawLine(PathPoints[i - 1].position, PathPoints[i].position);

        //Draw last point line if it's loop
        if (PathType == PathTypes.loop) Gizmos.DrawLine(PathPoints[0].position, PathPoints[PathPoints.Length - 1].position);
    }

    //Get position of point
    public IEnumerator<Transform> GetNextPointPosition()
    {
        //Don't get positions
        if (PathPoints == null || PathPoints.Length < 1) yield break;

        //Numerator snake
        while (true)
        {
            //Next point
            yield return PathPoints[NowPoint];

            //Dont draw or execute
            if (PathPoints.Length == 1) continue;

            if (Direction)
            {
                NowPoint++;
            }
            else
            {
                NowPoint--;
            }
            //On direct type    -> -> ->
            if (PathType == PathTypes.direct)
            {
                if (NowPoint <= 0) {
                    NowPoint++;
                } else if (NowPoint >= PathPoints.Length - 1) {
                    NowPoint--;
                }
            }
            //On loop type      >-> -> ->=
            if (PathType == PathTypes.loop)
            {
                if (NowPoint >= PathPoints.Length)
                {
                    NowPoint = 0;
                }
                else if (NowPoint < 0)
                {
                    NowPoint = PathPoints.Length - 1;
                }
            }
        }
    }
}
