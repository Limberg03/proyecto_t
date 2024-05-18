using Newtonsoft.Json;
using opentk.sitios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opentk.utilidades
{
    class ObjectBuilder
    {
        public static face PartJson(string fileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                face? objeto = JsonConvert.DeserializeObject<face>(jsonString);
                Console.WriteLine(objeto);
                if (objeto is null) throw new Exception("objeto nulo");
                return objeto;
            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }  
        }
     


        public static objeto ObjJson(string fileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                objeto? objeto = JsonConvert.DeserializeObject<objeto>(jsonString);
                Console.WriteLine(objeto);
                if (objeto is null) throw new Exception("objeto nulo");
                return objeto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static escenario SceneJson(string fileName)
        {
            try
            {
                string jsonString = File.ReadAllText(fileName);
                escenario? objeto = JsonConvert.DeserializeObject<escenario>(jsonString);
                Console.WriteLine(objeto);
                if (objeto is null) throw new Exception("objeto nulo");
                return objeto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }






























































        public static void ToJson(string fileName, object shape)
        {
            try
            {
                string jsonFile = JsonConvert.SerializeObject(shape);
                File.Create(fileName);
                File.WriteAllText(fileName, jsonFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
