using JIStudent.Models.Account;
using JIStudent.Models.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace JIStudent.ViewModels.Account
{
    public class AccountViewModels
    {
        public static List<SelectListItem> GetAllRoles(int roleId)
        {
            List<SelectListItem> roles = new List<SelectListItem>();

            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_RolesGetRolesByRoleId", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@RoleId", roleId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SelectListItem item = new SelectListItem();
                        item.Value = reader["RoleName"].ToString();
                        item.Text = reader["RoleName"].ToString();

                        roles.Add(item);
                    }
                }
            }

            return roles;
        }

        public static UserProfileModel GetUserProfileData(int currentUserId)
        {
            UserProfileModel userProfileModel = new UserProfileModel();

            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UsersGetUserProfileData", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@UserId", currentUserId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    reader.Read();

                    userProfileModel.FullName = reader["FullName"].ToString();
                    userProfileModel.Email = reader["Email"].ToString();
                    userProfileModel.Address = reader["Address"].ToString();
                }
            }

            return userProfileModel;
        }

        public static void UpdateUserProfile(UserProfileModel userProfileModel)
        {
            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UsersUpdateUserProfile", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    cmd.Parameters.AddWithValue("@UserId", WebSecurity.CurrentUserId);
                    cmd.Parameters.AddWithValue("@FullName", userProfileModel.FullName);
                    cmd.Parameters.AddWithValue("@Email", userProfileModel.Email);
                    cmd.Parameters.AddWithValue("@Address", userProfileModel.Address);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            using (SqlConnection conn = new SqlConnection(AppSetting.ConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_UsersGetAllUsers", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UserModel user = new UserModel();
                        user.UserId = Convert.ToInt32(reader["UserId"]);
                        user.UserName = reader["UserName"].ToString();
                        user.FullName = reader["FullName"].ToString();
                        user.Email = reader["Email"].ToString();

                        users.Add(user);
                    }
                }
            }

            return users;
        }
    }
}