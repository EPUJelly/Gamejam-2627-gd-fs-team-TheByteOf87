using UnityEngine;

public class camShake : MonoBehaviour
{
    public static float shake;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float shakeX = Random.Range(-1.5f, 1.5f);
        float shakeY = Random.Range(-1.5f, 1.5f);
        float shakeZ = Random.Range(-1.5f, 1.5f);

        shakeX *= shake;
        shakeY *= shake;
        shakeZ *= shake;

        if (shake > 0.05f)
        {
            shake -= 0.005f;
            
        }
        else
        {
            shake = 0;
        }
        this.transform.localPosition = new Vector3(shakeX, shakeY, shakeZ);
    }
}
