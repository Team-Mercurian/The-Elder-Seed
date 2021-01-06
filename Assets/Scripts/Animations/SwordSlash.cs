using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{

    private Material mat;
    private ParticleSystem psystem;
    private Vector4 tilingVector;
    private Vector4 offsetVector;

    private float time;

    public AnimationCurve tilingCurve;
    public float tilingMaxValue;
    public float tilingMinValue;
    public AnimationCurve offsetCurve;
    public float offsetMaxValue;
    public float offsetMinValue;
    public AnimationCurve AlphaCurve;
    public float lifetime;

    float test_timer;//delete
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material;
        psystem = GetComponent<ParticleSystem>();
        StartCoroutine(Slash());
    }

    private void Update() {
        test_timer += Time.deltaTime;
        if(test_timer > 3)
        {
            test_timer = 0;
            StartCoroutine(Slash());
        }
    } // delete whole update function later

    IEnumerator Slash()
    {
        psystem.Play();
        float timer = 0;
        float tilingX = 0;
        float offsetX = 0;
        float alphaX = 0;
        while(timer < lifetime)
        {
            timer += Time.deltaTime;

            tilingX = tilingCurve.Evaluate(Map(timer,0f,lifetime,0f,1f));
            tilingX = Map(tilingX,0f,1f,tilingMinValue,tilingMaxValue);
            tilingVector = new Vector4(tilingX,1,0f,0f);
            mat.SetVector("Vector2_D8B6DFB1", tilingVector); //tiling

            offsetX = offsetCurve.Evaluate(Map(timer,0f,lifetime,0f,1f));
            offsetX = Map(offsetX,0f,1f,offsetMinValue,offsetMaxValue);
            offsetVector = new Vector4(offsetX,0,0,0);
            mat.SetVector("Vector2_AA24C56", offsetVector); //offset

            alphaX = AlphaCurve.Evaluate(Map(timer,0f,lifetime,0f,1f));
            alphaX = Map(alphaX,0f,1f,0.5f,1f);
            mat.SetFloat("Vector1_A2614848", alphaX); //offset
            yield return null;
        }

    }

    public static float Map (float value, float from1, float to1, float from2, float to2) {
    return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
