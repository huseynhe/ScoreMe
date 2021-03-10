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
    public class OperatorInformationRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;

        #region Package
        private List<OperatorInformationDTO> GetOperatorInformations(Search search, out int _count)
        {
            _count = 0;
            var result = new List<OperatorInformationDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @"  ID
                           ,[OperatorType_EVID]
                           ,(select ev.Name from [dbo].[tbl_EnumValue] ev where ev.ID=op.OperatorType_EVID) as OperatorTypeDesc
	                       ,Name
	                       ,[OperatorChanelType_EVID]
	                       ,(select ev.Name from [dbo].[tbl_EnumValue] ev where ev.ID=op.OperatorChanelType_EVID) as OperatorChanelTypeDesc
	                       ,[InOutType_EVID]
	                       ,(select ev.Name+' ('+ ev.Description+')' from [dbo].[tbl_EnumValue] ev where ev.ID=op.InOutType_EVID) as InOutTypeDesc
	                       ,Price
	                       ,Point";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  FROM [DB_A62358_ScoreMe].[dbo].[tbl_OperatorInformation] op where op.Status=1  ";
            allQuery.Append(query);

            string queryName = @" and  op.Name like N'%'+@P_Name+'%'";


            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }



            if (search.isCount == false)
            {
                queryEnd = @" order by   op.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
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
                            result.Add(new OperatorInformationDTO()
                            {
                                //[ID],[Code],[Name],[N_Name],[Sort]
                                ID = reader.GetInt64OrDefaultValue(0),
                                OperatorTypeEVID = reader.GetInt32OrDefaultValue(1),
                                OperatorTypeDesc = reader.GetStringOrEmpty(2),
                                Name = reader.GetStringOrEmpty(3),
                                OperatorChanelTypeEVID = reader.GetInt32OrDefaultValue(4),
                                OperatorChanelTypeDesc = reader.GetStringOrEmpty(5),
                                InOutTypeEVID = reader.GetInt32OrDefaultValue(6),
                                InOutTypeDesc = reader.GetStringOrEmpty(7),
                                Price = reader.GetDecimalOrDefaultValue(8),
                                Point = reader.GetDecimalOrDefaultValue(9),
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
        public IList<OperatorInformationDTO> SW_GetOperatorInformations(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<OperatorInformationDTO> slist = GetOperatorInformations(search, out _count);
            return slist;
        }
        public int SW_GetOperatorInformationsCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetOperatorInformations(search, out _count);
            return _count;
        }
        #endregion
    }
}
