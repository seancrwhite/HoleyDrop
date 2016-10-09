using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    private static MusicManager mInstance = null;
    public static MusicManager Instance { get { return mInstance; } }

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            mInstance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
