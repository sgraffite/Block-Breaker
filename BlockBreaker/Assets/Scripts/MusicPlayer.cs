using UnityEngine;

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer MusicPlayerInstance = null;

	void Awake(){
		if(MusicPlayerInstance != null){
			Destroy(gameObject);
			Debug.Log("Duplicate music player self-destructing!");
			return;
		}

		MusicPlayerInstance = this;
		GameObject.DontDestroyOnLoad(gameObject);
	}
}
