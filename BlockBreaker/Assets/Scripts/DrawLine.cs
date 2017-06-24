using UnityEngine;

public class DrawLine : MonoBehaviour {

    private LineRenderer lineRenderer;
    private float counter;
    private float distance;

    public Transform origin;
    public Transform destination;

    public float lineDrawSpeed = 6f;

	// Use this for initialization
	void Start () {
        Initialize();
    }

    public void Initialize()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetWidth(.45f, .45f);

        distance = Vector2.Distance(origin.position, destination.position);
    }
	
	// Update is called once per frame
	void Update () {
		if(counter >= distance)
        {
            return;
        }

        counter += .1f / lineDrawSpeed;

        float x = Mathf.Lerp(0, distance, counter);

        Vector2 pointA = origin.position;
        Vector2 pointB = destination.position;

        Vector2 pointAlongLine = x * (Vector2)Vector3.Normalize(pointB - pointA) + pointA;

        lineRenderer.SetPosition(1, pointAlongLine);
	}

    public void MoveDestination(float direction)
    {
        var position = destination.position;
        position.x += direction;
        destination.position = position;
        counter = 0f;
    }
}
