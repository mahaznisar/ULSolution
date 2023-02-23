using AutoMapper;
using ULSolution.Core.Contracts.Services;
using System;

namespace ULSolution.Core.Services
{
    public class Service : IService
    {
        protected readonly IMapper _Mapper;

        public Service(IMapper mapper)
        {
            _Mapper = mapper;
        }
    }
}
