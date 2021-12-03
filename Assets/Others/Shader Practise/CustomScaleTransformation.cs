using UnityEngine;

public class CustomScaleTransformation : CustomTransformation
{

	public Vector3 scale;

	public override Vector3 Apply(Vector3 point)
	{
		point.x = point.x * scale.x;
		point.y = point.y * scale.y;
		point.z = point.z * scale.z;
		return point;
	}
}