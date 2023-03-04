using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swish : MonoBehaviour
{
    [Header("Vrouuuuu")]
    [SerializeField] private Vector3 vrouuuuuAmplitudes;
    [SerializeField] private Vector3 vrouuuuuDurations;
    private Vector3 vrouuuuuDt = Vector3.zero;
    private Vector3 vrouuuuuPingPongs = Vector3.zero;

    private Vector3 initPosition;
    private Vector3 tmpNewPosition;

    [Header("Fleuteuteu")]
    [SerializeField] private Vector3 fleuteuteuAmplitudes;
    [SerializeField] private Vector3 fleuteuteuDurations;
    private Vector3 fleuteuteuDt = Vector3.zero;
    private Vector3 fleuteuteuPingPongs = Vector3.zero;

    private Vector3 initRotation;
    private Vector3 tmpNewRotation;

    [SerializeField] private Card card;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            vrouuuuuDt[i] = vrouuuuuDurations[i] / 2f;
            fleuteuteuDt[i] = fleuteuteuDurations[i] / 2f;
        }

        initPosition = this.transform.position;
        initRotation = this.transform.eulerAngles;
    }

    void Update()
    {
        if (card == card.PlayerInfo.currentGrabbedCard) return;
        ComputeVrouuuuus();
        ComputeFleuteuteus();
    }

    void ComputeVrouuuuus()
    {
        tmpNewPosition = initPosition;
        for (int i = 0; i < 3; i++)
        {
            if (vrouuuuuDurations[i] != 0 && vrouuuuuAmplitudes[i] != 0)
            {
                vrouuuuuDt[i] += Time.deltaTime;
                vrouuuuuPingPongs[i] = Mathf.PingPong(vrouuuuuDt[i], vrouuuuuDurations[i]) / vrouuuuuDurations[i];

                float currentOffset = Mathf.Lerp(-vrouuuuuAmplitudes[i], vrouuuuuAmplitudes[i], vrouuuuuPingPongs[i]);
                tmpNewPosition[i] += currentOffset;
            }  
        }

        this.transform.position = tmpNewPosition;
    }

    void ComputeFleuteuteus()
    {
        tmpNewRotation = initRotation;
        for (int i = 0; i < 3; i++)
        {
            if (fleuteuteuDurations[i] != 0 && fleuteuteuAmplitudes[i] != 0)
            {
                fleuteuteuDt[i] += Time.deltaTime;
                fleuteuteuPingPongs[i] = Mathf.PingPong(fleuteuteuDt[i], fleuteuteuDurations[i]) / fleuteuteuDurations[i];

                float currentOffset = Mathf.Lerp(-fleuteuteuAmplitudes[i], fleuteuteuAmplitudes[i], fleuteuteuPingPongs[i]);
                tmpNewRotation[i] += currentOffset;
            }
        }

        this.transform.eulerAngles = tmpNewRotation;
    }

    public void ResetAnimation()
    {
        fleuteuteuDt = Vector3.zero;
        fleuteuteuPingPongs = Vector3.zero;
        vrouuuuuDt = Vector3.zero;
        vrouuuuuPingPongs = Vector3.zero;

        for (int i = 0; i < 3; i++)
        {
            vrouuuuuDt[i] = vrouuuuuDurations[i] / 2f;
            fleuteuteuDt[i] = fleuteuteuDurations[i] / 2f;
        }

        initPosition = this.transform.position;
        initRotation = this.transform.eulerAngles;
        tmpNewPosition = initPosition;
    }
}
