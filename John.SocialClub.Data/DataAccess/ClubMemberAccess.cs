// -----------------------------------------------------------------------
// <copyright file="ClubMemberAccess.cs" company="John">
// Socia Member club Demo ©2013
// </copyright>
// -----------------------------------------------------------------------

namespace John.SocialClub.Data.DataAccess
{
    using System;
    using System.Data;
    using System.Data.OleDb;
    using John.SocialClub.Data.DataModel;
    using John.SocialClub.Data.Sql;
    using System.Data.SqlClient;

    /// <summary>
    /// Data access class for ClubMember table
    /// </summary>
    public class ClubMemberAccess : ConnectionAccess, IClubMemberAccess
    {
        /// <summary>
        /// Method to get all club members
        /// </summary>
        /// <returns>Data table</returns>
        public DataTable GetAllClubMembers()
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter oleDbDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                oleDbDataAdapter.SelectCommand = new SqlCommand();
                oleDbDataAdapter.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                oleDbDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Assign the SQL to the command object
                oleDbDataAdapter.SelectCommand.CommandText = Scripts.SqlGetAllClubMembers;

                // Fill the datatable from adapter
                oleDbDataAdapter.Fill(dataTable);
            }

            return dataTable;            
        }

        /// <summary>
        /// Method to get club member by Id
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>Data row</returns>
        public DataRow GetClubMemberById(int id)
        {
            DataTable dataTable = new DataTable();
            DataRow dataRow;

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                dataAdapter.SelectCommand = new SqlCommand();
                dataAdapter.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                dataAdapter.SelectCommand.CommandType = CommandType.Text;
                dataAdapter.SelectCommand.CommandText = Scripts.sqlGetClubMemberById;

                // Add the parameter to the parameter collection
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Id", id);

                // Fill the datatable From adapter
                dataAdapter.Fill(dataTable);

                // Get the datarow from the table
                dataRow = dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;

                return dataRow;
            }
        }

        /// <summary>
        /// Method to search club members by multiple parameters
        /// </summary>
        /// <param name="occupation">occupation value</param>
        /// <param name="maritalStatus">marital status</param>
        /// <param name="operand">AND OR operand</param>
        /// <returns>Data table</returns>
        public DataTable SearchClubMembers(object occupation, object maritalStatus, string operand)
        {
            DataTable dataTable = new DataTable();

            using (SqlDataAdapter oleDbDataAdapter = new SqlDataAdapter())
            {
                // Create the command and set its properties
                oleDbDataAdapter.SelectCommand = new SqlCommand();
                oleDbDataAdapter.SelectCommand.Connection = new SqlConnection(this.ConnectionString);
                oleDbDataAdapter.SelectCommand.CommandType = CommandType.Text;

                // Assign the SQL to the command object
                oleDbDataAdapter.SelectCommand.CommandText = string.Format(Scripts.SqlSearchClubMembers, operand);

                // Add the input parameters to the parameter collection
                oleDbDataAdapter.SelectCommand.Parameters.AddWithValue("@Occupation", occupation == null ? DBNull.Value : occupation);
                oleDbDataAdapter.SelectCommand.Parameters.AddWithValue("@MaritalStatus", maritalStatus == null ? DBNull.Value : maritalStatus);

                // Fill the table from adapter
                oleDbDataAdapter.Fill(dataTable);
            }

            return dataTable;
        }        

        /// <summary>
        /// Method to add new member
        /// </summary>
        /// <param name="clubMember">club member model</param>
        /// <returns>true or false</returns>
        public bool AddClubMember(ClubMemberModel clubMember)
        {

            using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SocialClubDBConnection"].ConnectionString))
            {
                // Set the command object properties
                
                SqlCommand command= new SqlCommand();
                command.CommandType = CommandType.Text;
                command.CommandText = Scripts.SqlInsertClubMember;

                // Add the input parameters to the parameter collection
                command.Parameters.AddWithValue("@Name", clubMember.Name);
                command.Parameters.AddWithValue("@DateOfBirth",(DateTime)clubMember.DateOfBirth);
                command.Parameters.AddWithValue("@Occupation", (int)clubMember.Occupation);
                command.Parameters.AddWithValue("@MaritalStatus", (int)clubMember.MaritalStatus);
                command.Parameters.AddWithValue("@HealthStatus", (int)clubMember.HealthStatus);
                command.Parameters.AddWithValue("@Salary", clubMember.Salary);
                command.Parameters.AddWithValue("@NumberOfChildren", clubMember.NumberOfChildren);

                // Open the connection, execute the query and close the connection
                command.Connection = con;
               command.Connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                command.Connection.Close();

                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// Method to update club member
        /// </summary>
        /// <param name="clubMember">club member</param>
        /// <returns>true / false</returns>
        public bool UpdateClubMember(ClubMemberModel clubMember)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new SqlConnection(this.ConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Scripts.sqlUpdateClubMember;

                // Add the input parameters to the parameter collection
                dbCommand.Parameters.AddWithValue("@Name", clubMember.Name);
                dbCommand.Parameters.AddWithValue("@DateOfBirth", (DateTime)clubMember.DateOfBirth);
                dbCommand.Parameters.AddWithValue("@Occupation", (int)clubMember.Occupation);
                dbCommand.Parameters.AddWithValue("@MaritalStatus", (int)clubMember.MaritalStatus);
                dbCommand.Parameters.AddWithValue("@HealthStatus", (int)clubMember.HealthStatus);
                dbCommand.Parameters.AddWithValue("@Salary", clubMember.Salary);
                dbCommand.Parameters.AddWithValue("@NumberOfChildren", clubMember.NumberOfChildren);
                dbCommand.Parameters.AddWithValue("@Id", clubMember.Id);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// Method to delete a club member
        /// </summary>
        /// <param name="id">member id</param>
        /// <returns>true / false</returns>
        public bool DeleteClubMember(int id)
        {
            using (SqlCommand dbCommand = new SqlCommand())
            {
                // Set the command object properties
                dbCommand.Connection = new SqlConnection(this.ConnectionString);
                dbCommand.CommandType = CommandType.Text;
                dbCommand.CommandText = Scripts.sqlDeleteClubMember;

                // Add the input parameter to the parameter collection
                dbCommand.Parameters.AddWithValue("@Id", id);

                // Open the connection, execute the query and close the connection
                dbCommand.Connection.Open();
                var rowsAffected = dbCommand.ExecuteNonQuery();
                dbCommand.Connection.Close();

                return rowsAffected > 0;
            }
        }
    }
}
