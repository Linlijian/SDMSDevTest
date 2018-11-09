
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccess.SEC
{
    [Serializable]
    public class SECS02P002DTO : BaseDTO
    {
        public SECS02P002DTO()
        {
            Model = new SECS02P002Model();
        }

        public SECS02P002Model Model { get; set; }
        public List<SECS02P002Model> Models { get; set; }
    }

    public class SECS02P002ExecuteType : DTOExecuteType
    {
        public const string GetQuerySearchAll = "GetQuerySearchAll";
        public const string GetQueryCheckUserAdmin = "GetQueryCheckUserAdmin";
        public const string GetUserCOM = "GetUserCOM";
    }
}