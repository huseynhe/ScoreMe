using ScoreMe.DAL.DBModel;
using ScoreMe.DAL.Enum;
using ScoreMe.DAL.Repositories;
using ScoreMe.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoreMe.DAL
{
    public class DALOperation
    {

        public void AddCALLReportDetail(Int64 userID, string phoneNumer, tbl_CALLDetail detailItem)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            int _outCallCountSame = 0, _outCallCountOther = 0, _outCallForeignCount = 0, _inCallCount = 0, _inCallForeignCount = 0, _outMissedCallCount = 0, _inMissedCallCount = 0;
            decimal _outCallSecondSame = 0, _outCallMinuteSame = 0, _outCallSecondOther = 0, _outCallMinuteOther = 0;
            decimal _outCallForeignSecond = 0, _outCallForeignMinute = 0;
            decimal _inCallSecond = 0, _inCallMinute = 0, _inCallForeignSecond = 0, _inCallForeignMinute = 0;

            tbl_OperatorInformation _OperatorInformation = new tbl_OperatorInformation();
            decimal pointValue = 0;
            int month = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Month : 0;
            int year = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Year : 0;
            try
            {

                if (detailItem.InOutType == 1)
                {
                    if (detailItem.IsForeign == 0 && detailItem.Duration > 0)
                    {
                        bool IsSame = NumberHelper.ControlSameOrOther(phoneNumer, detailItem.PhonePrefix);
                        if (IsSame)
                        {
                            _outCallCountSame = 1;
                            _outCallSecondSame = (decimal)detailItem.Duration;
                            _outCallMinuteSame = Math.Ceiling(((decimal)detailItem.Duration / 60));

                            _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)CALLOperatorInformation.OutSameMinute, (int)OperatorChanelType.Call);
                            if (_OperatorInformation != null)
                            {
                                pointValue = _outCallMinuteSame * (_OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point);
                                AddUserPoint(userID, month, year, pointValue, (int)ChanelType.CALL);
                            }

                        }
                        else
                        {
                            _outCallCountOther = 1;
                            _outCallSecondOther = (decimal)detailItem.Duration;
                            _outCallMinuteOther = Math.Ceiling(((decimal)detailItem.Duration / 60));
                            _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)CALLOperatorInformation.OutOtherMinute, (int)OperatorChanelType.Call);
                            if (_OperatorInformation != null)
                            {
                                pointValue = _outCallMinuteOther * (_OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point);
                                AddUserPoint(userID, month, year, pointValue, (int)ChanelType.CALL);
                            }

                        }
                    }
                    else if (detailItem.IsForeign == 1 && detailItem.Duration > 0)
                    {
                        _outCallForeignCount = 1;
                        _outCallForeignSecond = (decimal)detailItem.Duration;
                        _outCallForeignMinute = Math.Ceiling(((decimal)detailItem.Duration / 60));
                        _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)CALLOperatorInformation.OutForeignMinute, (int)OperatorChanelType.Call);
                        if (_OperatorInformation != null)
                        {
                            pointValue = _outCallForeignMinute * (_OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point);
                            AddUserPoint(userID, month, year, pointValue, (int)ChanelType.CALL);
                        }


                    }
                    else if (detailItem.Duration == 0)
                    {
                        _outMissedCallCount = 1;
                        _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)CALLOperatorInformation.OutMissedCount, (int)OperatorChanelType.Call);
                        if (_OperatorInformation != null)
                        {
                            pointValue = _outMissedCallCount * (_OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point);
                            AddUserPoint(userID, month, year, pointValue, (int)ChanelType.CALL);
                        }
                    }

                }
                else
                {
                    if (detailItem.IsForeign == 0 && detailItem.Duration > 0)
                    {
                        _inCallCount = 1;
                        _inCallSecond = (decimal)detailItem.Duration;
                        _inCallMinute = Math.Ceiling(((decimal)detailItem.Duration / 60));

                        _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)CALLOperatorInformation.INMinute, (int)OperatorChanelType.Call);
                        if (_OperatorInformation != null)
                        {
                            pointValue = _inCallMinute * (_OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point);
                            AddUserPoint(userID, month, year, pointValue, (int)ChanelType.CALL);
                        }
                  

                    }
                    else if (detailItem.IsForeign == 1 && detailItem.Duration > 0)
                    {
                        _inCallForeignCount = 1;
                        _inCallForeignSecond = (decimal)detailItem.Duration;
                        _inCallForeignMinute = Math.Ceiling(((decimal)detailItem.Duration / 60));
                        _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)CALLOperatorInformation.INForeignMinute, (int)OperatorChanelType.Call);
                        if (_OperatorInformation != null)
                        {
                            pointValue = _inCallForeignMinute * (_OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point);
                        
                            AddUserPoint(userID, month, year, pointValue, (int)ChanelType.CALL);
                        }

                    }
                    else if (detailItem.Duration == 0)
                    {
                        _inMissedCallCount = 1;
                    }
                }

                tbl_CALLReport callReport = new tbl_CALLReport()
                {
                    CALLDetailID = detailItem.ID,
                    CALLModelID = detailItem.CALLModelID,
                    UserID = userID,
                    Month = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Month : 0,
                    Year = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Year : 0,
                    OutCallCountSame = _outCallCountSame,

                    OutCallSecondSame = _outCallSecondSame,
                    OutCallMinuteSame = _outCallMinuteSame,
                    OutCallCountOther = _outCallCountOther,
                    OutCallSecondOther = _outCallSecondOther,
                    OutCallMinuteOther = _outCallMinuteOther,
                    InCallCount = _inCallCount,
                    InCallSecond = _inCallSecond,
                    InCallMinute = _inCallMinute,
                    OutMissedCallCount = _outMissedCallCount,
                    InMissedCallCount = _inMissedCallCount,
                    OutCallForeignCount = _outCallForeignCount,
                    OutCallForeignSecond = _outCallForeignSecond,
                    OutCallForeignMinute = _outCallForeignMinute,
                    InCallForeignCount = _inCallForeignCount,
                    InCallForeignSecond = _inCallForeignSecond,
                    InCallForeignMinute = _inCallForeignMinute,

                };

                cRUDOperation.AddCALLReport(callReport);
             

            }
            catch (Exception ex)
            {


            }






        }
        public void AddSMSReportDetail(Int64 userID, string phoneNumer, tbl_SMSDetail detailItem, tbl_SMSSenderInfo smsSenderInfo)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            int _outMsjCountSame = 0, _outMsjCountOther = 0, _outMsjForeignCount = 0, _outMsjRoamingCount = 0;
            int _inMsjCount = 0, _inMsjForeignCount = 0;

            tbl_OperatorInformation _OperatorInformation = new tbl_OperatorInformation();
            decimal pointValue = 0;
            int month = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Month : 0;
            int year = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Year : 0;

            try
            {
                if (detailItem.IsShortMessage == 0)
                {
                    if (detailItem.InOutType == 1)
                    {
                        if (detailItem.IsForeign == 0 || detailItem.IsForeign == null)
                        {
                            bool IsSame = NumberHelper.ControlSameOrOther(phoneNumer, detailItem.PhonePrefix);
                            if (IsSame)
                            {
                                _outMsjCountSame = 1;
                                _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)SMSOperatorInformation.OutSameCount, (int)OperatorChanelType.Message);
                                if (_OperatorInformation != null)
                                {
                                    pointValue = 1 * _OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point;
                                    AddUserPoint(userID, month, year, pointValue, (int)ChanelType.SMS);
                                }
                            }
                            else
                            {
                                _outMsjCountOther = 1;
                                _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)SMSOperatorInformation.OutSameCount, (int)OperatorChanelType.Message);
                                if (_OperatorInformation != null)
                                {
                                    pointValue = 1 * _OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point;
                                    AddUserPoint(userID, month, year, pointValue, (int)ChanelType.SMS);
                                }

                            }
                        }
                        else if (detailItem.IsForeign == 1)
                        {
                            _outMsjForeignCount = 1;
                            _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)SMSOperatorInformation.OutSameCount, (int)OperatorChanelType.Message);
                            if (_OperatorInformation != null)
                            {
                                pointValue = 1 * _OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point;
                                AddUserPoint(userID, month, year, pointValue, (int)ChanelType.SMS);
                            }
                        }

                    }
                    else
                    {
                        if (detailItem.IsForeign == 0 || detailItem.IsForeign == null)
                        {
                            _inMsjCount = 1;
                            _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)SMSOperatorInformation.OutSameCount, (int)OperatorChanelType.Message);
                            if (_OperatorInformation != null)
                            {
                                pointValue = 1 * _OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point;
                                AddUserPoint(userID, month, year, pointValue, (int)ChanelType.SMS);
                            }

                        }
                        else if (detailItem.IsForeign == 1)
                        {
                            _inMsjForeignCount = 1;
                            _OperatorInformation = cRUDOperation.GetOperatorInformationByPrefixAndType(NumberHelper.GetNumberPrefix(phoneNumer), (int)SMSOperatorInformation.OutSameCount, (int)OperatorChanelType.Message);
                            if (_OperatorInformation != null)
                            {
                                pointValue = 1 * _OperatorInformation.Point == null ? 0 : (decimal)_OperatorInformation.Point;
                                AddUserPoint(userID, month, year, pointValue, (int)ChanelType.SMS);
                            }


                        }

                    }

                    tbl_SMSReport smsReport = new tbl_SMSReport()
                    {

                        SMSModelID = detailItem.SMSModelID,
                        SMSDetailID = detailItem.ID,
                        UserID = userID,
                        Month = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Month : 0,
                        Year = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Year : 0,
                        OutMsjCountSame = _outMsjCountSame,
                        OutMsjCountOther = _outMsjCountOther,
                        OutMsjForeignCount = _outMsjForeignCount,
                        InMsjCount = _inMsjCount,
                        InMsjForeignCount = _inMsjForeignCount,


                    };

                    cRUDOperation.AddSMSReport(smsReport);
                }
                else if (detailItem.IsShortMessage == 1)
                {
                    if (smsSenderInfo != null)
                    {
                        pointValue = 1 * smsSenderInfo.Point == null ? 0 : (decimal)smsSenderInfo.Point;
                        AddUserPoint(userID, month, year, pointValue, (int)ChanelType.SMS);
                    }

                    if (detailItem.IsParse == 1)
                    {
                        if (detailItem.SenderName == "Azericard")
                        {
                            //Mebleg:-30.00 AZN 
                            try
                            {
                                string[] arrayList = detailItem.Message.Split('\n');
                                if (arrayList[0].Substring(0, 7) == "Mebleg:")
                                {
                                    tbl_SMSReportShort smsRepotShort = new tbl_SMSReportShort();
                                    smsRepotShort.UserID = userID;
                                    smsRepotShort.SMSModelID = detailItem.SMSModelID;
                                    smsRepotShort.SMSDetailID = detailItem.ID;
                                    smsRepotShort.Month = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Month : 0;
                                    smsRepotShort.Year = detailItem.RecievedDate.HasValue == true ? detailItem.RecievedDate.Value.Year : 0;
                                    smsRepotShort.SenderName = detailItem.SenderName;
                                    string[] amountList = arrayList[0].Trim().Split(' ');
                                    if (amountList[0].Substring(7, 1) == "-")
                                    {
                                        smsRepotShort.IsExpense = 1;
                                    }
                                    else
                                    {
                                        smsRepotShort.IsExpense = 0;
                                    }
                                    string amount = string.Empty;
                                    if (amountList.Length > 2)
                                    {
                                        amount = amountList[0] + amountList[1];
                                        smsRepotShort.Currency = amountList[2];
                                    }
                                    else
                                    {
                                        amount = amountList[0];
                                        smsRepotShort.Currency = amountList[1];
                                    }

                                    string newamount = amount.Substring(8);
                                    smsRepotShort.Amount = decimal.Parse(newamount);


                                    smsRepotShort.CardNumber = arrayList[1].Substring(5);
                                    smsRepotShort.OperationDate = DateTime.Parse(arrayList[2].Substring(6));
                                    smsRepotShort.MerchantName = arrayList[3].Substring(9);
                                    string[] balance = arrayList[4].Split(' ');
                                    smsRepotShort.Balance = decimal.Parse(balance[0].Substring(7));
                                    smsRepotShort.BalanceCurrency = balance[1];
                                    cRUDOperation.AddSMSReportShort(smsRepotShort);
                                }
                            }
                            catch (Exception ex)
                            {


                            }



                        }
                    }
                }

        
                
            }
            catch (Exception ex)
            {


            }


        }
        public void AddAppConsumePoint(Int64 userID, string userName, tbl_AppConsumeDetail _appConsumeDetail)
        {
            try
            {
                CRUDOperation cRUDOperation = new CRUDOperation();
                decimal pointValue = 0;
                tbl_ApplicationInformation _applicationInformation = cRUDOperation.GetApplicationInformationByShortName(_appConsumeDetail.AppName);
                if (_applicationInformation != null)
                {
                    pointValue = 1 * (_applicationInformation.Point == null ? 0 : (decimal)_applicationInformation.Point);
                }
                AddUserPoint(userID, _appConsumeDetail.Month, _appConsumeDetail.Year, pointValue, (int)ChanelType.AppConsume);
            }
            catch (Exception ex)
            {

               
            }
     
        }
        public void AddNetConsumePoint(Int64 userID, string userName, tbl_NetConsumeDetail _netConsumeDetail)
        {
            try
            {
                CRUDOperation cRUDOperation = new CRUDOperation();
                decimal pointValue = 0;
                int operatorEVID = NetConsumeHelper.GetOperatorValueByKey(_netConsumeDetail.OperatorName.Trim());
                NetConsumeRepository netConsumeRepository = new NetConsumeRepository();
                if (_netConsumeDetail.Source_EVID == 4)
                {
                    tbl_PackagePrice packagePrice = netConsumeRepository.GetPackagePrice(operatorEVID, _netConsumeDetail.Consumed);
                    if (packagePrice != null)
                    {
                        pointValue = 1 * (packagePrice.Point == null ? 0 : (decimal)packagePrice.Point);
                    }

                }

                AddUserPoint(userID, _netConsumeDetail.Month, _netConsumeDetail.Year, pointValue, (int)ChanelType.NetConsume);
            }
            catch (Exception)
            {

           
            }
      
        }
        public void AddUserPoint(Int64 userId, int month, int year, decimal point, int type)
        {
            try
            {
                CRUDOperation cRUDOperation = new CRUDOperation();
                tbl_UserPoint userPoint = new tbl_UserPoint()
                {
                    UserID = userId,
                    Month = month,
                    Year = year,
                    Point = point,
                    Type = type

                };

                cRUDOperation.UpdateUserPointData(userPoint);
            }
            catch (Exception ex)
            {

             
            }
          
        }
    }
}
