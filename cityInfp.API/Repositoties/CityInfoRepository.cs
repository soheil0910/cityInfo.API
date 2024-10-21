﻿using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Repositoties
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDbContext _context;
        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities
                 .OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId,bool includePointsOfInterest)
        {
           if(includePointsOfInterest)
            {
                return await _context.Cities.Include(c=> c.PointOfInterest)
                 .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities
                 .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCity(int cityId,
            int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                .Where(p => p.CityId == cityId && p.Id == pointOfInterestId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
               .Where(p => p.CityId == cityId)
               .ToListAsync();
        }
    }
}
