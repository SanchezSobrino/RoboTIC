﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTest : MonoBehaviour
{


    public float blockSize = 2f;
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public int blocksX = 10;
    public int blocksZ = 10;
    public Material planeMaterial;

    private GameObject plane;
    private float lengthX;
    private float lengthZ;
    private AABB2D aabb;
    public float LengthX
    {
        get { return lengthX; }
        set { lengthX = value; }
    }
    public float LengthZ
    {
        get { return lengthZ; }
        set { lengthZ = value; }
    }
    public float X
    {
        get { return x; }
        set { x = value; }
    }

    public float Z
    {
        get { return z; }
        set { z = value; }
    }


    public AABB2D Aabb
    {
        get { return aabb; }
        set { aabb = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        createPlane();
        float[] lengths = calculateMeshLength(plane);
        float offset = 1.1f;

        Vector2 minPoint = new Vector2(plane.transform.position.x - (lengths[0] / 2f), plane.transform.position.z - (lengths[1] / 2f)-offset);
        Vector2 maxPoint = new Vector2(plane.transform.position.x + (lengths[0] / 2f), plane.transform.position.z + (lengths[1] / 2f));

        Debug.Log(minPoint.x + " " + minPoint.y);
        this.aabb = createAABB2DPlane(minPoint, maxPoint);
    }

    //Vector3 objectSize = Vector3.Scale(transform.localScale, GetComponent().mesh.bounds.size);

    private void createPlane()
    {

        this.plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.GetComponent<Renderer>().material = planeMaterial;

        plane.transform.position = new Vector3(x,y,z);
        plane.transform.rotation = Quaternion.identity;

        float[] planeLengths = calculateMeshLength(plane);
        this.lengthX = planeLengths[0];
        this.lengthZ = planeLengths[1];

        float scaleX = calculateScale(blockSize, blocksX, planeLengths[0], plane.transform.localScale.x);
        float scaleZ = calculateScale(blockSize, blocksZ, planeLengths[1], plane.transform.localScale.z);
        float scaleY = plane.transform.localScale.y;

        plane.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);

        plane.gameObject.SetActive(true);

    }


    private AABB2D createAABB2DPlane(Vector2 min, Vector2 max)
    {
     
        AABB2D aabb = new AABB2D(min.x, min.y, max.x, max.y);
        return aabb;
    }

    //0 min point
    //1 max point
    private Vector2[] calculatePlanePoints(GameObject plane,float fullLenghtX, float fullLenghtZ)
    {
        Vector2[] points = new Vector2[4];
        float lengthX = fullLenghtX / 2f;
        float lengthZ = fullLenghtZ / 2f;
        Vector3 position = plane.transform.position;

        points[0] = new Vector2(position.x - lengthX, position.z - lengthZ);
        points[1] = new Vector2(position.x + lengthX, position.z + lengthZ);
        points[2] = new Vector2(position.x - lengthX, position.z + lengthZ);
        points[3] = new Vector2(position.x + lengthX, position.z - lengthZ);

        return points;
    }

    private float calculateScale(float blockSize, float numberOfBlocks, float actualLenght, float actualScale)
    {
        return (numberOfBlocks * blockSize * actualScale) / actualLenght;
    }

    private float[]calculateMeshLength(GameObject objectToBeMeasured)
    {
        Vector3 objectSize = Vector3.Scale(objectToBeMeasured.transform.localScale, objectToBeMeasured.GetComponent<MeshFilter>().mesh.bounds.size);
        float[] size = new float[2];
        size[0] = objectSize.x;
        size[1] = objectSize.z;
        return size;
    }
}
