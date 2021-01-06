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
    public class EmployeeRepository
    {


        public List<PersonDTO> SW_GetEmployees(Search search)
        {
            var result = new List<PersonDTO>();
            string queryEnd = "";
            string head = "";


            head = @" emp.[ID]
                       ,emp.[FirstName]
                       ,emp.[LastName]
                       ,emp.[FatherName]
                       ,emp.[GenderType]
                       ,emp.UserId
					   ,us.[UserName]
                    ";


            StringBuilder allQuery = new StringBuilder();

            var query = @"SELECT " + head + @"   from [dbo].[tbl_Employee] emp ,[dbo].[tbl_User] us  
					   where emp.[UserId]=us.[ID] and us.Status=1 and emp.Status=1 and us.[UserType_EVID]=32  ";
            allQuery.Append(query);


            var nameQuery = @" and emp.FirstName like N'%' + @P_FirstName + '%'";
            if (!string.IsNullOrEmpty(search.Name))
            {
                allQuery.Append(nameQuery);
            }

            var surnameQuery = @" and emp.LastName like N'%' + @P_LastName + '%'";
            if (!string.IsNullOrEmpty(search.UserName))
            {
                allQuery.Append(surnameQuery);
            }

            allQuery.Append(queryEnd);


            using (var connection = new SqlConnection(ConnectionStrings.ConnectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(allQuery.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@P_FirstName", search.Name.GetStringOrEmptyData());
                    command.Parameters.AddWithValue("@P_LastName", search.SurName.GetStringOrEmptyData());

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {

                        PersonDTO personDTO = new PersonDTO()
                        {
                            EmployeeID = reader.GetInt64OrDefaultValue(0),
                            FirstName = reader.GetStringOrEmpty(1),
                            LastName = reader.GetStringOrEmpty(2),
                            FatherName = reader.GetStringOrEmpty(3),
                            GenderType = reader.GetInt32OrDefaultValue(4),
                            UserId = reader.GetInt64OrDefaultValue(5),
                            UserName = reader.GetStringOrEmpty(6),

                        };
                        if (personDTO.GenderType == 1)
                        {
                            personDTO.GenderTypeDesc = "Kişi";
                        }
                        if (personDTO.GenderType == 2)
                        {
                            personDTO.GenderTypeDesc = "Qadın";
                        }
                        result.Add(personDTO);

                    }
                }
                connection.Close();
            }

            return result;
        }


    }
}