using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class PngAlphaMaker : MonoBehaviour
{
    private string _texFolderName = "Textures/";
    public int _texCount = 30;
    public string _effectName;
    public int _width;
    public int _height;
    public int _index;
    public List<Texture2D> GetTextures(string FolderName)
    {
        List<Texture2D> texList = new List<Texture2D>();

        for(int i = 0; i < _texCount; ++i)
        {
            Texture2D tex = Resources.Load<Texture2D>(_texFolderName + FolderName + "/" + (i+1).ToString());
            texList.Add(tex);
        }
        return texList;
    }

    public List<Texture2D> GetTextures(string FolderName, int Index)
    {
        List<Texture2D> texList = new List<Texture2D>();

        for (int i = 0; i < _texCount; ++i)
        {
            Texture2D tex = Resources.Load<Texture2D>(_texFolderName + FolderName + "/" + (Index).ToString() + "/" + (i + 1).ToString());
            texList.Add(tex);
        }
        return texList;
    }

    public void ChangeToColor(List<Texture2D> texList, string FolderName)
    {
        for(int i = 0; i < _texCount; ++i)
        {
            for (int x = 0; x < 1280; ++x)
            {
                for (int y = 0; y < 720; ++y)
                {
                    Color c = texList[i].GetPixel(x, y);
                    c = ChangeColor(c);
                    texList[i].SetPixel(x, y, c);
                }
            }

            texList[i].Apply();
            byte[] bytes = texList[i].EncodeToPNG();
            File.WriteAllBytes("Assets/Resources/TransformTex/" + FolderName + "/" + i.ToString() + ".png", bytes);
        }
    }

    public void ChangeToColor(List<Texture2D> texList, string FolderName, int Index)
    {
        for (int i = 0; i < _texCount; ++i)
        {
            for (int x = 0; x < 1280; ++x)
            {
                for (int y = 0; y < 720; ++y)
                {
                    Color c = texList[i].GetPixel(x, y);
                    c = ChangeColor(c);
                    texList[i].SetPixel(x, y, c);
                }
            }

            texList[i].Apply();
            byte[] bytes = texList[i].EncodeToPNG();
            File.WriteAllBytes("Assets/Resources/TransformTex/" + FolderName + "/" + Index.ToString() + "/" + i.ToString() + ".png", bytes);
        }
    }

    public void ChangeToColor(List<Texture2D> texList, string FolderName, int Index, int Width, int Height)
    {
        for (int i = 0; i < _texCount; ++i)
        {
            for (int x = 0; x < Width; ++x)
            {
                for (int y = 0; y < Height; ++y)
                {
                    Color c = texList[i].GetPixel(x, y);
                    c = ChangeColor(c);
                    texList[i].SetPixel(x, y, c);
                }
            }

            texList[i].Apply();
            byte[] bytes = texList[i].EncodeToPNG();
            File.WriteAllBytes("Assets/Resources/TransformTex/" + FolderName + "/" + Index.ToString() + "/" + i.ToString() + ".png", bytes);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            List<Texture2D> texList = GetTextures(_effectName, 1);
            ChangeToColor(texList, _effectName, 1, _width, _height);
            texList = GetTextures(_effectName, 2);
            ChangeToColor(texList, _effectName, 2, _width, _height);
            texList = GetTextures(_effectName, 3);
            ChangeToColor(texList, _effectName, 3, _width, _height);
        }
    }

    public void ChangeToColor(Texture2D tex)
    {
        for(int x = 0; x < 900; ++x)
        {
            for(int y = 0; y <500; ++y)
            {
                Color c = tex.GetPixel(x, y);
                c = ChangeColor(c);
                tex.SetPixel(x, y, c);
            }
        }

        tex.Apply();
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes("Assets/Resources/TransformTex/1.png", bytes);
    }


    public Color ChangeColor(Color c)
    {
        float R = c.r;
        float G = c.g;
        float B = c.b;
        float A = c.a;

        float f;

        if(R >= G)
        {
            f = R;
        }
        else
        {
            f = G;
        }

        if(f >= B)
        {

        }
        else
        {
            f = B;
        }

        float value = 1.0f / f;

        R *= value;
        G *= value;
        B *= value;
        A = 1.0f / value;

        return new Color(R, G, B, A);
    }
}
