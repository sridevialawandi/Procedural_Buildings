using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenBuildingsC: MonoBehaviour
{
    public float RandomSeed=6;
    private int MinimunComponents = 3;
    private int MaximumComponents = 9;
    private GameObject[] Ground;
    private GameObject[] Floors;
    private GameObject[] Roof;

    void Start()
    {
        Ground=Resources.LoadAll<GameObject>("CGrounds");
        Floors=Resources.LoadAll<GameObject>("CFloor");
        Roof=Resources.LoadAll<GameObject>("CRoofs");
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