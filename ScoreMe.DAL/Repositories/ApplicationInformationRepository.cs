using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Objects;
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
    public class ApplicationInformationRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;
        #region EnumValue
        private List<tbl_ApplicationInformation> GetApplicationInformations(Search search, out int _count)
        {
            _count = 0;
            var result = new List<tbl_ApplicationInformation>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" api.ID,api.Platform,api.GroupName,api.AppName,
                          api.Author,api.Price,api.Point,api.NetUsage,api.ShortName";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"   from tbl_ApplicationInformation api where api.Status=1  ";
            allQuery.Append(query);

            string queryName = @" and  api.AppName like N'%'+@P_Name+'%'";


            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }

            string queryAuthor = @" and  api.Author like N'%'+@P_Author+'%'";

            if (!string.IsNullOrEmpty(search.UserName))
            {
                allQuery.Append(queryAuthor);
            }

            string queryCode = @" and  api.ShortName like N'%'+@P_ShortName+'%'";

            if (!string.IsNullOrEmpty(search.Code))
            {
                allQuery.Append(queryCode);
            }


            if (search.isCount == false)
            {
                queryEnd = @" order by api.UpdateDate desc, api.ID asc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
            }


            allQuery.Append(queryEnd);


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@PageNo", search.pageNumber);
                    command.Parameters.AddWithValue("@RecordsPerPage", search.pageSize);
                    command.Parameters.AddWithValue("@P_Name", search.Name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@P_Author", search.UserName.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@P_ShortName", search.Code.GetStringOrEmptyData());
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new tbl_ApplicationInformation()
                            {
                             
                                ID = reader.GetInt64OrDefaultValue(0),
                                Platform = reader.GetStringOrEmpty(1),
                                GroupName = reader.GetStringOrEmpty(2),
                                AppName = reader.GetStringOrEmpty(3),
                                Author = reader.GetStringOrEmpty(4),
                                Price = reader.GetDecimalOrDefaultValue(5),
                                Point = reader.GetDecimalOrDefaultValue(6),
                                NetUsage= reader.GetStringOrEmpty(7),
                                ShortName = reader.GetStringOrEmpty(8),
                            });
                        }
                        else
                        {

                            _count = reader.GetInt32OrDefaultValue(0);

                        }
                    }
                }
                connection.Close();
            }

            return result;
        }
        public IList<tbl_ApplicationInformation> SW_GetApplicationInformations(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<tbl_ApplicationInformation> slist = GetApplicationInformations(search, out _count);
            return slist;
        }
        public int SW_GetApplicationInformationsCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetApplicationInformations(search, out _count);
            return _count;
        }
        #endregion
    }
}
