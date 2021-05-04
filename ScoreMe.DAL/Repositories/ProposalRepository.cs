using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Model;
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
    public class ProposalRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;

        #region Proposal
        private List<ProposalDTO> GePropsals(Search search, out int _count)
        {
            _count = 0;
            var result = new List<ProposalDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" p.ID as ProposalID
                        ,p.Name as ProposalName
                        ,p.Description
                        ,p.Note
                        ,p.ProviderID
                        ,pr.Name as ProviderName
                        ,pr.UserId
                        ,p.IsPublic
                        ,p.StartDate
                        ,p.EndDate
                        ,pr.Type as ProviderType
                        ,ev.Code as ProvuderTypeCode";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  from [dbo].[tbl_Proposal] p
                                join [dbo].[tbl_Provider] pr on p.ProviderID=pr.ID and pr.Status=1
                                left join [dbo].[tbl_EnumValue] ev on pr.[Type]=ev.ID and ev.Status=1
                                where  p.Status=1 and pr.Status=1  ";
            allQuery.Append(query);

            string queryProposalID = @" and  p.ID=@P_ProposalID";

            if (search.ProposalID>0)
            {
                allQuery.Append(queryProposalID);
            }

            string queryProviderID = @" and  pr.ID=@P_ProviderID";

            if (search.ProviderID > 0)
            {
                allQuery.Append(queryProviderID);
            }
            string queryName = @" and  p.Name like N'%'+@P_Name+'%'";
            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }


            if (search.isCount == false)
            {
                queryEnd = @" order by   p.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
            }


            allQuery.Append(queryEnd);


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@PageNo", search.pageNumber);
                    command.Parameters.AddWithValue("@RecordsPerPage", search.pageSize);
                    command.Parameters.AddWithValue("@P_ProposalID", search.ProposalID);
                    command.Parameters.AddWithValue("@P_ProviderID", search.ProviderID);
                    command.Parameters.AddWithValue("@P_Name", search.Name.GetStringOrEmptyData());
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new ProposalDTO()
                            {
                               
                                ProposalID = reader.GetInt64OrDefaultValue(0),
                                ProposalName = reader.GetStringOrEmpty(1),
                                Description = reader.GetStringOrEmpty(2),
                                Note = reader.GetStringOrEmpty(3),
                                ProviderID = reader.GetInt64OrDefaultValue(4),
                                ProviderName = reader.GetStringOrEmpty(5),
                                OwnerUserID = reader.GetInt64OrDefaultValue(6),
                                IsPublic = reader.GetBoolean(7),
                                StartDate = reader.GetDateTimeOrEmpty(8),
                                EndDate = reader.GetDateTimeOrEmpty(9),
                                ProviderType=reader.GetInt64OrDefaultValue(10),
                                ProviderTypeCode=reader.GetStringOrEmpty(11)
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
        public IList<ProposalDTO> SW_GePropsals(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<ProposalDTO> slist = GePropsals(search, out _count);
            return slist;
        }
        public int SW_GePropsalssCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GePropsals(search, out _count);
            return _count;
        }
        #endregion

        #region GePropsalsByUserName
        internal List<ProposalDTO> GePropsalsByUserName(Search search, out int _count)
        {
            _count = 0;
            var result = new List<ProposalDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" p.ID as ProposalID
                        ,p.Name as ProposalName
                        ,p.Description
                        ,p.Note
                        ,p.ProviderID
                        ,pr.Name as ProviderName
                        ,pr.UserId as OwnerID
                        ,p.IsPublic
                        ,p.StartDate
                        ,p.EndDate
                        ,pr.Type as ProviderType
                        ,ev.Code as ProvuderTypeCode
                        ,u.ID as UserID";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"   from tbl_UserGroup ug
                                 join tbl_User u on ug.UserID=u.ID and u.Status=1
                                join tbl_Group g on ug.GroupID=g.ID and g.Status=1
                                join tbl_ProposalUserGroup pug on g.ID=pug.GroupID and pug.Status=1
                                join tbl_Proposal p on pug.ProposalID=p.ID and p.Status=1
                                join tbl_Provider pr on p.ProviderID=pr.ID and pr.Status=1
                                left join [dbo].[tbl_EnumValue] ev on pr.[Type]=ev.ID and ev.Status=1
                                where ug.Status=1 and u.UserName=@P_UserName ";
             allQuery.Append(query);

            if (search.isCount == false)
            {
                queryEnd = @" order by   p.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
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

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new ProposalDTO()
                            {

                                ProposalID = reader.GetInt64OrDefaultValue(0),
                                ProposalName = reader.GetStringOrEmpty(1),
                                Description = reader.GetStringOrEmpty(2),
                                Note = reader.GetStringOrEmpty(3),
                                ProviderID = reader.GetInt64OrDefaultValue(4),
                                ProviderName = reader.GetStringOrEmpty(5),
                                OwnerUserID = reader.GetInt64OrDefaultValue(6),
                                IsPublic = reader.GetBoolean(7),
                                StartDate = reader.GetDateTimeOrEmpty(8),
                                EndDate = reader.GetDateTimeOrEmpty(9),
                                ProviderType = reader.GetInt64OrDefaultValue(10),
                                ProviderTypeCode = reader.GetStringOrEmpty(11),
                                UserID = reader.GetInt64OrDefaultValue(12),


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
        public IList<ProposalDTO> SW_GePropsalsByUserName(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<ProposalDTO> slist = GePropsalsByUserName(search, out _count);
            return slist;
        }
        public int SW_GePropsalsCountByUserName(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GePropsalsByUserName(search, out _count);
            return _count;
        }
        #endregion

        #region GetProposalsByIsPublic
        internal List<ProposalDTO> GetProposalsByIsPublic(Search search, out int _count)
        {
            _count = 0;
            var result = new List<ProposalDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" p.ID as ProposalID
                        ,p.Name as ProposalName
                        ,p.Description
                        ,p.Note
                        ,p.ProviderID
                        ,pr.Name as ProviderName
                        ,pr.UserId
                        ,p.IsPublic
                        ,p.StartDate
                        ,p.EndDate
                        ,pr.Type as ProviderType
                        ,ev.Code as ProviderTypeCode";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  from [dbo].[tbl_Proposal] p
                                join [dbo].[tbl_Provider] pr on p.ProviderID=pr.ID and pr.Status=1
                                left join [dbo].[tbl_EnumValue] ev on pr.[Type]=ev.ID and ev.Status=1
                                where  p.Status=1 and pr.Status=1  and p.IsPublic=1 ";
            allQuery.Append(query);

  
            if (search.isCount == false)
            {
                queryEnd = @" order by   p.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
            }


            allQuery.Append(queryEnd);


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@PageNo", search.pageNumber);
                    command.Parameters.AddWithValue("@RecordsPerPage", search.pageSize);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new ProposalDTO()
                            {

                                ProposalID = reader.GetInt64OrDefaultValue(0),
                                ProposalName = reader.GetStringOrEmpty(1),
                                Description = reader.GetStringOrEmpty(2),
                                Note = reader.GetStringOrEmpty(3),
                                ProviderID = reader.GetInt64OrDefaultValue(4),
                                ProviderName = reader.GetStringOrEmpty(5),
                                OwnerUserID = reader.GetInt64OrDefaultValue(6),
                                IsPublic = reader.GetBoolean(7),
                                StartDate = reader.GetDateTimeOrEmpty(8),
                                EndDate = reader.GetDateTimeOrEmpty(9),
                                ProviderType = reader.GetInt64OrDefaultValue(10),
                                ProviderTypeCode = reader.GetStringOrEmpty(11)
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
        public IList<ProposalDTO> SW_GetProposalsByIsPublic(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<ProposalDTO> slist = GetProposalsByIsPublic(search, out _count);
            return slist;
        }
        public int SW_GetProposalsByIsPublicCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetProposalsByIsPublic(search, out _count);
            return _count;
        }
        #endregion

        #region GetFavoriteProposalsByUserName
        internal List<ProposalDTO> GetFavoriteProposalsByUserName(Search search, out int _count)
        {
            _count = 0;
            var result = new List<ProposalDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @" p.ID as ProposalID
                        ,p.Name as ProposalName
                        ,p.Description
                        ,p.Note
                        ,p.ProviderID
                        ,pr.Name as ProviderName
                        ,pr.UserId
                        ,p.IsPublic
                        ,p.StartDate
                        ,p.EndDate
                        ,pr.Type as ProviderType
                        ,ev.Code as ProvuderTypeCode
                        ,u.ID as UserID";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  from [dbo].[tbl_Proposal] p
                                join [dbo].[tbl_ProposalFavorite] pf on p.ID=pf.ProposalID and pf.Status=1 and pf.IsFavorite=1
                                join tbl_User u on pf.UserID=u.ID and u.Status=1
                                join [dbo].[tbl_Provider] pr on p.ProviderID=pr.ID and pr.Status=1
                                left join [dbo].[tbl_EnumValue] ev on pr.[Type]=ev.ID and ev.Status=1
                                where  p.Status=1 and pr.Status=1  and p.IsPublic=1  and u.UserName=@P_UserName";
            allQuery.Append(query);


            if (search.isCount == false)
            {
                queryEnd = @" order by   p.ID desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
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

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new ProposalDTO()
                            {

                                ProposalID = reader.GetInt64OrDefaultValue(0),
                                ProposalName = reader.GetStringOrEmpty(1),
                                Description = reader.GetStringOrEmpty(2),
                                Note = reader.GetStringOrEmpty(3),
                                ProviderID = reader.GetInt64OrDefaultValue(4),
                                ProviderName = reader.GetStringOrEmpty(5),
                                OwnerUserID = reader.GetInt64OrDefaultValue(6),
                                IsPublic = reader.GetBoolean(7),
                                StartDate = reader.GetDateTimeOrEmpty(8),
                                EndDate = reader.GetDateTimeOrEmpty(9),
                                ProviderType = reader.GetInt64OrDefaultValue(10),
                                ProviderTypeCode = reader.GetStringOrEmpty(11),
                                UserID=reader.GetInt64OrDefaultValue(12),
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
        public IList<ProposalDTO> SW_GetFavoriteProposalsByUserName(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<ProposalDTO> slist = GetFavoriteProposalsByUserName(search, out _count);
            return slist;
        }
        public int SW_GetFavoriteProposalsByUserNameCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetFavoriteProposalsByUserName(search, out _count);
            return _count;
        }
        #endregion

        public ProposalUserState GetProposalUserStateByUserID(Int64 userId,Int64 proposalId)
        {
            ProposalUserState proposalUserState = null;
            try
            {
                var query = @"select 
                                pus.ID,
                                pus.[UserID],
                                pus.ProposalID,
                                pus.[ProviderOfferAmount],
                                pus.UserDemandAmount,
                                pus.ProviderStateType,
                                (select ev.Name from [dbo].[tbl_EnumValue] ev where ev.[ID]=pus.ProviderStateType)  as ProviderStateTypeDesc,
                                pus.[UserStateType],
                                (select ev.Name from [dbo].[tbl_EnumValue] ev where ev.[ID]=pus.[UserStateType])  as UserStateTypeDesc
                                from dbo.tbl_ProposalUserState pus where pus.Status=1 and pus.UserID=@P_UserID and pus.ProposalID=@P_ProposalID ";

                using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@P_UserID", userId);
                        command.Parameters.AddWithValue("@P_ProposalID", proposalId);
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            proposalUserState = new ProposalUserState()
                            {
                                ID = reader.GetInt64OrDefaultValue(0),
                                UserID = reader.GetInt64OrDefaultValue(1),
                                ProposalID = reader.GetInt64OrDefaultValue(2),
                                ProviderOfferAmount = reader.GetDecimalOrDefaultValue(3),
                                UserDemandAmount = reader.GetDecimalOrDefaultValue(4),
                                ProviderStateType = reader.GetInt64OrDefaultValue(5),
                                ProviderStateTypeDesc = reader.GetStringOrEmpty(6),
                                UserStateType = reader.GetInt64OrDefaultValue(7),
                                UserStateTypeDesc = reader.GetStringOrEmpty(8),
                    
                            };



                        }

                    }
                    connection.Close();
                    return proposalUserState;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public List<ProposalUserState> GetProposalUserStateByProposalID( Int64 proposalId)
        {
            List<ProposalUserState> proposalUserStates = new List<ProposalUserState>();
            try
            {
                var query = @"select  pus.ID,
                                pus.[UserID],
                                pus.ProposalID,
                                pus.[ProviderOfferAmount],
                                pus.UserDemandAmount,
                                pus.ProviderStateType,
                                (select ev.Name from [dbo].[tbl_EnumValue] ev where ev.[ID]=pus.ProviderStateType)  as ProviderStateTypeDesc,
                                pus.[UserStateType],
                                (select ev.Name from [dbo].[tbl_EnumValue] ev where ev.[ID]=pus.[UserStateType])  as UserStateTypeDesc,
								ctm.Name+' '+ctm.Surname as CustomerFullName,
								pus.[ProviderOfferMonth],
								pus.[UserDemandMonth]
                                from dbo.tbl_ProposalUserState pus, [dbo].[tbl_Customer] ctm
								where pus.Status=1 and ctm.[Status]=1 and ctm.UserId=pus.UserID and pus.ProposalID=@P_ProposalID";

                using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@P_ProposalID", proposalId);
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            ProposalUserState proposalUserState = new ProposalUserState()
                            {
                                ID = reader.GetInt64OrDefaultValue(0),
                                UserID = reader.GetInt64OrDefaultValue(1),
                                ProposalID = reader.GetInt64OrDefaultValue(2),
                                ProviderOfferAmount = reader.GetDecimalOrDefaultValue(3),
                                UserDemandAmount = reader.GetDecimalOrDefaultValue(4),
                                ProviderStateType = reader.GetInt64OrDefaultValue(5),
                                ProviderStateTypeDesc = reader.GetStringOrEmpty(6),
                                UserStateType = reader.GetInt64OrDefaultValue(7),
                                UserStateTypeDesc = reader.GetStringOrEmpty(8),
                                CustomerFullName=reader.GetStringOrEmpty(9),
                                ProviderOfferMonth = reader.GetInt32OrDefaultValue(10),
                                UserDemandMonth = reader.GetInt32OrDefaultValue(11),
                            };
                            proposalUserStates.Add(proposalUserState);


                        }

                    }
                    connection.Close();
                    return proposalUserStates;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public int SW_DeleteProposalUserGroup(Int64 proposalId) 
        {
            int numberOfRowsAffectedCount = 0 ;
            try
            {
                var query = @"UPDATE [dbo].[tbl_ProposalUserGroup]
                                SET Status=0
                                WHERE ProposalID=@P_ProposalID ";

                using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@P_ProposalID", proposalId);
                        numberOfRowsAffectedCount = command.ExecuteNonQuery();

                       

                    }
                    connection.Close();
                    return numberOfRowsAffectedCount;
                }
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
        public int SW_DeleteProposalDetail(Int64 proposalId)
        {
            int numberOfRowsAffectedCount = 0;
            try
            {
                var query = @"UPDATE [dbo].[tbl_ProposalDetail]
                                SET Status=0
                                WHERE ProposalID=@P_ProposalID";

                using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@P_ProposalID", proposalId);
                        numberOfRowsAffectedCount = command.ExecuteNonQuery();



                    }
                    connection.Close();
                    return numberOfRowsAffectedCount;
                }
            }
            catch (Exception ex)
            {

                return -1;
            }

        }
    }
}
