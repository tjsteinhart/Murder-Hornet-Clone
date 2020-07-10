using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveController : MonoBehaviour, IGetStung
{
    #region Animation Variables
    [SerializeField] Animator warningAnim;
    [SerializeField] float warningClipTime = 200f;
    float timeToImpact;
    float animSpeed;
    float distance;
    HornetController hornet;
    #endregion

    

    // Start is called before the first frame update
    void Start()
    {
        hornet = FindObjectOfType<HornetController>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void UpdateWarningAnimation()
    {
        distance = Vector3.Distance(this.transform.position, hornet.transform.position);
        timeToImpact = distance / (hornet.CurrentSpeed());
        animSpeed = warningClipTime / timeToImpact;
        warningAnim.speed = animSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    public void GetStung()
    {
        Explode();
    }

    private void Explode()
    {

    }
}
