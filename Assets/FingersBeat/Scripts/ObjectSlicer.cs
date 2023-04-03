using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlicer : MonoBehaviour
{
    // public Transform slicePlane;

    public Mesh meshToSlice;
    public Transform planeTransform;

    private void Start()
    {
        Mesh slicedMesh = SliceMesh(meshToSlice, new Plane(planeTransform.up, planeTransform.position));

        // Create a new game object with a MeshFilter and MeshRenderer component for the first sliced piece
        GameObject slicedObject1 = new GameObject("Sliced Object 1");
        slicedObject1.transform.position = transform.position;
        slicedObject1.transform.rotation = transform.rotation;
        MeshFilter slicedFilter1 = slicedObject1.AddComponent<MeshFilter>();
        slicedFilter1.mesh = slicedMesh;
        MeshRenderer slicedRenderer1 = slicedObject1.AddComponent<MeshRenderer>();
        slicedRenderer1.material = GetComponent<MeshRenderer>().material;
        Rigidbody slicedRigidbody1 = slicedObject1.AddComponent<Rigidbody>();
        slicedRigidbody1.mass = meshToSlice.bounds.size.magnitude;
        slicedRigidbody1.AddForceAtPosition(planeTransform.up * 10.0f, planeTransform.position, ForceMode.Impulse);

        // Create a new game object with a MeshFilter and MeshRenderer component for the second sliced piece
        GameObject slicedObject2 = new GameObject("Sliced Object 2");
        slicedObject2.transform.position = transform.position;
        slicedObject2.transform.rotation = transform.rotation;
        MeshFilter slicedFilter2 = slicedObject2.AddComponent<MeshFilter>();
        slicedFilter2.mesh = slicedMesh;
        MeshRenderer slicedRenderer2 = slicedObject2.AddComponent<MeshRenderer>();
        slicedRenderer2.material = GetComponent<MeshRenderer>().material;
        Rigidbody slicedRigidbody2 = slicedObject2.AddComponent<Rigidbody>();
        slicedRigidbody2.mass = meshToSlice.bounds.size.magnitude;
        slicedRigidbody2.AddForceAtPosition(-planeTransform.up * 10.0f, planeTransform.position, ForceMode.Impulse);
    }

    private Mesh SliceMesh(Mesh mesh, Plane slicePlane)
{
    Vector3[] vertices = mesh.vertices;
    int[] triangles = mesh.triangles;
    Vector3[] normals = mesh.normals;

    List<Vector3> newVertices = new List<Vector3>();
    List<int> newTriangles = new List<int>();
    int[] sides = new int[3];
    Vector3 intersection1, intersection2;

    for (int i = 0; i < triangles.Length; i += 3)
    {
        // Check if all the vertices are on the same side of the slice plane
        for (int j = 0; j < 3; j++)
        {
            sides[j] = sides[j] = slicePlane.GetSide(vertices[triangles[i + j]]) ? 1 : 0;;
        }

        // If the triangle intersects the slice plane, split it into two triangles
        if (sides[0] != sides[1] || sides[1] != sides[2])
        {
            for (int j = 0; j < 3; j++)
            {
                int k = (j + 1) % 3;

                // Calculate the intersection point
                if (LinePlaneIntersection(vertices[triangles[i + j]], vertices[triangles[i + k]], slicePlane, out intersection1))
                {
                    intersection2 = intersection1; // Set intersection2 to intersection1 to avoid using an uninitialized variable
                    int index = newVertices.IndexOf(intersection1);

                    // If the intersection point hasn't been added yet, add it
                    if (index == -1)
                    {
                        newVertices.Add(intersection1);
                        index = newVertices.Count - 1;
                    }

                    // Create two new triangles
                    int[] newTriangle1 = { triangles[i + j], index, triangles[i + k] };
                    int[] newTriangle2 = { triangles[i + j], triangles[i + k], index };
                    int[] sides1 = { sides[j], slicePlane.GetSide(intersection1) ? 1 : 0, sides[k] };
                    int[] sides2 = { sides[j], sides1[1], slicePlane.GetSide(intersection2) ? 1 : 0 };

                    // Check which side each new triangle belongs to
                    if (sides1[0] == sides1[1] && sides1[1] == sides1[2])
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            newTriangles.Add(newTriangle1[l]);
                        }
                    }
                    else if (sides2[0] == sides2[1] && sides2[1] == sides2[2])
                    {
                        for (int l = 0; l < 3; l++)
                        {
                            newTriangles.Add(newTriangle2[l]);
                        }
                    }
                    else
                    {
                        Debug.LogError("Error: Could not determine side of new triangles.");
                    }
                }
            }
        }
        else
        {
            // If the triangle is entirely on one side of the slice plane, add it to the new mesh
            for (int j = 0; j < 3; j++)
            {
                newVertices.Add(vertices[triangles[i + j]]);
                newTriangles.Add(newVertices.Count - 1);
            }
        }
    }

    Mesh newMesh = new Mesh();
    newMesh.vertices = newVertices.ToArray();
    newMesh.triangles = newTriangles.ToArray();

    return newMesh;
}
    private bool LinePlaneIntersection(Vector3 p1, Vector3 p2, Plane plane, out Vector3 intersection)
    {
        float enter;
        intersection = Vector3.zero;
        Vector3 direction = p2 - p1;

        if (plane.Raycast(new Ray(p1, direction), out enter))
        {
            intersection = p1 + enter * direction.normalized;
            return true;
        }
        else
        {
            return false;
        }
    }
}