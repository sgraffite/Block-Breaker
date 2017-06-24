using UnityEngine;

public enum ToolType {Axe, Pickaxe, Shovel, Sword};

public class Tool : MonoBehaviour {

    private Rigidbody2D rigidbody2D;

    public int power;

    public bool collectOnlyMode = false;

    public ToolType type;

	public AudioClip toolBreakAudio;
    public AudioClip toolCollideAudio;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void TweakVelocity(float min, float max)
    {
        var xDirection = rigidbody2D.velocity.x > 0 ? 1 : -1;
        var yDirection = rigidbody2D.velocity.y > 0 ? 1 : -1;
        var tweak = new Vector2(Random.Range(min, max) * xDirection, Random.Range(min, max) * yDirection);
        rigidbody2D.velocity += tweak;

        //this.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(min, max), Random.Range(min, max)));
    }

    void OnCollisionEnter2D(Collision2D collision){
        TweakVelocity(0f, 0.1f);

        var tool = collision.gameObject.GetComponent<Tool>();
        if (tool != null)
        {
            AudioSource.PlayClipAtPoint(toolCollideAudio, transform.position);
            TweakVelocity(0.5f, 1f);
        }
	}
}