// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParamsInfo.cs" company="OTS">
//   2010
// </copyright>
// <summary>
//   Defines the ParamsInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ETradeCore.Entities
{
    using System;

    using IBM.Data.DB2;

    public struct ParamsInfo
    {
        private Byte Index;  //0...
        public readonly String Name; //"@XXXX"
        public readonly DB2Type Type;
        public readonly int Size; //size of type
        public readonly object Value;  //input value or initialize value

        public ParamsInfo(Byte indexInput, String nameInput, DB2Type typeInput, int sizeInput, object valueInput)
        {
            Index = indexInput;
            Name = nameInput;
            Type = typeInput;
            Size = sizeInput;
            Value = valueInput;
        }
    }
}