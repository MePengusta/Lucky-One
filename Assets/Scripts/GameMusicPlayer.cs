using UnityEngine;
using System.Collections;

public class GameMusicPlayer : MonoBehaviour
{

    private static GameMusicPlayer instance = null;
    private static AudioSource audioSource;
    public static GameMusicPlayer Instance
    {
        get { return instance; }
    }

	private void Start()
	{
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(audioSource);
	}

    public static void killMusic(){
        audioSource.Stop();
      //  Destroy(audioSource);
    }
    public static void startMusic(){
        audioSource.Play();

    }


    
}
