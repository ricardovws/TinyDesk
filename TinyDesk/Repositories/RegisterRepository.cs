using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyDesk.Models;

namespace TinyDesk.Repositories
{
    public interface IRegisterRepository
    {
        void Update(Order order, Register register);
        Register CatchRegister(string email);
    }

    public class RegisterRepository : BaseRepository<Register>, IRegisterRepository
    {
        public RegisterRepository(ApplicationContext context) : base(context)
        {

        }


        public Register CatchRegister(string email)
        {
            var register = context.Register.Where(r => r.Email == email).LastOrDefault();
            if(register == null)
            {
                return register = new Register { Email = email };
            }
            else
            {
                return register;
            }
        }


        public void Update(Order order, Register register)
        {
            var oldRegister = context.Register.Where(r => r.Email == register.Email).LastOrDefault();
            if(oldRegister != null)
            {
                context.Register.Remove(oldRegister);
            }
            context.Register.Add(register);
            context.SaveChanges();

            order.InsertRegisterId(register.Id);
            context.Order.Update(order);
            context.SaveChanges();
        }

    }
}
