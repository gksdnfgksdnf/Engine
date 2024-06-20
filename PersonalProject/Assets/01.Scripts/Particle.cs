using UnityEngine;

public class Particle : MonoBehaviour
{
    private ParticleSystem _particle;
    private PlayerInput _input;
    private Movement _movement;

    private void Awake()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
        _input = GetComponentInParent<PlayerInput>();
        _movement = GetComponentInParent<Movement>();
        _input.OnMove += StartParticle;
    }

    private void FixedUpdate()
    {
        //_particle.gameObject.transform.position = _movement._lastPos;
    }
    public void StartParticle(Vector3 dir)
    {
        _particle.Clear(false);
        _particle.Play();
    }


    public void StopParticle()
    {
        _particle.Stop();
    }
}
