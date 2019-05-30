using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    #region Variables
    private Transform _target;
    public enum State {Orbit, Transition, VisualizeInfo,};
    public State state;
    private Vector3 rot;
    private float sensitivity = 30f;
    int transformIndex;
    public Transform[] camPositions;
    #endregion

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Target").transform;
        state = State.Orbit;
        rot = transform.localRotation.eulerAngles;
    }

    private void Update()
    {

    }

    public void SetTargetForTransition(string t)
    {
        switch (t)
        {
            case "TopCollider":
                transformIndex = 4;
                break;
            case "BotCollider":
                transformIndex = 5;
                break;
            case "LeftCollider":
                transformIndex = 2;
                break;
            case "RightCollider":
                transformIndex = 3;
                break;
            case "FrontCollider":
                transformIndex = 1;
                break;
            case "BackCollider":
                transformIndex = 0;
                break;
        }
        Debug.Log(camPositions[transformIndex].eulerAngles.ToString());
        state = State.Transition;
    }

    private void LateUpdate()
    {
        BehaviourTree();  
    }

    private void BehaviourTree()
    {
        switch (state)
        {
            case State.Orbit:
                StateOrbit();
                break;
            case State.Transition:
                StateTransition();
                break;
            case State.VisualizeInfo:
                break;
            default:
                break;
        }
    }

    private void StateOrbit()
    {
        FaceTarget();
        RotateCamera();
        FollowTargetPosition();
    }

    private void FaceTarget()
    {
        transform.LookAt(_target.transform.position);
    }

    private void RotateCamera()
    {
        float inputX = Input.GetAxis("Vertical");
        float inputY = Input.GetAxis("Horizontal");
        inputY = inputY * -1;
        rot.x += inputX * sensitivity * Time.deltaTime;
        rot.y += inputY * sensitivity * Time.deltaTime;
        Quaternion localRotation = Quaternion.Euler(rot.x, rot.y, 0.0f);
        transform.rotation = localRotation;
    }

    private void FollowTargetPosition()
    {
        transform.position = _target.position;
    }

    private void StateTransition()
    {
        WarpCamera();
    }

    private void WarpCamera()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, camPositions[transformIndex].rotation, 5 * Time.deltaTime);
        if (transform.rotation == camPositions[transformIndex].localRotation)
            state = State.VisualizeInfo;
        // Warp cam to desired position. Desired position would be something like: camPositions[transformIndex]
    }
}
