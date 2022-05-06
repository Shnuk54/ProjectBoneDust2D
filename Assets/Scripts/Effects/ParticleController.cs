
using UnityEngine;
using System;


public class ParticleController : MonoBehaviour
{
   public Particle[] particls;
      private void Awake()
    {
    }

      public void Play(string name)
    {
        Particle p = Array.Find(particls, particle => particle.name == name);
        GameObject pr = Instantiate(p.particle,gameObject.transform);
        pr.transform.position = gameObject.transform.position;
        Destroy(pr,2f);
        p.particle.GetComponent<ParticleSystem>().Play();
    }
}
