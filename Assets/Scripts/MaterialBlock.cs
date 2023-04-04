using UnityEngine;

public class MaterialBlock : MonoBehaviour
{
    private Material material;

    private Renderer cubeRenderer;
    private MaterialPropertyBlock propertyBlock;


    //private void Init()
    //{
    //    propertyBlock.SetColor("_EmissionColor", material.GetColor("_EmissionColor"));
    //    propertyBlock.SetFloat("_Metallic", material.GetFloat("_Metallic"));
    //    propertyBlock.SetFloat("_Glossiness", material.GetFloat("_Glossiness"));
    //}

    public void SetColor(Color color)
    {
        cubeRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();


        propertyBlock.SetColor("_Color", color);
        cubeRenderer.SetPropertyBlock(propertyBlock);
    }
}
