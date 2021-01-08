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
    public class PackageRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;

        #region Package
        private List<PackageDTO> GetPackages(Search search, out int _count)
        {
            _count = 0;
            var result = new List<PackageDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @"  p.[ID],[Mobile_EVID],ev.name as Mobile_EVDesc, [PackageName],[PackageSize],[Validity],[ValidityDesc]";
            }
            else
            {
                head = @"  count(*) as totalcount ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"  FROM [dbo].[tbl_Package] p,[dbo].[tbl_EnumValue] ev
		                                        where p.[Mobile_EVID]=ev.ID and p.Status=1 and ev.Status=1   ";
            allQuery.Append(query);

            string queryName = @" and  p.PackageName like N'%'+@P_Name+'%'";


            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(queryName);
            }

            string queryCode = @" and  ev.Name like N'%'+@P_EVName+'%'";

            if (!string.IsNullOrEmpty(search.Code))
            {
                allQuery.Append(queryCode);
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
                    command.Parameters.AddWithValue("@P_EVName", search.Code.GetStringOrEmptyData());

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new PackageDTO()
                            {
                                //[ID],[Code],[Name],[N_Name],[Sort]
                                PackageID = reader.GetInt64OrDefaultValue(0),
                                Mobile_EVID = reader.GetInt64OrDefaultValue(1),
                                Mobile_EVDesc = reader.GetStringOrEmpty(2),
                                PackageName = reader.GetStringOrEmpty(3),
                                PackageSize=reader.GetDecimalOrDefaultValue(4),
                                Validity = reader.GetInt32OrDefaultValue(5),
                                ValidityDesc = reader.GetStringOrEmpty(6),
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
        public IList<PackageDTO> SW_GetPackages(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<PackageDTO> slist = GetPackages(search, out _count);
            return slist;
        }
        public int SW_GetPackagesCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetPackages(search, out _count);
            return _count;
        }
        #endregion

        #region PackagePriceDTO
        public List<PackagePriceDTO> GetPackagePricesByPackageID(Int64 packageID)
        {
            var result = new List<PackagePriceDTO>();
            PackagePriceDTO packagePrice = null;
            var query = @"	SELECT pp.ID,pp.PackageID,pp.Source_EVID, ev.Name as Source_EVDesc,
                                   pp.BeginDate,pp.EndDate,pp.Price,pp.Point
		                   FROM [dbo].[tbl_PackagePrice] pp,[dbo].[tbl_EnumValue] ev 
						   Where pp.Source_EVID=ev.ID and pp.Status=1 and ev.Status=1
	                             and pp.PackageID=@P_PackageID";


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@P_PackageID", packageID);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        packagePrice = new PackagePriceDTO()
                        {
                            PackagePriceID = reader.GetInt64OrDefaultValue(0),
                            PackageID = reader.GetInt64OrDefaultValue(1),
                            Source_EVID = reader.GetInt64OrDefaultValue(2),
                            Source_EVDesc = reader.GetStringOrEmpty(3),
                            BeginDate = reader.GetDateTimeOrEmpty(4),
                            EndDate = reader.GetDateTimeOrEmpty(5),
                            Price = reader.GetDecimalOrDefaultValue(6),
                            Point = reader.GetDecimalOrDefaultValue(7),
                        };

                        result.Add(packagePrice);
                    }
                }
                connection.Close();
            }

            return result;

        }
        #endregion
    }
}
