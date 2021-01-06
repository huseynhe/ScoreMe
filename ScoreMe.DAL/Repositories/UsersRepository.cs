﻿using ScoreMe.DAL;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.DTO;
using ScoreMe.DAL.Model;
using ScoreMe.DAL.Objects;
using ScoreMe.UTILITY;
using ScoreMe.UTILITY.Custom;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL.Repositories
{
    public class UsersRepository
    {
        private int pageNumber = 1;
        private int pageSize = 1000000;
        CRUDOperation CRUDOperation = new CRUDOperation();
        public bool ChangePassword(string UserName, string Password, out string ErrorMessage)
        {
            string result = "";
            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_ChangePassword", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.Add(new SqlParameter("@UserName", UserName));
            cmd.Parameters.Add(new SqlParameter("@NewPassword", Password));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                result = reader.GetStringOrEmpty(0);
            }
            connection.Close();
            cmd.Dispose();
            if (result == "Uğurlu")
            {
                ErrorMessage = "";
                return true;
            }
            else
            {
                ErrorMessage = result;
                return false;
            }
        }

        public bool CheckForUserName(int Id, string UserName)
        {
            CRUDOperation DO = new CRUDOperation();
            List<tbl_User> ul = DO.GetUsers();
            int cnt = Id > 0 ? ul.Where(u => u.UserName.ToLower() == UserName.ToLower() && u.ID != Id).Count() : ul.Where(u => u.UserName.ToLower() == UserName.ToLower()).Count();
            bool result = cnt > 0 ? true : false;
            return result;
        }

        public bool GetRelatedUsersCounts(int UserId)
        {
            var query = @"SELECT *
                        FROM [dbo].[GetRelatedUsersCounts](" + UserId.ToString() + ")";
            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query.ToString(), connection);
            SqlDataReader reader = cmd.ExecuteReader();
            bool result = false;
            while (reader.Read())
            {
                if (reader.GetInt32OrDefaultValue(1) > 0)
                {
                    result = true;
                }
            }
            connection.Close();
            cmd.Dispose();
            return result;
        }

        private List<UserDTO> GetUserDetails(Search search, out int _count)
        {
            _count = 0;
            var result = new List<UserDTO>();
            string queryEnd = "";
            string head = "";

            if (search.isCount == false)
            {
                head = @"  us.ID, us.UserName,us.Password,us.AccountLocked,us.UserType_EVID,ev.Name as UserTypeDesc";
            }
            else
            {
                head = @" count(us.ID) ";
            }


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @" from tbl_User us, [dbo].[tbl_EnumValue]  ev
                                     where us.Status=1 and us.UserType_EVID=ev.ID and ev.Status=1";


            allQuery.Append(query);

            var userName = @" and us.UserName like N'%' + @UserName + '%'";

            if (!string.IsNullOrEmpty(search.UserName))
            {
                allQuery.Append(userName);
            }


            if (search.isCount == false)
            {
                queryEnd = @" order by us.InsertDate desc OFFSET ( @PageNo - 1 ) * @RecordsPerPage ROWS
                             FETCH NEXT @RecordsPerPage ROWS ONLY";
            }


            allQuery.Append(queryEnd);



            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@PageNo", search.pageNumber);
                    command.Parameters.AddWithValue("@RecordsPerPage", search.pageSize);
                    command.Parameters.AddWithValue("@UserName", search.UserName.GetStringOrEmptyData());
                    //command.Parameters.AddWithValue("@id", id);

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (search.isCount == false)
                        {
                            result.Add(new UserDTO()
                            {
                                UserID = reader.GetInt64OrDefaultValue(0),
                                UserName = reader.GetStringOrEmpty(1),
                                Password = reader.GetStringOrEmpty(2),
                                AccountLocked = reader.GetInt32OrDefaultValue(3),
                                UserType_EVID = reader.GetInt64OrDefaultValue(4),
                                UserTypeDesc= reader.GetStringOrEmpty(5),
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
        public IList<UserDTO> SW_GetUserDetails(Search search)
        {
            int _count = 0;
            if (search.pageNumber <= 0 || search.pageSize <= 0)
            {
                search.pageNumber = pageNumber;
                search.pageSize = pageSize;
            }
            search.isCount = false;

            IList<UserDTO> slist = GetUserDetails(search, out _count);
            return slist;
        }
        public int SW_GetUserDetailsCount(Search search)
        {
            search.isCount = true;
            int _count = 0;
            GetUserDetails(search, out _count).FirstOrDefault();

            return _count;
        }

     

    }
}
