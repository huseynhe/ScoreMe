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
    }
}
