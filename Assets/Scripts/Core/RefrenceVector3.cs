using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemezawyDev
{
    public class RefrenceVector3
    {
        public float x;
        public float y;
        public float z;

        public RefrenceVector3()
        {}
        public RefrenceVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3 GetVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
