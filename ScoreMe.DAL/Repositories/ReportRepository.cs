using ScoreMe.DAL.DTO;
using ScoreMe.UTILITY;
using ScoreMe.UTILITY.Custom;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Repositories
{
    public class ReportRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;
        public List<ReportDTO> GetUserReports()
        {
            var result = new List<ReportDTO>();
            ReportDTO reportDTO = null;
            var query = @"select ev.Name, count(*) as Say from[dbo].[tbl_User] us
                                 left join [dbo].[tbl_EnumValue] ev on us.[UserType_EVID]=ev.ID and ev.Status=1
                                 where us.Status=1
                                 group by ev.Name ";


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    //command.Parameters.AddWithValue("@P_PersonID", personID);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        reportDTO = new ReportDTO()
                        {
                            name = reader.GetStringOrEmpty(0),
                            count = reader.GetInt32OrDefaultValue(1)

                        };

                        result.Add(reportDTO);
                    }
                }
                connection.Close();
            }

            return result;

        }

        public List<ReportDTO> GetProviderReports()
        {
            var result = new List<ReportDTO>();
            ReportDTO reportDTO = null;
            var query = @"select ev.Name, count(*) as Say from [dbo].[tbl_Provider] p
                                 left join [dbo].[tbl_EnumValue] ev on p.[Type]=ev.ID and ev.Status=1
                                 where p.Status=1
                                 group by ev.Name ;";


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    //command.Parameters.AddWithValue("@P_PersonID", personID);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        reportDTO = new ReportDTO()
                        {
                            name = reader.GetStringOrEmpty(0),
                            count = reader.GetInt32OrDefaultValue(1)

                        };

                        result.Add(reportDTO);
                    }
                }
                connection.Close();
            }

            return result;

        }
    }
}
