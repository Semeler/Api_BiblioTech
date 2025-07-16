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
                .ForMember(dest => dest.DataNascimento,
                    opt => opt.MapFrom(src =>
                        new DateOnly(src.DataNascimento.Year, src.DataNascimento.Month, src.DataNascimento.Day)
                    )
                );


            CreateMap<EmprestimoDto, Emprestimo>()
                .ForMember(dest => dest.DataInicio,
                    opt => opt.MapFrom(src =>
                        src.DataInicio != null
                            ? new DateOnly(src.DataInicio.Value.Year, src.DataInicio.Value.Month, src.DataInicio.Value.Day)
                            : (DateOnly?)null
                    )
                )
                .ForMember(dest => dest.DataPrevista,
                    opt => opt.MapFrom(src =>
                        src.DataPrevista != null
                            ? new DateOnly(src.DataPrevista.Value.Year, src.DataPrevista.Value.Month, src.DataPrevista.Value.Day)
                            : (DateOnly?)null
                    )
                )
                .ForMember(dest => dest.DataDevolucao,
                    opt => opt.MapFrom(src =>
                        src.DataDevolucao != null
                            ? new DateOnly(src.DataDevolucao.Value.Year, src.DataDevolucao.Value.Month, src.DataDevolucao.Value.Day)
                            : (DateOnly?)null
                    )
                )
                .ForMember(dest => dest.Livros, opt => opt.Ignore());

            CreateMap<FuncionarioDto, Funcionario>()
                .ForMember(dest => dest.DataAdmissao,
                    opt => opt.MapFrom(src =>
                        new DateOnly(src.DataAdmissao.Year, src.DataAdmissao.Month, src.DataAdmissao.Day)
                    )
                );

            CreateMap<LivroDto, Livro>()
                .ForMember(dest => dest.AnoPublicacao,
                    opt => opt.MapFrom(src =>
                        new DateOnly(src.AnoPublicacao.Year, src.AnoPublicacao.Month, src.AnoPublicacao.Day)
                    )
                )
                .ForMember(dest => dest.GeneroId, opt => opt.MapFrom(src => src.GeneroId))
                .ForMember(dest => dest.Emprestimos, opt => opt.Ignore())
                .ForMember(dest => dest.Fornecedores, opt => opt.Ignore());

            CreateMap<GeneroDto, Genero>();

            CreateMap<FornecedorDto, Fornecedor>()
                .ForMember(dest => dest.Livros, opt => opt.Ignore());

            CreateMap<EstoqueDto, Estoque>()
                .ForMember(dest => dest.LivroId, opt => opt.MapFrom(src => src.LivroId));
        }
    }
}
