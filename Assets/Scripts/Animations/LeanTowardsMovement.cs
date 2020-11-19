using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeanTowardsMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController = null;
    //[SerializeField] private float angleMult = 1;
    //[SerializeField] private float smooth = 1;
    //[SerializeField] private float velocity = 1;


    // Update is called once per frame
    void LateUpdate()
    {
        //float targetLean = new Vector2(Mathf.Abs(characterController.velocity.z),Mathf.Abs(characterController.velocity.x)).magnitude*angleMult;
        //float lean = Mathf.Lerp(transform.localRotation.x ,targetLean,speed);
        //Mathf.SmoothDamp(transform.localRotation.x, targetLean, ref velocity, smooth);
        //transform.localRotation = Quaternion.Euler(Mathf.SmoothDamp(transform.localRotation.x, targetLean, ref velocity, smooth),0,0);
        //Debug.Log(characterController.velocity);
    }
}
