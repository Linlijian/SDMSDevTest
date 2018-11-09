
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.SEC
{
    [Serializable]
    public class SECS01P001DTO : BaseDTO
    {
        public SECS01P001DTO()
        {
            Model = new SECS01P001Model();   // new โมเดล 
        }

        public SECS01P001Model Model { get; set; }   //model
        public List<SECS01P001Model> Models { get; set; }  //list 
    }

    public class SECS01P001ExecuteType : DTOExecuteType
    {
        public const string GetComLicenseID = "GetComLicenseID";
    }
}