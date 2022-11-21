using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineConnectedImage : Image
{
    [SerializeField] public float thickness = 10f;

    new void Awake()
    {
        base.Awake();
    }

    void Update()
    {
        // TODO: This is nice for changing layout, but not necessary for final game
        UpdateGeometry();
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);

        foreach (Transform target in transform)
        {
            int offset = vh.currentVertCount;

            float distance = Vector3.Distance(transform.position, target.position);
            float angle = GetAngle(transform.position, target.position);

            UIVertex vertex = UIVertex.simpleVert;

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, 0, 0);
            vh.AddVert(vertex);

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, 0, 0);
            vh.AddVert(vertex);

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(-thickness / 2, distance / transform.root.localScale.y, 0);
            vh.AddVert(vertex);

            vertex.position = Quaternion.Euler(0, 0, angle) * new Vector3(thickness / 2, distance / transform.root.localScale.y, 0);
            vh.AddVert(vertex);

            vh.AddTriangle(offset + 0, offset + 1, offset + 3);
            vh.AddTriangle(offset + 3, offset + 2, offset + 0);
        }
    }

    public float GetAngle(Vector2 source, Vector2 target)
    {
        return (float)(Mathf.Atan2(target.y - source.y, target.x - source.x) * (180 / Mathf.PI)) - 90f;
    }
}