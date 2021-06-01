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
    public class CALLRepository
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
        private static tbl_OperatorInformation GetOperatorInformation(string prefix, int type,int operatorChanelType)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_OperatorInformation operatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(prefix, type,(int) OperatorChanelType.Call);
            return operatorInformation;


        }
        public List<CALLReportDTO> SW_GetCALLReports(int userID, string userName, int year)
        {
            var result = new List<CALLReportDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetCALLReport](@P_USERID,@P_year) ";

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

                        CALLReportDTO cALLReportDTO = new CALLReportDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            Year = reader.GetInt32OrDefaultValue(2),
                            January = reader.GetDecimalOrDefaultValue2(3),
                            February = reader.GetDecimalOrDefaultValue2(4),
                            March = reader.GetDecimalOrDefaultValue2(5),
                            April = reader.GetDecimalOrDefaultValue2(6),
                            May = reader.GetDecimalOrDefaultValue2(7),
                            June = reader.GetDecimalOrDefaultValue2(8),
                            July = reader.GetDecimalOrDefaultValue2(9),
                            August = reader.GetDecimalOrDefaultValue2(10),
                            September = reader.GetDecimalOrDefaultValue2(11),
                            October = reader.GetDecimalOrDefaultValue2(12),
                            November = reader.GetDecimalOrDefaultValue2(13),
                            December = reader.GetDecimalOrDefaultValue2(14),


                        };


                        cALLReportDTO.Average = GetAverage(cALLReportDTO);

                        // string prefix = GetNumberPrefix(userName);
                        string prefix = NumberHelper.GetNumberPrefix(userName);
                        tbl_OperatorInformation operatorInformation = GetOperatorInformation(prefix, cALLReportDTO.INOUT_EVType,(int)OperatorChanelType.Call);
                        if (operatorInformation != null)
                        {
                            if (operatorInformation.Price == null)
                            {
                                cALLReportDTO.AveragePrice = null;
                            }
                            else
                            {
                                cALLReportDTO.AveragePrice = cALLReportDTO.Average * (decimal)operatorInformation.Price; ;
                            }

                            if (operatorInformation.Point == null)
                            {
                                cALLReportDTO.AveragePoint = null;
                            }
                            else
                            {
                                cALLReportDTO.AveragePoint = cALLReportDTO.Average * (decimal)operatorInformation.Point; ;
                            }
                        }


                        result.Add(cALLReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }

        public decimal GetAverage(CALLReportDTO item)
        {

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

            decimal average = averageTotal / (k == 0 ? 1 : k);

            return average;
        }
 
    }
}
