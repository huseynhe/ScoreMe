using ScoreMe.DAL.DBModel;
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
        public void AddCALLReportDetail(Int64 userID,string phoneNumer,  tbl_CALLDetail detailItem)
        {

            CRUDOperation cRUDOperation = new CRUDOperation();
            int _outCallCountSame = 0, _outCallCountOther = 0, _outCallForeignCount = 0, _inCallCount = 0, _inCallForeignCount = 0, _outMissedCallCount = 0, _inMissedCallCount = 0;
            decimal _outCallSecondSame = 0, _outCallMinuteSame = 0, _outCallSecondOther = 0, _outCallMinuteOther = 0;
            decimal _outCallForeignSecond = 0, _outCallForeignMinute = 0;
            decimal _inCallSecond = 0, _inCallMinute = 0, _inCallForeignSecond = 0, _inCallForeignMinute = 0;

           

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

                            }
                            else
                            {
                                _outCallCountOther = 1;
                                _outCallSecondOther = (decimal)detailItem.Duration;
                                _outCallMinuteOther = Math.Ceiling(((decimal)detailItem.Duration / 60));


                            }
                        }
                        else if (detailItem.IsForeign == 1 && detailItem.Duration > 0)
                        {
                            _outCallForeignCount = 1;
                            _outCallForeignSecond = (decimal)detailItem.Duration;
                            _outCallForeignMinute = Math.Ceiling(((decimal)detailItem.Duration / 60));


                        }
                        else if (detailItem.Duration == 0)
                        {
                            _outMissedCallCount = 1;
                        }

                    }
                    else
                    {
                        if (detailItem.IsForeign == 0 && detailItem.Duration > 0)
                        {
                            _inCallCount = 1;
                            _inCallSecond = (decimal)detailItem.Duration;
                            _inCallMinute = Math.Ceiling(((decimal)detailItem.Duration / 60));



                        }
                        else if (detailItem.IsForeign == 1 && detailItem.Duration > 0)
                        {
                            _inCallForeignCount = 1;
                            _inCallForeignSecond = (decimal)detailItem.Duration;
                            _inCallForeignMinute = Math.Ceiling(((decimal)detailItem.Duration / 60));


                        }
                        else if (detailItem.Duration == 0)
                        {
                            _inMissedCallCount = 1;
                        }
                    }

                    tbl_CALLReport callReport = new tbl_CALLReport()
                    {
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
    }
}
