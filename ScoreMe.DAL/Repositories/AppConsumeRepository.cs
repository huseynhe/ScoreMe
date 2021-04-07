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
        //private List<tbl_Package> GetPackageList(Int64 mobileEVID)
        //{

        //    CRUDOperation cRUDOperation = new CRUDOperation();
        //    List<tbl_Package> packages = cRUDOperation.GetPackagesByMobileEVID(mobileEVID);
        //    return packages;


        //}
        //public tbl_PackagePrice GetPackagePrice(Int64 mobileEVID, decimal? consumedNet)
        //{
        //    try
        //    {
        //        if (consumedNet == null || consumedNet == 0)
        //        {
        //            return null;
        //        }

        //        CRUDOperation cRUDOperation = new CRUDOperation();
        //        List<tbl_Package> packages = GetPackageList(mobileEVID);
        //        Int64 packageID = 0;
        //        foreach (var item in packages)
        //        {
        //            if (consumedNet < item.PackageSize)
        //            {
        //                packageID = item.ID;
        //                break;
        //            }
        //        }
        //        tbl_PackagePrice packagePrice = cRUDOperation.GetPackagePriceByID(packageID);
        //        return packagePrice;
        //    }
        //    catch (Exception ex)
        //    {

        //        return null;
        //    }


        //}

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
