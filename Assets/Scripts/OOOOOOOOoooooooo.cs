using UnityEngine;

public class OOOOOOOOoooooooo : MonoBehaviour
{

    private AudioSource _oooo;
    

    // Start is called before the first frame update
    void Start()
    {
        _oooo = GetComponent<AudioSource>();
    }

    public void OoooTime(Ability ability) {
        if (ability == Ability.Sound) {
            _oooo.Play();
        }
    }
}
