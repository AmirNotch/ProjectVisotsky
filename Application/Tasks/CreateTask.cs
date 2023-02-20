using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Task = Domain.Task;

namespace Application.Tasks
{
    public class CreateTask
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Task Task { get; set; }
            public string Id { get; set; }
        }
        
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Task).SetValidator(new TaskValidator());
            }
        }
        
        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }
            
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {

                var user = await _context.ListTasks.Where(x => 
                    x.Id.ToString() == request.Id).FirstOrDefaultAsync();

                var attendee = new Task
                {
                    ListTask = user,
                    Name = request.Task.Name,
                    Description = request.Task.Description,
                    CreatedAt = DateTime.UtcNow,
                    Status = 0
                };
                
                //request.ListTaskDto.Add(attendee);
                
                _context.Tasks.Add(attendee);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to create Task");
                }
                
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}