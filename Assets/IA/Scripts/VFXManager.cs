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


        if(ptIndex == 0)
        {
            pos.x += Random.Range(-0.2f, 0.3f);
            pos.y += Random.Range(-0.2f, 0.5f);
        }

        VFXPlay(tmpPt, pos);
    }

    public void SpawnVFX_2(int ptIndex, Vector3 pos, float angle)
    {
        int ranNum = Random.Range(0, 3);

        tmpPt = vfxPool.Get(ptIndex, transform.position).GetComponent<ParticleSystem>();

        tmpPt.transform.position = pos;
        tmpPt.transform.rotation = Quaternion.Euler(0, 0, angle);
        tmpPt.Play();

        StartCoroutine(PTActiveOff(tmpPt));
    }


    public void VFXPlay(ParticleSystem pt, Vector3 pos)
    {
        pt.transform.position = pos;
        pt.Play();

        StartCoroutine(PTActiveOff(pt));
    }

    IEnumerator PTActiveOff(ParticleSystem pt)
    {
        yield return new WaitForSeconds(0.4f);

        pt.gameObject.SetActive(false);
    }
}
