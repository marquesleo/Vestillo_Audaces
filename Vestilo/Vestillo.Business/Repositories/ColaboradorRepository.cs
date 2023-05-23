using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Lib;
using Vestillo.Connection;

namespace Vestillo.Business.Repositories
{
    public class ColaboradorRepository: GenericRepository<Colaborador>
    {
        public ColaboradorRepository()
            : base(new DapperConnection<Colaborador>())
        {
        }

        public IEnumerable<Colaborador> GetPorReferencia(string referencia, String TipoColaborador)
        {
            var SQL = new Select()
                .Campos("id, referencia, nome, razaosocial, idestado, IdVendedor, IdTransportadora, IdRota, idTabelaPreco,CnpjCpf ")
                .From("colaboradores");
            if (TipoColaborador != "")
            {
                if (TipoColaborador == "ClienteFornecedor")
                    SQL.Where("referencia like '%" + referencia + "%' And Cliente = 1 And Fornecedor = 1 And ativo = 1 And " + FiltroEmpresa("idEmpresa"));
                else
                    SQL.Where("referencia like '%" + referencia + "%' And " + TipoColaborador + " = 1 And ativo = 1 " + " AND " + FiltroEmpresa("idEmpresa"));
            }
            else
            {
                SQL.Where("referencia like '%" + referencia + "%' And ativo = 1 " + " AND " + FiltroEmpresa("idEmpresa"));
            }
            SQL.OrderBy("nome");
            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());

            //Colaborador col = new Colaborador();
            //return _cn.ExecuteToList(col, "referencia like '%" + referencia + "%' And " + TipoColaborador + " = 1");
        }

        public IEnumerable<Colaborador> GetPorNome(string nome , String TipoColaborador)
        {

            var SQL = new Select()
                 .Campos("id, referencia, nome, razaosocial, idestado,CnpjCpf ")
                 .From("colaboradores");
            if (TipoColaborador != "")
            {
                if (TipoColaborador == "ClienteFornecedor")
                    SQL.Where("nome like '%" + nome + "%' And Cliente = 1 And Fornecedor = 1 And ativo = 1 " + " AND " + FiltroEmpresa());
                else
                    SQL.Where("nome like '%" + nome + "%' And " + TipoColaborador + " = 1 And ativo = 1 " + " AND " + FiltroEmpresa());
            }
            else
            {
                SQL.Where("nome like '%" + nome + "%'  And ativo = 1 " + " AND " + FiltroEmpresa());
            }
            SQL.OrderBy("nome");

            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());
 
        }
         
        public Colaborador GetByColaborador(int Id)
        {

            var SQL = new Select()
                .Campos("id, referencia, nome, razaosocial, cnpjcpf, idestado, CnpjCpf, percentcomissao, idTabelaPreco,Bonificado,Pis,Cofins,Sefaz,cep ")
                .From("colaboradores")
                .Where("Id = " + Id );

            var colaborador = new Colaborador();
            _cn.ExecuteToModel(ref colaborador, SQL.ToString());
            return colaborador;
        }

        public Colaborador GetByColaboradorPraDevolucaoNFCe()
        {

            var SQL = new Select()
                .Campos("* ")
                .From("colaboradores")
                .Where("DevolucaoNfce = 1 AND ativo = 1" );

            var colaborador = new Colaborador();
            _cn.ExecuteToModel(ref colaborador, SQL.ToString());
            return colaborador;
        }

        //para preencher o grid da tela de browse
        public IEnumerable<Colaborador> GetAlgunsCampos()
        {
            var SQL = new Select()
                .Campos("C.id, C.referencia, C.nome, C.razaosocial,C.cnpjcpf,C.fornecedor,C.faccao,C.vendedor, C.orgaopublico,C.transportadora,C.cliente, C.ativo, IFNULL(V1.nome,'') As NomeVendedor,IFNULL(V2.nome,'') As NomeVendedor2, C.TipoCliente, C.DataPrimeiraCompra,C.Ddd,c.Telefone,C.Obscliente, C.Email, C.Cep ")
                .From("colaboradores as C ")
                .LeftJoin("colaboradores  V1", "V1.Id = C.idvendedor ")
                .LeftJoin("colaboradores  V2", "V2.Id = C.idvendedor2 ")
                .Where(FiltroEmpresa("C.idEmpresa"));

            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());           
        }

        //para preencher o grid quando necessario por tipo 
        public IEnumerable<Colaborador> GetAlgunsCamposPorTipo(string Tipo)
        {
            var SQL = new Select()
                .Campos("id, referencia, nome, razaosocial,cnpjcpf, ativo ")
                .From("colaboradores")
                .Where(Tipo + " = 1 AND  ativo = 1 AND " + FiltroEmpresa())
                .OrderBy("nome");

            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());
        }


        public IEnumerable<Colaborador> GetVendedoresDash() //DASH FINANCEIRO
        {
            var SQL = new Select()
                .Campos("id, referencia, nome, razaosocial,cnpjcpf, ativo ")
                .From("colaboradores")
                .Where( " Vendedor = 1  AND " + FiltroEmpresa())
                .OrderBy("nome");

            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());
        }

        //para preencher o grid da tela de filtro
        public IEnumerable<Colaborador> GetByIdList(int id, String TipoColaborador)
        {
            var SQL = new Select()
                 .Campos("id, referencia, nome,cnpjcpf, razaosocial ")
                 .From("colaboradores");
            if (TipoColaborador != "")
            {
                SQL.Where("id = " + id + " And " + TipoColaborador + " = 1 " + "  And ativo = 1 And " + FiltroEmpresa());
            }
            else
            {
                SQL.Where("id = " + id + " And ativo = 1 And " + FiltroEmpresa());
            }

            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());
        }

        public Colaborador GetByCnpj(string  Cnpj)
        {

            var SQL = new Select()
                .Campos("* ")
                .From("colaboradores")
                .Where("cnpjcpf = " + "'" + Cnpj + "'");

            var colaborador = new Colaborador();
            _cn.ExecuteToModel(ref colaborador, SQL.ToString());
            return colaborador;
        }

        public bool CnpfCpfExiste(string CnpjCpf, int id)
        {
            bool Encontrado = false;

            var SQL = new StringBuilder();
            var cn = new DapperConnection<Colaborador>();

            CnpjCpf = CnpjCpf.ToString().Replace(",", ".");


            if (id == 0)
            {
                SQL.AppendLine("select * from colaboradores WHERE cnpjcpf = " + "'" + CnpjCpf + "'" + " AND " + FiltroEmpresa());
            }
            else
            {
                SQL.AppendLine("select * from colaboradores WHERE cnpjcpf = " + "'" + CnpjCpf + "'" + " AND id <> " + id + " AND " + FiltroEmpresa());
            }


            Colaborador ret = new Colaborador();

            cn.ExecuteToModel(ref ret, SQL.ToString());

            if (ret != null)
            {
                Encontrado = true;
            }

            return Encontrado;

        }

        public void DefineLimiteDeCompra(int idCliente)
        {
            decimal SaldoDevedor = 0;
            StringBuilder sqlDevedor = new StringBuilder();
            var cn = new DapperConnection<ContasReceberView>();

            string sqlCliente = string.Empty;
            var cnCli = new DapperConnection<Colaborador>();

            sqlDevedor.AppendLine("	SELECT SUM(contasReceber.Saldo) AS SaldoDevedor FROM contasReceber WHERE IdCliente = " + idCliente);
            var cr = new ContasReceberView();
            var dados = cn.ExecuteStringSqlToList(cr, sqlDevedor.ToString());

            if (dados != null && dados.Count() > 0)
            {
                List<ContasReceberView> cred = new List<ContasReceberView>();
                cred = dados.ToList();
                SaldoDevedor = cred[0].SaldoDevedor;
            }

            sqlCliente = "UPDATE colaboradores SET LimiteCompra = LimiteCredito - " + SaldoDevedor.ToString().Replace(",", ".") + " WHERE id = " + idCliente;
            _cn.ExecuteNonQuery(sqlCliente);

            sqlCliente = "UPDATE colaboradores SET LimiteCompra = 0  WHERE LimiteCompra < 0 ";
            _cn.ExecuteNonQuery(sqlCliente);

        }


        public void ModificaRiscoCliente()
        {
            int DiasEmAtraso = VestilloSession.DiasParaBloqueio;
            string IdsAtrasados = string.Empty;
            StringBuilder sqlAtrasados = new StringBuilder();
            var cn = new DapperConnection<ContasReceber>();

           
            StringBuilder sqlEmpresa = new StringBuilder();
            var cnEmpresa = new DapperConnection<Empresa>();

            string sqlCliente = string.Empty;


            sqlEmpresa.AppendLine("SELECT DataInadimplencia FROM empresas WHERE id = " + VestilloSession.EmpresaLogada.Id);
            var emp = new Empresa();
            cnEmpresa.ExecuteToModel(ref emp, sqlEmpresa.ToString());

            

            String DataAgora = DateTime.Now.ToShortDateString(); //data = 01/12/2014


            if (emp != null)
            {
                String DataBanco = emp.DataInadimplencia.ToShortDateString();

                //verifico se a data da empresa é igual a data de hoje,se for não precisa executar novamente
                if (DataAgora == DataBanco)                                                    
                {
                    return;
                }
            }

           

            sqlAtrasados.AppendLine("	SELECT contasreceber.* FROM contasreceber ");
            sqlAtrasados.AppendLine("INNER JOIN colaboradores ON colaboradores.id = contasreceber.IdCliente");
            sqlAtrasados.AppendLine("WHERE ISNULL(DataPagamento)  AND saldo > 0 AND colaboradores.riscocliente <> 1  AND DATEDIFF(now(),DataVencimento) >=" + DiasEmAtraso);
            var cr = new ContasReceber();
            var dados = cn.ExecuteStringSqlToList(cr, sqlAtrasados.ToString());

            if (dados != null && dados.Count() > 0)
            {
                foreach (var item in dados)
                {
                    IdsAtrasados += item.IdCliente + ",";                    
                }               
            }

            if (!String.IsNullOrEmpty(IdsAtrasados))
            {
                IdsAtrasados = IdsAtrasados.Remove(IdsAtrasados.ToString().Length - 1, 1);
            }

            if (!String.IsNullOrEmpty(IdsAtrasados))
            {
                sqlCliente = "UPDATE colaboradores SET riscoclienteOriginal = RiscoCliente, RiscoCliente = 5 WHERE id IN ( " + IdsAtrasados + ")";
                _cn.ExecuteNonQuery(sqlCliente);
            }


            
            string sqlUpEmpresa = "update empresas set DataInadimplencia = " + "'" + TratarData(DataAgora) + "'" + " where id = " + VestilloSession.EmpresaLogada.Id;
            cnEmpresa.ExecuteNonQuery(sqlUpEmpresa);


        }

        public void ModificaRiscoCliente(int IdCliente)
        {
            int DiasEmAtraso = VestilloSession.DiasParaBloqueio;           
            StringBuilder sqlAtrasados = new StringBuilder();
            var cn = new DapperConnection<ContasReceber>();

            
            

            string sqlCliente = string.Empty;

            sqlAtrasados.AppendLine("	SELECT contasreceber.* FROM contasreceber ");
            sqlAtrasados.AppendLine("INNER JOIN colaboradores ON colaboradores.id = contasreceber.IdCliente");
            sqlAtrasados.AppendLine("WHERE ISNULL(DataPagamento) AND DataVencimento <= now() AND saldo > 0 AND contasreceber.IdCliente = " + IdCliente);
            var cr = new ContasReceber();
            var dados = cn.ExecuteStringSqlToList(cr, sqlAtrasados.ToString());

            if (dados == null || dados.Count() == 0)
            {
                sqlCliente = "UPDATE colaboradores SET RiscoCliente = riscoclienteOriginal WHERE id = " + IdCliente;
                _cn.ExecuteNonQuery(sqlCliente);
                
            }

            var cncli = new DapperConnection<Colaborador>();
            StringBuilder sqlClienteStatus = new StringBuilder();
            sqlClienteStatus.AppendLine("	SELECT * FROM colaboradores WHERE id = " + IdCliente);           
            var cli = new Colaborador();
            var dadosCli = cncli.ExecuteStringSqlToList(cli, sqlClienteStatus.ToString());

            if (dadosCli != null || dadosCli.Count() > 0)
            {
                List<Colaborador> crLid = new List<Colaborador>();
                crLid = dadosCli.ToList();
                var Risco = crLid[0].RiscoCliente;

                if (Risco == 5 && (dados == null || dados.Count() == 0))
                {
                    sqlCliente = "UPDATE colaboradores SET RiscoCliente = 2 WHERE id = " + IdCliente;
                    _cn.ExecuteNonQuery(sqlCliente);
                }
            }

        }

        public static string TratarData(object value)
        {
            string DataFormatada = String.Empty;
            if (value == null || value.ToString() == "" || value.ToString() == "0" || value.ToString() == " ")
                return "NULL";
            else
                DataFormatada = value.ToString().Substring(6, 4) + "-" + value.ToString().Substring(3, 2) + "-" + value.ToString().Substring(0, 2);
            return DataFormatada.ToString();
        }


        public bool PossuiCredito(int idCliente,decimal ValorDaCompra)
        {
            bool PossuiCredito = false;

            var SQL = new StringBuilder();
            var cn = new DapperConnection<Colaborador>();

            SQL.AppendLine("select * from colaboradores WHERE id = " + idCliente  + " AND " + FiltroEmpresa());



            Colaborador ret = new Colaborador();


            cn.ExecuteToModel(ref ret, SQL.ToString());


            if (ret != null)
            {
                if (ret.RiscoCliente == 1)
                {
                    PossuiCredito = true;
                }
                else if (ret.RiscoCliente != 5 && ret.RiscoCliente != 6)
                {

                    ValorDaCompra = VerificaValorDosPedidosLiberados(idCliente, ValorDaCompra);
                    if (ret.LimiteCompra >= ValorDaCompra )
                    {
                        PossuiCredito = true;
                    }
                }
               
            }

            return PossuiCredito;

        }

        public Colaborador GetByIdFuncionario(int IdFuncionario)
        {
            var SQL = new Select()
                .Campos("* ")
                .From("colaboradores")
                .Where("IdFuncionario = " + IdFuncionario);

            var colaborador = new Colaborador();
            _cn.ExecuteToModel(ref colaborador, SQL.ToString());
            return colaborador;
        }

        public Colaborador GetPorRefrencia(string Referencia)
        {
            string SQL = String.Empty;

            SQL = "SELECT * FROM colaboradores WHERE colaboradores.referencia = " + "'" + Referencia + "'";

            Colaborador ret = new Colaborador();

            _cn.ExecuteToModel(ref ret, SQL.ToString());

            return ret;

        }

        public int UltimoRegistro()
        {
            int UltimoId = 0;
            var SQL = new StringBuilder();
            var cn = new DapperConnection<Colaborador>();

            SQL.AppendLine("select MAX(Id) as Id from colaboradores ");



            Colaborador ret = new Colaborador();


            cn.ExecuteToModel(ref ret, SQL.ToString());

            UltimoId = ret.Id;
            return UltimoId;

        }


        public IEnumerable<Colaborador> GetPorReferenciaRota(string referencia, String TipoColaborador,int IdRota)
        {
            var SQL = new Select()
                .Campos("id, referencia, nome, razaosocial, idestado, IdVendedor, IdTransportadora, IdRota, idTabelaPreco,CnpjCpf ")
                .From("colaboradores");
            if (TipoColaborador != "")
            {
                if (TipoColaborador == "ClienteFornecedor")
                    SQL.Where("referencia like '%" + referencia + "%' And Cliente = 1 And Fornecedor = 1 And ativo = 1 And " + FiltroEmpresa("idEmpresa") + " AND IdRota = " + IdRota);
                else
                    SQL.Where("referencia like '%" + referencia + "%' And " + TipoColaborador + " = 1 And ativo = 1 " + " AND " + FiltroEmpresa("idEmpresa") + " AND IdRota = " + IdRota);
            }
            else
            {
                SQL.Where("referencia like '%" + referencia + "%' And ativo = 1 " + " AND " + FiltroEmpresa("idEmpresa") + " AND IdRota = " + IdRota);
            }
            SQL.OrderBy("nome");
            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());

            //Colaborador col = new Colaborador();
            //return _cn.ExecuteToList(col, "referencia like '%" + referencia + "%' And " + TipoColaborador + " = 1");
        }

        public IEnumerable<Colaborador> GetPorNomeRota(string nome, String TipoColaborador, int IdRota)
        {

            var SQL = new Select()
                 .Campos("id, referencia, nome, razaosocial, idestado,CnpjCpf ")
                 .From("colaboradores");
            if (TipoColaborador != "")
            {
                if (TipoColaborador == "ClienteFornecedor")
                    SQL.Where("nome like '%" + nome + "%' And Cliente = 1 And Fornecedor = 1 And ativo = 1 " + " AND " + FiltroEmpresa() + " AND IdRota = " + IdRota);
                else
                    SQL.Where("nome like '%" + nome + "%' And " + TipoColaborador + " = 1 And ativo = 1 " + " AND " + FiltroEmpresa() + " AND IdRota = " + IdRota);
            }
            else
            {
                SQL.Where("nome like '%" + nome + "%'  And ativo = 1 " + " AND " + FiltroEmpresa() + " AND IdRota = " + IdRota);
            }
            SQL.OrderBy("nome");
            var col = new Colaborador();
            return _cn.ExecuteStringSqlToList(col, SQL.ToString());

        }

        public decimal VerificaValorDosPedidosLiberados(int CodCliente, decimal CompraAtual)
        {
            decimal Totalpedidos = 0;

            var cn = new DapperConnection<PedidoVenda>();

            string SQL = String.Empty;

            SQL = "select * from pedidovenda where pedidovenda.ClienteId =" + CodCliente + " and (pedidovenda.Status = 7 or pedidovenda.Status = 8)";

            var ped = new PedidoVenda();
            var DadosPedido = cn.ExecuteStringSqlToList(ped, SQL.ToString());

            Totalpedidos = CompraAtual;

        
            foreach (var Item in DadosPedido)
            {
                using (var pedido = new Vestillo.Business.Repositories.ItemLiberacaoPedidoVendaRepository())
                {
                    Totalpedidos += pedido.GetValorDaLiberacao(Item.Id);
                }
            }
            

            return Totalpedidos;

        }

        public void ReferenciaMarketPlace(int IdColaborador,int IdPedido )
        {
            string NovaReferencia = String.Empty;
            string SQL = String.Empty;

            try
            {
                using (ContadorCodigoRepository ContRep = new ContadorCodigoRepository())
                {
                    var dadosCont = ContRep.GetByNome("Colaborador");
                    if (dadosCont != null && dadosCont.Ativo == true)
                    {
                        NovaReferencia = ContRep.GetProximo("Colaborador");
                    }
                    else
                    {
                        int UltimoRegistro = this.UltimoRegistro();
                        UltimoRegistro += 1;
                        NovaReferencia = UltimoRegistro.ToString("D8");

                    }
                    SQL = "Update colaboradores set colaboradores.idempresa = " + VestilloSession.EmpresaLogada.Id + " ,colaboradores.razaosocial = colaboradores.nome,colaboradores.idpais = 1058,ativo = 1,colaboradores.cliente = 1, colaboradores.referencia = " + "'" + NovaReferencia + "'" +
                           " WHERE colaboradores.id = " + IdColaborador;
                    _cn.ExecuteNonQuery(SQL);


                    var RefPedido = String.Empty;
                    var dadosContPed = ContRep.GetByNome("PedidoVenda");
                    if (dadosContPed != null && dadosContPed.Ativo == true)
                    {
                        RefPedido = ContRep.GetProximo("PedidoVenda");
                    }
                    else
                    {
                        int UltimoRegistro = this.UltimoRegistro();
                        UltimoRegistro += 1;
                        RefPedido = UltimoRegistro.ToString("D8");
                    }
                    var cn = new DapperConnection<PedidoVenda>();
                    SQL = "update pedidovenda set Status = 1, Referencia = " + "'" + RefPedido + "'" + " where pedidovenda.Id = " + IdPedido;
                    cn.ExecuteNonQuery(SQL);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
                
        }

        public void AtualizaEnderecoMarketPlace(string Endereco, string Bairro, string mun, string uf,int IdCliente)
        {
            string SQL = String.Empty;
            int IdMuni = 0;
            int IdEst = 0;

            SQL = "Select * from municipiosibge WHERE municipiosibge.municipio = " + "'" + mun + "'";

            var cnMuni = new DapperConnection<MunicipioIbge>();
            var muni = new MunicipioIbge();

            cnMuni.ExecuteToModel(ref muni, SQL.ToString());
            IdMuni = muni.Id;


            SQL = "Select * from estados WHERE estados.abreviatura = " + "'" + uf + "'";

            var cnEst = new DapperConnection<Estado>();
            var est = new Estado();

            cnEst.ExecuteToModel(ref est, SQL.ToString());
            IdEst = est.Id;


            SQL = "Update colaboradores set colaboradores.endereco = " + "'" + Endereco + "'" + ",colaboradores.bairro = " + "'" + Bairro + "'"  + " ,idestado = " + IdEst + " ,idmunicipio = " + IdMuni + " WHERE colaboradores.id = " + IdCliente;
            _cn.ExecuteNonQuery(SQL);

        }
    }
}
