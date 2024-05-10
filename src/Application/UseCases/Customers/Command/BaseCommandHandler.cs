using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Command
{
    public abstract class BaseCommandHandler<TRequest, TResponse, TWriteRepo> : IRequestHandler<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        protected readonly IMapper _mapper;
        protected readonly TWriteRepo _writeRepo;

        protected BaseCommandHandler(TWriteRepo writeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _writeRepo = writeRepo;
        }
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
