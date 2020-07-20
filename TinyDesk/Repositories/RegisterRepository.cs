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
    }

    public class RegisterRepository : BaseRepository<Register>, IRegisterRepository
    {
        public RegisterRepository(ApplicationContext context) : base(context)
        {

        }

        public void Update(Order order, Register register)
        {
            context.Register.Update(register);
            context.SaveChanges();

            order.InsertRegisterId(register.Id);
            context.Order.Update(order);
            context.SaveChanges();
        }

    }
}
