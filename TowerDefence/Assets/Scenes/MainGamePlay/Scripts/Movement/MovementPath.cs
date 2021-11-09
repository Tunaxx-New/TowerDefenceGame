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
}
