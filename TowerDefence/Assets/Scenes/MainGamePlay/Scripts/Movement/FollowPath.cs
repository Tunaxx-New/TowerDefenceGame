using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

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

    public MoveTypes      MoveType;
    public MovementPath[] StartPath;
    public MovementPath[] EndPath;
    public MovementPath   Path;
    public int  OnPathNum  = 0;
    public bool OnStartPath = true;
    [SerializeField]  public float        LinearSpeed = 1;
    [SerializeField]  public float        AcceleratedSpeed = 1;
    [SerializeField]  public float        EndDistance = .1f;

    private float  time;
    private float  lerp;
    private double period;

    //Cycle from Movement path
    private IEnumerator<Transform> Points;
    [SerializeField] public int NowPoint;   //End point for object
    [SerializeField] public PathTypes PathType;   //Type
    [SerializeField] public bool Direction;   //End point for object
    [SerializeField] public Transform[] PathPoints; //Al points to move

    //Objects behaviors
    private Transform selfTransform;

    [SerializeField] Transform AnticipationAngle;

    void Start()
    {
        OnPathNum = RandomBetween0t2();
        Path = StartPath[OnPathNum];
        PathPoints = Path.PathPoints;
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

        Vector3 vectorToTarget = Points.Current.position - AnticipationAngle.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        AnticipationAngle.rotation = Quaternion.Slerp(AnticipationAngle.rotation, q, Time.deltaTime * 100);

        //main movement linear
        if (MoveType == MoveTypes.linear)
        {
            selfTransform.position = Vector2.MoveTowards(selfTransform.position, Points.Current.position, Time.deltaTime * (LinearSpeed - Path.slowSpeed));
        } 
        else if (MoveType == MoveTypes.accelerated)
        {
            time += Time.deltaTime;                                             //Adding time

            if (time >= period)time = 0;                                        //Nulling time

            lerp = (float)(1 - Math.Cos((AcceleratedSpeed - Path.slowSpeed) * time)) / 2;          //Current speed of accelerated movement

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
            if (NowPoint > -1)
            {
                yield return PathPoints[NowPoint];
            }

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
                else if (NowPoint >= PathPoints.Length)
                {
                    if (OnStartPath) ChangePath();
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

    int RandomBetween0t2()
    {
        return Random.Range(0, 3);
    }

    void ChangePath()
    {
        OnPathNum = RandomBetween0t2();
        Path = EndPath[OnPathNum];
        PathPoints = Path.PathPoints;

        NowPoint = 0;

        if (Path == null)
        {
            Debug.Log("No path declarated!");
            return;
        }

        Points = GetNextPointPosition();    //From MovementPath
        Points.MoveNext();                  //Start couritine

        if (Points.Current == null)
        {
            Debug.Log("No points declarated!");
            return;
        }
        OnStartPath = false;
    }
}
