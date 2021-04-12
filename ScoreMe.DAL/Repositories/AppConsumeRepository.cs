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
using System.Threading.Tasks;

namespace ScoreMe.DAL.Repositories
{
    public class AppConsumeRepository
    {
        private List<tbl_AppInformation> AppInformationList = null;
        
        public AppConsumeRepository()
        {
            AppInformationList = GetAppInformations();
        }
        private List<tbl_AppInformation> GetAppInformations()
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            List<tbl_AppInformation> appInformations = cRUDOperation.GetAppInformations();
            return appInformations;


          }
        public AppConsumeReportDTO GetAppConsumePriceAndPoint(AppConsumeReportDTO item)
        {
            try
            {
                if (item == null)
                {
                    return null;
                }

          
                foreach (var listitem in AppInformationList)
                {
                    if (listitem.CategoryType==item.AppType)
                    {
                        item.AveragePoint = item.Average*listitem.PointUsage;
                        break;
                    }
                }
                return item;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
        public AppConsumeReportDTO GetAppCountPriceAndPoint(AppConsumeReportDTO item)
        {
            try
            {
                if (item == null)
                {
                    return null;
                }


                foreach (var listitem in AppInformationList)
                {
                    if (listitem.CategoryType == item.AppType)
                    {
                        item.AveragePoint = item.Average * listitem.PointCount;
                        break;
                    }
                }
                return item;
            }
            catch (Exception ex)
            {

                return null;
            }


        }
        public List<AppConsumeReportDTO> SW_GetAppConsumeReports(int userID, string userName, int year)
        {
            var result = new List<AppConsumeReportDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetAppUsage](@P_USERID,@P_year) ";

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

                        AppConsumeReportDTO appConsumeReportDTO = new AppConsumeReportDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            AppType = reader.GetInt32OrDefaultValue(1),
                            AppTypeDesc = reader.GetStringOrEmpty(2),
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


                        appConsumeReportDTO.Average = GetAverage(appConsumeReportDTO);
                        appConsumeReportDTO = GetAppConsumePriceAndPoint(appConsumeReportDTO);

                        result.Add(appConsumeReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<AppConsumeReportDTO> SW_GetAppCountReports(int userID, string userName, int year)
        {
            var result = new List<AppConsumeReportDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetAppCount](@P_USERID,@P_year) ";

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

                        AppConsumeReportDTO appConsumeReportDTO = new AppConsumeReportDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            AppType = reader.GetInt32OrDefaultValue(1),
                            AppTypeDesc = reader.GetStringOrEmpty(2),
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


                        appConsumeReportDTO.Average = GetAverage(appConsumeReportDTO);
                        appConsumeReportDTO = GetAppCountPriceAndPoint(appConsumeReportDTO);

                        result.Add(appConsumeReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<AppConsumeReportDTO> SW_GetUnitAppCountReports(int userID, string userName, int year)
        {
            var result = new List<AppConsumeReportDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetUnitAppUsage](@P_USERID,@P_year) ";

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

                        AppConsumeReportDTO appConsumeReportDTO = new AppConsumeReportDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            AppType = reader.GetInt32OrDefaultValue(1),
                            AppTypeDesc = reader.GetStringOrEmpty(2),
                            AppName=reader.GetStringOrEmpty(3),
                            Year = reader.GetInt32OrDefaultValue(4),
                            January = reader.GetDecimalOrDefaultValue2(5),
                            February = reader.GetDecimalOrDefaultValue2(6),
                            March = reader.GetDecimalOrDefaultValue2(7),
                            April = reader.GetDecimalOrDefaultValue2(8),
                            May = reader.GetDecimalOrDefaultValue2(9),
                            June = reader.GetDecimalOrDefaultValue2(10),
                            July = reader.GetDecimalOrDefaultValue2(11),
                            August = reader.GetDecimalOrDefaultValue2(12),
                            September = reader.GetDecimalOrDefaultValue2(13),
                            October = reader.GetDecimalOrDefaultValue2(14),
                            November = reader.GetDecimalOrDefaultValue2(15),
                            December = reader.GetDecimalOrDefaultValue2(16),


                        };


                        appConsumeReportDTO.Average = GetAverage(appConsumeReportDTO);
                        appConsumeReportDTO = GetAppConsumePriceAndPoint(appConsumeReportDTO);

                        result.Add(appConsumeReportDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
        public decimal GetAverage(AppConsumeReportDTO item)
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
