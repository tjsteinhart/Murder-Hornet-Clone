using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveController : MonoBehaviour, IGetStung
{
    #region Animation Variables
    [SerializeField] Animator explosiveAnimator;
    [SerializeField] float warningClipTime = 200f;
    float timeToImpact;
    float animSpeed;
    float distance;
    HornetController hornet;
    #endregion

    #region Explosion Variables
    [SerializeField] float explosionForce = 2000f;
    [SerializeField] float explosionRadius = 4f;
    [SerializeField] ParticleSystem explodeParticles;
    [SerializeField] GameObject explosiveObject;
    [SerializeField] Collider triggerCollider;
    #endregion

    private void Awake()
    {
        explosiveAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hornet = FindObjectOfType<HornetController>();
        explosiveAnimator.enabled = false;
    }

    private void OnEnable()
    {
        EventManager.Instance.onStartGameplay += StartWarningAnim;
        EventManager.Instance.onEndGamePlay += StopWarningAnim;
    }

    private void OnDisable()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.onStartGameplay -= StartWarningAnim;
            EventManager.Instance.onEndGamePlay -= StopWarningAnim;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWarningAnimation();
    }


    private void StartWarningAnim()
    {
        explosiveAnimator.enabled = true;
    }

    private void StopWarningAnim()
    {
        explosiveAnimator.enabled = false;
    }

    private void UpdateWarningAnimation()
    {
        distance = Vector3.Distance(this.transform.position, hornet.transform.position);
        timeToImpact = distance / (hornet.CurrentSpeed());
        animSpeed = warningClipTime / timeToImpact;
        explosiveAnimator.speed = animSpeed;
    }

    public void GetStung()
    {
        Explode();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach(Collider objectInRange in colliders)
        {
            TargetController target = objectInRange.GetComponent<TargetController>();
            if(target != null)
            {
                target.GetMyRigidBody().AddExplosionForce(explosionForce, transform.position, explosionRadius);
                target.GetStung();
            }
        }

        triggerCollider.enabled = false;
        explodeParticles.Play();
        StopWarningAnim();
        Destroy(explosiveObject);
        Destroy(gameObject, 8f);
    }
}
