﻿using UnityEngine;

public class EditorSurfacePoint : MonoBehaviour
{
    private int[] mapPos = new int[3];
    private float blockLength;
    private Transform editorSurface;
    public void SetPosition(int x, int z)
    {
        mapPos[0] = x;
        mapPos[1] = 0;
        mapPos[2] = z;
    }

    public void OnSelect()
    {
        EventAggregator.Instance.Publish(new MsgEditorSurfaceTapped(this));
    }

    public Vector3 Up()
    {
        transform.position += new Vector3(0, blockLength, 0);
        return transform.position;
    }

    public Vector3 Down()
    {
        transform.position -= new Vector3(0, blockLength, 0);
        return transform.position;
    }

    public int SurfacePointPositionX
    {
        get
        {
            return mapPos[0];
        }
    }

    public int SurfacePointPositionZ
    {
        get
        {
            return mapPos[2];
        }
    }

    public float BlockLength { get => blockLength; set => blockLength = value; }
    public Transform EditorSurface { get => editorSurface; set => editorSurface = value; }

    public void GenerateBoxCollider()
    {
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(blockLength, blockLength, blockLength);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.01f);
    }
}