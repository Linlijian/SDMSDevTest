
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.MIS
{
    [Serializable]
    public class MISS01P002DTO : BaseDTO
    {
        public MISS01P002DTO()
        {
            Model = new MISS01P002Model();   // new โมเดล 
        }

        public MISS01P002Model Model { get; set; }   //model
        public List<MISS01P002Model> Models { get; set; }  //list 
    }

    public class MISS01P002ExecuteType : DTOExecuteType
    {

        public const string UpdateAssignment = "UpdateAssignment";
        public const string UpdateFilePacket = "UpdateFilePacket";
        public const string GetAllAssign = "GetAllAssign";
        public const string GetFilePacket = "GetFilePacket";
        public const string Update = "Update";
        public const string Insert = "Insert";
        public const string GetAssignment = "GetAssignment";
    }
}