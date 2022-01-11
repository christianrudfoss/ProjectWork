using App.Models;
using System;

namespace App.Extensions
{
    public static class ExtensionMethods
    {
        internal static void SetMinutesOfWork(this WorkResponseModel workResponseModel)
        {
            if (workResponseModel != null)
            {
                if (workResponseModel.Start != null && workResponseModel.End != null)
                {
                    if (workResponseModel.Start < workResponseModel.End)
                    {
                        TimeSpan ts = (DateTime)workResponseModel.End - (DateTime)workResponseModel.Start;
                        workResponseModel.MinutesOfWork = Math.Round(ts.TotalMinutes,0);
                    }
                }
            }
        } 
    }
}
