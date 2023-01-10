using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Repository;

namespace Vestillo.Business.Controllers
{
    public class ColaboradorController : GenericController<Colaborador, ColaboradorRepository>
    {
        public IEnumerable<Colaborador> GetPorReferencia(string referencia,string tipoColaborador)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.GetPorReferencia(referencia, tipoColaborador);
            }
        }

        public IEnumerable<Colaborador> GetPorNome(string nome, string tipoColaborador)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.GetPorNome(nome, tipoColaborador);
            }
        }

        public Colaborador GetByColaborador(int Id)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.GetByColaborador(Id);
            }
        }

        public IEnumerable<Colaborador> GetByIdList(int id, String TipoColaborador)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.GetByIdList(id, TipoColaborador);
            }
        }

        public IEnumerable<Colaborador> GetAlgunsCampos()
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.GetAlgunsCampos();
            }
        }

        public IEnumerable<Colaborador> GetAlgunsCamposPorTipo(string Tipo)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.GetAlgunsCamposPorTipo(Tipo);
            }
        }

        public bool CnpfCpfExiste(string CnpjCpf, int id)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.CnpfCpfExiste(CnpjCpf,id);
            }

        }


        public override void Save(ref Colaborador colaborador)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                try
                {
                    repository.BeginTransaction();

                    int IdColaborador = colaborador.Id;

                    //PEGA A REFERENCIA
                    base.Save(ref colaborador);

                    //grava a grade de maquinas da facção
                    using (MaquinaColaboradorRepository gradeRepository = new MaquinaColaboradorRepository())
                    {
                        var grades = gradeRepository.GetListByColaborador(colaborador.Id);

                        foreach (var g in grades)
                        {
                            gradeRepository.Delete(g.Id);
                        }

                        foreach (var gr in colaborador.Grade)
                        {
                            MaquinaColaborador g = gr;
                            g.Id = 0;
                            g.IdColaborador = colaborador.Id;
                            gradeRepository.Save(ref g);
                        }
                    }

                    //Grava imagem da facção
                    using (ImagemRepository imgFaccao = new ImagemRepository())
                    {
                        var img = imgFaccao.GetImagem("idcolaborador", colaborador.Id);

                        foreach (var i in img)
                        {
                            imgFaccao.Delete(i.Id);
                        }

                        if (colaborador.imgFaccao != null)
                        {
                           
                            Imagem imagem = new Imagem();
                            imagem.Id = 0;
                            imagem.IdColaborador = colaborador.Id;
                            imagem.imagem = colaborador.imgFaccao;
                            imagem.IdProduto = null;
                            imagem.IdFuncionario = null;
                            imagem.IdMateriaPrima = null;
                            imgFaccao.Save(ref imagem);
                           
                        }


                        img = imgFaccao.GetImagem("IdVendedor", colaborador.Id);

                        foreach (var i in img)
                        {
                            imgFaccao.Delete(i.Id);
                        }

                        if (colaborador.ImagemVendedor != null)
                        {

                            Imagem imagem = new Imagem();
                            imagem.Id = 0;
                            imagem.IdColaborador = null;
                            imagem.IdVendedor = colaborador.Id;
                            imagem.imagem = colaborador.ImagemVendedor;
                            imagem.IdProduto = null;
                            imagem.IdFuncionario = null;
                            imagem.IdMateriaPrima = null;
                            imgFaccao.Save(ref imagem);

                        }

                    }

                    using (ParcelaPadraoClienteRepository parcelaRepository = new ParcelaPadraoClienteRepository())
                    {
                        if (colaborador.Parcelas != null && colaborador.Parcelas.Count > 0)
                        {
                            parcelaRepository.DeleteAllPorColaborador(colaborador.Id);
                            foreach (ParcelaPadraoCliente parcela in colaborador.Parcelas)
                            {
                                ParcelaPadraoCliente parcelaSave;
                                parcelaSave = parcela;
                                parcelaSave.ColaboradorId = colaborador.Id;


                                parcelaRepository.Save(ref parcelaSave);
                            }
                        }
                    }


                    //VALIDA LIMITE DE CRÉDITO
                    if (VestilloSession.ControlaInadimplenciaCliente)
                    {
                        repository.DefineLimiteDeCompra(colaborador.Id);
                    }

                    if(VestilloSession.UsaPortalRepresentante)
                    {
                        //PreencheApi(colaborador);
                    }

                   

                    repository.CommitTransaction();
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        public override void Delete(int id)
        {

            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                try
                {
                    repository.BeginTransaction();

                    //deleta a grade de maquinas da facção
                    using (MaquinaColaboradorRepository gradeRepository = new MaquinaColaboradorRepository())
                    {
                        var grades = gradeRepository.GetListByColaborador(id);

                        foreach (var g in grades)
                        {
                            gradeRepository.Delete(g.Id);
                        }
                        
                    }

                    //deleta a imagem da facção
                    using (ImagemRepository imgFaccao = new ImagemRepository())
                    {
                        var img = imgFaccao.GetImagem("idcolaborador",id);

                        foreach (var i in img)
                        {
                            imgFaccao.Delete(i.Id);
                        }

                    }

                    //Se for Facção, deleta da tabela fichafaccao
                    using (FichaFaccaoRepository fichaFaccaoRepository = new FichaFaccaoRepository())
                    {
                        var fichafaccao = fichaFaccaoRepository.GetByIdFaccao(id);

                        foreach (var ff in fichafaccao)
                        {
                            fichaFaccaoRepository.Delete(ff.Id);
                        }

                    }

                    base.Delete(id);

                    repository.CommitTransaction();
                }
                catch (Exception ex)
                {
                    repository.RollbackTransaction();
                    throw new Vestillo.Lib.VestilloException(ex);
                }
            }
        }

        public void PreencheApi(Colaborador Dados)
        {

           /* var Api = new Vestillo.Api.Models.ClientesModel();
            Api.Id = Dados.Id;
            Api.Nome = Dados.Nome;
            Api.Data_cadastro = Dados.DataCadastro.ToString();
            Api.Data_nascimento = Dados.DataCadastro.ToString();
            Api.Tipo = "F";
            Api.Cnpjcpf = Dados.CnpjCpf;
            Api.Telefone = Dados.Telefone;
            Api.Email = Dados.Email;
            Api.Cep = Dados.Cep.ToString();
            Api.Endereco = Dados.Endereco.ToString();
            Api.Numero = Dados.Numero.ToString();
            Api.Bairro = Dados.Bairro.ToString();
            Api.Cidade = "Nova Friburgo";
            Api.UF = "RJ";
            Api.Complemento = Dados.Complemento;
            Api.Inserir();*/

        }

        public Colaborador GetByIdFuncionario(int IdFuncionario)
        {
            using (ColaboradorRepository repository = new ColaboradorRepository())
            {
                return repository.GetByIdFuncionario(IdFuncionario);
            }
        }
    }
}
