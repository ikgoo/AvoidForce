using System;
using TWOPROLIB.ScriptableObjects.EntitysAttributes;

namespace TWOPROLIB.Script.EntitysAttributes
{
    [Serializable]
    public class LiveEntityAttributes
    {
        /// <summary>
        /// 속성
        /// </summary>
        public Attributes attributes;

        /// <summary>
        /// 수량
        /// </summary>
        public int amount;

        public  LiveEntityAttributes(Attributes attributes, int amount)
        {
            this.attributes = attributes;
            this.amount = amount;
        }
        
    }
}
