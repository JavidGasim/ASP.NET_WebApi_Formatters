using System.Linq.Expressions;
using WebApiDemo3_22_10.Entities;
using WebApiDemo3_22_10.Repositories.Abstract;
using WebApiDemo3_22_10.Services.Abstract;

namespace WebApiDemo3_22_10.Services.Concrete
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task AddAsync(Student entity)
        {
            await _studentRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(Student entity)
        {
            await _studentRepository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _studentRepository.GetAllAsync();
        }

        public async Task<Student> GetAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _studentRepository.GetAsync(predicate);
        }

        public async Task UpdateAsync(Student entity)
        {
            await _studentRepository.UpdateAsync(entity);
        }
    }
}
