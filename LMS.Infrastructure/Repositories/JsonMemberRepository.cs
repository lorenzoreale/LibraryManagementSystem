using System.Text.Json;
using LMS.Domain.Entities;
using LMS.Domain.Interfaces;

namespace LMS.Infrastructure.Repositories
{
    public class JsonMemberRepository : IMemberRepository
    {
        private readonly string _filepath = "members_data.json";

        public JsonMemberRepository()
        {
            if(!File.Exists(_filepath))
                File.WriteAllText(_filepath, "[]");
        }
        
        public void Add(Member member)
        {
            var members = GetAll();

            members.Add(member);

            var jsonText = JsonSerializer.Serialize(members);

            File.WriteAllText(_filepath, jsonText);

        }

        public List<Member> GetAll()
        {
            var jsonText = File.ReadAllText(_filepath);

            return JsonSerializer.Deserialize<List<Member>>(jsonText) ?? new List<Member>();
        }

        public void Delete(Guid id)
        {
           var members = GetAll();
           var memberToRemove = members.FirstOrDefault(m => m.Id == id);

           if (memberToRemove == null)
                throw new InvalidOperationException("Member not found.");
           
           members.Remove(memberToRemove);

           var jsonText = JsonSerializer.Serialize(members);
           File.WriteAllText(_filepath, jsonText);
        }

        public Member? GetById(Guid id)
        {
            var members = GetAll();
            return members.FirstOrDefault(m => m.Id == id);
        }
    }
}