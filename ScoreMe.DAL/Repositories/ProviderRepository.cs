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

        internal List<ProviderReportDTO> GetProviderReport(Search search, out int _count)
        {
            var result = new List<ProviderReportDTO>();
            _count = 0;
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
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

            }
            else
            {
                head = @" count(pr.ID) ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  from tbl_Provider pr, tbl_proposal p 
                                     where pr.Status=1  and p.Status=1 and pr.ID=p.ProviderID ";
            allQuery.Append(query);


            var providerIDQuery = @" and pr.ID=@P_ProviderID";
            if (search.ProviderID>0)
            {
                allQuery.Append(providerIDQuery);
            }

            var dateQuery = @" and  p.InsertDate between @P_FromDate and @P_ToDate";
            if (search.FromtDate!=null &&search.ToDate!=null)
            {
                allQuery.Append(dateQuery);
            }

            if (search.isCount == false)
            {
                queryEnd = " group by pr.ID,pr.Name order by pr.Name";
               // queryEnd = @" order by   emp.[FirstName] asc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS FETCH NEXT @RecordsPerPage ROWS ONLY";
            }


            allQuery.Append(queryEnd);

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    //command.Parameters.AddWithValue("@PageNo", search.pageNumber);
                    //command.Parameters.AddWithValue("@RecordsPerPage", search.pageSize);
                    //command.Parameters.AddWithValue("@P_FirstName", search.Name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@P_ProviderID", search.ProviderID);
                    command.Parameters.AddWithValue("@P_FromDate", search.FromtDate.HasValue?search.FromtDate.Value:DateTime.Now);
                    command.Parameters.AddWithValue("@P_ToDate", search.ToDate.HasValue?search.ToDate.Value:DateTime.Now);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new ProviderReportDTO()
                            {
                                ProviderID = reader.GetInt64OrDefaultValue(0),
                                ProviderName = reader.GetStringOrEmpty(1),
                                DeclaredProposalCount = reader.GetInt32OrDefaultValue(2),
                                AppliedProposalCount = reader.GetInt32OrDefaultValue(3),
                                AccteptedProposalCount = reader.GetInt32OrDefaultValue(4),
                                RejectedProposalCount = reader.GetInt32OrDefaultValue(5),
                                WaitingProposalCount = reader.GetInt32OrDefaultValue(6),
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

        public IList<ProviderReportDTO> SW_GetProviderReports(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<ProviderReportDTO> slist = GetProviderReport(search, out _count);
            return slist;
        }
    }
}
