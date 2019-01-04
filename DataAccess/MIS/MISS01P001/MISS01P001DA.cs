using System;
using System.Data;
using System.Globalization;
using System.Linq;
using UtilityLib;

namespace DataAccess.MIS
{
    public class MISS01P001DA : BaseDA
    {
        public MISS01P001DTO DTO { get; set; }

        #region ====Property========
        public MISS01P001DA()
        {
            DTO = new MISS01P001DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MISS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P001ExecuteType.GetAll: return GetAll(dto);
                case MISS01P001ExecuteType.GetByID: return GetByID(dto);

            }
            return dto;
        }
        private MISS01P001DTO GetAll(MISS01P001DTO dto)
        {
            
            return dto;
        }
        private MISS01P001DTO GetByID(MISS01P001DTO dto)
        {

            return dto;
        }
        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MISS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P001ExecuteType.Insert: return Insert(dto);
            }
            return dto;
        }
        private MISS01P001DTO Insert(MISS01P001DTO dto)
        {
            
            return dto;
        }
        #endregion

        #region ====Update==========
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MISS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P001ExecuteType.Update: return Update(dto);
            }
            return dto;
        }
        private MISS01P001DTO Update(MISS01P001DTO dto)
        {
            
            return dto;
        }
       
        #endregion

        #region ====Delete==========
        protected override BaseDTO DoDelete(BaseDTO baseDTO)
        {
            var dto = (MISS01P001DTO)baseDTO;
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