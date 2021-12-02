using System.Collections;
using UnityEngine;

public class TransformationGrid : MonoBehaviour
{

	public Transform prefab;

	[Range(0,10)]
	public int gridResolution = 10;

	Transform[] grid;

	public bool SeeGenerator = false;

	[Range(0,0.5f)]
	public float GeneratorSpeedFloat;

	void Awake()
	{
		grid = new Transform[gridResolution * gridResolution * gridResolution];

		if (SeeGenerator)
		{
			if (GeneratorSpeedFloat == 0)
			{
				FastGrid();
				return;
			}
			StartCoroutine(GridGeneratorSeconds(GeneratorSpeedFloat));
		}
		else
		{
			FastGrid();
		}
	}

	void FastGrid()
	{
		for (int i = 0, z = 0; z < gridResolution; z++)
		{
			for (int y = 0; y < gridResolution; y++)
			{
				for (int x = 0; x < gridResolution; x++, i++)
				{
					grid[i] = CreateGridPosition(x, y, z);
				}
			}
		}
	}

	IEnumerator GridGeneratorSeconds(float speed)
	{
		int i = 0;
		for (int x = 0; x < gridResolution; x++)
		{
			for (int y = 0; y < gridResolution; y++)
			{
				for (int z = 0; z < gridResolution; z++)
				{
					grid[i] = CreateGridPosition(x, y, z);
					yield return new WaitForSeconds(speed);
					i++;
				}
			}
		}
	}

	Transform CreateGridPosition(int x, int y, int z)
	{
		Transform position = Instantiate<Transform>(prefab);
		//position.parent = this.transform;
		//position.position = new Vector3(x, y, z);
		position.position = GetCoordinates(x, y, z);
		position.GetComponent<MeshRenderer>().material.color = new Color(
			(float)x / gridResolution,
			(float)y / gridResolution,
			(float)z / gridResolution
			);

		return position;
	}

	Vector3 GetCoordinates(int x, int y, int z)
	{
		//took a while to understand exactly wahts here but basically
		// without 0.5 it is the inverse of the grid resolution & with the 0.5 shift whole grid back by half which puts the origin in the middleo fthe grid 
		// still dont understand the whole - (grid reso - 1) formula exactly is there a way to do this except additive?? prob but too lazy to find out
		return new Vector3(
			x - (gridResolution - 1) * 0.5f,
			y - (gridResolution - 1) * 0.5f,
			z - (gridResolution - 1) * 0.5f
		);
	}
}