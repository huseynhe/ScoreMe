using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.DTO;
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
            tbl_OperatorInformation operatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(prefix, type);
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
                            January = reader.GetInt32OrDefaultValue(3),
                            February = reader.GetInt32OrDefaultValue(4),
                            March = reader.GetInt32OrDefaultValue(5),
                            April = reader.GetInt32OrDefaultValue(6),
                            May = reader.GetInt32OrDefaultValue(7),
                            June = reader.GetInt32OrDefaultValue(8),
                            July = reader.GetInt32OrDefaultValue(9),
                            August = reader.GetInt32OrDefaultValue(10),
                            September = reader.GetInt32OrDefaultValue(11),
                            October = reader.GetInt32OrDefaultValue(12),
                            November = reader.GetInt32OrDefaultValue(13),
                            December = reader.GetInt32OrDefaultValue(14),


                        };

                        decimal averageTotal = (sMSReportDTO.January + sMSReportDTO.February + sMSReportDTO.March + sMSReportDTO.April
                            + sMSReportDTO.May + sMSReportDTO.June + sMSReportDTO.July + sMSReportDTO.August + sMSReportDTO.September
                            + sMSReportDTO.October + sMSReportDTO.November + sMSReportDTO.December);
                        decimal average = averageTotal / 12;


                        sMSReportDTO.Average = average;

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
                                sMSReportDTO.AveragePrice = average * (decimal)operatorInformation.Price; ;
                            }
                        }
                    
                     
                        result.Add(sMSReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
    }
}
