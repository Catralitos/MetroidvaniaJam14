/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour {

    public Material material;
    private Material _material;

    private SpriteRenderer[] _spriteRenderers;
    
    private float dissolveAmount;
    private float dissolveSpeed;
    private bool isDissolving;

    private void Start() {
        if (material == null) {
            material = transform.Find("Body").GetComponent<MeshRenderer>().material;
        }

        _material = new Material(material);
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (var sprite in _spriteRenderers)
        {
            sprite.material = _material;
        }

    }

    private void Update() {
        if (!gameObject.activeSelf)
        {
            ResetDissolve();
            return;
        }
        if (isDissolving) {
            dissolveAmount = Mathf.Clamp01(dissolveAmount + dissolveSpeed * Time.deltaTime);
            _material.SetFloat("_DissolveAmount", dissolveAmount);
        } else {
            dissolveAmount = Mathf.Clamp01(dissolveAmount - dissolveSpeed * Time.deltaTime);
            _material.SetFloat("_DissolveAmount", dissolveAmount);
        }
    }

    public void StartDissolve(float dissolveSpeed, Color dissolveColor) {
        if (!gameObject.activeSelf)
        {
            ResetDissolve();
            return;
        }
        isDissolving = true;
        _material.SetColor("_DissolveColor", dissolveColor);
        this.dissolveSpeed = dissolveSpeed;
    }

    public void StopDissolve(float dissolveSpeed, Color dissolveColor) {
        if (!gameObject.activeSelf)
        {
            ResetDissolve();
            return;
        }
        isDissolving = false;
        _material.SetColor("_DissolveColor", dissolveColor);
        this.dissolveSpeed = dissolveSpeed;
    }

    public void ResetDissolve()
    {
        _material.SetFloat("_DissolveAmount", 0f);
    }

}
