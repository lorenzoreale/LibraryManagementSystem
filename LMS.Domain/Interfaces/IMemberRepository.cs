using LMS.Domain.Entities;

namespace LMS.Domain.Interfaces
{
    public interface IMemberRepository
    {
        void Add(Member member);

        List<Member> GetAll();

        void Delete(Guid id);

        Member? GetById(Guid id);
    }
}