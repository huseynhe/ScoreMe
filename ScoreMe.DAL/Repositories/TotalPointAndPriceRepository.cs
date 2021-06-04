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
    public class TotalPointAndPriceRepository
    {
        public List<TotalPointAndPriceDTO> SW_GetTotalPointReports( int year)
        {
            var result = new List<TotalPointAndPriceDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetTotalPoint](@P_year) ";

            allQuery.Append(query);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
              
                    SqlParameter pyear = new SqlParameter("@P_year", SqlDbType.Int);
                    pyear.Value = year;
                    command.Parameters.Add(pyear);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        TotalPointAndPriceDTO totalPointAndPriceDTO = new TotalPointAndPriceDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            UserName = reader.GetStringOrEmpty(1),
                            CustomerFullName = reader.GetStringOrEmpty(2),
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


                        totalPointAndPriceDTO.Average = GetAverage(totalPointAndPriceDTO);

                        result.Add(totalPointAndPriceDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
        public List<TotalPointAndPriceDTO> SW_GetTotalPriceReports(int year)
        {
            var result = new List<TotalPointAndPriceDTO>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select * from  [GetTotalPrice](@P_year) ";

            allQuery.Append(query);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {

                    SqlParameter pyear = new SqlParameter("@P_year", SqlDbType.Int);
                    pyear.Value = year;
                    command.Parameters.Add(pyear);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        TotalPointAndPriceDTO totalPointAndPriceDTO = new TotalPointAndPriceDTO()
                        {
                            INOUT_EVType = reader.GetInt32OrDefaultValue(0),
                            UserName = reader.GetStringOrEmpty(1),
                            CustomerFullName = reader.GetStringOrEmpty(2),
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


                        totalPointAndPriceDTO.Average = GetAverage(totalPointAndPriceDTO);

                        result.Add(totalPointAndPriceDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }
        public decimal GetAverage(TotalPointAndPriceDTO item)
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
