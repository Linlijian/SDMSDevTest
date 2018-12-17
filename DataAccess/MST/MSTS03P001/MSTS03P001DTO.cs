
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.MST
{
    [Serializable]
    public class MSTS03P001DTO : BaseDTO
    {
        public MSTS03P001DTO()
        {
            Model = new MSTS03P001Model();   // new โมเดล 
        }

        public MSTS03P001Model Model { get; set; }   //model
        public List<MSTS03P001Model> Models { get; set; }  //list 
    }

    public class MSTS03P001ExecuteType : DTOExecuteType
    {
        public const string GetDetailByID = "GetDetailByID";
        public const string Insert = "Insert";
        public const string CallSPInsertExcel = "CallSPInsertExcel";
        public const string Confirm = "Confirm";
        public const string Update = "Update";
        public const string ValidateExl = "ValidateExl";
        public const string GetExl = "GetExl";


    }
}