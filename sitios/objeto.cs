using Newtonsoft.Json;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace opentk.sitios
{
    public class objeto : IDibujar
    {

        Dictionary<string, face> conjFace { get; set; }

        public float width { get; set; }
        public float height { get; set; }
        public float depth { get; set; }


        //guarda la posicion inicial del centro del objeto
        private Vector3 _origen = Vector3.Zero;
        private Vector3 _posicion = Vector3.Zero;
        public Vector3 Origen { get => _origen; set => _origen = value; } //
        public Vector3 Posicion { get => _posicion; set => _posicion = value; }


        [JsonConstructor]
        public objeto(float width, float height, float depth, origen posicion, Dictionary<string, face> partes)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;
            Origen = posicion.vector3();
            Posicion = Origen;
            conjFace = partes;
        }

        public face getFace(string clave)
        {
            return conjFace[clave];
        }


       

        public void SetPos(origen posicion)
        {
            Origen = posicion.vector3();
            Posicion = Origen;
            LoadParts();
        }


        public void LoadParts()
        {
            foreach (var item in conjFace)
            {
                item.Value.Init(Origen);
            }
        }

        public void SetMatrix(Matrix4 ViewMatrix)
        {
            foreach (face item in conjFace.Values)
            {
                item.SetViewProjectionMatrix(ViewMatrix);
            }
        }

        public void dibujar()
        {
            foreach (var item in conjFace)
            {
                item.Value.dibujar(_origen);
            }
        }






        /*--------------- TRANSFORMACIONES --------------*/
       

        public void Escalar(float size, Vector3 origen)
        {
            foreach (var item in conjFace)
            {
                item.Value.Escalar(size, origen);
            }
        }

        public void Traslacion(float x, float y, float z)
        {
            foreach (var cara in conjFace)
            {
                cara.Value.Traslacion(x, y, z);
            }
        }

        public void Rotacion(float x, float y, float z, Vector3 c)
        {
            foreach (var cara in conjFace)
            {
                cara.Value.Rotacion(x, y, z, c);
            }
        }



        /*     public void RotateY(float y)
             {
                 foreach (var item in conjFace)
                 {
                     item.Value.RotateY(y);
                 }
             }

             public void RotateZ(float z)
             {
                 foreach (var item in conjFace)
                 {
                     item.Value.RotateZ(z);
                 }
             }*/

    }
}
