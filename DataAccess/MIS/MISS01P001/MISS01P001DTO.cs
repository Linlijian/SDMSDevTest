
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.MIS
{
    [Serializable]
    public class MISS01P001DTO : BaseDTO
    {
        public MISS01P001DTO()
        {
            Model = new MISS01P001Model();   // new โมเดล 
        }

        public MISS01P001Model Model { get; set; }   //model
        public List<MISS01P001Model> Models { get; set; }  //list 
    }

    public class MISS01P001ExecuteType : DTOExecuteType
    {
        public const string GetNo = "GetNo";
        public const string Insert = "Insert";
        public const string GetAllAssign = "GetAllAssign";
        public const string Confirm = "Confirm";
        public const string Update = "Update";
        public const string ValidateExl = "ValidateExl";
        public const string GetExl = "GetExl";


    }
}