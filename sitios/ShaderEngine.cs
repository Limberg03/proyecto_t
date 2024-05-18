using System;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace opentk.sitios
{
    class ShaderEngine
    {
        //atributos

        private int Id;
        private bool DisposedValue = false;

        //constructor

        public ShaderEngine()
        {
            string vertexPath = "../../../Recursos/Shaders/VertexShader.glsl";
            string fragmentPath = "../../../Recursos/Shaders/FragmentShader.glsl";

            Init(vertexPath, fragmentPath);
        }
        public ShaderEngine(string vertexPath, string fragmentPath)
        {
            Init(vertexPath, fragmentPath);
        }


        private void Init(string vertexPath, string fragmentPath)
        {
            int vertexShader, fragmentShader; //el id de cada shader

            //extraemos el codigo fuente de cada shader respectivamente
            string vertexShaderSource;
            using (StreamReader reader = new StreamReader(vertexPath))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            string fragmentShaderSource;
            using (StreamReader reader = new StreamReader(fragmentPath))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }

            //compilamos cada shader
            vertexShader = CompileShader(ShaderType.VertexShader, vertexShaderSource);
            fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);

            //creamos el programa y unimos los dos shaders
            Id = GL.CreateProgram();

            GL.AttachShader(Id, vertexShader);
            GL.AttachShader(Id, fragmentShader);
            GL.LinkProgram(Id);

            //limpiamos
            GL.DetachShader(Id, vertexShader);
            GL.DetachShader(Id, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        private int CompileShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);  //generamos 
            GL.ShaderSource(shader, source); //unimos el shader generado al codigo fuente
            GL.CompileShader(shader); //compilamos

            //si hay algun error se verá en consola para debugging
            string infoLogVert = GL.GetShaderInfoLog(shader);
            if (infoLogVert != string.Empty)
                Console.WriteLine(infoLogVert);

            return shader;
        }


        public void SetUniformColor4(string name, Color4 color)
        {
            GL.Uniform4(GetUniformLocation(name), color);
        }

        public void SetUniformMatrix4(string name, Matrix4 matrix)
        {
            GL.UniformMatrix4(GetUniformLocation(name), false, ref matrix);
        }


        private int GetUniformLocation(string name)
        {
            int location = GL.GetUniformLocation(Id, name);
            if (location == -1)
                Console.WriteLine("Warning: uniform " + name + " - no existe!");
            return location;
        }


        public void Use()
        {
            GL.UseProgram(Id);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!DisposedValue)
            {
                GL.DeleteProgram(Id);

                DisposedValue = true;
            }
        }

        ~ShaderEngine()
        {
            GL.DeleteProgram(Id);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


    }
}
