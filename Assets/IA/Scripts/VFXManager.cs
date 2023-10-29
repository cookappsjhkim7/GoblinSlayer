using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    //public List<ParticleSystem> ptData;

    ParticleSystem tmpPt;

    public PoolManager vfxPool;

    public void SpawnVFX(int ptIndex, Vector3 pos)
    {
        int ranNum = Random.Range(0, 3);

        tmpPt = vfxPool.Get(ptIndex, transform.position).GetComponent<ParticleSystem>();


        pos.x += Random.Range(-0.3f, 0.7f);
        pos.y += Random.Range(-0.3f, 0.7f);

        VFXPlay(tmpPt, pos);
    }

    public void VFXPlay(ParticleSystem pt, Vector3 pos)
    {
        pt.transform.position = pos;
        pt.Play();
    }
}
