﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HurtowniaReptiGood.Models.Interfaces
{
    public interface ISubiektAPIService
    {
        Task DownloadAndUpdateProductsStockFromSubiektGT();
        //Task UpdateStockInDatabase(List<ProductAPI> productsListFromSubiektAPI);
    }
}
