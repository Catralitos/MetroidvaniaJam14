using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float transitionTime;
    private float _timer;

    private BoxCollider2D _box;
    private bool _retracted;
    
    // Start is called before the first frame update
    private void Start()
    {
        _box = GetComponent<BoxCollider2D>();
        _timer = transitionTime;
        _retracted = true;
    }

    // Update is called once per frame
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            if (_retracted)
            {
                //animação de sair
                _box.enabled = true;
            }
            else
            {
                //animaçao de retract
                _box.enabled = false;
            }

            _timer = transitionTime;
        }
    }
}
