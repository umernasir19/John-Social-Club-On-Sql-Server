// -----------------------------------------------------------------------
// <copyright file="Scripts.cs" company="John">
// Socia Member club Demo ©2013
// </copyright>
// -----------------------------------------------------------------------

namespace John.SocialClub.Data.Sql
{
    /// <summary>
    /// DBConstants static class contains sql string constants
    /// </summary>
    public static class Scripts
    {
        /// <summary>
        /// Sql to get a club member details by Id
        /// </summary>
        public static readonly string sqlGetClubMemberById = "Select" +
            " Id, Name, DOb, Occupation, MaritalStatus, Health, Salary, No_Of_Children" +
            " From ClubMember Where Id = @Id";

        /// <summary>
        /// Sql to get all club members
        /// </summary>
        public static readonly string SqlGetAllClubMembers = "Select" +
            " Id, Name, DOb, Occupation, MaritalStatus, Health, Salary, No_Of_Children" +
            " From ClubMember";

        /// <summary>
        /// sql to insert a club member details
        /// </summary>
        public static readonly string SqlInsertClubMember = "Insert Into" +
            " ClubMember(Name, DOb, Occupation, MaritalStatus, Health, Salary, No_Of_Children)" +
            " Values(@Name, @DateOfBirth, @Occupation, @MaritalStatus, @HealthStatus, @Salary, @NumberOfChildren)";

        /// <summary>
        /// sql to search for club members
        /// </summary>
        public static readonly string SqlSearchClubMembers = "Select " +
            " Id, Name, DOb, Occupation, MaritalStatus, Health, Salary, NO_Of_Children" +
            " From ClubMember Where (@Occupation Is NULL OR @Occupation = Occupation) {0}" +
            " (@MaritalStatus Is NULL OR @MaritalStatus = MaritalStatus)";

        /// <summary>
        /// sql to update club member details
        /// </summary>
        public static readonly string sqlUpdateClubMember = "Update ClubMember " +
            " Set [Name] = @Name, [DOb] = @DateOfBirth, [Occupation] = @Occupation, [MaritalStatus] = @MaritalStatus, " +
            " [Health] = @HealthStatus, [Salary] = @Salary, [No_Of_Children] = @NumberOfChildren Where ([Id] = @Id)";

        /// <summary>
        /// sql to delete a club member record
        /// </summary>
        public static readonly string sqlDeleteClubMember = "Delete From ClubMember Where (Id = @Id)";
    }
}
