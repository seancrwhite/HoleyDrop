using UnityEngine;
using System.Collections;

public class DestroyOffscreen : MonoBehaviour {
    public float offscreenR = 4f;
    public float fade = .1f;
    public delegate void OnDestroy();
    public event OnDestroy DestroyCallBack;

    private bool offscreen;

    private void Update() {
        var posR = transform.localScale.x;

        offscreen = posR > offscreenR;

        if(offscreen){
            var color = GetComponent<Renderer>().material.color;
            Color newColor = new Color(color.r, color.g, color.b, color.a - fade);
            GetComponent<Renderer>().material.color = newColor;
            if(newColor.a <= 0)
                OnOutOfBounds();
        }

    }

    public void OnOutOfBounds(){
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);

        if(DestroyCallBack != null){
            DestroyCallBack();
        }
    }
}