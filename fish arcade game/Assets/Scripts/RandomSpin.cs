using UnityEngine;

public class RandomSpin : MonoBehaviour
{
    [SerializeField] private bool isRect = false;
    [SerializeField] private Vector2 minMax;
    private float r;
    private float rTarg;
    [SerializeField] private float speed;
    private bool isRotating = false;
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
        if (isRotating == false) 
        {
            rTarg = Random.Range(minMax.x, minMax.y);
            isRotating = true;
            Invoke("ResetStartRot", 2);
        }
        else
        {
            r = Mathf.Lerp(r, rTarg, speed);
            if(isRect)
            {
                this.GetComponent<RectTransform>().rotation = Quaternion.Euler(this.GetComponent<RectTransform>().rotation.eulerAngles.x, this.GetComponent<RectTransform>().rotation.eulerAngles.y, r);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y, r);
            }

        }
    }

    private void ResetStartRot()
    {
        isRotating = false;
    }
}
