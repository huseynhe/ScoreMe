using ScoreMe.DAL.DBModel;
using System;
using System.Collections.Generic;
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
                                    where u.UserName == username && u.Status == 1 && c.Status == 1
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
        public tbl_User DeleteUser(Int64 id, int userId)
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
        public tbl_User ChangePassword(Int64 id, Int64 userId,string newpassword)
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
        public tbl_User ValidLogin(string username,string password) {
            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {


                    var item = (from p in context.tbl_User
                                where p.UserName == username && p.Password==password && p.Status == 1
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
                        oldItem.ServiceId = item.ServiceId;
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
        public tbl_EnumCategory DeleteEnumCategory(Int64 id, int userId)
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
        public tbl_EnumValue DeleteEnumValue(Int64 id, int userId)
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
                                 where p.Status == 1 && p.EnumCategoryID== enumCategoryID
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
        public tbl_Package DeletePackage(Int64 id, int userId)
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
                                 select p);

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
        public tbl_PackagePrice DeletePackagePrice(Int64 id, int userId)
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
        public List<tbl_NetConsume> GetNetConsumes()
        {

            try
            {
                using (var context = new DB_A62358_ScoreMeEntities())
                {
                    var items = (from p in context.tbl_NetConsume
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

    }
}
