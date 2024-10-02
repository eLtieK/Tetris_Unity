using UnityEngine;
using UnityEngine.UI;

public class MoveBackground : MonoBehaviour
{
    [Range(-1f,1f)]
    public float speed;
    public float offset;
    private Material mat;

    void Start()
    {
        speed = 0.5f;
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {   
        offset += (Time.deltaTime * speed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
