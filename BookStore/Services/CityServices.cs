﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookStore.Models;
using BookStore.ViewModels.UserAddress;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services
{
    public class CityServices : Service
    {
        public CityServices(bookstoreContext bookstoreContext, IWebHostEnvironment webHostEnvironment, IMapper mapper) : base(bookstoreContext, webHostEnvironment, mapper)
        {
        }

        internal async Task<List<CityAddress>> GetAllCityAndDistrictAsync()
        {
            return await _bookstoreContext.CityAddresses
                .Include(d => d.DistrictAddresses)
                .ThenInclude(d=>d.Wards)
                .ToListAsync();
        }

        internal async Task<List<DistrictAddress>> GetDistrictByCityIdAsync(int id)
        {
            return await _bookstoreContext.DistrictAddresses
                .Where(d => d.CityAddressId == id)
                   .ToListAsync();
        }

        internal async Task<object> GetCityAndDistrictAsync(UserAddressPostModel userAddressPostModel)
        {
            return await _bookstoreContext.Ward
                .Where(c => c.Id == userAddressPostModel.WardId)
                .Where(c => c.DistrictAddressId == userAddressPostModel.DistrictAddressId)
                .Where(c => c.CityAddressId == userAddressPostModel.CityAddressId).FirstOrDefaultAsync();
        }

        internal async Task<Ward> GetWard(int wardId)
        {
            return await _bookstoreContext.Ward.FindAsync(wardId);
        }
    }
}
