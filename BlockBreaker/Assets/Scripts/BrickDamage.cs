using UnityEngine;
using System.Collections;

public class BrickDamage : MonoBehaviour {
	
	public Sprite[] damageSprites;

	private SpriteRenderer spriteRendererInstance;

	void Start () {
		spriteRendererInstance = GetComponent<SpriteRenderer>();
		spriteRendererInstance.GetComponent<Renderer>().enabled = false;
	}

	public void UpdateDamage (int maxHits, int hits) {
		if (!spriteRendererInstance.GetComponent<Renderer>().enabled) {
			spriteRendererInstance.GetComponent<Renderer>().enabled = true;
		}
	
		var spriteIndex = Mathf.CeilToInt(10 * hits / maxHits);
		if (damageSprites[spriteIndex] == null) {
			Debug.LogError("Damage Sprites " + spriteIndex.ToString() + " is missing its sprite!");
		}

		spriteRendererInstance.sprite = damageSprites[spriteIndex];
	}
}
