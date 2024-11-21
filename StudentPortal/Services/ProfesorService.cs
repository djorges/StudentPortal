using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Entities;

namespace StudentPortal.Services
{
    public class ProfesorService
    {
        private readonly DBMain _dbMain;

        public ProfesorService(DBMain dbMain)
        {
            _dbMain = dbMain;
        }

        public async Task<List<Profesor>> GetTopRated()
        {
            return await _dbMain.Profesores
                .Where(p => p.Activo)                        
                .OrderByDescending(p => p.PuntuacionEncuestas)
                .Take(3)
                .ToListAsync();
        }
    }
}
