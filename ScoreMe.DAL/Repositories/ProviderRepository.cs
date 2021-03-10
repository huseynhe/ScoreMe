using ScoreMe.DAL.DBModel;
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
    public class ProviderRepository
    {
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
    }
}
