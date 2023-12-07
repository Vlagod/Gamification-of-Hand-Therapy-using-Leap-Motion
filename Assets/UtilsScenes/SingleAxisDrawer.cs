using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SingleAxisDrawer : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    }

    public Axis axisType = Axis.X;
    public float axisLength = 1.0f;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        SetAxisColor();
        UpdateAxis();
    }

    private void SetAxisColor()
    {
        switch (axisType)
        {
            case Axis.X:
                lineRenderer.startColor = Color.red;
                lineRenderer.endColor = Color.red;
                break;
            case Axis.Y:
                lineRenderer.startColor = Color.green;
                lineRenderer.endColor = Color.green;
                break;
            case Axis.Z:
                lineRenderer.startColor = Color.blue;
                lineRenderer.endColor = Color.blue;
                break;
        }
    }

    private void UpdateAxis()
    {
        Vector3 endPos = transform.position;
        switch (axisType)
        {
            case Axis.X:
                endPos += transform.right * axisLength;
                break;
            case Axis.Y:
                endPos += transform.up * axisLength;
                break;
            case Axis.Z:
                endPos += transform.forward * axisLength;
                break;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, endPos);
    }
}