using ScoreMe.DAL.DBModel;
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
    public class ProviderRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;


        #region Proposal
        private List<Provider> GetProviders(Search search, out int _count)
        {
            _count = 0;
            var result = new List<Provider>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @"  p.ID as ProviderID
                        ,p.Name as ProviderName
                        ,p.Description
                        ,P.ParentID 
                        ,(select pr.NAme from tbl_Provider pr where pr.ID=p.ParentID) as ParentName
                        ,p.UserID
                        ,u.UserName
                        ,p.Type as ProviderType
                        ,ev.Name as ProvuderTypeDesc
                        ,p.RelatedPersonName
                        ,p.RelatedPersonProfession
                        ,p.VOEN
                        ,p.RelatedPersonPhone ";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @" from tbl_Provider p
                            join tbl_User u on p.UserId=u.ID and u.Status=1
                            left join [dbo].[tbl_EnumValue] ev on p.[Type]=ev.ID and ev.Status=1
                            where p.Status=1  ";
            allQuery.Append(query);

      

            string queryName = @" and  p.Name like N'%'+@P_Name+'%'";
            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }
            string queryVOEN = @" and  p.VOEN like N'%'+@P_VOEN+'%'";
            if (!string.IsNullOrEmpty(search.Code))
            {
                allQuery.Append(queryVOEN);
            }
            string queryuserName = @" and  u.UserName like N'%'+@P_UserName+'%'";
            if (!string.IsNullOrEmpty(search.UserName))
            {
                allQuery.Append(queryuserName);
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
                    command.Parameters.AddWithValue("@P_Name", search.Name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@P_VOEN", search.Code.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@P_UserName", search.UserName.GetStringOrEmptyData());
            

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new Provider()
                            {

                                ID = reader.GetInt64OrDefaultValue(0),
                                Name = reader.GetStringOrEmpty(1),
                                Description = reader.GetStringOrEmpty(2),
                                ParentID = reader.GetInt64OrDefaultValue(3),
                                ParentName = reader.GetStringOrEmpty(4),
                                UserID = reader.GetInt64OrDefaultValue(5),
                                UserName = reader.GetStringOrEmpty(6),
                                Type = reader.GetInt64OrDefaultValue(7),
                                TypeDesc = reader.GetStringOrEmpty(8),
                                RelatedPersonName = reader.GetStringOrEmpty(9),
                                RelatedPersonProfession = reader.GetStringOrEmpty(10),
                                VOEN = reader.GetStringOrEmpty(11),
                                RelatedPersonPhone = reader.GetStringOrEmpty(12),
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
        public IList<Provider> SW_GetProviders(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<Provider> slist = GetProviders(search, out _count);
            return slist;
        }
        public int SW_GetProvidersCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetProviders(search, out _count);
            return _count;
        }
        #endregion
        public Provider GetProviderByID(Int64 providerID)
        {
            Provider provider = null;
            try
            {
                var query = @"select 
                        usr.ID as UserID,
                        usr.UserName,
                        usr.Password,
                        usr.UserType_EVID,
                        provider.ID as ProviderID,
                        provider.Name,
                        provider.ParentID,
                        provider.Type,
                        provider.[Description],
                        provider.[RegionId],
                        provider.[Address],
                        provider.RelatedPersonName,
                        provider.RelatedPersonProfession,
                        provider.RelatedPersonPhone,
                        provider.VOEN
                         from tbl_User usr,tbl_Provider provider 
                         where usr.ID=provider.UserId and usr.Status=1 and provider.Status=1 and provider.ID=@P_ProviderID ";

                using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
                {
                    connection.Open();

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@P_ProviderID", providerID);

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            provider = new Provider()
                            {
                                UserID = reader.GetInt64OrDefaultValue(0),
                                UserName = reader.GetStringOrEmpty(1),
                                Password = reader.GetStringOrEmpty(2),
                                UserType_EVID = reader.GetInt64OrDefaultValue(3),
                                ID = reader.GetInt64OrDefaultValue(4),
                                Name = reader.GetStringOrEmpty(5),
                                ParentID = reader.GetInt64OrDefaultValue(6),
                                Type = reader.GetInt64OrDefaultValue(7),
                                Description = reader.GetStringOrEmpty(8),
                                RegionId = reader.GetInt64OrDefaultValue(9),
                                Address = reader.GetStringOrEmpty(10),
                                RelatedPersonName = reader.GetStringOrEmpty(11),
                                RelatedPersonProfession = reader.GetStringOrEmpty(12),
                                RelatedPersonPhone = reader.GetStringOrEmpty(13),
                                VOEN = reader.GetStringOrEmpty(14)


                            };



                        }

                    }
                    connection.Close();
                    return provider;
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        #region GetProviderReportsByDatePeriod
        public ProviderReportDTO SW_GetProviderReportsByDatePeriod(Search search)
        {
            ProviderReportDTO providerReportDTO = null ;

            string head = "";
            head = @" pr.ID,pr.Name,
                       (select Count(*)from tbl_Proposal prop where pr.ID=prop.ProviderID and prop.Status=1) as YerlesdirilmisXidmetSay,
                      (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.UserStateType=19 ) as MuracietEdilmisXidmetSay,
                      (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.UserStateType=20 and pus.ProviderStateType=15 ) as QebulEdilmisXidmetSay,
                      (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.ProviderStateType=16 ) as RedEdilmisXidmetSay,
                       (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.ProviderStateType=17 ) as GozlemedekiXidmetSay";

            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  from tbl_Provider pr, tbl_proposal p 
                                     where pr.Status=1  and p.Status=1 and pr.ID=p.ProviderID
                                     and pr.ID=@P_ProviderID and  p.InsertDate between @P_FromDate and @P_ToDate
                                     group by pr.ID,pr.Name order by pr.Name";
            allQuery.Append(query);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@P_ProviderID", search.ProviderID);
                    command.Parameters.AddWithValue("@P_FromDate", search.FromtDate.HasValue ? search.FromtDate.Value : DateTime.Now);
                    command.Parameters.AddWithValue("@P_ToDate", search.ToDate.HasValue ? search.ToDate.Value : DateTime.Now);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            providerReportDTO=new ProviderReportDTO()
                            {
                                ProviderID = reader.GetInt64OrDefaultValue(0),
                                ProviderName = reader.GetStringOrEmpty(1),
                                DeclaredProposalCount = reader.GetInt32OrDefaultValue(2),
                                AppliedProposalCount = reader.GetInt32OrDefaultValue(3),
                                AccteptedProposalCount = reader.GetInt32OrDefaultValue(4),
                                RejectedProposalCount = reader.GetInt32OrDefaultValue(5),
                                WaitingProposalCount = reader.GetInt32OrDefaultValue(6),
                            };
                        }

                    }
                }
                connection.Close();
            }

            return providerReportDTO;
        }

        #endregion

        public ProviderReportDTO SW_GetProviderReportsByYearAndMonths(Search search)
        {
            ProviderReportDTO providerReportDTO = null; ;

            string head = "";
            head = @" pr.ID,pr.Name,
                       (select Count(*)from tbl_Proposal prop where pr.ID=prop.ProviderID and prop.Status=1) as YerlesdirilmisXidmetSay,
                      (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.UserStateType=19 ) as MuracietEdilmisXidmetSay,
                      (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.UserStateType=20 and pus.ProviderStateType=15 ) as QebulEdilmisXidmetSay,
                      (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.ProviderStateType=16 ) as RedEdilmisXidmetSay,
                       (select Count(*)  from tbl_Proposal prop,  tbl_ProposalUserState pus
                       where prop.Status=1 and pus.Status=1 and
                        pr.ID=prop.ProviderID and prop.ID=pus.ProposalID and pus.ProviderStateType=17 ) as GozlemedekiXidmetSay";

            StringBuilder allQuery = new StringBuilder();
            if (string.IsNullOrEmpty(search.Months))
            {
                var query = @"SELECT " + head + @"  from tbl_Provider pr, tbl_proposal p 
                                     where pr.Status=1  and p.Status=1 and pr.ID=p.ProviderID
                                     and pr.ID=" + search.ProviderID
                            + " and YEAR(p.InsertDate)=" + search.Year
                            + " group by pr.ID,pr.Name order by pr.Name";
                allQuery.Append(query);
            }
            else
            {
                var query = @"SELECT " + head + @"  from tbl_Provider pr, tbl_proposal p 
                                     where pr.Status=1  and p.Status=1 and pr.ID=p.ProviderID
                                     and pr.ID=" + search.ProviderID
                            + " and YEAR(p.InsertDate)=" + search.Year
                            + " and  MONTH(p.InsertDate) in (" + search.Months + ")"
                            + " group by pr.ID,pr.Name order by pr.Name";
                allQuery.Append(query);
            }
   

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        providerReportDTO = new ProviderReportDTO()
                        {
                            ProviderID = reader.GetInt64OrDefaultValue(0),
                            ProviderName = reader.GetStringOrEmpty(1),
                            DeclaredProposalCount = reader.GetInt32OrDefaultValue(2),
                            AppliedProposalCount = reader.GetInt32OrDefaultValue(3),
                            AccteptedProposalCount = reader.GetInt32OrDefaultValue(4),
                            RejectedProposalCount = reader.GetInt32OrDefaultValue(5),
                            WaitingProposalCount = reader.GetInt32OrDefaultValue(6),
                        };


                    }
                }
                connection.Close();
            }
            return providerReportDTO;
        }

    }
}
