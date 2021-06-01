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
    public class GroupRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;

        #region Package
        private List<GroupDTO> GetGroups(Search search, out int _count)
        {
            _count = 0;
            var result = new List<GroupDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @"   g.ID as GroupID, g.Name,g.Description,g.StartLimit,g.EndLimit,g.GroupType,ev.Name as GroupTypeDesc ";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  from [dbo].[tbl_Group] g
                          left join [dbo].[tbl_EnumValue] ev on g.GroupType=cast(ev.Code as int) and ev.EnumCategoryID=21 
                           where g.Status=1 ";
            allQuery.Append(query);

            string queryName = @" and  g.Name like N'%'+@P_Name+'%'";


            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }

       
            if (search.isCount == false)
            {
                queryEnd = @" order by   g.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
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

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new GroupDTO()
                            {
                                //[ID],[Code],[Name],[N_Name],[Sort]
                                ID = reader.GetInt64OrDefaultValue(0),
                                Name = reader.GetStringOrEmpty(1),
                                Description = reader.GetStringOrEmpty(2),
                                StartLimit = reader.GetDecimalOrDefaultValue(3),
                                EndLimit = reader.GetDecimalOrDefaultValue(4),
                                GroupType = reader.GetInt32OrDefaultValue(5),
                                GroupTypeDesc = reader.GetStringOrEmpty(6),
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
        public IList<GroupDTO> SW_GetGroups(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<GroupDTO> slist = GetGroups(search, out _count);
            return slist;
        }
        public int SW_GetGroupsCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetGroups(search, out _count);
            return _count;
        }
        #endregion
    }
}
