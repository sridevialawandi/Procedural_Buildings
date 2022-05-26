using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBuildingsF: MonoBehaviour
{
    public float RandomSeed=5;
    private int MinimunComponents = 3;
    private int MaximumComponents = 7;
    private GameObject[] Ground;
    private GameObject[] Floors;
    private GameObject[] Roof;

    void Start()
    {
        Ground=Resources.LoadAll<GameObject>("FGrounds");
        Floors=Resources.LoadAll<GameObject>("FFloor");
        Roof=Resources.LoadAll<GameObject>("FRoofs");
        StartBuild();
    }
    void StartBuild()
    {

        int totalComponents = Random.Range(MinimunComponents, MaximumComponents);
        float totalheight = 0;
        totalheight += RenderComponent(Ground, totalheight);
        for (int i = 1; i < totalComponents; i++)
        {
            totalheight += RenderComponent(Floors, totalheight);
        }

        RenderComponent(Roof, totalheight);
    }

    float RenderComponent(GameObject[] pieceArray, float inputHeight) //function to find the height of component
    {
        Transform randomTransform = pieceArray[Random.Range(0, pieceArray.Length)].transform;
        GameObject clone = Instantiate(randomTransform.gameObject, this.transform.position + new Vector3 (0, inputHeight, 0), transform.rotation) as GameObject;
        Mesh cloneMesh = clone.GetComponentInChildren<MeshFilter>().mesh;
        Bounds bounds = cloneMesh.bounds;
        float totalheight = bounds.size.y;

        clone.transform.SetParent(this.transform);

        return totalheight;
    }

}