using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public float delay = 2.0f;
    public bool active = true;
    public Vector2 delayRange = new Vector2(1, 2);
    public GameObject groundPrefab;

    private void Start(){
        ResetDelay();
        StartCoroutine(GroundGenerator());
    }

    IEnumerator GroundGenerator(){
        yield return new WaitForSeconds(delay);

        if(active){
            var newTransform = transform.position;
            GameObjectUtil.Instantiate(groundPrefab, newTransform);
            ResetDelay();
        }
        StartCoroutine(GroundGenerator());
    }

    private void ResetDelay(){
        delay = Random.Range(delayRange.x, delayRange.y);
    }

}