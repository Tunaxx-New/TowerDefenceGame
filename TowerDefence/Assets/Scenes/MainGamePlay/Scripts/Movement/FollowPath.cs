using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowPath : MonoBehaviour
{
    //Linear speed or accelerated
    public enum MoveTypes
    {
        linear,
        accelerated
    }
    //Path type cycle movement
    public enum PathTypes
    {
        direct,
        loop
    }

    public MoveTypes    MoveType;
    public MovementPath Path;
    [SerializeField]  public float        LinearSpeed = 1;
    [SerializeField]  public float        AcceleratedSpeed = 1;
    [SerializeField]  public float        EndDistance = .1f;

    private float  time;
    private float  lerp;
    private double period;

    //Cycle from Movement path
    private IEnumerator<Transform> Points;
    [SerializeField] public int NowPoint;   //End point for object
    [SerializeField] public MovementPath PathMove;   //End point for object
    [SerializeField] public PathTypes PathType;   //Type
    [SerializeField] public bool Direction;   //End point for object
    [SerializeField] public Transform[] PathPoints; //Al points to move

    //Objects behaviors
    private Transform selfTransform;

    void Start()
    {
        for (int i = 0; i < PathMove.PathPoints.Length; i++) PathPoints[i] = PathMove.PathPoints[i];
        //Accelerated math cos
        period = 2 * Math.PI / AcceleratedSpeed;

        selfTransform = GetComponent<Transform>();
        if (Path == null)
        {
            Debug.Log("No path declarated!");
            return;
        }

        Points = GetNextPointPosition();    //From MovementPath
        Points.MoveNext();                       //Start couritine

        if (Points.Current == null) {
            Debug.Log("No points declarated!");
            return;
        }

        selfTransform.position = Points.Current.position;
    }

    void Update()
    {
        //Useless procession
        //if (Points == null || Points.Current == null) return;

        //main movement linear
        if (MoveType == MoveTypes.linear)
        {
            selfTransform.position = Vector2.MoveTowards(selfTransform.position, Points.Current.position, Time.deltaTime * LinearSpeed);
        } 
        else if (MoveType == MoveTypes.accelerated)
        {
            time += Time.deltaTime;                                             //Adding time

            if (time >= period)time = 0;                                        //Nulling time

            lerp = (float)(1 - Math.Cos(AcceleratedSpeed * time)) / 2;          //Current speed of accelerated movement

            selfTransform.position = Vector3.Lerp(selfTransform.position, Points.Current.position, lerp);   //Lerp
        }

        var distance = (selfTransform.position - Points.Current.position).sqrMagnitude;
        if (distance < EndDistance)
        {
            Points.MoveNext();
            lerp = 0;
            time = 0;
        }
    }
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
                if (NowPoint <= 0)
                {
                    NowPoint++;
                }
                else if (NowPoint >= PathPoints.Length - 1)
                {
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
