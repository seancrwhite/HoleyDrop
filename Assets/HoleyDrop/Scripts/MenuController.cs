using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
