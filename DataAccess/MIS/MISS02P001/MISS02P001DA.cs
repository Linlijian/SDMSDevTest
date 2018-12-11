﻿using System.Data;
using System.Linq;
using UtilityLib;

namespace DataAccess.MIS
{
    public class MISS02P001DA : BaseDA
    {
        public MISS02P001DTO DTO { get; set; }

        #region ====Property========
        public MISS02P001DA()
        {
            DTO = new MISS02P001DTO();
        }

        #endregion

        #region ====Select==========
        protected override BaseDTO DoSelect(BaseDTO baseDTO)
        {
            var dto = (MISS02P001DTO)baseDTO;
            switch (dto.Execute.ExecuteType)
            {
                case MISS02P001ExecuteType.GetAll: return GetAll(dto);
                case MISS02P001ExecuteType.GetByID: return GetByID(dto);
                case MISS02P001ExecuteType.GetDetailByID: return GetDetailByID(dto);

            }
            return dto;
        }

        private MISS02P001DTO GetAll(MISS02P001DTO dto)
        {
            string strSQL;
            var parameters = CreateParameter();

            if (dto.Model.YEAR != null)
            {
                 strSQL = @"  SELECT YEAR, 
                                      SUM(CASE TYPE_DAY WHEN 'W'      THEN 1 ELSE 0 END) as ALL_SATURDAY_W ,
                                      SUM(CASE TYPE_DAY WHEN 'H'      THEN 1 ELSE 0 END) as ALL_HOLIDAY,
                                      SUM(CASE TYPE_DAY WHEN 'S'      THEN 1 ELSE 0 END) as ALL_SPP,
                                      SUM(CASE TYPE_DAY WHEN 'I'      THEN 1 ELSE 0 END) as ALL_DEPLOYMENT_IT,
                                      SUM(CASE TYPE_DAY WHEN 'D'      THEN 1 ELSE 0 END) as ALL_DEPLOYMENT_DATE
                                FROM [SDDB].[dbo].[VSMS_DEPLOY]
                                WHERE (1=1) AND YEAR = @YEAR
                                GROUP BY YEAR";
                parameters.AddParameter("YEAR", dto.Model.YEAR);
            }
            else
            {
                strSQL = @"  SELECT YEAR, 
                                      SUM(CASE TYPE_DAY WHEN 'W'      THEN 1 ELSE 0 END) as ALL_SATURDAY_W ,
                                      SUM(CASE TYPE_DAY WHEN 'H'      THEN 1 ELSE 0 END) as ALL_HOLIDAY,
                                      SUM(CASE TYPE_DAY WHEN 'S'      THEN 1 ELSE 0 END) as ALL_SPP,
                                      SUM(CASE TYPE_DAY WHEN 'I'      THEN 1 ELSE 0 END) as ALL_DEPLOYMENT_IT,
                                      SUM(CASE TYPE_DAY WHEN 'D'      THEN 1 ELSE 0 END) as ALL_DEPLOYMENT_DATE
                                FROM [SDDB].[dbo].[VSMS_DEPLOY]
                                WHERE (1=1)
                                GROUP BY YEAR";
            }

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS02P001Model>();
            }

            return dto;
        }

        private MISS02P001DTO GetByID(MISS02P001DTO dto)
        {
            string strSQL = @"SELECT * FROM [dbo].[VSMS_DEPLOY] WHERE (1=1)
                                            AND YEAR = @YEAR
                                            AND COM_CODE = @COM_CODE
                                       ORDER BY DAY ,MONTH,YEAR,TYPE_DAY DESC";
            var parameters = CreateParameter();

            parameters.AddParameter("YEAR", dto.Model.YEAR);
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Models = result.OutputDataSet.Tables[0].ToList<MISS02P001Model>();
            }

            return dto;
        }

        private MISS02P001DTO GetDetailByID(MISS02P001DTO dto)
        {
            string strSQL = @"SELECT * FROM [dbo].[VSMS_DEPLOY] WHERE (1=1)
                                            AND YEAR = @YEAR
                                            AND COM_CODE = @COM_CODE";
            var parameters = CreateParameter();

            parameters.AddParameter("YEAR", dto.Model.YEAR);
            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            if (result.Success(dto))
            {
                dto.Model.Details = result.OutputDataSet.Tables[0].ToList<MISS02P001DetailPModel>();
            }

            return dto;
        }

        #endregion

        #region ====Insert==========
        protected override BaseDTO DoInsert(BaseDTO baseDTO)
        {
            var dto = (MISS02P001DTO)baseDTO;
            if (dto.Model.Details.Count() > 0)
            {
                foreach (var item in dto.Model.Details)
                {
                    string strSQL1 = @"INSERT INTO [dbo].[VSMS_DEPLOY]
                                       ([COM_CODE]
                                       ,[DAY]
                                       ,[MONTH]
                                       ,[YEAR]
                                       ,[DEPLOYMENT_DATE]
                                       ,[TYPE_DAY]
                                       ,[CRET_BY]
                                       ,[CRET_DATE]
                                       ,[MNT_BY]
                                       ,[MNT_DATE])
                            VALUES
                                        (@COM_CODE 
                                        ,@DAY 
                                        ,@MONTH 
                                        ,@YEAR 
                                        ,@DEPLOYMENT_DATE 
                                        ,@TYPE_DAY
                                        ,@CRET_BY 
                                        ,@CRET_DATE 
                                        ,@MNT_BY 
                                        ,@MNT_DATE)";

                    var parameters1 = CreateParameter();

                    parameters1.AddParameter("COM_CODE", dto.Model.COM_CODE);
                    parameters1.AddParameter("DAY", item.DAY);
                    parameters1.AddParameter("MONTH", item.MONTH);
                    parameters1.AddParameter("YEAR", item.YEAR);
                    parameters1.AddParameter("DEPLOYMENT_DATE", item.DEPLOYMENT_DATE);
                    parameters1.AddParameter("TYPE_DAY", item.TYPE_DAY);
                    parameters1.AddParameter("CRET_BY", dto.Model.CRET_BY);
                    parameters1.AddParameter("CRET_DATE", dto.Model.CRET_DATE);
                    parameters1.AddParameter("MNT_BY", dto.Model.MNT_BY);
                    parameters1.AddParameter("MNT_DATE", dto.Model.MNT_DATE);


                    var result = _DBMangerNoEF.ExecuteNonQuery(strSQL1, parameters1, CommandType.Text);
                    if (!result.Success(dto))
                    {
                        dto.Result.IsResult = false;
                        dto.Result.ResultMsg = result.ErrorMessage;
                        break;
                    }
                }
            }
            else
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = "No data to save";
            }
            return dto;
        }
        private MISS02P001DTO InsertTemp(MISS02P001DTO dto)
        {
            var parameters1 = CreateParameter();

            parameters1.AddParameter("error_code", null, ParameterDirection.Output);
            parameters1.AddParameter("COM_CODE", dto.Model.COM_CODE);
            parameters1.AddParameter("YEAR", dto.Model.YEAR);

            var result = _DBMangerNoEF.ExecuteDataSet("dbo.SP_VSMS_DEPLOY", parameters1);

            if (!result.Status)
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = result.ErrorMessage;
            }

            return dto;
        }
        #endregion

        #region ====Update==========
        //protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        //{
        //    var dto = (MISS02P001DTO)baseDTO;
        //    if (dto.Model.Details.Count() > 0)
        //    {
        //        foreach (var item in dto.Model.Details)
        //        {
        //            if (isInsert(item, dto.Model.COM_CODE))
        //            {
        //                #region ====Insert==========
        //                string strSQL = @"INSERT INTO [dbo].[VSMS_DEPLOY]
        //                               ([COM_CODE]
        //                               ,[DAY]
        //                               ,[MONTH]
        //                               ,[YEAR]
        //                               ,[DEPLOYMENT_DATE]
        //                               ,[TYPE_DAY]
        //                               ,[CRET_BY]
        //                               ,[CRET_DATE]
        //                               ,[MNT_BY]
        //                               ,[MNT_DATE])
        //                    VALUES
        //                                (@COM_CODE 
        //                                ,@DAY 
        //                                ,@MONTH 
        //                                ,@YEAR 
        //                                ,@DEPLOYMENT_DATE 
        //                                ,@TYPE_DAY
        //                                ,@CRET_BY 
        //                                ,@CRET_DATE 
        //                                ,@MNT_BY 
        //                                ,@MNT_DATE)";

        //                var parameters = CreateParameter();

        //                parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
        //                parameters.AddParameter("DAY", item.DAY);
        //                parameters.AddParameter("MONTH", item.MONTH);
        //                parameters.AddParameter("YEAR", item.YEAR);
        //                parameters.AddParameter("DEPLOYMENT_DATE", item.DEPLOYMENT_DATE);
        //                parameters.AddParameter("TYPE_DAY", item.TYPE_DAY);
        //                parameters.AddParameter("CRET_BY", dto.Model.CRET_BY);
        //                parameters.AddParameter("CRET_DATE", dto.Model.CRET_DATE);
        //                parameters.AddParameter("MNT_BY", dto.Model.MNT_BY);
        //                parameters.AddParameter("MNT_DATE", dto.Model.MNT_DATE);


        //                var result1 = _DBMangerNoEF.ExecuteNonQuery(strSQL, parameters, CommandType.Text);
        //                if (!result1.Success(dto))
        //                {
        //                    dto.Result.IsResult = false;
        //                    dto.Result.ResultMsg = result1.ErrorMessage;
        //                    break;
        //                }
        //                #endregion
        //            }
        //            else
        //            {
        //                #region ====Update==========
        //                string strSQL1 = @"UPDATE [dbo].[VSMS_DEPLOY]
        //                                SET DAY = @DAY,
        //                                    MONTH = @MONTH,
        //                                    DEPLOYMENT_DATE = @DEPLOYMENT_DATE,
        //                                    TYPE_DAY = @TYPE_DAY,
        //                                    MNT_BY = @MNT_BY,
        //                                    MNT_DATE = @MNT_DATE
        //                                WHERE COM_CODE = @COM_CODE 
        //                                    AND YEAR = @YEAR
        //                                    AND DEPLOYMENT_DATE = @DEPLOYMENT_DATE";

        //                var parameters1 = CreateParameter();

        //                parameters1.AddParameter("COM_CODE", dto.Model.COM_CODE);
        //                parameters1.AddParameter("DAY", item.DAY);
        //                parameters1.AddParameter("MONTH", item.MONTH);
        //                parameters1.AddParameter("YEAR", item.YEAR);
        //                parameters1.AddParameter("DEPLOYMENT_DATE", item.DEPLOYMENT_DATE);
        //                parameters1.AddParameter("TYPE_DAY", item.TYPE_DAY);
        //                parameters1.AddParameter("MNT_BY", dto.Model.MNT_BY);
        //                parameters1.AddParameter("MNT_DATE", dto.Model.MNT_DATE);


        //                var result = _DBMangerNoEF.ExecuteNonQuery(strSQL1, parameters1, CommandType.Text);
        //                if (!result.Success(dto))
        //                {
        //                    dto.Result.IsResult = false;
        //                    dto.Result.ResultMsg = result.ErrorMessage;
        //                    break;
        //                }
        //                #endregion
        //            }
        //        }
        //    }
        //    else
        //    {
        //        dto.Result.IsResult = false;
        //        dto.Result.ResultMsg = "No data to save";
        //    }
        //    return dto;
        //}
        protected override BaseDTO DoUpdate(BaseDTO baseDTO)
        {
            var dto = (MISS02P001DTO)baseDTO;
            if (dto.Model.Details.Count() > 0)
            {
                InsertTemp(dto);
                if (dto.Result.IsResult)
                {
                    foreach (var item in dto.Model.Details)
                    {
                        if (isInsert(item, dto.Model.COM_CODE))
                        {
                            #region ====Insert==========
                            string strSQL = @"INSERT INTO [dbo].[VSMS_DEPLOY]
                                                       ([COM_CODE]
                                                       ,[DAY]
                                                       ,[MONTH]
                                                       ,[YEAR]
                                                       ,[DEPLOYMENT_DATE]
                                                       ,[TYPE_DAY]
                                                       ,[CRET_BY]
                                                       ,[CRET_DATE]
                                                       ,[MNT_BY]
                                                       ,[MNT_DATE])
                                            VALUES
                                                        (@COM_CODE 
                                                        ,@DAY 
                                                        ,@MONTH 
                                                        ,@YEAR 
                                                        ,@DEPLOYMENT_DATE 
                                                        ,@TYPE_DAY
                                                        ,@CRET_BY 
                                                        ,@CRET_DATE 
                                                        ,@MNT_BY 
                                                        ,@MNT_DATE)";

                            var parameters = CreateParameter();

                            parameters.AddParameter("COM_CODE", dto.Model.COM_CODE);
                            parameters.AddParameter("DAY", item.DAY);
                            parameters.AddParameter("MONTH", item.MONTH);
                            parameters.AddParameter("YEAR", item.YEAR);
                            parameters.AddParameter("DEPLOYMENT_DATE", item.DEPLOYMENT_DATE);
                            parameters.AddParameter("TYPE_DAY", item.TYPE_DAY);
                            parameters.AddParameter("CRET_BY", dto.Model.CRET_BY);
                            parameters.AddParameter("CRET_DATE", dto.Model.CRET_DATE);
                            parameters.AddParameter("MNT_BY", dto.Model.MNT_BY);
                            parameters.AddParameter("MNT_DATE", dto.Model.MNT_DATE);


                            var result1 = _DBMangerNoEF.ExecuteNonQuery(strSQL, parameters, CommandType.Text);
                            if (!result1.Success(dto))
                            {
                                dto.Result.IsResult = false;
                                dto.Result.ResultMsg = result1.ErrorMessage;
                                break;
                            }
                            #endregion
                        }
                        else
                        {
                            #region ====Update==========
                            var parameters1 = CreateParameter();

                            parameters1.AddParameter("error_code", null, ParameterDirection.Output);
                            parameters1.AddParameter("COM_CODE", dto.Model.COM_CODE);
                            parameters1.AddParameter("DAY", item.DAY);
                            parameters1.AddParameter("MONTH", item.MONTH);
                            parameters1.AddParameter("YEAR", item.YEAR);
                            parameters1.AddParameter("DEPLOYMENT_DATE", item.DEPLOYMENT_DATE);
                            parameters1.AddParameter("TYPE_DAY", item.TYPE_DAY);
                            parameters1.AddParameter("MNT_BY", dto.Model.CRET_BY);
                            parameters1.AddParameter("MNT_DATE", dto.Model.CRET_DATE);

                            var result = _DBMangerNoEF.ExecuteNonQuery("dbo.[SP_VSMS_DEPLOY_001]", parameters1);

                            if (result.OutputData["error_code"] != "0")
                            {
                                dto.Result.IsResult = false;
                                dto.Result.ResultMsg = result.OutputData["error_code"].Trim();
                                break;
                            }
                            #endregion
                        }
                    }
                }
                
            }
            else
            {
                dto.Result.IsResult = false;
                dto.Result.ResultMsg = "No data to save";
            }
            return dto;
        }

        public bool isInsert(MISS02P001DetailPModel date, string COM_CODE)
        {
            string strSQL = @"SELECT COUNT(DEPLOYMENT_DATE) AS DEPLOYMENT_DATE 
                                            FROM [dbo].[VSMS_DEPLOY_TEMP] WHERE (1=1)
                                            AND DEPLOYMENT_DATE = @date
                                            AND COM_CODE = @COM_CODE 
                                            AND YEAR = @YEAR";
            var parameters = CreateParameter();

            parameters.AddParameter("date", date.DEPLOYMENT_DATE);
            parameters.AddParameter("COM_CODE", COM_CODE);
            parameters.AddParameter("YEAR", date.YEAR);

            var result = _DBMangerNoEF.ExecuteDataSet(strSQL, parameters, commandType: CommandType.Text);

            string DEPLOYMENT_DATE = result.OutputDataSet.Tables[0].Rows[0][0].ToString();
            bool isInert;

            if (DEPLOYMENT_DATE == "0")
                isInert = true;
            else
                isInert = false;

            return isInert;
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
                    string strSQL1 = @"DELETE FROM [dbo].[VSMS_DEPLOY] 
                                            WHERE COM_CODE = @COM_CODE 
                                            AND YEAR = @YEAR";

                    var parameters1 = CreateParameter();

                    parameters1.AddParameter("COM_CODE", dto.Model.COM_CODE);
                    parameters1.AddParameter("YEAR", item.YEAR);

                    var result = _DBMangerNoEF.ExecuteNonQuery(strSQL1, parameters1, CommandType.Text);
                    if (!result.Success(dto))
                    {
                        dto.Result.IsResult = false;
                        dto.Result.ResultMsg = result.ErrorMessage;
                        break;
                    }
                }
            }
            
            return dto;
        }

       
       
        #endregion
    }
}