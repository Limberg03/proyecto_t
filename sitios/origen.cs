using Newtonsoft.Json;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace opentk.sitios
{
    public class origen
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        [JsonConstructor]
        public origen(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public Vector3 vector3()
        {
            return new Vector3(X, Y, Z);
        }

        public static origen Vector3ToPoint(Vector3 vector3)
        {
            return new origen(vector3.X, vector3.Y, vector3.Z);
        }
    }
}
