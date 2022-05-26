using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBuildingsT: MonoBehaviour
{
    public float RandomSeed=5;
    private int MinimunComponents = 3;
    private int MaximumComponents = 10;
    private GameObject[] Ground;
    private GameObject[] Floors;
    private GameObject[] Roof;

    void Start()
    {
        Ground=Resources.LoadAll<GameObject>("TGrounds");
        Floors=Resources.LoadAll<GameObject>("TFloor");
        Roof=Resources.LoadAll<GameObject>("TRoofs");
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