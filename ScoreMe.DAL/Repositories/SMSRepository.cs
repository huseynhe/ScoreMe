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
                            Year = reader.GetInt32OrDefaultValue(2),
                            January = reader.GetInt32OrNull(3),
                            February = reader.GetInt32OrNull(4),
                            March = reader.GetInt32OrNull(5),
                            April = reader.GetInt32OrNull(6),
                            May = reader.GetInt32OrNull(7),
                            June = reader.GetInt32OrNull(8),
                            July = reader.GetInt32OrNull(9),
                            August = reader.GetInt32OrNull(10),
                            September = reader.GetInt32OrNull(11),
                            October = reader.GetInt32OrNull(12),
                            November = reader.GetInt32OrNull(13),
                            December = reader.GetInt32OrNull(14),


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
                            Year = reader.GetInt32OrDefaultValue(2),
                            January = reader.GetInt32OrNull(3),
                            February = reader.GetInt32OrNull(4),
                            March = reader.GetInt32OrNull(5),
                            April = reader.GetInt32OrNull(6),
                            May = reader.GetInt32OrNull(7),
                            June = reader.GetInt32OrNull(8),
                            July = reader.GetInt32OrNull(9),
                            August = reader.GetInt32OrNull(10),
                            September = reader.GetInt32OrNull(11),
                            October = reader.GetInt32OrNull(12),
                            November = reader.GetInt32OrNull(13),
                            December = reader.GetInt32OrNull(14),


                        };


                        sMSReportDTO.Average = GetAverageShort(sMSReportDTO);

                        string prefix = GetNumberPrefix(userName);
                        tbl_OperatorInformation operatorInformation = GetAverageCost(prefix, sMSReportDTO.INOUT_EVType);
                        if (operatorInformation != null)
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
    }
}
