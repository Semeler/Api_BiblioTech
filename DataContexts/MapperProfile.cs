using ApiLocadora.Dtos;
using ApiLocadora.Models;
using AutoMapper;

namespace ApiLocadora.DataContexts
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<ClienteDto, Cliente>()
                .ForMember(
                    dest => dest.DataNascimento, 
                    opt => opt.MapFrom(
                        src => new DateOnly(src.DataNascimento.Year, src.DataNascimento.Month, src.DataNascimento.Day)
                    )
                );
            CreateMap<EmprestimoDto, Emprestimo>()
                .ForMember(
                    dest => dest.DataInicio,
                    opt => opt.MapFrom(
                        src => new DateOnly(src.DataInicio.Year, src.DataInicio.Month, src.DataInicio.Day)
                    )
                )
                .ForMember(
                dest => dest.DataPrevista,
                opt => opt.MapFrom(
                    src => new DateOnly(src.DataPrevista.Year, src.DataPrevista.Month, src.DataPrevista.Day)
                )
            );
            CreateMap<FuncionarioDto, Funcionario>()
                .ForMember(
                    dest => dest.DataAdmissao, 
                    opt => opt.MapFrom(
                        src => new DateOnly(src.DataAdmissao.Year, src.DataAdmissao.Month, src.DataAdmissao.Day)
                    )
                );
            CreateMap<DevolucaoDto, Devolucao>()
                .ForMember(
                    dest => dest.DataDevolucao, 
                    opt => opt.MapFrom(
                        src => new DateOnly(src.DataDevolucao.Year, src.DataDevolucao.Month, src.DataDevolucao.Day)
                    )
                );
            CreateMap<LivroDto, Livro>()
                .ForMember(
                    dest => dest.AnoPublicacao, 
                    opt => opt.MapFrom(
                        src => new DateOnly(src.AnoPublicacao.Year, src.AnoPublicacao.Month, src.AnoPublicacao.Day)
                    )
                );
            CreateMap<GeneroDto, Genero>();
            
            CreateMap<FornecedorDto, Fornecedor>();
            
            CreateMap<EstoqueDto, Estoque>();
        }
    }
}
