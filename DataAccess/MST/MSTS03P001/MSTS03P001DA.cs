using System;
using System.Data;
using System.Globalization;
using System.Linq;
using UtilityLib;

namespace DataAccess.MST
{
    public class MSTS03P001DA : BaseDA
    {
        public MSTS03P001DTO DTO { get; set; }

        #region ====Property========
        public MSTS03P001DA()
        {
            DTO = new MSTS03P001DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MSTS03P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MSTS03P001ExecuteType.GetAll: return GetAll(dto);
                case MSTS03P001ExecuteType.GetByID: return GetByID(dto);
            }
            return dto;
        }

        private MSTS03P001DTO GetAll(MSTS03P001DTO dto)
        {
            
            return dto;
        }
        private MSTS03P001DTO GetByID(MSTS03P001DTO dto)
        {
            
            return dto;
        }
        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MSTS03P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MSTS03P001ExecuteType.Insert: return Insert(dto);
                
            }
            return dto;
        }
        private MSTS03P001DTO Insert(MSTS03P001DTO dto)
        {
           
            return dto;
        }
        #endregion

        #region ====Update==========
        
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MSTS03P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MSTS03P001ExecuteType.Update: return Update(dto);
            }
            return dto;
        }
        private MSTS03P001DTO Update(MSTS03P001DTO dto)
        {
           
            return dto;
        }
        #endregion

        #region ====Delete==========
        protected override BaseDTO DoDelete(BaseDTO baseDTO)
        {
            var dto = (MSTS03P001DTO)baseDTO;
            if (dto.Models.Count() > 0)
            {
                foreach (var item in dto.Models)
                {
                    
                }
            }

            return dto;
        }
        #endregion
    }
}