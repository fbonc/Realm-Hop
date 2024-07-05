using UnityEngine;

public class CustomSkyboxCube : MonoBehaviour
{
    void Start()
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer renderer = gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[24];
        Vector2[] uv = new Vector2[24];
        int[] triangles = new int[36];

        // Define vertices
        vertices[0] = new Vector3(-1, 1, -1); // Top Left Front
        vertices[1] = new Vector3(1, 1, -1);  // Top Right Front
        vertices[2] = new Vector3(-1, -1, -1);// Bottom Left Front
        vertices[3] = new Vector3(1, -1, -1); // Bottom Right Front

        vertices[4] = new Vector3(1, 1, 1);   // Top Right Back
        vertices[5] = new Vector3(-1, 1, 1);  // Top Left Back
        vertices[6] = new Vector3(1, -1, 1);  // Bottom Right Back
        vertices[7] = new Vector3(-1, -1, 1); // Bottom Left Back

        vertices[8] = vertices[4]; // Right Top Front
        vertices[9] = vertices[1]; // Right Top Back
        vertices[10] = vertices[6];// Right Bottom Front
        vertices[11] = vertices[3];// Right Bottom Back

        vertices[12] = vertices[5];// Left Top Back
        vertices[13] = vertices[0];// Left Top Front
        vertices[14] = vertices[7];// Left Bottom Back
        vertices[15] = vertices[2];// Left Bottom Front

        vertices[16] = vertices[5];// Top Back Left
        vertices[17] = vertices[4];// Top Back Right
        vertices[18] = vertices[0];// Top Front Left
        vertices[19] = vertices[1];// Top Front Right

        vertices[20] = vertices[2];// Bottom Front Left
        vertices[21] = vertices[3];// Bottom Front Right
        vertices[22] = vertices[7];// Bottom Back Left
        vertices[23] = vertices[6];// Bottom Back Right

        // Define UVs
        uv[0] = new Vector2(0.25f, 0.66f);
        uv[1] = new Vector2(0.5f, 0.66f);
        uv[2] = new Vector2(0.25f, 0.33f);
        uv[3] = new Vector2(0.5f, 0.33f);

        uv[4] = new Vector2(0.75f, 0.66f);
        uv[5] = new Vector2(1.0f, 0.66f);
        uv[6] = new Vector2(0.75f, 0.33f);
        uv[7] = new Vector2(1.0f, 0.33f);

        uv[8] = new Vector2(0.5f, 1.0f);
        uv[9] = new Vector2(0.75f, 1.0f);
        uv[10] = new Vector2(0.5f, 0.66f);
        uv[11] = new Vector2(0.75f, 0.66f);

        uv[12] = new Vector2(0.0f, 0.66f);
        uv[13] = new Vector2(0.25f, 0.66f);
        uv[14] = new Vector2(0.0f, 0.33f);
        uv[15] = new Vector2(0.25f, 0.33f);

        uv[16] = new Vector2(0.25f, 1.0f);
        uv[17] = new Vector2(0.5f, 1.0f);
        uv[18] = new Vector2(0.25f, 0.66f);
        uv[19] = new Vector2(0.5f, 0.66f);

        uv[20] = new Vector2(0.25f, 0.0f);
        uv[21] = new Vector2(0.5f, 0.0f);
        uv[22] = new Vector2(0.25f, 0.33f);
        uv[23] = new Vector2(0.5f, 0.33f);

        // Define triangles
        // Front
        triangles[0] = 0;
        triangles[1] = 2;
        triangles[2] = 1;
        triangles[3] = 2;
        triangles[4] = 3;
        triangles[5] = 1;

        // Back
        triangles[6] = 4;
        triangles[7] = 6;
        triangles[8] = 5;
        triangles[9] = 6;
        triangles[10] = 7;
        triangles[11] = 5;

        // Right
        triangles[12] = 8;
        triangles[13] = 10;
        triangles[14] = 9;
        triangles[15] = 10;
        triangles[16] = 11;
        triangles[17] = 9;

        // Left
        triangles[18] = 12;
        triangles[19] = 14;
        triangles[20] = 13;
        triangles[21] = 14;
        triangles[22] = 15;
        triangles[23] = 13;

        // Top
        triangles[24] = 16;
        triangles[25] = 18;
        triangles[26] = 17;
        triangles[27] = 18;
        triangles[28] = 19;
        triangles[29] = 17;

        // Bottom
        triangles[30] = 20;
        triangles[31] = 22;
        triangles[32] = 21;
        triangles[33] = 22;
        triangles[34] = 23;
        triangles[35] = 21;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        filter.mesh = mesh;

        // Assign the Skybox Material to the Renderer
        renderer.material = new Material(Shader.Find("Skybox/6 Sided"));
    }
}