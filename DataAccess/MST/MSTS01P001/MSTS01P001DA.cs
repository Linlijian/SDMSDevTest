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

            string strSQL1 = @"INSERT INTO [dbo].[VSMS_MANDAY]
                                       ([COM_CODE]
                                        ,[ISSUE_TYPE]
                                        ,[TYPE_RATE]
                                        ,[MAN_PLM_SA]
                                        ,[MAN_PLM_QA]
                                        ,[MAN_PLM_PRG]
                                        ,[CRET_BY]
                                        ,[CRET_DATE]
                                        ,[MNT_BY]
                                        ,[MNT_DATE])
                            VALUES
                                        (@COM_CODE
                                        ,@ISSUE_TYPE
                                        ,@TYPE_RATE
                                        ,@MAN_PLM_SA
                                        ,@MAN_PLM_QA
                                        ,@MAN_PLM_PRG
                                        ,@CRET_BY
                                        ,@CRET_DATE
                                        ,@MNT_BY
                                        ,@MNT_DATE)";

            var parameters1 = CreateParameter();
            parameters1.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters1.AddParameter("ISSUE_TYPE", dto.Model.ISSUE_TYPE);
            parameters1.AddParameter("TYPE_RATE", dto.Model.TYPE_RATE);
            parameters1.AddParameter("MAN_PLM_SA", dto.Model.MAN_PLM_SA);
            parameters1.AddParameter("MAN_PLM_QA", dto.Model.MAN_PLM_QA);
            parameters1.AddParameter("MAN_PLM_PRG", dto.Model.MAN_PLM_PRG);
            parameters1.AddParameter("CRET_BY", dto.Model.CRET_BY);
            parameters1.AddParameter("CRET_DATE", dto.Model.CRET_DATE);
            parameters1.AddParameter("MNT_BY", dto.Model.MNT_BY);
            parameters1.AddParameter("MNT_DATE", dto.Model.MNT_DATE);

            var result = _DBMangerNoEF.ExecuteNonQuery(strSQL1, parameters1, CommandType.Text);
            if (!result.Success(dto))
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = result.ErrorMessage;
            }

            return dto;
        }
        #endregion

        #region ====Update==========

        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MSTS01P001DTO)baseDTO;
 
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