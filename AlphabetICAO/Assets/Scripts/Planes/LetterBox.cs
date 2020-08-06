using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour
{
    TextMesh letters;
    Plane parentPlane;
    // Start is called before the first frame update
    void Start()
    {
        parentPlane = transform.parent.GetComponent<Plane>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithPlane();
        RotateToCamera();
    }

    private void MoveWithPlane()
    {
        transform.position = parentPlane.LetterPosition;
    }

    private void RotateToCamera()
    {
        var _newRotation = Quaternion.LookRotation(Camera.main.transform.position) * Quaternion.Euler(180, 0, -180);
        transform.rotation = Quaternion.Slerp(transform.rotation, _newRotation, 10f);
    }
}
