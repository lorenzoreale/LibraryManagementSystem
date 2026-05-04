using LMS.Domain.Entities;

namespace LMS.Domain.Interfaces
{
    public interface IMemberRepository
    {
        void Add(Member member);

        List<Member> GetAll();

        void Delete(Guid id); // a member record at least is deactivated, not phisically removed from the system

        Member? GetById(Guid id);
    }
}