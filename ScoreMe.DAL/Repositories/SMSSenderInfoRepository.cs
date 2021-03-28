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
    public class SMSSenderInfoRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;

        #region Package
        private List<SMSSenderInfoDTO> GetSMSSenderInfos(Search search, out int _count)
        {
            _count = 0;
            var result = new List<SMSSenderInfoDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" ID
	                     ,ActivityType
	                     ,(select ev.Name +' ('+ev.Description+')' from [dbo].[tbl_EnumValue] ev where ev.ID=si.ActivityType) as ActivityTypeDesc
	                     ,SenderName
	                     ,Number
                         ,Price
                         ,Point
                         ,Cheque
                         ,IsParse
                         ,si.Description";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  FROM [dbo].[tbl_SMSSenderInfo] si  where si.Status=1  ";
            allQuery.Append(query);

            string queryName = @" and  si.SenderName like N'%'+@P_Name+'%'";


            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }



            if (search.isCount == false)
            {
                queryEnd = @" order by   si.ActivityType desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
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
                            SMSSenderInfoDTO senderInfoDTO= new SMSSenderInfoDTO()
                            {
                                //[ID],[Code],[Name],[N_Name],[Sort]
                                ID = reader.GetInt64OrDefaultValue(0),
                                ActivityType = reader.GetInt32OrDefaultValue(1),
                                ActivityTypeDesc = reader.GetStringOrEmpty(2),
                                SenderName = reader.GetStringOrEmpty(3),
                                Number = reader.GetStringOrEmpty(4),
                                Price = reader.GetDecimalOrDefaultValue(5),
                                Point = reader.GetDecimalOrDefaultValue(6),
                                Cheque = reader.GetDecimalOrDefaultValue(7),
                                IsParse = reader.GetInt32OrDefaultValue(8),
                                Description = reader.GetStringOrEmpty(9),
                            };
                            if (senderInfoDTO.IsParse==1)
                            {
                                senderInfoDTO.IsParseDesc = "Bəli";
                            }
                            else
                            {
                                senderInfoDTO.IsParseDesc = "Xeyir";
                            }
                            result.Add(senderInfoDTO);
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
        public IList<SMSSenderInfoDTO> SW_GetGetSMSSenderInfos(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<SMSSenderInfoDTO> slist = GetSMSSenderInfos(search, out _count);
            return slist;
        }
        public int SW_GetSMSSenderInfosCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetSMSSenderInfos(search, out _count);
            return _count;
        }
        #endregion
    }
}
