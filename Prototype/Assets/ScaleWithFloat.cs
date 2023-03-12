using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithFloat : MonoBehaviour
{
    [SerializeField] float scaleMultiplier = 1;
    [SerializeField] float maxThreshold = 0.1f;
    Vector3 initScale;
    Vector3 previousScale;
    // Start is called before the first frame update
    void Start()
    {
        initScale = this.transform.localScale;
        previousScale = initScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SmoothScale(float f)
    {
        Vector3 newScale = initScale + new Vector3(f, f, f) * scaleMultiplier;

        if (this.transform.localScale.x < newScale.x)
        {
            if (newScale.x - this.transform.localScale.x > maxThreshold)
            {
                this.transform.localScale = newScale;
            }
           
        } else
        {
            this.transform.localScale = (previousScale + newScale) / 2;
        }

        previousScale = this.transform.localScale;
       
    }
}
