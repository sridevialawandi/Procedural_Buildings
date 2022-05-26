using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoisePlane : MonoBehaviour
{
    public float power = 3.0f;
    public float scale = 1.0f;
    public int texture_width = 64;
	public int texture_height = 64;
	//public float scale = 10;
    private Vector2 v2SampleStart = new Vector2(0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        MakeSomeNoise ();
     	// create a new GameObject and give it a MeshFilter and a MeshRenderer
		GameObject s = new GameObject("Textured Mesh");
		s.AddComponent<MeshFilter>();
		s.AddComponent<MeshRenderer>();

		// associate my mesh with this object
		//s.GetComponent<MeshFilter>().mesh = my_mesh;

		// change the color of the object
		Renderer rend = s.GetComponent<Renderer>();
		rend.material.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);

		// create a texture
		Texture2D texture = make_a_texture();

		// attach the texture to the mesh
		Renderer renderer = s.GetComponent<Renderer>();
		renderer.material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t")) 
        {
            v2SampleStart = new Vector2(Random.Range (0.0f, 100.0f), Random.Range (0.0f, 100.0f));
            MakeSomeNoise();
		}
    }
    void MakeSomeNoise() {
             MeshFilter mf = GetComponent<MeshFilter>();
             Vector3[] vertices = mf.mesh.vertices;
             for (int i = 0; i < vertices.Length; i++) {    
             float xCoord = v2SampleStart.x + vertices[i].x  * scale;
             float yCoord = v2SampleStart.y + vertices[i].z  * scale;
                 vertices[i].y = (Mathf.PerlinNoise (xCoord, yCoord) - 0.5f) * power; 
             }
             mf.mesh.vertices = vertices;
             mf.mesh.RecalculateBounds();
             mf.mesh.RecalculateNormals();
         }
    	Texture2D make_a_texture() {

		// create the texture and an array of colors that will be copied into the texture
		Texture2D texture = new Texture2D (texture_width, texture_height);
		Color[] colors = new Color[texture_width * texture_height];

		// create the Perlin noise pattern in "colors"
		for (int i = 0; i < texture_width; i++)
			for (int j = 0; j < texture_height; j++) {
				float x = scale * i / (float) texture_width;
				float y = scale * j / (float) texture_height;
				float t = Mathf.PerlinNoise (x, y);                          // Perlin noise!
				colors [j * texture_width + i] = new Color (t, t, t, 1.0f);  // gray scale values (r = g = b)
			}

		// copy the colors into the texture
		texture.SetPixels(colors);

		// do texture-y stuff, probably including making the mipmap levels
		texture.Apply();

		// return the texture
		return (texture);
	}
    }