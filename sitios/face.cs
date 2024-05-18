using Newtonsoft.Json;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using opentk.sitios;

using opentk.utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace opentk.sitios
{
    public class face : IDibujar
    {

        Vector3[] Vertices = Array.Empty<Vector3>();
        uint[] Indices;
        Color4 Color;
        public float width { get; set; }
        public float height { get; set; }
        public float depth { get; set; }


        protected Matrix4 ModelMatrix;
        protected Matrix4 MVPMatrix;
        protected Matrix4 ViewProjectionMatrix;

        protected Matrix4 Rotations;
        protected Matrix4 Translations;
        protected Matrix4 Scales;

        int VertexBufferObject;
        int VertexArrayObject;
        int IndexBufferObject;
        ShaderEngine Shader;

        //guarda la posicion inicial del centro del objeto
        private Vector3 _origen = Vector3.Zero;
        private Vector3 _posicion = Vector3.Zero;
        public Vector3 Origen { get => _origen; set => _origen = value; } //
        public Vector3 Posicion { get => _posicion; set => _posicion = value; }

        //constantes
        readonly Color4 DefaultColor = new Color4(142, 138, 125, 255);

        [JsonConstructor]
        public face(float width, float height, float depth, float[] vertices, uint[] indices, origen origen, Color4? color)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
            ModelMatrix = Matrix4.Identity;
            ViewProjectionMatrix = Matrix4.Identity;
            MVPMatrix = ModelMatrix;
            SetOrigin(origen);
            Load(vertices, indices, color ?? DefaultColor);
        }



        private void SetOrigin(origen origen)
        {
            if (origen != null) Origen = origen.vector3();
            else Origen = Vector3.Zero;

        }


        public void SetPosition(Vector3 initialPosition)
        {
            Posicion = initialPosition;

        }

        private void SetVertices(Vector3 position)
        {
            List<Vector3> vertexlist = new();
            foreach (Vector3 vertex in Vertices)
            {
                Vector3 newPosition = vertex + position;
                vertexlist.Add(newPosition);
            }
            Vertices = vertexlist.ToArray();
        }


        private void Load(float[] vertexArray, uint[] indices, Color4 color)
        {
            Vertices = Converter.ParseToVector3Array(vertexArray);
            Indices = indices;
            Color = color;
        }

        internal void Init(Vector3 PosInicial)
        {
            SetPosition(PosInicial);
            SetVertices(Origen + Posicion);
            VertexArrayObject = GL.GenVertexArray(); //generamos el VAO
            VertexBufferObject = GL.GenBuffer(); //generamos el VBO
            IndexBufferObject = GL.GenBuffer(); //generamos el IBO

            GL.BindVertexArray(VertexArrayObject); //habilitamos el VAO 

            //enlazamos el VBO con un buffer de openGL y lo inicializamos
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, GetLength(Vertices) * sizeof(float), Vertices, BufferUsageHint.StaticDraw);

            //enlazamos el IBO con un buffer de openGL y lo inicializamos
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, Indices.Length * sizeof(uint), Indices, BufferUsageHint.StaticDraw);

            //configuramos los atributos del vertexbuffer y lo habilitamos (el primer 0 indica el location en el vertexShader)
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, sizeof(float) * 3, 0);

            SetShaders();
        }


        //metodos estáticos
        private static int GetLength(Vector3[] array)
        {
            return array.Length * 3;
        }


        //metodos principales

        public void SetShaders()
        {
            CShader();
            Shader.SetUniformColor4("u_Color", Color);
        }

        // TODO:
        // remove ViewProjection Matrxi from this class to window
        public void SetViewProjectionMatrix(Matrix4 viewProjectionMatrix)
        {
            ViewProjectionMatrix = viewProjectionMatrix;
            CalculateMvpMatrix();
        }

        protected void CalculateMvpMatrix()
        {
            MVPMatrix = ModelMatrix * ViewProjectionMatrix;
        }

        private void BindMatrix()
        {
            Shader.SetUniformMatrix4("mvp", MVPMatrix);
        }


        public void dibujar(Vector3 origen)
        {                  

            CalculateMvpMatrix();
                                                                                                                               //habtd
            Shader.Use();
            BindMatrix();
            GL.BindVertexArray(VertexArrayObject);

            //Dibujamos
            GL.DrawElements(PrimitiveType.Triangles, Indices.Length, DrawElementsType.UnsignedInt, 0);

                                       
                foreach (Vector3 vertex in Vertices)
                {
                    Vector4 v4 = new Vector4(origen.X + (vertex.X * width), origen.Y + (vertex.Y * height), origen.Z + (vertex.Z * depth), 1);
                    Vector4 res = v4 * ModelMatrix;
                    //v4 = Vector4.Transform(v4, matrixMain);
                    GL.Vertex4(res);
                }
                GL.End();            

        }       

        private void CShader()
        {
            //creamos y usamos el program shader
            Shader = new ShaderEngine();
            Shader.Use();
        }

        /*--------------- TRANSFORMACIONES --------------*/

        public void Traslacion(float x, float y, float z)
        {
            //direction = direction + Posicion;
            Translations = Matrix4.CreateTranslation(x,y,z);
            ModelMatrix = ModelMatrix * Translations;
        }

        public void Escalar(float c, Vector3 origen)
        {
            Matrix4 mTraslateOrigin = Matrix4.CreateTranslation(-origen);
            Matrix4 mScale = Matrix4.CreateScale(c, c, c);
            Matrix4 mTraslate = Matrix4.CreateTranslation(origen);
            ModelMatrix = mTraslateOrigin * mScale * mTraslate * ModelMatrix;


           // Scales = Matrix4.CreateScale(factor);
            //ModelMatrix = ModelMatrix * Scales;
        }

        public void Rotacion(float x, float y, float z, Vector3 origen)
        {

            Matrix4 mTraslateOrigin = Matrix4.CreateTranslation(-origen);
            Matrix4 mRotate = new Matrix4();
            if (x != 0)
            {
                mRotate = Matrix4.CreateRotationX(x);
            }
            else
          if (y != 0)
            {
                mRotate = Matrix4.CreateRotationY(y);
            }
            else
          if (z != 0)
            {
                mRotate = Matrix4.CreateRotationZ(z);
            }
            Matrix4 mTraslate = Matrix4.CreateTranslation(origen);
            ModelMatrix = mTraslateOrigin * mRotate * mTraslate * ModelMatrix;
        }


        /*public void RotateY(float angle)
        {
            Matrix4 rotateY = Matrix4.Rotate(this.Origen, angle);

            Rotations = Matrix4.CreateRotationY(angle);
            ModelMatrix = ModelMatrix * Rotations;
        }

        public void RotateZ(float angle)
        {
            Matrix4 rotateZ = Matrix4.Rotate(this.Origen, angle);

            Rotations = Matrix4.CreateRotationZ(angle);
            ModelMatrix = ModelMatrix * Rotations;
        }*/


    }
}
