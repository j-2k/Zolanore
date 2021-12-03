using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRotationTransformation : CustomTransformation
{
    public Vector3 rotation;

    public override Vector3 Apply(Vector3 point)
    {
        return point;
    }
}
