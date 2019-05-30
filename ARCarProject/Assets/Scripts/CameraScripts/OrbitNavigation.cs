using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitNavigation : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    CameraBehaviour _cameraBehaviour;

    private void Start()
    {
        _cameraBehaviour = GameObject.FindGameObjectWithTag("CameraRoot").GetComponent<CameraBehaviour>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetKeyDown(KeyCode.Mouse0) && _cameraBehaviour.state == CameraBehaviour.State.Orbit)
        {
            if (Physics.Raycast(ray, out hit))
            {
                print(hit.collider.name);
                _cameraBehaviour.SetTargetForTransition(hit.collider.name.ToString());
            }
        }
    }
}
