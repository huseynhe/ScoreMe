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
    public class UserPhoneRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;


        private List<UserPhoneDTO> GetUserPhonesDTO(Search search, out int _count)
        {
            _count = 0;
            var result = new List<UserPhoneDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @"  usi.[ID]
                          ,usi.[UserID]
	                      ,usr.UserName
	                      ,cst.Name as CustomerName
	                      ,cst.[Surname] as CustomerSurname
                          ,usi.[CompanyName]
                          ,usi.[ModelName]
                          ,usi.[ModelNumber]
                          ,usi.[SerialNumber]
                          ,usi.[IMEI1]
                          ,usi.[IMEI2]
                          ,usi.[OSName]
                          ,usi.[OSVersion]
                          ,usi.InsertDate ";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"     FROM [DB_A62358_ScoreMe].[dbo].[tbl_UserPhoneInforamtion] usi
                          join [dbo].[tbl_User] usr on usi.UserID=usr.ID and usr.Status=1
                          left join [dbo].[tbl_Customer] cst on cst.UserId=usr.ID and usr.Status=1
                          where usi.Status=1 ";


            allQuery.Append(query);


         
            var userNameQuery = @" and usr.UserName like N'%' + @P_UserName + '%'";
            if (!string.IsNullOrEmpty(search.UserName))
            {
                allQuery.Append(userNameQuery);
            }

            var customerNameQuery = @" and cst.Name like N'%' + @P_Name+ '%'";
            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(customerNameQuery);
            }


            if (search.isCount == false)
            {
                queryEnd = @" order by   usi.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
            }


            allQuery.Append(queryEnd);


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@PageNo", search.pageNumber);
                    command.Parameters.AddWithValue("@RecordsPerPage", search.pageSize);
                    command.Parameters.AddWithValue("@P_UserName", search.UserName.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@P_Name", search.Name.GetStringOrEmptyData());
           
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            UserPhoneDTO userPhoneDTO = new UserPhoneDTO()

                            {
                                ID = reader.GetInt64OrDefaultValue(0),
                                UserID = reader.GetInt64OrDefaultValue(1),
                                UserName = reader.GetStringOrEmpty(2),
                                CustomerName = reader.GetStringOrEmpty(3),
                                CustomerSurname = reader.GetStringOrEmpty(4),

                                CompanyName = reader.GetStringOrEmpty(5),
                                ModelName = reader.GetStringOrEmpty(6),

                                ModelNumber = reader.GetStringOrEmpty(7),
                                SerialNumber = reader.GetStringOrEmpty(8),

                                IMEI1 = reader.GetStringOrEmpty(9),
                                IMEI2 = reader.GetStringOrEmpty(10),
                                OSName = reader.GetStringOrEmpty(11),
                                OSVersion = reader.GetStringOrEmpty(12),
                                InsertDate=reader.GetDateTimeOrEmpty(13)
                            };

                        
                            result.Add(userPhoneDTO);
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

        public IList<UserPhoneDTO> SW_GetUserPhonesDTO(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<UserPhoneDTO> slist = GetUserPhonesDTO(search, out _count);
            return slist;
        }
        public int SW_GetUserPhonesDTOCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetUserPhonesDTO(search, out _count);
            return _count;
        }

        //public UserPhoneDTO GetUserPhonesDTOByID(Int64 id)
        //{
        //    ApplicationDTO itemData = new ApplicationDTO();

        //    var query = @"SELECT  app.[ID]
        //             ,[IsRepeated]
        //             ,[PreviousID]
        //             ,[ApplicationNumber]
        //             ,[OfficialNumber]
        //             ,[EntryFormType]
        //             ,(select t.Name from dbo.tbl_Type t where t.ID=app.EntryFormType) as EntryFormTypeDesc                  
        //             ,[ShortContent]
        //             ,[CreateDate]
        //             ,app.OrganisationID
        //             ,org.Name as OrganisationName
        //             ,app.OfficialPersonID
        //             ,CONCAT(operson.FirstName,' ', operson.LastName, ' ',operson.FatherName) as FullName
        //             ,app.FolderTypeID
        //             ,(select t.Name from dbo.tbl_Type t where t.ID=app.FolderTypeID) as FolderTypeDesc 
        //             , app.PositionID
        //             ,(select t.Name from dbo.tbl_Type t where t.ID=app.PositionID) as PositionDesc
	       //          ,app.OfficialDate
        //             ,app.SheetCount
        //              ,[AcceptEmployeeID]
        //              ,(select emp.FirstName+' '+emp.LastName from dbo.tbl_Employee emp where emp.ID=app.AcceptEmployeeID) as AcceptEmployeeDesc  
        //        from [dbo].[tbl_OfficialApplication] app 
        //                  left join [dbo].[tbl_Organisation] org on app.OrganisationID=org.ID and org.Status=1
        //                  left join dbo.tbl_OfficialPerson operson on app.OfficialPersonID=operson.ID and operson.Status=1
        //                  where app.Status=1 and app.ID=@P_ID ";



        //    using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
        //    {
        //        connection.Open();

        //        using (var command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@P_ID", id);
        //            var reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {

        //                itemData = new ApplicationDTO()
        //                {
        //                    ID = reader.GetInt64OrDefaultValue(0),
        //                    IsRepeated = reader.GetBoolOrFalse(1),
        //                    IsRepeatedDesc = reader.GetBoolOrFalse(1) == false ? "Xeyr" : "Bəli",
        //                    PreviousID = reader.GetInt64OrDefaultValue(2),
        //                    ApplicationNumber = reader.GetStringOrEmpty(3),
        //                    OfficialNumber = reader.GetStringOrEmpty(4),

        //                    EntryFormType = reader.GetInt32OrDefaultValue(5),
        //                    EntryFormTypeDesc = reader.GetStringOrEmpty(6),

        //                    ShortContent = reader.GetStringOrEmpty(7),
        //                    CreateDate = reader.GetDateTimeOrNow(8),
        //                    OrganisationID = reader.GetInt32OrDefaultValue(9),
        //                    OrganisationName = reader.GetStringOrEmpty(10),
        //                    OfficialPersonID = reader.GetInt32OrDefaultValue(11),
        //                    OfficialPersonFullName = reader.GetStringOrEmpty(12),
        //                    FolderType = reader.GetInt32OrDefaultValue(13),
        //                    FolderTypeDesc = reader.GetStringOrEmpty(14),
        //                    PositionID = reader.GetInt32OrDefaultValue(15),
        //                    PositionName = reader.GetStringOrEmpty(16),
        //                    OfficialDate = reader.GetDateTimeOrNow(17),
        //                    SheetCount = reader.GetInt32OrDefaultValue(18),
        //                    AcceptEmployeeID = reader.GetInt64OrDefaultValue(19),
        //                    AcceptEmployeeDesc = reader.GetStringOrEmpty(20),
        //                };


        //            }
        //        }
        //        connection.Close();
        //    }

        //    return itemData;
        //}
    }
}
