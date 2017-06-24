using UnityEngine;

public class Paddle : MonoBehaviour {
	public bool autoPlay = false;

    private Inventory inventoryInstance;

    private LineRenderer lineRendererInstance;
    private DrawLine drawLine;

    private Tool currentTool;

    public AudioClip swapToolAudio;
    public AudioClip collectToolAudio;

    private bool gameHasStarted = false;
	private Vector3 paddleToToolVector;

	void Start(){
        inventoryInstance = GameObject.FindObjectOfType<Inventory>();
        inventoryInstance.InstantiateTools();

        lineRendererInstance = GameObject.FindObjectOfType<LineRenderer>();
        drawLine = lineRendererInstance.GetComponent<DrawLine>();

        currentTool = inventoryInstance.GetCurrentTool();
        currentTool.transform.position = new Vector2 (8f, 1.75f);
        currentTool.gameObject.SetActive(true);

        paddleToToolVector = currentTool.transform.position - this.transform.position;
    }

	void Update () {

        HandleScrollWheelInputs();

        HandleMouseClickInputs();

        HandleHotkeyInputs();

        if (autoPlay) {
			MoveAutomatically();
			return;
		}
	}

    private void HandleMouseClickInputs()
    {
        if (!gameHasStarted)
        {
            // Lock the tool relative to the paddle
            currentTool.transform.position = this.transform.position + paddleToToolVector;

            // Launch the tool
            if (Input.GetMouseButtonDown(0))
            {
                currentTool.GetComponent<Rigidbody2D>().velocity = new Vector2(2f, 10f);
                //currentTool.GetComponent<Rigidbody2D>().AddForce(new Vector2(20f, 100f));
                gameHasStarted = true;
            }

            MoveWithMouse();
            return;
        }

        // Right mouse
        if (Input.GetMouseButtonDown(1))
        {
            currentTool.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 10f);
            //currentTool.GetComponent<Rigidbody2D>().AddForce(new Vector2(200f, 1000f));
        }
    }

    private void HandleScrollWheelInputs()
    {
        // Scroll wheel
        var d = Input.GetAxis("Mouse ScrollWheel");
        // Scroll up
        if (d > 0f)
        {
            inventoryInstance.SwapTool(false);
            RefreshCurrentTool();
            AudioSource.PlayClipAtPoint(swapToolAudio, transform.position);
        }
        // Scroll down
        else if (d < 0f)
        {
            inventoryInstance.SwapTool(true);
            RefreshCurrentTool();
            AudioSource.PlayClipAtPoint(swapToolAudio, transform.position);
        }

        MoveWithMouse();
    }

    private void HandleHotkeyInputs()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventoryInstance.SelectTool(0);
            RefreshCurrentTool();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventoryInstance.SelectTool(1);
            RefreshCurrentTool();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventoryInstance.SelectTool(2);
            RefreshCurrentTool();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventoryInstance.SelectTool(3);
            RefreshCurrentTool();
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            drawLine.MoveDestination(-1f);
            //transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            drawLine.MoveDestination(1f);
            //transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    private void RefreshCurrentTool()
    {
        currentTool = inventoryInstance.GetCurrentTool();
    }

    public void CollectTool(Tool tool)
    {
        inventoryInstance.CollectTool(tool);
        RefreshCurrentTool();
        AudioSource.PlayClipAtPoint(collectToolAudio, transform.position);
    }

    void MovePaddle(float x){
		Vector3 paddlePosition = new Vector3(.5f, this.transform.position.y, this.transform.position.z);
		paddlePosition.x = Mathf.Clamp(x, 1f, 15f);
		this.transform.position = paddlePosition;

        drawLine.origin = this.transform;
        drawLine.Initialize();
    }

	void MoveWithMouse(){
		var mousePositionInBlocks = Input.mousePosition.x / Screen.width * 16;
		MovePaddle (mousePositionInBlocks);
	}

	void MoveAutomatically(){
		var ballPosition = currentTool.transform.position;
		MovePaddle (ballPosition.x);
	}
}
