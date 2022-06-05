using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ImageWithRoundedCorners : MonoBehaviour {
    private static readonly int Props = Shader.PropertyToID("_WidthHeightRadius");

    private Material material;
    public float radius;
    private void Start()
    {
        Refresh();
    }
    void OnRectTransformDimensionsChange()
    {
        Refresh();
    }

    private void OnValidate()
    {
        Refresh();
    }

    private void Refresh(){
        if (material == null)
        {
            material = Instantiate(Resources.Load<Material>("Shader/RoundedCornersTextureMaterial"));
        }
        var rect = ((RectTransform)transform).rect;
        material.SetVector(Props, new Vector4(rect.width, rect.height, radius * 2, 0));
        if (gameObject.GetComponent<Image>() != null)
        {
            gameObject.GetComponent<Image>().material = material;
            //gameObject.GetComponent<Image>().sprite = null;
        }
        else if (gameObject.GetComponent<RawImage>() != null)
        {
            gameObject.GetComponent<RawImage>().material = material;
        }
    }
    public void Refresh(Vector2 r) 
    {
        material.SetVector(Props, new Vector4(r.x, r.y, radius * 2, 0));
        if (gameObject.GetComponent<Image>() != null)
        {
            gameObject.GetComponent<Image>().material = material;
            gameObject.GetComponent<Image>().sprite = null;
        }
        else if (gameObject.GetComponent<RawImage>() != null)
        {
            gameObject.GetComponent<RawImage>().material = material;
        }
    }
}