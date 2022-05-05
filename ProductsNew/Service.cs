using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductsNew
{
    public class Service
    {
        decimal firstRange = 10;
        decimal secondRange=15;
        decimal thirdRange=20;
        decimal fourthRange=40;

        public decimal PriceEstimated(decimal volume)
        {
            if(volume <= 100)
            {
                return firstRange;
            }
            if(volume < 500)
            {
                return secondRange;
            }
            if(volume <1000)
            {
                return thirdRange;
            }
            else
            {
                return fourthRange;
            }
         //   return volume;
        }

        public decimal VolumeEstimated(decimal length, decimal width, decimal height)
        {
            if(length !=0 && width !=0 && height !=0)
            return length*width*height;
            else
                return 0;
        }
    }
}