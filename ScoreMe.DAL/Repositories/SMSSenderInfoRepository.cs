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
                head = @" distinct si.ID,
                            si.ActivityType
	                       ,(select ev.Name +' ('+ev.Description+')' from [dbo].[tbl_EnumValue] ev where ev.ID=si.ActivityType) as ActivityTypeDesc
	                        ,sd.SenderName
	                        ,si.Number
                            ,si.Price
                            ,si.Point
                            ,si.Cheque
                            ,si.IsParse
                            ,si.Description";
            }
            else
            {
                head = @"  count (distinct sd.SenderName)  as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @" from [dbo].[tbl_SMSDetail] sd 
                        left join [dbo].[tbl_SMSSenderInfo] si on sd.SenderName=si.SenderName  and si.Status=1
                        where sd.Status=1  ";
            allQuery.Append(query);

            string queryName = @" and  sd.SenderName like N'%'+@P_Name+'%'";


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
                            SMSSenderInfoDTO senderInfoDTO = new SMSSenderInfoDTO()
                            {
                                //[ID],[Code],[Name],[N_Name],[Sort]
                                ID = reader.GetInt64OrDefaultValue(0),
                                ActivityType = reader.GetInt32OrDefaultValue(1),
                                ActivityTypeDesc = reader.GetStringOrEmpty(2),
                                SenderName = reader.GetStringOrEmpty(3),
                                Number = reader.GetStringOrEmpty(4),
                                Price = reader.GetDecimalOrDefaultValue2(5),
                                Point = reader.GetDecimalOrDefaultValue2(6),
                                Cheque = reader.GetDecimalOrDefaultValue2(7),
                                IsParse = reader.GetInt32OrNull(8),
                                Description = reader.GetStringOrEmpty(9),
                            };
                            if (senderInfoDTO.IsParse == 1)
                            {
                                senderInfoDTO.IsParseDesc = "Bəli";
                            }
                            else if (senderInfoDTO.IsParse == 0)
                            {
                                senderInfoDTO.IsParseDesc = "Xeyir";
                            }
                            else
                            {
                                senderInfoDTO.IsParseDesc = "";
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
