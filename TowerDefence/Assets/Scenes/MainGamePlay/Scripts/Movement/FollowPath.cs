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

    //Objects behaviors
    private Transform selfTransform;

    void Start()
    {
        //Accelerated math cos
        period = 2 * Math.PI / AcceleratedSpeed;

        selfTransform = GetComponent<Transform>();
        if (Path == null)
        {
            Debug.Log("No path declarated!");
            return;
        }

        Points = Path.GetNextPointPosition();    //From MovementPath
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
}
