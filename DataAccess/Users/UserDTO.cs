﻿using System;
using System.Collections.Generic;
using UtilityLib;
using DataAccess.SEC;

namespace DataAccess.Users
{
    [Serializable]
    public class UserDTO : BaseDTO
    {
        public UserDTO()
        {
            Model = new UserModel();
        }
        public UserModel Model { get; set; }
        public List<ModuleModel> ConfigGerarals { get; set; }
        public List<AppModel> Apps { get; set; }

    }

    public class UserExecuteType : DTOExecuteType
    {
        public const string GetUser = "GetUser"; 
        public const string GetConfigGeraral = "GetConfigGeraral";
        public const string GetConfigSys = "GetConfigSys";
        public const string GetApp = "GetApp";
    }


    //UserDA dal = new UserDA();
    //UserDTO dto = new UserDTO();
    //dto.Query.ExecuteType = UserExecuteType.GetByInRelateID;
    //        dal.Select(dto);
}