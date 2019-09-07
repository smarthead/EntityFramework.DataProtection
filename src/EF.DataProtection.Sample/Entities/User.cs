using System;

namespace EF.DataProtection.Sample.Entities
{
    public class User
    {
        public long Id { get; set; }

        protected User() { }
        
        public User(PersonalData data)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            PersonalData = data
                ?? throw new ArgumentNullException(nameof(data));
        }
        
        public virtual PersonalData PersonalData { get; protected set; }
    }
}