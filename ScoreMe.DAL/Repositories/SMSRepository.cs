using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Enum;
using ScoreMe.UTILITY;
using ScoreMe.UTILITY.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Repositories
{
    public class SMSRepository
    {
        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }
        private static string GetNumberPrefix(string userName)
        {

            string userFullName = RemoveSpecialCharacters(userName);
            int length = userFullName.Length;
            string prefix = string.Empty;
            switch (length)
            {
                case 9:
                    prefix = userFullName.Substring(0, 2);
                    break;
                case 10:
                    prefix = userFullName.Substring(1, 2);
                        break;
                case 12:
                    prefix = userFullName.Substring(3, 2);
                    break;
                case 13:
                    prefix = userFullName.Substring(4, 2);
                    break;
                default:
                    prefix = string.Empty;
                    break;
            }

            
            return prefix;
        }
        private static tbl_OperatorInformation GetAverageCost(string prefix, int type) {

            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_OperatorInformation operatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(prefix, type,(int)OperatorChanelType.Message);
            return operatorInformation;


        }
        private static tbl_SMSSenderInfo GetShortAverageCost(string senderName)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_SMSSenderInfo senderInfo = cRUDOperation.GetSMSSenderInfoByName(senderName);
            return senderInfo;


        }
        public List<SMSReportDTO> SW_GetSMSReports(int userID, string userName, int year)
        {
            var result = new List<SMSReportDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetMessageReport](@P_USERID,@P_year) ";

            allQuery.Append(query);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    SqlParameter puserID = new SqlParameter("@P_USERID", SqlDbType.Int);
                    puserID.Value = userID;
                    command.Parameters.Add(puserID);
                    SqlParameter pyear = new SqlParameter("@P_year", SqlDbType.Int);
                    pyear.Value = year;
                    command.Parameters.Add(pyear);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        SMSReportDTO sMSReportDTO = new SMSReportDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            OperationType = reader.GetStringOrEmpty(2),
                            Year = reader.GetInt32OrDefaultValue(3),
                            January = reader.GetDecimalOrDefaultValue2(4),
                            February = reader.GetDecimalOrDefaultValue2(5),
                            March = reader.GetDecimalOrDefaultValue2(6),
                            April = reader.GetDecimalOrDefaultValue2(7),
                            May = reader.GetDecimalOrDefaultValue2(8),
                            June = reader.GetDecimalOrDefaultValue2(9),
                            July = reader.GetDecimalOrDefaultValue2(10),
                            August = reader.GetDecimalOrDefaultValue2(11),
                            September = reader.GetDecimalOrDefaultValue2(12),
                            October = reader.GetDecimalOrDefaultValue2(13),
                            November = reader.GetDecimalOrDefaultValue2(14),
                            December = reader.GetDecimalOrDefaultValue2(15),



                        };


                        sMSReportDTO.Average = GetAverage(sMSReportDTO);

                        string prefix = GetNumberPrefix(userName);
                        tbl_OperatorInformation  operatorInformation= GetAverageCost(prefix, sMSReportDTO.INOUT_EVType);
                        if (operatorInformation!=null)
                        {
                            if (operatorInformation.Price == null)
                            {
                                sMSReportDTO.AveragePrice = null;
                            }
                            else
                            {
                                sMSReportDTO.AveragePrice = sMSReportDTO.Average * (decimal)operatorInformation.Price; ;
                            }

                            if (operatorInformation.Point == null)
                            {
                                sMSReportDTO.AveragePoint = null;
                            }
                            else
                            {
                                sMSReportDTO.AveragePoint = sMSReportDTO.Average * (decimal)operatorInformation.Point; ;
                            }
                        }
                    
                     
                        result.Add(sMSReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<SMSReportDTO> SW_GetSMSReportShortMsjs(int userID, string userName, int year)
        {
            var result = new List<SMSReportDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetMessageReportShortSMS](@P_USERID,@P_year) ";

            allQuery.Append(query);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    SqlParameter puserID = new SqlParameter("@P_USERID", SqlDbType.Int);
                    puserID.Value = userID;
                    command.Parameters.Add(puserID);
                    SqlParameter pyear = new SqlParameter("@P_year", SqlDbType.Int);
                    pyear.Value = year;
                    command.Parameters.Add(pyear);
                    //command.Parameters.AddWithValue("@P_USERID", userID);
                    //command.Parameters.AddWithValue("@P_year", year);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        SMSReportDTO sMSReportDTO = new SMSReportDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            OperationType = reader.GetStringOrEmpty(2),
                            Year = reader.GetInt32OrDefaultValue(3),
                            January = reader.GetInt32OrNull(4),
                            February = reader.GetInt32OrNull(5),
                            March = reader.GetInt32OrNull(6),
                            April = reader.GetInt32OrNull(7),
                            May = reader.GetInt32OrNull(8),
                            June = reader.GetInt32OrNull(9),
                            July = reader.GetInt32OrNull(10),
                            August = reader.GetInt32OrNull(11),
                            September = reader.GetInt32OrNull(12),
                            October = reader.GetInt32OrNull(13),
                            November = reader.GetInt32OrNull(14),
                            December = reader.GetInt32OrNull(15),


                        };


                        sMSReportDTO.Average = GetAverageShort(sMSReportDTO);
                        string senderName = string.Empty;
                        try
                        {
                            senderName = sMSReportDTO.Name.Split(' ')[0];
                        }
                        catch (Exception ex)
                        {

                            
                        }
                
                        tbl_SMSSenderInfo senderInfo = GetShortAverageCost(senderName);
                        if (senderInfo != null)
                        {
                            if (senderInfo.Price == null)
                            {
                                sMSReportDTO.AveragePrice = null;
                            }
                            else
                            {
                                sMSReportDTO.AveragePrice = sMSReportDTO.Average * (decimal)senderInfo.Price; ;
                            }

                            if (senderInfo.Point == null)
                            {
                                sMSReportDTO.AveragePoint = null;
                            }
                            else
                            {
                                sMSReportDTO.AveragePoint = sMSReportDTO.Average * (decimal)senderInfo.Point; ;
                            }
                        }


                        result.Add(sMSReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<SMSReportDTO> SW_GetSMSReportShorParsetMsjs(int userID, string userName, int year)
        {
            var result = new List<SMSReportDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetMessageReportShortParseSMS](@P_USERID,@P_year) ";

            allQuery.Append(query);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    SqlParameter puserID = new SqlParameter("@P_USERID", SqlDbType.Int);
                    puserID.Value = userID;
                    command.Parameters.Add(puserID);
                    SqlParameter pyear = new SqlParameter("@P_year", SqlDbType.Int);
                    pyear.Value = year;
                    command.Parameters.Add(pyear);
                    //command.Parameters.AddWithValue("@P_USERID", userID);
                    //command.Parameters.AddWithValue("@P_year", year);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        SMSReportDTO sMSReportDTO = new SMSReportDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            OperationType = reader.GetStringOrEmpty(2),
                            Year = reader.GetInt32OrDefaultValue(3),
                            January = reader.GetInt32OrNull(4),
                            February = reader.GetInt32OrNull(5),
                            March = reader.GetInt32OrNull(6),
                            April = reader.GetInt32OrNull(7),
                            May = reader.GetInt32OrNull(8),
                            June = reader.GetInt32OrNull(9),
                            July = reader.GetInt32OrNull(10),
                            August = reader.GetInt32OrNull(11),
                            September = reader.GetInt32OrNull(12),
                            October = reader.GetInt32OrNull(13),
                            November = reader.GetInt32OrNull(14),
                            December = reader.GetInt32OrNull(15),


                        };


                        sMSReportDTO.Average = GetAverageShort(sMSReportDTO);
                        sMSReportDTO.AveragePrice = sMSReportDTO.Average;


                        result.Add(sMSReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
        public decimal GetAverage(SMSReportDTO item) {

            int k = 0;
            decimal averageTotal = 0;
            if (item.January.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.January;
            }
            if (item.February.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.February;
            }
            if (item.March.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.March;
            }
            if (item.April.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.April;
            }
            if (item.May.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.May;
            }

            if (item.June.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.June;
            }
            if (item.July.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.July;
            }
            if (item.August.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.August;
            }
            if (item.September.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.September;
            }
            if (item.October.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.October;
            }
            if (item.November.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.November;
            }
            if (item.December.HasValue)
            {
                k++;
                averageTotal = averageTotal + (int)item.December;
            }

            decimal average = averageTotal / (k==0?1:k);

            return average;
        }
        public decimal GetAverageShort(SMSReportDTO item)
        {

            int k = 0;
            decimal averageTotal = 0;
            if (item.January>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.January;
            }
            if (item.February>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.February;
            }
            if (item.March>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.March;
            }
            if (item.April>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.April;
            }
            if (item.May>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.May;
            }

            if (item.June>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.June;
            }
            if (item.July>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.July;
            }
            if (item.August>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.August;
            }
            if (item.September>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.September;
            }
            if (item.October>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.October;
            }
            if (item.November>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.November;
            }
            if (item.December>0)
            {
                k++;
                averageTotal = averageTotal + (int)item.December;
            }

            decimal average = averageTotal / (k == 0 ? 1 : k);

            return average;
        }

        public List<SMSReportShortDTO> SW_GetSMSReportShorParseList(string userName, int year)
        {
            var result = new List<SMSReportShortDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select  srs.[SenderName],IsExpense,
                                CASE
                                    WHEN IsExpense=1  THEN N'Məxaric'
                                    WHEN IsExpense = 0 THEN N'Mədaxil'
                                END AS IsExpenseDesc,Amount,Currency,CardNumber,OperationDate,MerchantName,Balance,BalanceCurrency
                                 from [dbo].[tbl_SMSReportShort] srs, tbl_User u
                                where srs.UserID=u.ID and u.UserName=@P_UserName and srs.[Year]=@P_Year
                                order by srs.OperationDate desc";

            allQuery.Append(query);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    SqlParameter puseName = new SqlParameter("@P_UserName", SqlDbType.NVarChar);
                    puseName.Value = userName;
                    command.Parameters.Add(puseName);
                    SqlParameter pyear = new SqlParameter("@P_Year", SqlDbType.Int);
                    pyear.Value = year;
                    command.Parameters.Add(pyear);
                    //command.Parameters.AddWithValue("@P_USERID", userID);
                    //command.Parameters.AddWithValue("@P_year", year);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        SMSReportShortDTO sMSReportShortDTO = new SMSReportShortDTO()
                        {
                            SenderName = reader.GetStringOrEmpty(0),
                            IsExpense = reader.GetInt32OrDefaultValue(1),
                            IsExpenseDesc = reader.GetStringOrEmpty(2),
                            Amount = reader.GetDecimalOrDefaultValue(3),
                            Currency = reader.GetStringOrEmpty(4),
                            CardNumber = reader.GetStringOrEmpty(5),
                            OperationDate = reader.GetDateTimeOrEmpty(6),
                            MerchantName = reader.GetStringOrEmpty(7),
                            Balance = reader.GetDecimalOrDefaultValue(8),
                            BalanceCurrency = reader.GetStringOrEmpty(9),
                           


                        };



                        result.Add(sMSReportShortDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
    }
}
