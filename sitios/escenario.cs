using Newtonsoft.Json;
using opentk.utilidades;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace opentk.sitios
{
    public class escenario : IDibujar
    {
        Dictionary<string, objeto> ConjObjetos { get; set; }


        //guarda la posicion inicial del centro del objeto
        private Vector3 _origen = Vector3.Zero;
        private Vector3 _posicion = Vector3.Zero;
        public Vector3 Origen { get => _origen; set => _origen = value; } //
        public Vector3 Posicion { get => _posicion; set => _posicion = value; }

        [JsonConstructor]
        public escenario(Dictionary<string, objeto> objects)
        {
            ConjObjetos = objects;
        }

        public escenario() { ConjObjetos = new(); }


        public static escenario LoadScene(string fileName)
        {
            return ObjectBuilder.SceneJson(fileName);
        }


        public void OnLoad()
        {
            foreach (var item in ConjObjetos)
            {
                item.Value.LoadParts();
            }
        }
    

        public objeto getObjeto(string clave)
        {
            return ConjObjetos[clave];
        }


        public void SetMatrix(Matrix4 ViewMatrix)
        {
            foreach (objeto item in ConjObjetos.Values)
            {
                item.SetMatrix(ViewMatrix);
            }
        }

      

        public void dibujar()
        {
            foreach (var item in ConjObjetos)
            {
                item.Value.dibujar();
            }
        }


        /*--------------- TRANSFORMACIONES --------------*/




        public void Escalacion(float size)
        {
            foreach (var item  in ConjObjetos)
            {
                item.Value.Escalar(size, _origen);
            }
        }

        public void Traslacion(float x, float y, float z)
        {
            foreach (var item in ConjObjetos)
            {
                item.Value.Traslacion(x, y, z);
            }

        }

        public void Rotacion(float x, float y, float z)
        {
            foreach (var item in ConjObjetos)
            {
                item.Value.Rotacion(x, y, z, _origen);
            }
        }

        //public void Traslacion(Vector3 direccion)
        //{
        //    foreach (var item in ConjObjetos)
        //    {
        //        item.Value.Traslacion(direccion-this.Origen);
        //    }
        //}

        //public void Escalar(Vector3 factor)
        //{
        //    foreach (var item in ConjObjetos)
        //    {
        //        item.Value.Escalar(factor);
        //    }
        //}

        //public void Rotate(float x,float y,float z)
        //{
        //    foreach (var item in ConjObjetos)
        //    {
        //        item.Value.Rotate(x, y, z);
        //    }
        //}

     /*   public void RotateY(float y)
        {
            foreach (var item in ConjObjetos)
            {
                item.Value.RotateY(y);
            }
        }

        public void RotateZ(float z)
        {
            foreach (var item in ConjObjetos)
            {
                item.Value.RotateZ(z);
            }
        }*/


    }
}
