using ScoreMe.DAL.Model;
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
