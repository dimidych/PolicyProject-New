using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DevicePlatformEntity
{
    public class DevicePlatformRepository : IDevicePlatformRepository
    {
        private readonly IDevicePlatformDbContext _dbContext;

        public DevicePlatformRepository(IDevicePlatformDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DevicePlatform> AddDevicePlatform(DevicePlatform newDevicePlatform)
        {
            if (newDevicePlatform == null || string.IsNullOrEmpty(newDevicePlatform.DevicePlatformName))
                throw new ArgumentException("Платформа не задана");

            var existed = await _dbContext.DevicePlatforms.AsNoTracking().FirstOrDefaultAsync(x =>
                x.DevicePlatformName.Equals(newDevicePlatform.DevicePlatformName,
                    StringComparison.InvariantCultureIgnoreCase));

            if (existed != null)
                throw new Exception($"Платформа {newDevicePlatform.DevicePlatformName} уже существует");

            newDevicePlatform.DevicePlatformId = (short)(_dbContext.DevicePlatforms.Any()
                ? await _dbContext.DevicePlatforms.MaxAsync(x => x.DevicePlatformId) + 1
                : 1);
            var result = await _dbContext.DevicePlatforms.AddAsync(newDevicePlatform);
            await (_dbContext as DbContext).SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<DevicePlatform>> GetDevicePlatform(short? devicePlatformId = null)
        {
            var result = new List<DevicePlatform>();

            if (devicePlatformId == null)
                result = await _dbContext.DevicePlatforms.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.DevicePlatforms.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.DevicePlatformId == devicePlatformId));

            return result;
        }
    }
}