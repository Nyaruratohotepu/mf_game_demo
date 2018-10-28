using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//<summary>
//    若物体挂载该脚本，此脚本会合并所有子物体网格
//    若挂载物体是空物体，则此脚本会添加新的MeshFilter和MeshRenderer
//</summary>

public class MeshCom : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        MeshCombine();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void MeshCombine()
    {
        //第一步 获得所有子物体的MeshFilterh和MeshRenderer
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>();

        int childrenNum = meshFilters.Length;

        //第二步 新建CombineInstance数组，存贮子物体信息
        CombineInstance[] combines = new CombineInstance[childrenNum];

        //第三步 存储子物体的材质球，贴图
        Material[] materials = new Material[childrenNum];
        Texture2D[] textures = new Texture2D[childrenNum];

        //创建新材质
        Material materialNew = new Material(meshRenderers[0].sharedMaterial.shader);
        materialNew.CopyPropertiesFromMaterial(meshRenderers[0].sharedMaterial);
        //创建新贴图
        Texture2D textureNew = new Texture2D(2048, 2048);
        Rect[] rects = textureNew.PackTextures(textures, 10, 2048);


        for (int i = 0; i < childrenNum; i++)
        {
            //保存子物体的网格
            combines[i].mesh = meshFilters[i].sharedMesh;
            //保存子物体的 网格转换矩阵(用来将本地坐标系转为全局坐标系)
            combines[i].transform = transform.worldToLocalMatrix * meshFilters[i].transform.localToWorldMatrix;       //不乘后面那一项会导致合并后物体位置改变
            //保存子物体的材质球
            materials[i] = meshRenderers[i].sharedMaterial;

            //确保：有贴图并且贴图可读可写（选中图片，在inspection面板中）
            Texture2D tx = materials[i].GetTexture("_MainTex") as Texture2D;

            Texture2D tx2D = new Texture2D(tx.width, tx.height, TextureFormat.ARGB32, false);
            tx2D.SetPixels(tx.GetPixels(0, 0, tx.width, tx.height));
            tx2D.Apply();
            textures[i] = tx2D;

            Vector2[] uvs = new Vector2[meshFilters[i].mesh.uv.Length];

            //把网格的uv根据贴图的rect刷一遍
            for (int j = 0; j < uvs.Length; j++)
            {
                uvs[j].x = rects[i].x + meshFilters[i].mesh.uv[j].x * rects[i].width;
                uvs[j].y = rects[i].y + meshFilters[i].mesh.uv[j].y * rects[i].height;
            }

            meshFilters[i].mesh.uv = uvs;



            //将子物体摧毁
            DestroyImmediate(meshFilters[i].gameObject);
        }






        //新建物体合集的网格
        gameObject.AddComponent<MeshFilter>().sharedMesh = new Mesh();
        //用上文保存的合并信息数组来合并网格，将子物体的网格合并到脚本挂载物体上
        gameObject.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combines);
        //为新物体指定材质
        gameObject.AddComponent<MeshRenderer>().sharedMaterial = materialNew;
        //为新物体指定碰撞体
        gameObject.AddComponent<MeshCollider>();

        Destroy(gameObject.GetComponent<MeshCom>());
    }
}
