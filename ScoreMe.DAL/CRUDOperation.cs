using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Model;
using ScoreMe.DAL.Repositories;
using ScoreMe.UTILITY;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL
{
    public class CRUDOperation
    {

        #region tbl_Provider
        public tbl_Provider AddProvider(tbl_Provider item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Provider.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Provider DeleteProvider(Int64 id, int userId)
        {

            try
            {
                tbl_Provider oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Provider
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Provider.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Provider> GetProviders()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Provider
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Provider GetProviderById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Provider
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Provider GetProviderByVOEN(string voen)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Provider
                                where p.VOEN == voen && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Provider UpdateProvider(tbl_Provider item)
        {
            try
            {
                tbl_Provider oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Provider
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.Name = item.Name;
                        oldItem.Type = item.Type;
                        oldItem.UserId = item.UserId;
                        oldItem.Description = item.Description;
                        oldItem.Address = item.Address;
                        oldItem.RelatedPersonName = item.RelatedPersonName;
                        oldItem.RelatedPersonPhone = item.RelatedPersonPhone;
                        oldItem.RelatedPersonProfession = item.RelatedPersonProfession;
                        oldItem.RP_HomePhone = item.RP_HomePhone;
                        oldItem.VOEN = item.VOEN;
                        oldItem.ParentID = item.ParentID;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_Provider.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Provider GetProviderByUserName(string username)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var itemDb = (from u in context.tbl_User
                                  join p in context.tbl_Provider on u.ID equals p.UserId
                                  where u.UserName == username && u.Status == 1 && p.Status == 1 && u.UserType_EVID == 9
                                  select p).FirstOrDefault();



                    return itemDb;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Provider UpdateLogoPic(tbl_Provider provider)
        {
            try
            {
                tbl_Provider oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Provider
                               where p.ID == provider.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.LogoLinkPath = provider.LogoLinkPath;
                        oldItem.LogoLinkName = provider.LogoLinkName;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = provider.UpdateUser;

                        context.tbl_Provider.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion

        #region tbl_Customer
        public tbl_Customer AddCustomer(tbl_Customer item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Customer.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Customer DeleteCustomer(Int64 id, int userId)
        {

            try
            {
                tbl_Customer oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Customer
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Customer.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Customer> GetCustomers()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Customer
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Customer GetCustomerById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Customer
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Customer GetCustomerByUserId(Int64 userId)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Customer
                                where p.UserId == userId && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Customer GetCustomerByUserName(string username)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var itemUser = (from u in context.tbl_User
                                    join c in context.tbl_Customer on u.ID equals c.UserId
                                    where u.UserName == username && u.Status == 1 && c.Status == 1 && u.UserType_EVID == 8
                                    select c).FirstOrDefault();



                    return itemUser;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Customer UpdateCustomer(tbl_Customer item)
        {
            try
            {
                tbl_Customer oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Customer
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.Name = item.Name;
                        oldItem.Surname = item.Surname;
                        oldItem.FatherName = item.FatherName;
                        oldItem.PhoneNumber = item.PhoneNumber;
                        oldItem.IdentityCode = item.IdentityCode;
                        oldItem.Email = item.Email;
                        oldItem.RegionId = item.RegionId;
                        oldItem.Address = item.Address;

                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_Customer.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_User
        public tbl_User AddUser(tbl_User item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_User.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_User DeleteUser(Int64 id, Int64 userId)
        {

            try
            {
                tbl_User oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_User
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_User.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_User> GetUsers()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_User
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_User> GetUsersByTypeEVID(int evID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_User
                                 where p.Status == 1 && p.UserType_EVID == evID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_User GetUserById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_User
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_User GetUserByUserName(string username)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_User
                                where p.UserName == username && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_User UpdateUser(tbl_User item)
        {
            try
            {
                tbl_User oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_User
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.UserName = item.UserName;
                        oldItem.Password = item.Password;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;
                        oldItem.UserType_EVID = item.UserType_EVID;

                        context.tbl_User.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_User ChangePassword(Int64 id, Int64 userId, string newpassword)
        {
            try
            {
                tbl_User oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_User
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.Password = newpassword;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;

                        context.tbl_User.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_User ChangePasswordByUserName(string username, Int64 userId, string newpassword)
        {
            try
            {
                tbl_User oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_User
                               where p.UserName == username && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.Password = newpassword;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;

                        context.tbl_User.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tbl_User ValidLogin(string username, string password)
        {
            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_User
                                where p.UserName == username && p.Password == password && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region tbl_Permission

        public tbl_Permission AddPermission(tbl_Permission item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Permission.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Permission DeletePermission(Int64 id, int userId)
        {

            try
            {
                tbl_Permission oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Permission
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Permission.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Permission> GetPermissons()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Permission
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Permission GetPermissionById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Permission
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Permission UpdatePermission(tbl_Permission item)
        {
            try
            {
                tbl_Permission oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Permission
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.RoleId = item.RoleId;
                        oldItem.PermissionModule = item.PermissionModule;
                        oldItem.PermissionName = item.PermissionName;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_Permission.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_Proposal

        public tbl_Proposal AddProposal(tbl_Proposal item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Proposal.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Proposal AddProposalNew(tbl_Proposal proposalItem, List<tbl_ProposalDetail> proposalDetails,
            List<tbl_ProposalUserGroup> proposalUserGroups)
        {
            tbl_Proposal dbItem = null;
            using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        proposalItem.Status = 1;
                        proposalItem.InsertDate = DateTime.Now;
                        proposalItem.UpdateDate = DateTime.Now;
                        dbItem = context.tbl_Proposal.Add(proposalItem);
                        context.SaveChanges();
                        foreach (var proposalDetail in proposalDetails)
                        {
                            proposalDetail.ProposalID = dbItem.ID;
                            proposalDetail.Status = 1;
                            proposalDetail.InsertDate = DateTime.Now;
                            proposalDetail.UpdateDate = DateTime.Now;
                            context.tbl_ProposalDetail.Add(proposalDetail);
                            context.SaveChanges();
                        }
                        foreach (var proposalUserGroup in proposalUserGroups)
                        {
                            proposalUserGroup.ProposalID = dbItem.ID;
                            proposalUserGroup.Status = 1;
                            proposalUserGroup.InsertDate = DateTime.Now;
                            proposalUserGroup.UpdateDate = DateTime.Now;
                            context.tbl_ProposalUserGroup.Add(proposalUserGroup);
                            context.SaveChanges();
                        }

                        transaction.Commit();
                    }

                    catch (Exception ex)

                    {
                        transaction.Rollback();
                        throw ex;

                    }

                }
            }
            return dbItem;
        }
        public tbl_Proposal DeleteProposal(Int64 id, int userId)
        {

            try
            {
                tbl_Proposal oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Proposal
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Proposal.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Proposal> GetProposals()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Proposal
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_Proposal> GetProposalsByProviderID(Int64 providerid)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Proposal
                                 where p.Status == 1 && p.ProviderID == providerid
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_Proposal> GetProposalsByUserID(Int64 userid)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    var items = (from ug in context.tbl_UserGroup
                                 join g in context.tbl_Group on ug.GroupID equals g.ID
                                 join pug in context.tbl_ProposalUserGroup on g.ID equals pug.GroupID
                                 join p in context.tbl_Proposal on pug.ProposalID equals p.ID
                                 where ug.Status == 1 && pug.Status == 1 && p.Status == 1 && ug.UserID == userid
                                 select p);


                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_Proposal> GetProposalsByIsPublic()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    var items = (from p in context.tbl_Proposal
                                 where p.Status == 1 && p.IsPublic == true
                                 select p);


                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Proposal GetProposalById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Proposal
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Proposal UpdateProposal(tbl_Proposal item)
        {
            try
            {
                tbl_Proposal oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Proposal
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.Name = item.Name;
                        oldItem.Description = item.Description;
                        oldItem.Note = item.Note;
                        oldItem.ProviderID = item.ProviderID;
                        oldItem.IsPublic = item.IsPublic;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_Proposal.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_ProviderService

        public tbl_ProviderService AddProviderService(tbl_ProviderService item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProviderService.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProviderService DeleteProviderService(Int64 id, int userId)
        {

            try
            {
                tbl_ProviderService oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProviderService
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProviderService.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProviderService> GetProviderServices()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProviderService
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProviderService GetProviderServiceById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProviderService
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProviderService UpdateProviderService(tbl_ProviderService item)
        {
            try
            {
                tbl_ProviderService oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProviderService
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.ProviderId = item.ProviderId;
                        oldItem.Name = item.Name;
                        oldItem.Description = item.Description;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_ProviderService.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_ProviderUserProposal
        public tbl_ProviderUserProposal AddProviderUserProposal(tbl_ProviderUserProposal item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProviderUserProposal.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProviderUserProposal DeleteProviderUserProposal(Int64 id, int userId)
        {

            try
            {
                tbl_ProviderUserProposal oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProviderUserProposal
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProviderUserProposal.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProviderUserProposal> GetProviderUserProposals()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProviderUserProposal
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProviderUserProposal GetProviderUserProposalById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProviderUserProposal
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProviderUserProposal UpdateProviderUserProposal(tbl_ProviderUserProposal item)
        {
            try
            {
                tbl_ProviderUserProposal oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProviderUserProposal
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.AcceptStatus = item.AcceptStatus;
                        oldItem.UserId = item.UserId;
                        oldItem.ServiceId = item.ServiceId;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_ProviderUserProposal.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_ProviderUserRating
        public tbl_ProviderUserRating AddProviderUserRating(tbl_ProviderUserRating item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProviderUserRating.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProviderUserRating DeleteProviderUserRating(Int64 id, int userId)
        {

            try
            {
                tbl_ProviderUserRating oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProviderUserRating
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProviderUserRating.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProviderUserRating> GetProviderUserRatings()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProviderUserRating
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProviderUserRating GetProviderUserRatingById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProviderUserRating
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProviderUserRating UpdateProviderUserRating(tbl_ProviderUserRating item)
        {
            try
            {
                tbl_ProviderUserRating oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProviderUserRating
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.UserId = item.UserId;
                        oldItem.ServiceId = item.ServiceId;
                        oldItem.RatingValueId = item.RatingValueId;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;
                        oldItem.RatingTimestamp = item.RatingTimestamp;

                        context.tbl_ProviderUserRating.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_RatingType
        public tbl_RatingType AddRatingType(tbl_RatingType item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_RatingType.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_RatingType DeleteRatingType(Int64 id, int userId)
        {

            try
            {
                tbl_RatingType oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_RatingType
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_RatingType.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_RatingType> GetRatingTypes()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_RatingType
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_RatingType GetRatingTypeById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_RatingType
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_RatingType UpdateRatingType(tbl_RatingType item)
        {
            try
            {
                tbl_RatingType oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_RatingType
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.RatingType = item.RatingType;
                        oldItem.Description = item.Description;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_RatingType.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_RatingValue

        public tbl_RatingValue AddRatingValue(tbl_RatingValue item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_RatingValue.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_RatingValue DeleteRatingValue(Int64 id, int userId)
        {

            try
            {
                tbl_RatingValue oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_RatingValue
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_RatingValue.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_RatingValue> GetRatingValues()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_RatingValue
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_RatingValue GetRatingValueById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_RatingValue
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_RatingValue UpdateRatingValue(tbl_RatingValue item)
        {
            try
            {
                tbl_RatingValue oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_RatingValue
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.RatingTypeId = item.RatingTypeId;
                        oldItem.Value = item.Value;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_RatingValue.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_Role

        public tbl_Role AddRole(tbl_Role item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Role.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Role DeleteRole(Int64 id, int userId)
        {

            try
            {
                tbl_Role oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Role
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Role.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Role> GetRoles()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Role
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Role GetRoleById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Role
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Role UpdateRole(tbl_Role item)
        {
            try
            {
                tbl_Role oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Role
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.RoleName = item.RoleName;
                        oldItem.RoleDescription = item.RoleDescription;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_Role.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_UserService
        public tbl_UserService AddUserService(tbl_UserService item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_UserService.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_UserService DeleteUserService(Int64 id, int userId)
        {

            try
            {
                tbl_UserService oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_UserService
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_UserService.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_UserService> GetUserServices()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_UserService
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_UserService GetUserServiceById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_UserService
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_UserService UpdateUserService(tbl_UserService item)
        {
            try
            {
                tbl_UserService oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_UserService
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.UserId = item.UserId;
                        oldItem.ServiceId = item.ServiceId;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_UserService.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_UserServiceRating
        public tbl_UserServiceRating AddUserServiceRating(tbl_UserServiceRating item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_UserServiceRating.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_UserServiceRating DeleteUserServiceRating(Int64 id, int userId)
        {

            try
            {
                tbl_UserServiceRating oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_UserServiceRating
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_UserServiceRating.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_UserServiceRating> GetUserServiceRatings()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_UserServiceRating
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_UserServiceRating GetUserServiceRatingById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_UserServiceRating
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_UserServiceRating UpdateUserServiceRating(tbl_UserServiceRating item)
        {
            try
            {
                tbl_UserServiceRating oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_UserServiceRating
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.UserId = item.UserId;
                        oldItem.ServiceId = item.ServiceId;
                        oldItem.RatingValueId = item.RatingValueId;
                        oldItem.RatingTimestamp = item.RatingTimestamp;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_UserServiceRating.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_EnumCategory
        public tbl_EnumCategory AddEnumCategory(tbl_EnumCategory item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_EnumCategory.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_EnumCategory DeleteEnumCategory(Int64 id, Int64 userId)
        {

            try
            {
                tbl_EnumCategory oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_EnumCategory
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_EnumCategory.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_EnumCategory> GetEnumCategorys()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_EnumCategory
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_EnumCategory GetEnumCategoryById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_EnumCategory
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_EnumCategory GetEnumCategoryByName(string name)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_EnumCategory
                                where p.Name == name && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_EnumCategory UpdateEnumCategory(tbl_EnumCategory item)
        {
            try
            {
                tbl_EnumCategory oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_EnumCategory
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.Name = item.Name;
                        oldItem.Code = item.Code;
                        oldItem.Description = item.Description;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_EnumCategory.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_EnumValue
        public tbl_EnumValue AddEnumValue(tbl_EnumValue item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_EnumValue.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_EnumValue DeleteEnumValue(Int64 id, Int64 userId)
        {

            try
            {
                tbl_EnumValue oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_EnumValue
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_EnumValue.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_EnumValue> GetEnumValues()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_EnumValue
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_EnumValue> GetEnumValuesByEnumCategoryID(Int64 enumCategoryID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_EnumValue
                                 where p.Status == 1 && p.EnumCategoryID == enumCategoryID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_EnumValue> GetEnumValuesByEnumCategoryCode(string enumCategoryCode)
        {

            try
            {
                Int64 enumCategoryID = 0;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var item = (from ec in context.tbl_EnumCategory
                                where ec.Status == 1 && ec.Code == enumCategoryCode
                                select ec).FirstOrDefault();

                    enumCategoryID = item.ID;

                }
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_EnumValue
                                 where p.Status == 1 && p.EnumCategoryID == enumCategoryID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_EnumValue GetEnumValueById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_EnumValue
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_EnumValue GetEnumValueByName(string name)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_EnumValue
                                where p.Name == name && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_EnumValue UpdateEnumValue(tbl_EnumValue item)
        {
            try
            {
                tbl_EnumValue oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_EnumValue
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.Name = item.Name;
                        oldItem.Code = item.Code;
                        oldItem.Description = item.Description;
                        oldItem.EnumCategoryID = item.EnumCategoryID;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_EnumValue.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_Region
        public tbl_Region AddRegion(tbl_Region item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Region.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Region DeleteRegion(Int64 id, int userId)
        {

            try
            {
                tbl_Region oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Region
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Region.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Region> GetRegions()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Region
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_Region> GetRegionsByType(int type)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Region
                                 where p.Status == 1 && p.Type == type
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Region GetRegionById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Region
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Region UpdateRegion(tbl_Region item)
        {
            try
            {
                tbl_Region oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Region
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.Name = item.Name;
                        oldItem.Type = item.Type;
                        oldItem.ParentId = item.ParentId;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_Region.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_Package
        public tbl_Package AddPackage(tbl_Package item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Package.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Package DeletePackage(Int64 id, Int64 userId)
        {

            try
            {
                tbl_Package oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Package
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Package.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Package> GetPackages()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Package
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_Package> GetPackagesByMobileEVID(Int64 mobileEVID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Package
                                 where p.Status == 1 && p.Mobile_EVID == mobileEVID
                                 select p).OrderBy(x => x.PackageSize);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Package GetPackageByID(Int64 id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Package
                                where p.ID == id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Package UpdatePackage(tbl_Package item)
        {
            try
            {
                tbl_Package oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Package
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.Mobile_EVID = item.Mobile_EVID;
                        oldItem.PackageName = item.PackageName;
                        oldItem.PackageSize = item.PackageSize;
                        oldItem.Validity = item.Validity;
                        oldItem.ValidityDesc = item.ValidityDesc;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_Package.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_PackagePrice
        public tbl_PackagePrice AddPackagePrice(tbl_PackagePrice item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_PackagePrice.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_PackagePrice DeletePackagePrice(Int64 id, Int64 userId)
        {

            try
            {
                tbl_PackagePrice oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_PackagePrice
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_PackagePrice.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_PackagePrice> GetPackagePrices()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_PackagePrice
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_PackagePrice> GetPackagePricesByPackageID(Int64 packageID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_PackagePrice
                                 where p.Status == 1 && p.PackageID == packageID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_PackagePrice GetPackagePriceByID(Int64 id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_PackagePrice
                                where p.ID == id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_PackagePrice UpdatePackagePrice(tbl_PackagePrice item)
        {
            try
            {
                tbl_PackagePrice oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_PackagePrice
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.Source_EVID = item.Source_EVID;
                        oldItem.PackageID = item.PackageID;
                        oldItem.BeginDate = item.BeginDate;
                        oldItem.EndDate = item.EndDate;
                        oldItem.Price = item.Price;
                        oldItem.Point = item.Point;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_PackagePrice.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_NetConsume
        public tbl_NetConsume AddNetConsume(tbl_NetConsume item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_NetConsume.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_NetConsume DeleteNetConsume(Int64 id, int userId)
        {

            try
            {
                tbl_NetConsume oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_NetConsume
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_NetConsume.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_NetConsume> GetNetConsumes(Int64 userId, Int64 sourceEV, Int64 mobileEV)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_NetConsume
                                 where p.Status == 1 && p.UserId == userId && p.Source_EVID == sourceEV && p.Mobile_EVID == mobileEV
                                 select p);

                    return items.OrderByDescending(x => x.Year).OrderByDescending(y => y.Month).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<tbl_NetConsume> GetNetConsumesByYear(Int64 userId, Int64 sourceEV, Int64 mobileEV, int year)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_NetConsume
                                 where p.Status == 1 && p.UserId == userId && p.Source_EVID == sourceEV
                                 && p.Mobile_EVID == mobileEV && p.Year == year
                                 select p);

                    return items.OrderBy(y => y.Month).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_NetConsume> GetNetConsumesByUserID(Int64 userID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_NetConsume
                                 where p.Status == 1 && p.UserId == userID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_NetConsume> GetNetConsumesByUserIDAndYear(Int64 userID, int year)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_NetConsume
                                 where p.Status == 1 && p.UserId == userID && p.Year == year
                                 select p);

                    return items.OrderByDescending(x => x.Month).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_NetConsume GetNetConsumeByID(Int64 id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_NetConsume
                                where p.ID == id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_NetConsume UpdateNetConsume(tbl_NetConsume item)
        {
            try
            {
                tbl_NetConsume oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_NetConsume
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.UserId = item.UserId;
                        oldItem.Source_EVID = item.Source_EVID;
                        oldItem.Year = item.Year;
                        oldItem.Month = item.Month;
                        oldItem.Hour = item.Hour;
                        oldItem.Minute = item.Minute;
                        oldItem.Consumed = item.Consumed;
                        oldItem.Speed = item.Speed;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;


                        context.tbl_NetConsume.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_ProposalDetail
        public tbl_ProposalDetail AddProposalDetail(tbl_ProposalDetail item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProposalDetail.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProposalDetail DeleteProposalDetail(Int64 id, int userId)
        {

            try
            {
                tbl_ProposalDetail oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProposalDetail
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProposalDetail.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalDetail> GetProposalDetails()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalDetail
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_ProposalDetail> GetProposalDetailsByProposalID(Int64 proposalID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalDetail
                                 where p.Status == 1 && p.ProposalID == proposalID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProposalDetail GetProposalDetailByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalDetail
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProposalDetail UpdateProposalDetail(tbl_ProposalDetail item)
        {
            try
            {
                tbl_ProposalDetail oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProposalDetail
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.ProposalID = item.ProposalID;
                        oldItem.ProposolKey = item.ProposolKey;
                        oldItem.ProposolValue = item.ProposolValue;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_ProposalDetail.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_Group
        public tbl_Group AddGroup(tbl_Group item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Group.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Group DeleteGroup(Int64 id, int userId)
        {

            try
            {
                tbl_Group oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Group
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Group.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Group> GetGroups()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Group
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Group GetGroupByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Group
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Group GetGroupByName(string name)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Group
                                where p.Name == name && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Group UpdateGroup(tbl_Group item)
        {
            try
            {
                tbl_Group oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Group
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.Name = item.Name;
                        oldItem.Description = item.Description;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_Group.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_User> GetUsersByGroupID(Int64 groupid)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from g in context.tbl_Group
                                 join ug in context.tbl_UserGroup on g.ID equals ug.GroupID
                                 join user in context.tbl_User on ug.UserID equals user.ID
                                 where g.Status == 1 && ug.Status == 1 && user.Status == 1
                                 && g.ID == groupid
                                 select user).ToList();

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_Group> GetGroupsByUserID(Int64 userid)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from usr in context.tbl_User
                                 join ug in context.tbl_UserGroup on usr.ID equals ug.UserID
                                 join g in context.tbl_Group on ug.GroupID equals g.ID
                                 where g.Status == 1 && ug.Status == 1 && usr.Status == 1
                                 && usr.ID == userid
                                 select g).ToList();

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion


        #region tbl_UserGroup
        public tbl_UserGroup AddUserGroup(tbl_UserGroup item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_UserGroup.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_UserGroup DeleteUserGroup(Int64 id, int userId)
        {

            try
            {
                tbl_UserGroup oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_UserGroup
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_UserGroup.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_UserGroup> GetUserGroups()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_UserGroup
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_UserGroup GetUserGroupByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_UserGroup
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_UserGroup UpdateUserGroup(tbl_UserGroup item)
        {
            try
            {
                tbl_UserGroup oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_UserGroup
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.GroupID = item.GroupID;
                        oldItem.UserID = item.UserID;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_UserGroup.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_ProposalUserGroup
        public tbl_ProposalUserGroup AddProposalUserGroup(tbl_ProposalUserGroup item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProposalUserGroup.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProposalUserGroup DeleteProposalUserGroup(Int64 id, int userId)
        {

            try
            {
                tbl_ProposalUserGroup oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProposalUserGroup
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProposalUserGroup.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalUserGroup> GetProposalUserGroups()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserGroup
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProposalUserGroup GetProposalUserGroupByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalUserGroup
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProposalUserGroup UpdateProposalUserGroup(tbl_ProposalUserGroup item)
        {
            try
            {
                tbl_ProposalUserGroup oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProposalUserGroup
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.ProposalID = item.ProposalID;
                        oldItem.GroupID = item.GroupID;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_ProposalUserGroup.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Proposal> GetProposalsByGroupID(Int64 groupid)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from g in context.tbl_Group
                                 join pug in context.tbl_ProposalUserGroup on g.ID equals pug.GroupID
                                 join pr in context.tbl_Proposal on pug.ProposalID equals pr.ID
                                 where g.Status == 1 && pug.Status == 1 && pr.Status == 1
                                 && g.ID == groupid
                                 select pr).ToList();

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_Group> GetGroupsByPropsalID(Int64 propsalid)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from pr in context.tbl_Proposal
                                 join pug in context.tbl_ProposalUserGroup on pr.ID equals pug.ProposalID
                                 join g in context.tbl_Group on pug.GroupID equals g.ID
                                 where g.Status == 1 && pug.Status == 1 && pr.Status == 1
                                 && pr.ID == propsalid
                                 select g).ToList();

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_ProposalUserGroup> GetProposalUserGroupsByProposalID(Int64 proposalID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserGroup
                                 where p.Status == 1 && p.ProposalID == proposalID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region AddSMSDetail
        public tbl_SMSDetail AddSMSDetail(tbl_SMSDetail item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_SMSDetail.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_SMSDetail DeleteSMSDetail(Int64 id, int userId)
        {

            try
            {
                tbl_SMSDetail oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_SMSDetail
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_SMSDetail.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_SMSDetail> GetSMSDetails()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_SMSDetail
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_SMSDetail> GetSMSDetailsByModelID(Int64 modelID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_SMSDetail
                                 where p.Status == 1 && p.SMSModelID == modelID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_SMSDetail GetSMSDetailByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_SMSDetail
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_SMSDetail UpdateSMSDetail(tbl_SMSDetail item)
        {
            try
            {
                tbl_SMSDetail oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_SMSDetail
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.SenderName = item.SenderName;
                        oldItem.SenderPhoneNumber = item.SenderPhoneNumber;
                        oldItem.RecievedDate = item.RecievedDate;
                        oldItem.SendDate = item.SendDate;
                        oldItem.Message = item.Message;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_SMSDetail.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }


        #endregion
        #region tbl_SMSModel
        public List<tbl_SMSModel> GetSMSModels()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_SMSModel
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_SMSModel GetSMSModelByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_SMSModel
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_SMSModel AddSMSModel(tbl_SMSModel smsModel, List<tbl_SMSDetail> smsDetails)
        {
            tbl_SMSModel dbItem = null;
            using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        smsModel.Status = 1;
                        smsModel.InsertDate = DateTime.Now;
                        smsModel.UpdateDate = DateTime.Now;
                        dbItem = context.tbl_SMSModel.Add(smsModel);
                        context.SaveChanges();
                        foreach (var smsDetail in smsDetails)
                        {
                            smsDetail.SMSModelID = dbItem.ID;
                            smsDetail.Status = 1;
                            smsDetail.InsertDate = DateTime.Now;
                            smsDetail.UpdateDate = DateTime.Now;
                            context.tbl_SMSDetail.Add(smsDetail);
                            context.SaveChanges();
                        }


                        transaction.Commit();
                    }

                    catch (Exception ex)

                    {
                        transaction.Rollback();
                        throw ex;

                    }

                }
            }
            return dbItem;
        }
        public tbl_SMSModel UpdateSMSModel(tbl_SMSModel item)
        {
            try
            {
                tbl_SMSModel oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_SMSModel
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.UserID = item.UserID;
                        oldItem.TotalMessageCount = item.TotalMessageCount;
                        oldItem.ShortMessageCount = item.ShortMessageCount;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_SMSModel.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_SMSModel DeleteSMSModel(Int64 id, int userId)
        {

            try
            {
                tbl_SMSModel oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_SMSModel
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_SMSModel.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_ProposalUserState
        public tbl_ProposalUserState AddProposalUserState(tbl_ProposalUserState item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProposalUserState.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProposalUserState DeleteProposalUserState(Int64 id, int userId)
        {

            try
            {
                tbl_ProposalUserState oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProposalUserState
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProposalUserState.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalUserState> GetProposalUserStates()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserState
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_ProposalUserState> GetProposalUserStatesByProposalID(Int64 proposalID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserState
                                 where p.Status == 1 && p.ProposalID == proposalID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_ProposalUserState> GetProposalUserStatesByUserID(Int64 userID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserState
                                 where p.Status == 1 && p.UserID == userID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_ProposalUserState> GetProposalUserStatesByProviderStateType(Int64 providerStateType)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserState
                                 where p.Status == 1 && p.ProviderStateType == providerStateType
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_ProposalUserState> GetProposalUserStatesByUserStateType(Int64 userStateType)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserState
                                 where p.Status == 1 && p.UserStateType == userStateType
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProposalUserState GetProposalUserStateByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalUserState
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProposalUserState UpdateProposalUserState(tbl_ProposalUserState item)
        {
            try
            {
                tbl_ProposalUserState oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProposalUserState
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.ProposalID = item.ProposalID;
                        oldItem.UserID = item.UserID;
                        oldItem.ProviderOfferAmount = item.ProviderOfferAmount;
                        oldItem.ProviderOfferMonth = item.ProviderOfferMonth;
                        oldItem.UserDemandAmount = item.UserDemandAmount;
                        oldItem.UserDemandMonth = item.UserDemandMonth;
                        oldItem.ProviderStateType = item.ProviderStateType;
                        oldItem.UserStateType = item.UserStateType;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_ProposalUserState.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_SMSSenderInfo
        public List<tbl_SMSSenderInfo> GetSMSSenderInfos()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_SMSSenderInfo
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_SMSSenderInfo GetSMSSenderInfoByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_SMSSenderInfo
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_SMSSenderInfo GetSMSSenderInfoByName(string senderName)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_SMSSenderInfo
                                where p.SenderName == senderName && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_SMSSenderInfo AddSMSSenderInfo(tbl_SMSSenderInfo item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_SMSSenderInfo.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_SMSSenderInfo UpdateSMSSenderInfo(tbl_SMSSenderInfo item)
        {
            try
            {
                tbl_SMSSenderInfo oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_SMSSenderInfo
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.SenderName = item.SenderName;
                        oldItem.Number = item.Number;
                        oldItem.ActivityType = item.ActivityType;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;
                        context.tbl_SMSSenderInfo.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_SMSSenderInfo DeleteSMSSenderInfo(Int64 id, int userId)
        {

            try
            {
                tbl_SMSSenderInfo oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_SMSSenderInfo
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_SMSSenderInfo.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion


        #region tbl_ProposalDocument
        public tbl_ProposalDocument AddProposalDocument(tbl_ProposalDocument item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProposalDocument.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProposalDocument DeleteProposalDocument(Int64 id, int userId)
        {

            try
            {
                tbl_ProposalDocument oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProposalDocument
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProposalDocument.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalDocument> GetProposalDocuments()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalDocument
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_ProposalDocument> GetProposalDocumentsByProposalID(Int64 proposalID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalDocument
                                 where p.Status == 1 && p.ProposalID == proposalID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProposalDocument GetProposalDocumentByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalDocument
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProposalDocument UpdateProposalDocument(tbl_ProposalDocument item)
        {
            try
            {
                tbl_ProposalDocument oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProposalDocument
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.ImageLinkPath = item.ImageLinkPath;
                        oldItem.ImageLinkName = item.ImageLinkName;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_ProposalDocument.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_UserDocument
        public tbl_UserDocument AddUserDocument(tbl_UserDocument item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_UserDocument.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_UserDocument DeleteUserDocument(Int64 id, int userId)
        {

            try
            {
                tbl_UserDocument oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_UserDocument
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_UserDocument.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_UserDocument> GetUserDocuments()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_UserDocument
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_UserDocument> GetUserDocumentsByUserID(Int64 userID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_UserDocument
                                 where p.Status == 1 && p.UserID == userID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_UserDocument> GetUserDocumentsByUserIDAndImageTypeEVID(Int64 userID, int imageType_EVID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_UserDocument
                                 where p.Status == 1 && p.UserID == userID && p.ImageType_EVID == imageType_EVID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_UserDocument GetUserDocumentByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_UserDocument
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_UserDocument UpdateUserDocument(tbl_UserDocument item)
        {
            try
            {
                tbl_UserDocument oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_UserDocument
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.ImageLinkPath = item.ImageLinkPath;
                        oldItem.ImageLinkName = item.ImageLinkName;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_UserDocument.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_CALLModel
        public List<tbl_CALLModel> GetCALLModels()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_CALLModel
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_CALLModel GetCALLModelByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_CALLModel
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_CALLModel AddCALLModel(tbl_CALLModel callModel, List<tbl_CALLDetail> callDetails)
        {
            tbl_CALLModel dbItem = null;
            using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
            {

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {

                        callModel.Status = 1;
                        callModel.InsertDate = DateTime.Now;
                        callModel.UpdateDate = DateTime.Now;
                        dbItem = context.tbl_CALLModel.Add(callModel);
                        context.SaveChanges();
                        foreach (var callDetail in callDetails)
                        {
                            callDetail.CALLModelID = dbItem.ID;
                            callDetail.Status = 1;
                            callDetail.InsertDate = DateTime.Now;
                            callDetail.UpdateDate = DateTime.Now;
                            context.tbl_CALLDetail.Add(callDetail);
                            context.SaveChanges();
                        }


                        transaction.Commit();
                    }

                    catch (Exception ex)

                    {
                        transaction.Rollback();
                        throw ex;

                    }

                }
            }
            return dbItem;
        }
        public tbl_CALLModel UpdateCALLModel(tbl_CALLModel item)
        {
            try
            {
                tbl_CALLModel oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_CALLModel
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.UserID = item.UserID;
                        oldItem.TotalCallCount = item.TotalCallCount;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_CALLModel.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_CALLModel DeleteCALLModel(Int64 id, int userId)
        {

            try
            {
                tbl_CALLModel oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_CALLModel
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_CALLModel.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region CALLDetail
        public tbl_CALLDetail AddCALLDetail(tbl_CALLDetail item)
        {

            try
            {
                using (DB_A62358_ScoreMeEntities context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_CALLDetail.Add(item);
                    context.SaveChanges();
                    return item;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_CALLDetail DeleteCALLDetail(Int64 id, int userId)
        {

            try
            {
                tbl_CALLDetail oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_CALLDetail
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_CALLDetail.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_CALLDetail> GetCALLDetails()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_CALLDetail
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<tbl_CALLDetail> GetCALLDetailsByModelID(Int64 modelID)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_CALLDetail
                                 where p.Status == 1 && p.CALLModelID == modelID
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_CALLDetail GetCALLDetailByID(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_CALLDetail
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_CALLDetail UpdateCALLDetail(tbl_CALLDetail item)
        {
            try
            {
                tbl_CALLDetail oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_CALLDetail
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {

                        oldItem.PhoneNumber = item.PhoneNumber;
                        oldItem.Duration = item.Duration;
                        oldItem.RecievedDate = item.RecievedDate;
                        oldItem.SendDate = item.SendDate;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;

                        context.tbl_CALLDetail.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_Employee
        public tbl_Employee AddEmployee(tbl_Employee item)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_Employee.Add(item);
                    context.SaveChanges();
                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_Employee DeleteEmployee(int id, int userId)
        {

            try
            {
                tbl_Employee oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_Employee
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_Employee.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_Employee> GetEmployees()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_Employee
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_Employee GetEmployeeById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Employee
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Employee UpdateEmployee(tbl_Employee item)
        {
            try
            {
                tbl_Employee oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_Employee
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.FirstName = item.FirstName;
                        oldItem.LastName = item.LastName;
                        oldItem.FatherName = item.FatherName;
                        oldItem.GenderType = item.GenderType;
                        oldItem.UserId = item.UserId;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;
                        context.tbl_Employee.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_Employee GetEmployeeByUserId(Int64 userId)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_Employee
                                where p.UserId == userId && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region AccessRights
        public bool AddAccessRights(tbl_AccessRight ar, out string ErrorMessage)
        {
            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    AccessRightsRepository ARR = new AccessRightsRepository();
                    bool accessrightscheck = ARR.CheckAccessRights(0, ar.UserId, ar.Controller, ar.Action, 0);
                    if (accessrightscheck)
                    {
                        ErrorMessage = "Bu icazə artıq daxil edilib.";
                        return false;
                    }
                    ar.Status = 1;
                    context.tbl_AccessRight.Add(ar);
                    if (context.SaveChanges() == 0)
                    {
                        ErrorMessage = "İcazəni əlavə edərkən xəta baş verdi. Zəhmət olmasa yenidən cəhd edin.";
                        return false;
                    }
                    ErrorMessage = "İcazəni uğurla əlavə edilmiştir";
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool UpdateAccessRights(tbl_AccessRight ar, out string ErrorMessage)
        {
            DB_A62358_ScoreMeEntities entities = new DB_A62358_ScoreMeEntities();
            tbl_AccessRight uar = entities.tbl_AccessRight.Where(x => x.Status == 1 && x.ID == ar.ID).FirstOrDefault();
            try
            {
                AccessRightsRepository ARR = new AccessRightsRepository();
                bool accessrightscheck = ARR.CheckAccessRights(ar.ID, ar.UserId, ar.Controller, ar.Action, ar.HasAccess);
                if (accessrightscheck)
                {
                    ErrorMessage = "Bu icazə adı artıq daxil edilib.";
                    return false;
                }
                uar.UserId = ar.UserId;
                uar.Controller = ar.Controller;
                uar.ControllerDesc = ar.ControllerDesc;
                uar.ActionDesc = ar.ActionDesc;
                uar.Action = ar.Action;
                uar.HasAccess = ar.HasAccess;
                entities.tbl_AccessRight.Attach(uar);
                entities.Entry(uar).State = System.Data.Entity.EntityState.Modified;
                if (entities.SaveChanges() == 0)
                {
                    ErrorMessage = "İstifadəçi icazəsini redaktə edərkən xəta baş verdi. Zəhmət olmasa yenidən cəhd edin.";
                    return false;
                }
                ErrorMessage = "İcazə uğurla redaktə edilmişdir";
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                entities.Dispose();
            }
        }
        public bool DeleteAccessRights(int Id, int userId, out string ErrorMessage)
        {
            try
            {
                tbl_AccessRight oar;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oar = (from ar in context.tbl_AccessRight
                           where ar.ID == Id
                           select ar).FirstOrDefault();
                }
                if (oar != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oar.Status = 0;
                        oar.UpdateDate = DateTime.Now;
                        oar.UpdateUser = userId;
                        context.tbl_AccessRight.Attach(oar);
                        context.Entry(oar).State = System.Data.Entity.EntityState.Modified;
                        if (context.SaveChanges() == 0)
                        {
                            ErrorMessage = "İstifadəçi icazəsini silərkən xəta baş verdi. Zəhmət olmasa yenidən cəhd edin";
                            return false;
                        }
                        ErrorMessage = "Icazə uğurla silinmişdir";
                        return true;
                    }
                }
                else
                {
                    Exception e = new Exception(Id.ToString() + " İD-li istifadəçi icazəsi yoxdur!");
                    throw e;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public tbl_AccessRight GetAccessRight(int Id)
        {
            DB_A62358_ScoreMeEntities entities = new DB_A62358_ScoreMeEntities();
            tbl_AccessRight ar = entities.tbl_AccessRight.Where(x => x.Status == 1 && x.ID == Id).FirstOrDefault();
            entities.Dispose();
            return ar;
        }
        public List<tbl_AccessRight> GetAccessRights()
        {
            DB_A62358_ScoreMeEntities entities = new DB_A62358_ScoreMeEntities();
            List<tbl_AccessRight> ARL = entities.tbl_AccessRight.Where(x => x.Status == 1).ToList();
            entities.Dispose();
            return ARL;
        }
        #endregion

        #region DataChange Log
        public bool SaveToLog_DataChange(int UserId, int OperationType, string TableName, int OriginalId, List<MyTypes.DataChangeLog> Changes)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.ConnectionString_Log);
            connection.Open();
            string query;
            try
            {
                if (OperationType == 1 | OperationType == 3)
                {
                    query = @"INSERT INTO [dbo].[log_dataChange] (UserId, OperationType, TableName, OriginalId, OperationDateTime)
                            VALUES(@UserId, @OperationType, @TableName, @OriginalId, GETDATE())";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@OperationType", OperationType);
                    cmd.Parameters.AddWithValue("@TableName", TableName);
                    cmd.Parameters.AddWithValue("@OriginalId", OriginalId);
                    cmd.CommandType = System.Data.CommandType.Text;
                    int result = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    bool r = true;
                    foreach (MyTypes.DataChangeLog Change in Changes)
                    {
                        query = @"INSERT INTO [dbo].[log_dataChange] (UserId, OperationType, TableName, OriginalId, ColumnName, OldValue, NewValue, OperationDateTime)
                                VALUES(@UserId, @OperationType, @TableName, @OriginalId, @ColumnName, @OldValue, @NewValue, GETDATE())";
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@OperationType", OperationType);
                        cmd.Parameters.AddWithValue("@TableName", TableName);
                        cmd.Parameters.AddWithValue("@OriginalId", OriginalId);
                        cmd.Parameters.AddWithValue("@ColumnName", Change.ColumnName);
                        cmd.Parameters.AddWithValue("@OldValue", Change.OldValue);
                        cmd.Parameters.AddWithValue("@NewValue", Change.NewValue);
                        int eachresult = (int)cmd.ExecuteNonQuery();
                        if (eachresult == 0)
                        {
                            r = false;
                        }
                        cmd.Dispose();
                    }
                    return r;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        #endregion

        #region tbl_ProposalUserSave
        public tbl_ProposalUserSave AddProposalUserSave(tbl_ProposalUserSave item)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProposalUserSave.Add(item);
                    context.SaveChanges();
                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProposalUserSave DeleteProposalUserSave(Int64 id, Int64 userId)
        {

            try
            {
                tbl_ProposalUserSave oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProposalUserSave
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProposalUserSave.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalUserSave> GetProposalUserSaves()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalUserSave
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProposalUserSave GetProposalUserSaveById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalUserSave
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProposalUserSave UpdateProposalUserSave(tbl_ProposalUserSave item)
        {
            try
            {
                tbl_ProposalUserSave oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProposalUserSave
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.ProposalID = item.ProposalID;
                        oldItem.UserID = item.UserID;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;
                        context.tbl_ProposalUserSave.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalUserSave> GetProposalUserSavesByUserId(Int64 userId)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalUserSave
                                where p.UserID == userId && p.Status == 1
                                select p).ToList();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public List<tbl_ProposalUserSave> GetProposalUserSavesByProposalId(Int64 proposalId)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalUserSave
                                where p.ProposalID == proposalId && p.Status == 1
                                select p).ToList();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion
        #region tbl_ProposalLikeDislike
        public tbl_ProposalLikeDislike AddProposalLikeDislike(tbl_ProposalLikeDislike item)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProposalLikeDislike.Add(item);
                    context.SaveChanges();
                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProposalLikeDislike DeleteProposalLikeDislike(Int64 id, Int64 userId)
        {

            try
            {
                tbl_ProposalLikeDislike oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProposalLikeDislike
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProposalLikeDislike.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalLikeDislike> GetProposalLikeDislikes()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalLikeDislike
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProposalLikeDislike GetProposalLikeDislikeById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalLikeDislike
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProposalLikeDislike UpdateProposalLikeDislike(tbl_ProposalLikeDislike item)
        {
            try
            {
                tbl_ProposalLikeDislike oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProposalLikeDislike
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.ProposalID = item.ProposalID;
                        oldItem.UserID = item.UserID;
                        oldItem.IsLike = item.IsLike;
                        oldItem.IsDislike = item.IsDislike;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;
                        context.tbl_ProposalLikeDislike.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public Int64 GetProposalLikeCountByUserId(Int64 userId)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalLikeDislike
                                where p.UserID == userId && p.Status == 1 && p.IsLike == 1
                                select p).ToList().Count;

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public int GetProposalLikeDislikeCountByProposalId(Int64 proposalId, out int dislikecount)
        {
            int likecount = 0;
            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    likecount = (from p in context.tbl_ProposalLikeDislike
                                 where p.ProposalID == proposalId && p.Status == 1 && p.IsLike == 1
                                 select p).ToList().Count;


                    dislikecount = (from p in context.tbl_ProposalLikeDislike
                                    where p.ProposalID == proposalId && p.Status == 1 && p.IsDislike == 1
                                    select p).ToList().Count;

                    return likecount;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

        #region tbl_ProposalCommission
        public tbl_ProposalCommission AddProposalCommission(tbl_ProposalCommission item)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    item.Status = 1;
                    item.InsertDate = DateTime.Now;
                    item.UpdateDate = DateTime.Now;
                    context.tbl_ProposalCommission.Add(item);
                    context.SaveChanges();
                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public tbl_ProposalCommission DeleteProposalCommission(Int64 id, Int64 userId)
        {

            try
            {
                tbl_ProposalCommission oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {

                    oldItem = (from p in context.tbl_ProposalCommission
                               where p.ID == id && p.Status == 1
                               select p).FirstOrDefault();

                }

                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {
                        oldItem.Status = 0;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = userId;
                        context.tbl_ProposalCommission.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();

                    }
                }

                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }
                return oldItem;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalCommission> GetProposalCommissions()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_ProposalCommission
                                 where p.Status == 1
                                 select p);

                    return items.ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public tbl_ProposalCommission GetProposalCommissionById(Int64 Id)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalCommission
                                where p.ID == Id && p.Status == 1
                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public tbl_ProposalCommission UpdateProposalCommission(tbl_ProposalCommission item)
        {
            try
            {
                tbl_ProposalCommission oldItem;
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    oldItem = (from p in context.tbl_ProposalCommission
                               where p.ID == item.ID && p.Status == 1
                               select p).FirstOrDefault();

                }
                if (oldItem != null)
                {
                    using (var context = new DB_A62358_ScoreMeEntities())
                    {


                        oldItem.ProposalID = item.ProposalID;
                        oldItem.Commission = item.Commission;
                        oldItem.UpdateDate = DateTime.Now;
                        oldItem.UpdateUser = item.UpdateUser;
                        context.tbl_ProposalCommission.Attach(oldItem);
                        context.Entry(oldItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                        return oldItem;
                    }
                }
                else
                {
                    Exception ex = new Exception("Bu nomrede setir recor yoxdur");
                    throw ex;
                }


            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<tbl_ProposalCommission> GetProposalCommissionByProviderId(Int64 providerId)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from pc in context.tbl_ProposalCommission
                                join p in context.tbl_Proposal on pc.ProposalID equals p.ID
                                where p.ProviderID == providerId && p.Status == 1 && pc.Status == 1
                                select pc).ToList();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public tbl_ProposalCommission GetProposalCommissionByProposalId(Int64 proposalId)
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_ProposalCommission
                                where p.ProposalID == proposalId && p.Status == 1

                                select p).FirstOrDefault();

                    return item;

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        #endregion

    }
}
