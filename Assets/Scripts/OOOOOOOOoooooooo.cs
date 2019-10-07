using UnityEngine;

public class OOOOOOOOoooooooo : MonoBehaviour
{

    private AudioSource _oooo;
    

    // Start is called before the first frame update
    void Start()
    {
        _oooo = GetComponent<AudioSource>();
    }

    
    void FixedUpdate() {
        if (_oooo.isPlaying && _oooo.volume < 1) {
            _oooo.volume += Time.deltaTime / 5;
        }
    }

    public void OoooTime(Ability ability) {
        if (ability == Ability.Sound) {
            _oooo.Play();
        }
    }
}
