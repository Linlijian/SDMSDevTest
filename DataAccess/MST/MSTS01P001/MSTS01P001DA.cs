using System;
using System.Data;
using System.Globalization;
using System.Linq;
using UtilityLib;

namespace DataAccess.MST
{
    public class MSTS01P001DA : BaseDA
    {
        public MSTS01P001DTO DTO { get; set; }

        #region ====Property========
        public MSTS01P001DA()
        {
            DTO = new MSTS01P001DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MSTS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MSTS01P001ExecuteType.GetAll: return GetAll(dto);
                case MSTS01P001ExecuteType.GetByID: return GetByID(dto);
            }
            return dto;
        }

        private MSTS01P001DTO GetAll(MSTS01P001DTO dto)
        {

            return dto;
        }
        private MSTS01P001DTO GetByID(MSTS01P001DTO dto)
        {

            return dto;
        }
        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MSTS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MSTS01P001ExecuteType.Insert: return Insert(dto);

            }
            return dto;
        }
        private MSTS01P001DTO Insert(MSTS01P001DTO dto)
        {

            return dto;
        }
        #endregion

        #region ====Update==========

        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MSTS01P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MSTS01P001ExecuteType.Update: return Update(dto);
            }
            return dto;
        }
        private MSTS01P001DTO Update(MSTS01P001DTO dto)
        {

            return dto;
        }
        #endregion

        #region ====Delete==========
        protected override BaseDTO DoDelete(BaseDTO baseDTO)
        {
            var dto = (MSTS01P001DTO)baseDTO;
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