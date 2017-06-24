using UnityEngine;

public class Brick : MonoBehaviour {
	public AudioClip damageAudio;
	public int MaxHits;
	public ToolType toolNeeded;
	private int timesHit;

	public GameObject brokenChunks;
	public Color32 brokenChunkColor;
	public BrickDamage brickDamage;

	private SpriteRenderer spriteRendererInstance;

	/*
	private Color32[] brickColorArray = new Color32[]{
		new Color32( 0xFF, 0xFF, 0xFF, 0xFF ),
		new Color32( 0x66, 0xCC, 0xFF, 0xFF ),
		new Color32( 0x00, 0x33, 0xFF, 0xFF ),
		new Color32( 0x33, 0xFF, 0x33, 0xFF ),
		new Color32( 0xff, 0xff, 0x00, 0xFF ),
		new Color32( 0xff, 0x66, 0x00, 0xFF ),
		new Color32( 0xff, 0x00, 0x00, 0xFF ),
		new Color32( 0xcc, 0x00, 0xff, 0xFF ),
	};*/
	
    
	void Start () {
        timesHit = 0;

		spriteRendererInstance = GetComponent<SpriteRenderer>();
		if (spriteRendererInstance.sprite == null) {
			Debug.LogError("Sprite " + spriteRendererInstance.name + " is missing its sprite!");
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		var tool = collision.gameObject.GetComponent<Tool> ();
		if (tool == null) {
			Debug.LogError ("Expected collision of type <Tool>");
			return;
		}

		if (tool.type != toolNeeded) {
            if (!tool.collectOnlyMode)
            {
                AudioSource.PlayClipAtPoint(tool.toolBreakAudio, transform.position);
            }
			return;
		}

		AudioSource.PlayClipAtPoint (damageAudio, transform.position);
        Debug.Log(" tool.power = " + tool.power);

        timesHit += tool.power;
		ShowBrokenChunks();
		if (timesHit >= MaxHits) {
			Destroy (gameObject);
			return;
		}

		brickDamage.UpdateDamage (MaxHits, timesHit);
	}

	private void ShowBrokenChunks(){
		var brokenChunksPuff = Instantiate(brokenChunks, transform.position, Quaternion.identity) as GameObject;
		brokenChunkColor.a = 255; // Make sure the color shows up
		brokenChunksPuff.GetComponent<Renderer>().material.color = brokenChunkColor;
	}

	private void SetRandomColor(){
		spriteRendererInstance.color = new Color32 ((byte)Random.Range (0, 254), (byte)Random.Range (0, 254), (byte)Random.Range (0, 254), (byte)254);
	}
}
