using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class PerfilService
    {
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;

        public PerfilService(DBMain dbMain, IMapper mapper)
        {
            _dbMain = dbMain;
            _mapper = mapper;
        }

        public async Task<List<PerfilDto>> GetAll() {
            var listaPerfil = await _dbMain.Perfiles.ToListAsync();

            return _mapper.Map<List<PerfilDto>>(listaPerfil);
        }
    }
}
