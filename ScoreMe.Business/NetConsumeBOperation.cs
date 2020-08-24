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
    public class NetConsumeBOperation
    {
        public BaseOutput GetMontlyAverage(Int64 userId, Int64 sourceEV,Int64 mobileEV,  int year, int lastMontCount, int firstMountCount, int startMont, int endMonth, out decimal _monthlyAverage)
        {
            CRUDOperation operation = new CRUDOperation();
            BaseOutput baseOutput;
            _monthlyAverage = 0;
        
            try
            {

                if (year == 0)
                {

                    if (lastMontCount > 0)
                    {
                        decimal totalConsumed = 0;
                        decimal avreageConsumed = 0;
                        List<tbl_NetConsume> lists = operation.GetNetConsumes(userId, sourceEV,mobileEV).Take(lastMontCount).ToList<tbl_NetConsume>(); ;
                        foreach (tbl_NetConsume item in lists)
                        {
                            totalConsumed = totalConsumed + (item.Consumed==null?0:(decimal)+item.Consumed);
                        }
                        avreageConsumed = (totalConsumed / lastMontCount) *1024;
                        _monthlyAverage = GetDataPrice(avreageConsumed, mobileEV);
                    }
                   
               
                    return baseOutput = new BaseOutput(true, BOResultTypes.Success.GetHashCode(), BOBaseOutputResponse.SuccessResponse, "");

                }

                else
                {
                    if (firstMountCount > 0)
                    {
                        decimal totalConsumed = 0;
                        decimal avreageConsumed = 0;
                        List<tbl_NetConsume> lists  = operation.GetNetConsumesByYear(userId, sourceEV, mobileEV,year).Take(firstMountCount).ToList<tbl_NetConsume>(); ;
                        foreach (tbl_NetConsume item in lists)
                        {
                            totalConsumed = totalConsumed + (item.Consumed == null ? 0 : (decimal)+item.Consumed);
                        }
                        avreageConsumed = (totalConsumed / firstMountCount) * 1024;
                        _monthlyAverage = GetDataPrice(avreageConsumed, mobileEV);
                    }

                    if (startMont>0 &&endMonth>= startMont)
                    {
                        decimal totalConsumed = 0;
                        decimal avreageConsumed = 0;
                        List<tbl_NetConsume> lists = operation.GetNetConsumesByYear(userId, sourceEV, mobileEV, year).Where(x => x.Month >= startMont && x.Month <= endMonth).ToList<tbl_NetConsume>();
                        foreach (tbl_NetConsume item in lists)
                        {
                            totalConsumed = totalConsumed + (item.Consumed == null ? 0 : (decimal)+item.Consumed);
                        }
                        int count = endMonth - startMont + 1;
                        avreageConsumed = (totalConsumed / count) * 1024;
                        _monthlyAverage = GetDataPrice(avreageConsumed, mobileEV);
                    }
                    return baseOutput = new BaseOutput(true, BOResultTypes.NotFound.GetHashCode(), BOBaseOutputResponse.NotFoundResponse, "");

                }


            }
            catch (Exception ex)
            {

                _monthlyAverage = 0;
                return baseOutput = new BaseOutput(false, BOResultTypes.Danger.GetHashCode(), BOBaseOutputResponse.DangerResponse, ex.Message);
            }
        }


        public decimal GetDataPrice(decimal itemData, Int64 mobileEV) 
        {
            decimal dataPrice = 0;
            CRUDOperation operation = new CRUDOperation();
            List<tbl_Package> packages = operation.GetPackagesByMobileEVID(mobileEV);

            foreach (var item in packages)
            {
                if (item.PackageSize>itemData)
                {
                    tbl_PackagePrice packagePrice = operation.GetPackagePricesByPackageID(item.ID).FirstOrDefault();
                    dataPrice = packagePrice == null ? 0 : (packagePrice.Price==null?0:(decimal)packagePrice.Price);
                    break;
                }
            }
            return dataPrice;
        }
    }
}
