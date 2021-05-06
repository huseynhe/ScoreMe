using ScoreMe.DAL;
using ScoreMe.DAL.CodeObjects;
using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.Business
{
    public class GroupBusinessOperation
    {
        CRUDOperation operation = new CRUDOperation();

        #region tbl_Group 
        public BaseOutput GetGroups(out List<tbl_Group> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var groups = operation.GetGroups();
                itemsOut = groups;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetGroupByID(Int64 id, out tbl_Group itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var group = operation.GetGroupByID(id); ;

                itemOut = group;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                itemOut = null;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetGroupByName(string name, out tbl_Group itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                var group = operation.GetGroupByName(name); ;

                itemOut = group;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddGroup(tbl_Group item, out tbl_Group itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_Group group = operation.AddGroup(item);
                itemOut = group;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateGroup(tbl_Group item, out tbl_Group itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_Group group = operation.UpdateGroup(item);
                itemOut = group;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteGroup(Int64 id, out tbl_Group itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                tbl_Group group = operation.DeleteGroup(id, 0);
                itemOut = group;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        
        public BaseOutput GetUsersByGroupID(Int64 groupid, out List<tbl_User> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var users = operation.GetUsersByGroupID(groupid);
                itemsOut = users;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput GetGroupsByUserID(Int64 userid, out List<tbl_Group> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var groups = operation.GetGroupsByUserID(userid);
                itemsOut = groups;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
        #region tbl_UserGroup
        public BaseOutput GetUserGroups(out List<tbl_UserGroup> itemsOut)
        {
            BaseOutput baseOutput;
            itemsOut = null;
            try
            {
                var userGroups = operation.GetUserGroups();
                itemsOut = userGroups;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput AddUserGroup(tbl_UserGroup item, out tbl_UserGroup itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_UserGroup userGroup = operation.AddUserGroup(item);
                itemOut = userGroup;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput UpdateUserGroup(tbl_UserGroup item, out tbl_UserGroup itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {

                tbl_UserGroup userGroup = operation.UpdateUserGroup(item);
                itemOut = userGroup;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");


            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        public BaseOutput DeleteUserGroup(Int64 id, out tbl_UserGroup itemOut)
        {
            BaseOutput baseOutput;
            itemOut = null;
            try
            {
                tbl_UserGroup userGroup = operation.DeleteUserGroup(id, 0);
                itemOut = userGroup;
                return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

            }
            catch (Exception ex)
            {

                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }
        #endregion
    }
}
