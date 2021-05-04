using ScoreMe.DAL.DTO;
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
    public class EnumRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;

        #region EnumCategory
        private List<EnumDTO> GetEnumCategories(Search search, out int _count)
        {
            _count = 0;
            var result = new List<EnumDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" [ID],[Code],[Name],[Description]";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  FROM [dbo].[tbl_EnumCategory] ec where ec.Status=1   ";
            allQuery.Append(query);

            string queryName = @" and  ec.Name like N'%'+@P_Name+'%'";
            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }

            string queryCode = @" and  ec.Code like N'%'+@P_Code+'%'";

            if (!string.IsNullOrEmpty(search.Code))
            {
                allQuery.Append(queryCode);
            }



            if (search.isCount == false)
            {
                queryEnd = @" order by   ec.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
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
                    command.Parameters.AddWithValue("@P_Code", search.Code.GetStringOrEmptyData());

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new EnumDTO()
                            {
                                //[ID],[Code],[Name],[N_Name],[Sort]
                                EnumCategoryID = reader.GetInt64OrDefaultValue(0),
                                EnumCategoryCode = reader.GetStringOrEmpty(1),
                                EnumCategoryName = reader.GetStringOrEmpty(2),
                                EnumCategoryDesc = reader.GetStringOrEmpty(3)
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
        public IList<EnumDTO> SW_GetEnumCategories(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<EnumDTO> slist = GetEnumCategories(search, out _count);
            return slist;
        }
        public int SW_GetEnumCategoriesCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetEnumCategories(search, out _count);
            return _count;
        }
        #endregion

        #region EnumValue
        private List<EnumDTO> GetEnumValues(Search search, out int _count)
        {
            _count = 0;
            var result = new List<EnumDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" ev.[ID],ev.[Code],ev.[Name],ev.[Description],ev.EnumCategoryID, ec.Name as EnumCategoryName";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"   FROM [dbo].[tbl_EnumValue] ev, [dbo].[tbl_EnumCategory] ec
                                                 where ec.ID=ev.EnumCategoryID and ec.Status=1 and ev.Status=1  ";
            allQuery.Append(query);

            string queryName = @" and  ev.Name like N'%'+@P_Name+'%'";


            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }

            string queryCode = @" and  ev.Code like N'%'+@P_Code+'%'";

            if (!string.IsNullOrEmpty(search.Code))
            {
                allQuery.Append(queryCode);
            }



            if (search.isCount == false)
            {
                queryEnd = @" order by   ec.Name desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
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
                    command.Parameters.AddWithValue("@P_Code", search.Code.GetStringOrEmptyData());

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new EnumDTO()
                            {
                                EnumValueID = reader.GetInt64OrDefaultValue(0),
                                EnumValueCode = reader.GetStringOrEmpty(1),
                                EnumValueName = reader.GetStringOrEmpty(2),
                                EnumValueDesc = reader.GetStringOrEmpty(3),
                                EnumCategoryID= reader.GetInt64OrDefaultValue(4),
                                EnumCategoryName = reader.GetStringOrEmpty(5),
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
        public IList<EnumDTO> SW_GetEnumValues(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<EnumDTO> slist = GetEnumValues(search, out _count);
            return slist;
        }
        public int SW_GetEnumValuesCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetEnumValues(search, out _count);
            return _count;
        }
        #endregion
    }
}
