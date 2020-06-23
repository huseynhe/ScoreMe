using ScoreMe.DAL.DBModel;
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
    public class RegionRepository
    {
        public tbl_Region SV_GetSQLRegionsById(int id)
        {
            CRUDOperation cRUDOperation = new CRUDOperation();
            tbl_Region region = cRUDOperation.GetRegionById(id);
            return region;
        }
        public List<tbl_Region> SV_GetSQLRegionsByParent(Int64 parentId)
        {

            var result = new List<tbl_Region>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"select r.ID,r.Name,r.ParentId from [dbo].[tbl_Region] r where r.ParentId=@P_Parent and r.Status=1";

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@P_Parent", parentId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        result.Add(new tbl_Region()
                        {
                            ID = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            ParentId = reader.GetInt64OrDefaultValue(2),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }

        public List<tbl_Region> SV_GetSQLRegionsByChild(Int64 childId)
        {

            var result = new List<tbl_Region>();
            StringBuilder allQuery = new StringBuilder();
            var query = @"WITH UserCTE AS (
                          SELECT  Id, name, ParentId,0 AS steps
                          FROM [dbo].[tbl_Region]
                          WHERE Id =@P_ChildID and Status=1  
                          UNION ALL  
                          SELECT mgr.Id, mgr.name, mgr.ParentId, usr.steps +1 AS steps
                          FROM UserCTE AS usr
                            INNER JOIN  [dbo].[tbl_Region] AS mgr
                              ON usr.ParentId = mgr.Id
                        )
                        SELECT u.Id,u.Name,u.ParentId,u.steps FROM UserCTE AS u order by u.steps desc";

            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@P_ChildID", childId);
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        result.Add(new tbl_Region()
                        {
                            ID = reader.GetInt64OrDefaultValue(0),
                            Name = reader.GetStringOrEmpty(1),
                            ParentId = reader.GetInt64OrDefaultValue(2),

                        });
                    }
                }
                connection.Close();
            }

            return result;
        }
    }
}
