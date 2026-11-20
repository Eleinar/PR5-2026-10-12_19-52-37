using System;
using System.Collections;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private Light light;

    [SerializeField]
    private KeyCode key = KeyCode.F;

    [SerializeField]
    private float animSpeed = 5f;

    private bool isOn = false;


    private Vector3 torchOnPosition = new Vector3(0.3f, -0.3f, 0.5f);
    private Vector3 torchOffPosition = new Vector3(0.3f, -0.3f, 0.2f);

    private Vector3 torchOnRotation = new Vector3(0, 0, 0);
    private Vector3 torchOffRotation = new Vector3(-90, 0, 0);

    private Coroutine flashAnim;


    void Start()
    {
        if(light == null)
        {
            light = GetComponent<Light>();
        }
        light.enabled = isOn;
        transform.localPosition = isOn ? torchOnPosition : torchOffPosition;
        transform.localRotation = Quaternion.Euler(isOn ? torchOnRotation : torchOffRotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            isOn = !isOn;
            

        
            if(flashAnim != null)
            {
                StopCoroutine(flashAnim);
            }
            flashAnim = StartCoroutine(FlashAnimation(isOn));
        }

        
    }

    IEnumerator FlashAnimation(bool on)
    {
        if(on) { light.enabled = true; }

        Vector3 targetPosition = isOn ? torchOnPosition : torchOffPosition;
        Quaternion targetRotation = Quaternion.Euler(isOn ? torchOnRotation : torchOffRotation);

        float t = 0f;

        while(t < 1f)
        {
            t += Time.deltaTime / animSpeed;
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, t);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, t);
            yield return null;
        }
        transform.localPosition = targetPosition;
        transform.localRotation = targetRotation;

        if(!on) { light.enabled = false; }
        flashAnim = null;

    }
}
