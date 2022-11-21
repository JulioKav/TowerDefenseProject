using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineConnectedImage : Image
{
    [SerializeField] public float thickness = 10f;
    [SerializeField] public float radius = 10f;
    [SerializeField] public int circleResolution = 10;

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

        if (transform.childCount == 0) return;

        int offset = vh.currentVertCount;

        // Single vertical line from skill halfway to next skills
        float longestYDistance = 0;
        float longestXDistance = 0;
        float realRadius = radius;
        foreach (Transform target in transform)
        {
            float yDistance = Math.Abs(transform.position.y - target.position.y);
            if (yDistance > longestYDistance) longestYDistance = yDistance;

            float xDistance = Math.Abs(target.position.x - transform.position.x);
            if (xDistance > longestXDistance) longestXDistance = xDistance;
        }
        longestXDistance /= transform.root.localScale.x;
        longestYDistance /= transform.root.localScale.y;
        realRadius = Math.Min(radius, Math.Abs(longestXDistance) / 2);
        float firstSegmentLength = longestYDistance / 2 - realRadius;

        UIVertex vertex = UIVertex.simpleVert;

        vertex.position = new Vector3(-thickness / 2, 0, 0);
        vh.AddVert(vertex);
        vertex.position = new Vector3(thickness / 2, 0, 0);
        vh.AddVert(vertex);
        vertex.position = new Vector3(-thickness / 2, -firstSegmentLength, 0);
        vh.AddVert(vertex);
        vertex.position = new Vector3(thickness / 2, -firstSegmentLength, 0);
        vh.AddVert(vertex);
        vh.AddTriangle(offset + 0, offset + 1, offset + 3);
        vh.AddTriangle(offset + 3, offset + 2, offset + 0);


        // Remaining lines from skill to each next skills
        foreach (Transform target in transform)
        {

            float distance = Math.Abs(transform.position.y - target.position.y);
            float thirdSegmentLength = (distance / 2) / transform.root.localScale.y - realRadius;

            float x = target.position.x - transform.position.x;
            float y = target.position.y - transform.position.y;
            x /= transform.root.localScale.x;
            y /= transform.root.localScale.y;

            realRadius = Math.Min(radius, Math.Abs(x) / 2);
            float yCenter = y + (distance / 2) / transform.root.localScale.y;
            float radiusXOffset = Math.Sign(x) * realRadius;

            // Horizontal lines between skill and each next skill
            offset = vh.currentVertCount;
            vertex.position = new Vector3(radiusXOffset, yCenter + thickness / 2, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector3(x - radiusXOffset, yCenter + thickness / 2, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector3(radiusXOffset, yCenter - thickness / 2, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector3(x - radiusXOffset, yCenter - thickness / 2, 0);
            vh.AddVert(vertex);
            vh.AddTriangle(offset + 0, offset + 1, offset + 3);
            vh.AddTriangle(offset + 3, offset + 2, offset + 0);

            // Remaining half way vertical lines from skill to each next skill
            offset = vh.currentVertCount;
            vertex.position = new Vector3(x - thickness / 2, y, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector3(x + thickness / 2, y, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector3(x - thickness / 2, y + thirdSegmentLength, 0);
            vh.AddVert(vertex);
            vertex.position = new Vector3(x + thickness / 2, y + thirdSegmentLength, 0);
            vh.AddVert(vertex);
            vh.AddTriangle(offset + 0, offset + 1, offset + 3);
            vh.AddTriangle(offset + 3, offset + 2, offset + 0);

            if (realRadius == 0) return;

            // First half circle
            offset = vh.currentVertCount;
            float circleCenterX = x / 2 - Math.Sign(x) * realRadius;
            float circleCenterY = yCenter + realRadius;
            Vector3 circleCenter = new Vector3(circleCenterX, circleCenterY, 0);
            for (int i = 0; i < circleResolution; i++)
            {
                float angle = ((float)i / (circleResolution - 1)) * (float)(Math.PI / 2);
                angle += (x > 0) ? -(float)(Math.PI) : -(float)(Math.PI / 2);

                float xPos1 = (realRadius - thickness / 2) * (float)Math.Cos(angle);
                float yPos1 = (realRadius - thickness / 2) * (float)Math.Sin(angle);
                float xPos2 = (realRadius + thickness / 2) * (float)Math.Cos(angle);
                float yPos2 = (realRadius + thickness / 2) * (float)Math.Sin(angle);

                Vector3 posOffset = new Vector3(radiusXOffset, yCenter + realRadius, 0);

                vertex.position = new Vector3(xPos1, yPos1, 0) + posOffset;
                vh.AddVert(vertex);
                vertex.position = new Vector3(xPos2, yPos2, 0) + posOffset;
                vh.AddVert(vertex);
            }
            for (int i = 0; i < circleResolution - 1; i++)
            {
                vh.AddTriangle(offset + 2 * i + 0, offset + 2 * i + 1, offset + 2 * i + 3);
                vh.AddTriangle(offset + 2 * i + 3, offset + 2 * i + 2, offset + 2 * i + 0);
            }

            // Second half circle
            offset = vh.currentVertCount;
            for (int i = 0; i < circleResolution; i++)
            {
                float angle = ((float)i / (circleResolution - 1)) * (float)(Math.PI / 2);
                angle += (x > 0) ? 0 : (float)(Math.PI / 2);

                float xPos1 = (realRadius - thickness / 2) * (float)Math.Cos(angle);
                float yPos1 = (realRadius - thickness / 2) * (float)Math.Sin(angle);
                float xPos2 = (realRadius + thickness / 2) * (float)Math.Cos(angle);
                float yPos2 = (realRadius + thickness / 2) * (float)Math.Sin(angle);

                Vector3 posOffset = new Vector3(x - radiusXOffset, yCenter - realRadius, 0);

                vertex.position = new Vector3(xPos1, yPos1, 0) + posOffset;
                vh.AddVert(vertex);
                vertex.position = new Vector3(xPos2, yPos2, 0) + posOffset;
                vh.AddVert(vertex);
            }
            for (int i = 0; i < circleResolution - 1; i++)
            {
                vh.AddTriangle(offset + 2 * i + 0, offset + 2 * i + 1, offset + 2 * i + 3);
                vh.AddTriangle(offset + 2 * i + 3, offset + 2 * i + 2, offset + 2 * i + 0);
            }
        }
    }

    new void OnValidate()
    {
        base.OnValidate();
        thickness = Math.Max(thickness, 1);
        radius = Math.Max(radius, 1);
        circleResolution = Math.Max(circleResolution, 2);
    }
}