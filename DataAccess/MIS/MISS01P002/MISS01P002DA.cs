using System;
using System.Data;
using System.Globalization;
using System.Linq;
using UtilityLib;

namespace DataAccess.MIS
{
    public class MISS01P002DA : BaseDA
    {
        public MISS01P002DTO DTO { get; set; }

        #region ====Property========
        public MISS01P002DA()
        {
            DTO = new MISS01P002DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MISS01P002DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P002ExecuteType.GetAll: return GetAll(dto);
                case MISS01P002ExecuteType.GetByID: return GetByID(dto);
            }
            return dto;
        }
        private MISS01P002DTO GetByID(MISS01P002DTO dto)
        {
            
            return dto;
        }
        private MISS01P002DTO GetAll(MISS01P002DTO dto)
        {
            

            return dto;
        }
        
        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MISS01P002DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P002ExecuteType.Insert: return Insert(dto);
            }
            return dto;
        }
        private MISS01P002DTO Insert(MISS01P002DTO dto)
        {
            
            return dto;
        }
        #endregion

        #region ====Update==========
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MISS01P002DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS01P002ExecuteType.Update: return Update(dto);
            }
            return dto;
        }
        private MISS01P002DTO Update(MISS01P002DTO dto)
        {
            
            return dto;
        }
        #endregion

        #region ====Delete==========
        protected override BaseDTO DoDelete(BaseDTO baseDTO)
        {
            var dto = (MISS02P001DTO)baseDTO;
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