using UnityEngine;

public class LoseCollider : MonoBehaviour {

	private LevelManager levelManager;

	void Start(){
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}

	void OnTriggerEnter2D (Collider2D trigger) {
		//Debug.Log("Trigger");
		var tool = trigger.gameObject.GetComponent<Tool> ();
		if (tool == null) {
            return;
		}

        // Collect the tool!
        if (tool.collectOnlyMode == true)
        {
            var paddle = GameObject.FindObjectOfType<Paddle>();
            paddle.CollectTool(tool);
            // Get the tool!
            return;
        }

        levelManager.LoadLevel("Lose screen");
    }
	
	void OnCollisionEnter2D (Collision2D collider) {
		//Debug.Log("Collision");
	}
}
