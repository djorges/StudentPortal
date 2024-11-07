using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Entities;
using StudentPortal.Models;

namespace StudentPortal.Services
{
    public class EmpleadoService
    {
        private readonly DBMain _dbMain;
        private readonly IMapper _mapper;

        public EmpleadoService(DBMain dbMain, IMapper mapper)
        {
            _dbMain = dbMain;
            _mapper = mapper;
        }

        public async Task<List<EmpleadoDto>> Listar()
        {
            var listaEmpleados = await _dbMain.Empleados
                .Include(em => em.Perfil)
                .ToListAsync();

            return _mapper.Map<List<EmpleadoDto>>(listaEmpleados);
        }

        public async Task<EmpleadoDto> ObtenerPorId(int id)
        {
            var empleado = await _dbMain.Empleados
                .Include(em => em.Perfil)
                .Where(em => em.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<EmpleadoDto?> Guardar(EmpleadoDto empleadoDto) {
            var empleado = _mapper.Map<Empleado>(empleadoDto);

            var newEmpleado = await _dbMain.Empleados.AddAsync(empleado);
            await _dbMain.SaveChangesAsync();

            return _mapper.Map<EmpleadoDto>(newEmpleado.Entity);
        }

        public async Task<EmpleadoDto?> Editar(EmpleadoDto empleadoDto) {
            var empleado = await _dbMain.Empleados
                .Include(em => em.Perfil)
                .Where(em => em.Id == empleadoDto.Id)
                .FirstOrDefaultAsync();

            if (empleado != null) {
                empleado.Nombre = empleadoDto.Nombre;
                empleado.Salario = empleadoDto.Salario;
                empleado.IdPerfil = empleadoDto.IdPerfil;

                _dbMain.Empleados.Update(empleado);
                await _dbMain.SaveChangesAsync();
            }

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<EmpleadoDto> Eliminar(int id)
        {
            var empleado = await _dbMain.Empleados
                .Include(em => em.Perfil)
                .Where(em => em.Id == id)
                .FirstOrDefaultAsync();

            if (empleado != null)
            {
                _dbMain.Empleados.Remove(empleado);
                await _dbMain.SaveChangesAsync();
            }

            return _mapper.Map<EmpleadoDto>(empleado);
        }
    }
}
