using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandObjectOnContact : MonoBehaviour
{
    #region Serialized Variables 

    [SerializeField] private GameObject objectToExpand;
    [SerializeField] private float defaultSize = 1f;
    [SerializeField] private float bigSize = 1.15f;
    [SerializeField] private float speed = 1f;

    #endregion

    public IEnumerator GrowAndShrink()
    {
        yield return AnimateScale(bigSize * Vector3.one);
        yield return AnimateScale(defaultSize * Vector3.one);
    }

    private IEnumerator AnimateScale(Vector3 endSize)
    {
        bool endProcess = false;
        Vector3 vel = Vector3.zero;
        while(!endProcess)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, endSize, ref vel, speed); 

            float dist = Vector3.Distance(transform.localScale, endSize);

            if(dist <= 0.1f)
            {
                endProcess = true;
            }

            yield return null;
        }
    }

}
