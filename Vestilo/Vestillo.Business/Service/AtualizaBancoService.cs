using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service
{
    public class AtualizaBancoService
    {
        AtualizaBancoRepository _repository = new AtualizaBancoRepository();

        public void AplicarPatch(string versaoSistema)
        {
            try
            {
                string versaoBanco = VestilloSession.VersaoBanco;

                if (versaoBanco != versaoSistema)
                {
                    switch (versaoBanco)
                    {
                        case "1.0.0.0":
                            AplicarPatch1001();
                            break;
                        case "1.0.0.1":
                            AplicarPatch1002();
                            break;
                        case "1.0.0.2":
                            AplicarPatch1003();
                            break;
                        case "1.0.0.3":
                            AplicarPatch1004();
                            break;
                        case "1.0.0.4":
                            AplicarPatch1005();
                            break;
                        case "1.0.0.5":
                            AplicarPatch1006();
                            break;
                        case "1.0.0.6":
                            AplicarPatch1007();
                            break;
                        case "1.0.0.7":
                            AplicarPatch1008();
                            break;
                        case "1.0.0.8":
                            AplicarPatch1009();
                            break;
                        case "1.0.0.9":
                            AplicarPatch1010();
                            break;
                        case "1.0.0.10":
                            AplicarPatch1011();
                            break;
                        case "1.0.1.1":
                            AplicarPatch1012();
                            break;
                        case "1.0.1.2":
                            AplicarPatch1013();
                            break;
                        case "1.0.1.3":
                            AplicarPatch1014();
                            break;
                        case "1.0.1.4":
                            AplicarPatch1015();
                            break;
                        case "1.0.1.5":
                            AplicarPatch1016();
                            break;
                        case "1.0.1.6":
                            AplicarPatch2000();
                            break;
                        case "2.0.0.0":
                            AplicarPatch2001();
                            break;
                        case "2.0.0.1":
                            AplicarPatch2002();
                            break;
                        case "2.0.0.2":
                            AplicarPatch2003();
                            break;
                        case "2.0.0.3":
                             AplicarPatch2005();
                            break;
                        case "2.0.0.5":
                            AplicarPatch2006();
                            break;
                        case "2.0.0.6":
                            AplicarPatch2007();
                            break;
                        case "2.0.0.7":
                            AplicarPatch2009();
                            break;
                        case "2.0.0.9":
                            AplicarPatch2010();
                            break;
                        case "2.0.1.0":
                            AplicarPatch2011();
                            break;
                        case "2.0.1.1":
                            AplicarPatch2012();                            
                            break;
                        case "2.0.1.2":
                            AplicarPatch2013();                            
                            break;
                        case "2.0.1.3":
                            AplicarPatch2014();
                            break;
                        case "2.0.1.4":
                            AplicarPatch2015();
                            break;
                        case "2.0.1.5":
                            AplicarPatch2016();
                            break;
                        case "2.0.1.6":
                            AplicarPatch2017();
                            break;
                        case "2.0.1.7":
                            AplicarPatch2018();
                            break;
                        case "2.0.1.8":
                            AplicarPatch2019();
                            break;
                        case "2.0.1.9":
                            AplicarPatch2020();
                            break;
                        case "2.0.2.0":
                            AplicarPatch2021();
                            break;
                        case "2.0.2.1":
                            AplicarPatch2022();
                            break;
                        case "2.0.2.2":
                            AplicarPatch2023();
                            break;
                        case "2.0.2.3":
                            AplicarPatch2024();
                            break;
                        case "2.0.2.4":
                            AplicarPatch2025();
                            break;
                        case "2.0.2.5":
                            AplicarPatch2026();
                            break;
                        case "2.0.2.6":
                            AplicarPatch2027();
                            break;
                        case "2.0.2.7":
                            AplicarPatch2028();
                            break;
                        case "2.0.2.8":
                            AplicarPatch2029();
                            break;
                        case "2.0.2.9":
                            AplicarPatch2030();
                            break;
                        case "2.0.3.0":
                            AplicarPatch2031();
                            break;
                        case "2.0.3.1":
                            AplicarPatch2032();
                            break;
                        case "2.0.3.2":
                            AplicarPatch2033();
                            break;
                        case "2.0.3.3":
                            AplicarPatch2034();
                            break;
                        case "2.0.3.4":
                            AplicarPatch2035();
                            break;
                        case "2.0.3.5":
                            AplicarPatch2036();
                            break;
                        case "2.0.3.6":
                            AplicarPatch2037();
                            break;
                        case "2.0.3.7":
                            AplicarPatch2038();
                            break;
                        case "2.0.3.8":
                            AplicarPatch2039();
                            break;
                        case "2.0.3.9":
                            AplicarPatch2040();
                            break;
                        case "2.0.4.0":
                            AplicarPatch2041();
                            break;
                        case "2.0.4.1":
                            AplicarPatch2042();
                            break;
                        case "2.0.4.2":
                            AplicarPatch2043();
                            break;
                        case "2.0.4.3":
                            AplicarPatch2044();
                            break;
                        case "2.0.4.4":
                            AplicarPatch2045();
                            break;
                        case "2.0.4.5":
                            AplicarPatch2046();
                            break;
                        case "2.0.4.6":
                            AplicarPacth2047();
                            break;
                        case "2.0.4.7":
                            AplicarPacth2048();
                            break;
                        case "2.0.4.8":
                            AplicarPacth2049();
                            break;
                        case "2.0.4.9":
                            AplicarPacth2050();
                            break;
                        case "2.0.5.0":
                            AplicarPacth2051();
                            break;
                        case "2.0.5.1":
                            AplicarPacth2052();
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch1001()
        {
            List<string> comandos = new List<string>();
            try
            {

                //comandos.Add("insert into parametros(Chave,Valor,EmpresaId) values ('CUSTO_EMPRESA','4',NULL);");
                //comandos.Add("ALTER TABLE premios ADD COLUMN modalidade INT NULL DEFAULT 0;");
                comandos.Add("UPDATE parametros SET Valor = '1.0.0.1' WHERE Chave = 'VersaoBanco';");
                _repository.ExecutarComandos(comandos);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool AplicarPatch1002()
        {
            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("alter table produtos add column TempoPacote decimal (10,4) DEFAULT '1' NULL;");
                comandos.Add("alter table produtodetalhes add column TamanhoUnico tinyint (1)  NULL , add column CorUnica tinyint (1)  NULL  after TamanhoUnico;");
                comandos.Add("alter table movimentacaoestoque add column Baixa tinyint (1)  NULL;");
                comandos.Add("UPDATE parametros SET Valor = '1.0.0.2' WHERE Chave = 'VersaoBanco';");
                _repository.ExecutarComandos(comandos);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool AplicarPatch1003()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("update produtos set ano = 2016 where ano = 0;");
                comandos.Add("update produtos set ano = 2017 where ano = 1;");
                comandos.Add("update produtos set ano = 2018 where ano = 2;");
                comandos.Add("update produtos set ano = 2019 where ano = 3;");
                comandos.Add("update produtos set ano = 2020 where ano = 4;");
                comandos.Add("update produtos set ano = 2021 where ano = 5;");
                comandos.Add("update produtos set ano = 2022 where ano = 6;");

                comandos.Add("insert into permissoes(Chave,Descricao) values ('Contadores','Acesso a tela de Contadores');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Contadores.Visualizar','Visualizar tela de Contadores');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Contadores.Alterar','Alterar tela de Contadores');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('GerarEtiqueta','Acesso a tela de Etiquetas');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('CurvaAbc','Acesso a tela de Curva Abc');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('FluxoCaixa','Acesso a tela de FluxoCaixa de caixa');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('FluxoCaixa.Simular','Acesso a simulação do FluxoCaixa de caixa');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('FluxoCaixa.Imprimir','Acesso a impressão do FluxoCaixa de caixa');");
                comandos.Add("INSERT INTO Permissoesgrupo (PermissaoId, GrupoId) SELECT P.Id, G.Id FROM 	permissoes P, grupos G WHERE  P.Id NOT IN (SELECT PG.Permissaoid FROM Permissoesgrupo PG WHERE PG.grupoid = G.id);");

                /*
                comandos.Add("CREATE TABLE Ocorrencias (Id int(11) NOT NULL AUTO_INCREMENT,Abreviatura varchar(5) NOT NULL,Descricao varchar(50) NOT NULL,Tipo tinyint(1) DEFAULT NULL,PRIMARY KEY (Id)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8  ;");
                comandos.Add("alter table funcionarios add column Referencia varchar (10)  NULL ;");
                */
                comandos.Add("insert into contadorescodigo(Nome,Descricao,Prefixo,ContadorAtual,CasasDecimais,Ativo) values ('Funcionario','Referência no cadastro de Funcionários','FUN','0','10','1');");

                /*
                comandos.Add("CREATE TABLE operacaooperadora (id int(11) NOT NULL AUTO_INCREMENT, PacoteId int(11) DEFAULT NULL, OperacaoId int(11) DEFAULT NULL, FuncionarioId int(11) DEFAULT NULL, FaccaoId int(11) DEFAULT NULL, usuario varchar(60) DEFAULT NULL, data datetime DEFAULT NULL,  PRIMARY KEY (id));");
                */

                //comandos.Add("alter table pedidocompra add column Semana int (11)  NULL;");

                comandos.Add("ALTER TABLE nfe ADD COLUMN PedidosIds TEXT NULL;");
                comandos.Add("ALTER TABLE nfe ADD COLUMN PedidosRefs TEXT NULL;");

                //comandos.Add("ALTER TABLE operacaopadrao ADD COLUMN Manual TINYINT(1) NULL;");
                comandos.Add("UPDATE parametros SET Valor = '1.0.0.3' WHERE Chave = 'VersaoBanco';"); // FIXO         
                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                Funcoes.FinalizarProcessamento();
                throw ex;
            }

        }


        private bool AplicarPatch1004()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("alter table estoque change Empenhado Empenhado decimal (14,5)  NULL;");
                comandos.Add("alter table estoque change Saldo Saldo decimal (14,5)  NULL;");
                comandos.Add(" insert into permissoes(Chave,Descricao) values ('Caixas','Acesso a tela de Caixas'); ");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Caixas.Incluir','Incluir Caixas');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Caixas.Alterar','Alterar Caixas');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Caixas.Excluir','Excluir Caixas');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Caixas.Visualizar','Visualizar Caixas');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Caixas.Abrir','Abrir Caixas');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Caixas.Fechar','Fechar Caixas');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Caixas.Movimento','Movimento dos Caixas');");

                comandos.Add(" CREATE TABLE caixas ( " +
                "id int(10) NOT NULL AUTO_INCREMENT, " +
                "idempresa int(11) NOT NULL, " +
                "referencia varchar(8) NOT NULL, " +
                "descricao varchar(15) NOT NULL, " +
                "dataultabertura datetime DEFAULT NULL, " +
                "dataultfechamento datetime DEFAULT NULL, " +
                "Ativo tinyint(1) DEFAULT NULL, " +
                "saldo decimal(14,5) DEFAULT NULL, " +
                "PRIMARY KEY (id), " +
                "KEY FK_caixas_Empresa (idempresa), " +
                "CONSTRAINT FK_caixas_Empresa FOREIGN KEY (idempresa) REFERENCES empresas (Id) " +
                ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8; ");

                comandos.Add("CREATE TABLE totaiscaixas ( " +
                "id int(10) unsigned NOT NULL AUTO_INCREMENT, " +
                "idempresa int(11) NOT NULL, " +
                "idcaixa int(10) NOT NULL,  " +
                "Idcolaborador int(11) DEFAULT NULL, " +
                "datamovimento datetime DEFAULT NULL, " +
                "tipo tinyint(2) NOT NULL,  " +
                "dinheiro decimal(14,5) DEFAULT NULL, " +
                "cheque decimal(14,5) DEFAULT NULL, " +
                "cartaocredito decimal(14,5) DEFAULT NULL, " +
                "cartaodebito decimal(14,5) DEFAULT NULL,  " +
                "operadoracredito varchar(100) DEFAULT NULL,  " +
                "operadoradebito varchar(100) DEFAULT NULL, " +
                "outros decimal(14,5) DEFAULT NULL,  " +
                "observacao text,  " +
                "PRIMARY KEY (id),  " +
                "KEY FK_totaiscaixa_Colaborador (Idcolaborador),  " +
                "KEY FK_totaiscaixa_Empresa (idempresa),  " +
                "KEY FK_totaiscaixa_Caixa (idcaixa), " +
                "CONSTRAINT FK_totaiscaixa_Caixa FOREIGN KEY (idcaixa) REFERENCES caixas (id), " +
                "CONSTRAINT FK_totaiscaixa_Colaborador FOREIGN KEY (Idcolaborador) REFERENCES colaboradores (id), " +
                "CONSTRAINT FK_totaiscaixa_Empresa FOREIGN KEY (idempresa) REFERENCES empresas (Id) " +
                ") ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1; ");

                comandos.Add(" insert into caixas " +
                " (idempresa, referencia, descricao, dataultabertura, " +
                " dataultfechamento, " +
                " Ativo,saldo " +
                " ) " +
                " values " +
                " (1,'001','*PRINCIPAL',NOW(),NULL,1,0); ");


                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId) values ('HABILITA_CONTROLE_CAIXA','2',NULL);");
                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId) values ('DEFINE_CAIXA_PADRAO','1',NULL); ");

                comandos.Add(" ALTER TABLE nfce ADD COLUMN IdPedido INT NULL AFTER Valorfcpestado; ");
                comandos.Add(" ALTER TABLE nfceitens ADD COLUMN IdItemPedidoVenda INT NULL AFTER alqfcp; ");

                comandos.Add(" UPDATE produtodetalhes SET TamanhoUnico = 0,CorUnica = 0; ");

                comandos.Add(" alter table contasreceber change NumTitulo NumTitulo varchar (20)  NOT NULL; ");

                comandos.Add(" alter table contaspagar change NumTitulo NumTitulo varchar (20)  NOT NULL; ");


                comandos.Add("UPDATE parametros SET Valor = '1.0.0.4' WHERE Chave = 'VersaoBanco';"); // FIXO
                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool AplicarPatch1005()
        {
            List<string> comandos = new List<string>();
            try
            {

                //comandos.Add("insert into parametros(Chave,Valor,EmpresaId) values ('CUSTO_EMPRESA','4',NULL);");
                //comandos.Add("ALTER TABLE premios ADD COLUMN modalidade INT NULL DEFAULT 0;");
                comandos.Add("UPDATE parametros SET Valor = '1.0.0.5' WHERE Chave = 'VersaoBanco';");
                _repository.ExecutarComandos(comandos);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool AplicarPatch1006()
        {
            List<string> comandos = new List<string>();
            try
            {

                //comandos.Add("insert into parametros(Chave,Valor,EmpresaId) values ('CUSTO_EMPRESA','4',NULL);");
                //comandos.Add("ALTER TABLE premios ADD COLUMN modalidade INT NULL DEFAULT 0;");
                comandos.Add("UPDATE parametros SET Valor = '1.0.0.6' WHERE Chave = 'VersaoBanco';");
                _repository.ExecutarComandos(comandos);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool AplicarPatch1007()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);



                //string _startPath = Application.StartupPath;//Caminho do exe.               
                //Process.Start(_startPath + @"\NFe.exe", " SILENT");                
                //Process.Start(_startPath + @"\NFCe.exe", " SILENT");
                //System.Threading.Thread.Sleep(1000 * 50);
                //comandos.Add("insert into parametros(Chave,Valor,EmpresaId) values ('CUSTO_EMPRESA','4',NULL);");
                comandos.Add("alter table produtodetalhes change TamanhoUnico TamanhoUnico tinyint (1) DEFAULT '0' NOT NULL , change CorUnica CorUnica tinyint (1) DEFAULT '0' NOT NULL ;");
                comandos.Add("UPDATE colaboradores set colaboradores.particms = 100;");


                //comandos.Add("ALTER TABLE pedidocompra ADD COLUMN PrevisaoFornecedor DATETIME NULL AFTER PrevisaoEntrega;");


                //comandos.Add("ALTER TABLE pedidocompra ADD COLUMN OrdensReferencia VARCHAR(255) NULL;");

                /*
                comandos.Add(" CREATE TABLE pedidocompraordemproducao (" +
                             "  id INT NOT NULL AUTO_INCREMENT," +
                             " pedidocompraid INT NOT NULL," +
                             "  ordemproducaoid INT NOT NULL," +
                             "  PRIMARY KEY (id)," +
                             "  INDEX FK_PedidoCompraOrdem_PedidoCompra_idx (pedidocompraid ASC)," +
                             "  INDEX FK_PedidoCompraOrdem_idx (ordemproducaoid ASC)," +
                             "  CONSTRAINT FK_PedidoCompraOrdem_PedidoCompra" +
                             "    FOREIGN KEY (pedidocompraid)" +
                             "    REFERENCES vestillo.pedidocompra (Id)" +
                             "    ON DELETE NO ACTION" +
                             "    ON UPDATE NO ACTION," +
                             "  CONSTRAINT FK_PedidoCompraOrdem" +
                             "    FOREIGN KEY (ordemproducaoid)" +
                             "    REFERENCES vestillo.ordemproducao (id)" +
                             "    ON DELETE NO ACTION" +
                             "    ON UPDATE NO ACTION);");
                */

                //comandos.Add("alter table parametros add column VisaoCliente int (10) DEFAULT '1' NOT NULL;");
                comandos.Add("INSERT INTO vestillo.parametros (Chave,Valor,VisaoCliente) VALUES ('SISTEMAS_CONTRATADOS', '1',2);");
                comandos.Add("INSERT INTO parametros (Chave, Valor,VisaoCliente,EmpresaId) SELECT 'DIA_EMAIL_COBRANCA','31/10/2019',2,id from empresas;");
                comandos.Add("INSERT INTO parametros (Chave, Valor,VisaoCliente,EmpresaId) SELECT 'TEXTO_EMAIL_COBRANCA','Caro cliente segue seu(s) titulo(s) próximo(s) ao vencimento',2,id from empresas;");
                comandos.Add("INSERT INTO parametros (Chave, Valor,VisaoCliente,EmpresaId) SELECT 'PRAZO_EMAIL_COBRANCA','10',2,id from empresas;");
                comandos.Add("UPDATE parametros set VisaoCliente = 2 where chave in('CONTROLE_ESTOQUE_ATIVO','ENVIO_XML_AUTOMATICO','USA_TABELA_PRECO_BASE','USA_CONFERENCIA','ESTOQUE_PEDIDO','LIGAR_TIMER_PEDIDO', " +
                             " 'IMPRESSAO_PERSONALIZADA','LEI_DA_MODA','DECONTA_ICMS_PIS_COFINS','EXIBIR_TIPO_NEGOCIO','EXIBIR_ESTIMATIVA_PROD','UTILIZA_SINTEGRA','QTD_DIAS_INADIMPLENTE','CONTROLA_INADIMPLENCIA','OBRIGA_ALERTA_ATIVIDADE','VersaoBanco','HABILITA_CONTROLE_CAIXA','SISTEMAS_CONTRATADOS');");

                comandos.Add("INSERT INTO vestillo.parametros (Chave,Valor,VisaoCliente) VALUES ('OPERACAO_PACOTE', '2',1);");
                comandos.Add("alter table contasreceber add column EnviadoCobranca int (10) DEFAULT '0' NOT NULL;");
                comandos.Add("insert into contadorescodigo(Id,Nome,Descricao,Prefixo,ContadorAtual,CasasDecimais,Ativo) values ( NULL,'SEQ_CODBARRAS','Contador do código de barras do sistema','','1','99','1');");

                comandos.Add("alter table itenspedidocompra add column Selecionado int (10) DEFAULT '0' NOT NULL  after QtdAtendida;");
                comandos.Add("DELETE from parametros where Chave = 'SEQ_CODBARRAS';");

                comandos.Add("UPDATE parametros SET Valor = '1.0.0.7' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private bool AplicarPatch1008()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                //string _startPath = Application.StartupPath;//Caminho do exe.               
                //Process.Start(_startPath + @"\NFe.exe", " SILENT");                
                //Process.Start(_startPath + @"\NFCe.exe", " SILENT");
                //System.Threading.Thread.Sleep(1000 * 50);
                //comandos.Add("insert into parametros(Chave,Valor,EmpresaId) values ('CUSTO_EMPRESA','4',NULL);");

                comandos.Add(" CREATE TABLE planoanual ( " +
                             "  id int(11) NOT NULL AUTO_INCREMENT, " +
                             " EmpresaId int(11) DEFAULT NULL, " +
                             " Referencia varchar(45) DEFAULT NULL, " +
                             " Usuario varchar(255) DEFAULT NULL, " +
                             " Descricao varchar(255) DEFAULT NULL,  " +
                             " Data datetime DEFAULT NULL, " +
                             " Obs text, " +
                             " AnoBase int(11) DEFAULT NULL, " +
                             " Semana varchar(45) DEFAULT NULL, " +
                             " HoraBase int(11) DEFAULT NULL,  " +
                             " PRIMARY KEY (id), " +
                             " KEY FK_planoanual_Empresa (EmpresaId), " +
                             " CONSTRAINT FK_planoanual_Empresa FOREIGN KEY (EmpresaId) REFERENCES empresas (Id)   " +
                             " ) ENGINE=InnoDB DEFAULT CHARSET=utf8 ; ");

                comandos.Add(" CREATE TABLE planoanualdetalhes (  " +
                             " Id int(11) NOT NULL AUTO_INCREMENT,  " +
                              " PlanoId int(11) DEFAULT NULL, " +
                              " GrupoId int(11) DEFAULT NULL, " +
                              " Mes int(11) DEFAULT NULL, " +
                              " Costureira int(11) DEFAULT NULL, " +
                              " DiasUteis int(11) DEFAULT NULL, " +
                              " Jornada decimal(14,5) DEFAULT NULL, " +
                              " Presenca decimal(14,5) DEFAULT NULL, " +
                              " Aproveitamento decimal(14,5) DEFAULT NULL, " +
                              " Eficiencia decimal(14,5) DEFAULT NULL, " +
                              " TempoMedio decimal(14,5) DEFAULT NULL, " +
                              " PRIMARY KEY (Id), " +
                              " KEY FK_planoanualdetalhes (PlanoId), " +
                              " KEY FK_planoanualdetalhes_Grupo (GrupoId), " +
                              " CONSTRAINT FK_planoanualdetalhes FOREIGN KEY (PlanoId) REFERENCES planoanual (id), " +
                              " CONSTRAINT FK_planoanualdetalhes_Grupo FOREIGN KEY (GrupoId) REFERENCES grupoprodutos (id)  " +
                              " ) ENGINE=InnoDB DEFAULT CHARSET=utf8; ");


                //comandos.Add(" ALTER TABLE pacotes ADD COLUMN concluido DECIMAL(10,5) NOT NULL DEFAULT 0 AFTER cupomemgrupo; ");

                comandos.Add(" insert into permissoes(Chave,Descricao) values ('RelPedidoCompra','Acesso ao Rel Pedido de Compras');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT 	DISTINCT P.Id AS PermissaoId,  " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'RelPedidoCompra%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");

                comandos.Add(" insert into permissoes(Chave,Descricao) values ('ListaCompra','Relatório Lista de Compra');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT 	DISTINCT P.Id AS PermissaoId,  " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'ListaCompra%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");

                comandos.Add(" insert into permissoes(Chave,Descricao) values ('PlanoAnual','Plano Anual de Produção');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT 	DISTINCT P.Id AS PermissaoId,  " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'PlanoAnual%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");




                comandos.Add("UPDATE parametros SET Valor = '1.0.0.8' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch1009()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                //string _startPath = Application.StartupPath;//Caminho do exe.               
                //Process.Start(_startPath + @"\NFe.exe", " SILENT");                
                //Process.Start(_startPath + @"\NFCe.exe", " SILENT");
                //System.Threading.Thread.Sleep(1000 * 50);
                //comandos.Add("insert into parametros(Chave,Valor,EmpresaId) values ('CUSTO_EMPRESA','4',NULL);");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('QTD_MAXIMA_DIAS_LOG','30',NULL,'2'); ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('DIA_LIMPEZA_LOG','31/12/2019',NULL,'2'); ");

                //comandos.Add("ALTER TABLE grupooperacoes ADD COLUMN balanceamentoId INT NULL AFTER setorId;");

                comandos.Add(" CREATE TABLE balanceamentos ( Id INT NOT NULL AUTO_INCREMENT, EmpresaId INT NOT NULL, Referencia VARCHAR(45) NULL," +
                             " Descricao VARCHAR(70) NULL, Usuario VARCHAR(100) NULL, Data DATETIME NULL, Ordens TEXT NULL, Pacotes TEXT NULL, " +
                             " Produtos TEXT NULL, PRIMARY KEY (Id), INDEX FK_Balamceamentos_Empresa_idx (EmpresaId ASC), " +
                             " CONSTRAINT FK_Balamceamentos_Empresa FOREIGN KEY (EmpresaId) REFERENCES vestillo.empresas (Id) ON DELETE RESTRICT ON UPDATE RESTRICT);");

                comandos.Add(" CREATE TABLE balanceamentoproduto (Id int(11) NOT NULL AUTO_INCREMENT, BalanceamentoId int(11) DEFAULT NULL, " +
                              " SetorId int(11) DEFAULT NULL,ProdutoId int(11) DEFAULT NULL,Quantidade decimal(15,5) DEFAULT NULL,Tempo decimal(15,5) DEFAULT NULL, " +
                              " TempoPacote decimal(15,5) DEFAULT NULL, PRIMARY KEY (Id), KEY FK_balanceamentoproduto_balanceamentos_idx (BalanceamentoId), " +
                              " KEY FK_balanceamentoproduto_setor_idx (SetorId), CONSTRAINT FK_balanceamentoproduto_balanceamentos FOREIGN KEY (BalanceamentoId) REFERENCES balanceamentos (Id) ON DELETE RESTRICT  ON UPDATE RESTRICT," +
                              " CONSTRAINT FK_balanceamentoproduto_setor FOREIGN KEY (SetorId) REFERENCES setores (Id)  ON DELETE RESTRICT  ON UPDATE RESTRICT);");


                comandos.Add(" CREATE TABLE balanceamentoproducao (Id int(11) NOT NULL AUTO_INCREMENT, BalanceamentoId int(11) DEFAULT NULL,SetorId int(11) DEFAULT NULL, Total decimal(15,5) DEFAULT NULL," +
                              " Operadoras decimal(15,5) DEFAULT NULL, Eficiencia decimal(10,2) DEFAULT NULL, MinutoProducao decimal(15,5) DEFAULT NULL, Status int(11) NOT NULL DEFAULT '0', PRIMARY KEY (Id), " +
                              " KEY FK_balanceamentoproducao_balanceamento_idx (BalanceamentoId), KEY FK_balanceamantoproducao_setores_idx (SetorId), CONSTRAINT FK_balanceamantoproducao_setores FOREIGN KEY (SetorId) REFERENCES setores (Id) ON DELETE RESTRICT ON UPDATE RESTRICT, " +
                              " CONSTRAINT FK_balanceamentoproducao_balanceamento FOREIGN KEY (BalanceamentoId) REFERENCES balanceamentos (Id) ON DELETE RESTRICT ON UPDATE RESTRICT);");

                comandos.Add("insert into permissoes(Chave,Descricao) values ('Balanceamento','Acesso a tela de Balanceamento');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Balanceamento.Incluir','Incluir Balanceamento');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Balanceamento.Alterar','Alterar Balanceamento');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Balanceamento.Excluir','Excluir Balanceamento');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Balanceamento.Visualizar','Visualizar Balanceamento');");


                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) SELECT 	DISTINCT P.Id AS PermissaoId, G.Id AS GrupoId " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Balanceamento%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");



                comandos.Add("UPDATE parametros SET Valor = '1.0.0.9' WHERE Chave = 'VersaoBanco';");



                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool AplicarPatch1010()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('COPIA_DESCRICAO_CUPOM','2',NULL,'1'); ");
                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('CASAS_TEMPO','4',NULL,'1'); ");
                //comandos.Add(" ALTER TABLE ordemproducaomateriais ADD COLUMN DestinoId INT NULL AFTER itemordemproducaoid,ADD COLUMN sequencia INT NULL AFTER DestinoId; ");
                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('USA_FILTRO_PACOTE','2',NULL,'1'); ");


                comandos.Add("DELETE a FROM parametros AS a, parametros AS b WHERE a.Chave=b.Chave AND a.id > b.id");

                comandos.Add("DELETE a FROM destinos AS a, destinos AS b WHERE a.Descricao=b.Descricao AND a.id > b.id");

                comandos.Add("DELETE a FROM percentuaisempresas AS a, percentuaisempresas AS b WHERE a.EmpresaId=b.EmpresaId AND a.id > b.id");



                comandos.Add("UPDATE parametros SET Valor = '1.0.0.10' WHERE Chave = 'VersaoBanco';");


                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch1011()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("alter table ordemproducao add column TotalItens decimal (10,4) DEFAULT '0' NOT NULL");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('EXIBIR_TELEFONE_NFE','2',NULL,'1');");

                comandos.Add("UPDATE parametros SET Valor = '1.0.1.1' WHERE Chave = 'VersaoBanco';");


                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch1012()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("CREATE TABLE operacaofaccao ( " +
                             " id int(11) NOT NULL AUTO_INCREMENT, " +
                             " PacoteId int(11) DEFAULT NULL, " +
                             " OperacaoId int(11) DEFAULT NULL, " +
                             " FaccaoId int(11) DEFAULT NULL, " +
                             " usuario varchar(60) DEFAULT NULL, " +
                             " data datetime DEFAULT NULL, " +
                             " sequencia varchar(3) DEFAULT NULL, " +
                             " PRIMARY KEY (id));");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('QTD_PACOTE_PADRAO','40',NULL,'1');");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('DIAS_PERCENTUAIS_EMPRESA','30',NULL,'1');");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('CASAS_MOVIMENTO','2',NULL,'1');");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('ATUALIZA_PRECO','2',NULL,'1');");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('VALIDA_DIFERIMENTO','1',NULL,'1');");

                comandos.Add("UPDATE parametros SET Valor = '1.0.1.2' WHERE Chave = 'VersaoBanco';");

                comandos.Add("UPDATE usuarios SET Senha = '13023722' WHERE Nome = 'User';");


                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch1013()
        {
            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('HABILITA_DATA_SAIDA','2',NULL,'2');");

                comandos.Add("alter table pedidovenda add column DataSaida datetime  NULL;");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('PESQUISA_ITEM_EAN_XML','2',NULL,'2');");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('TRATA_OPERACAO_MANUAL','2',NULL,'2');");

                comandos.Add(" insert into permissoes(Chave,Descricao) values ('Cargo','Acesso a tela de Cargos'); ");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Cargo.Incluir','Incluir Cargos');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ('Cargo.Alterar','Alterar Cargos');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Cargo.Excluir','Excluir Cargos');");
                comandos.Add("insert into permissoes(Chave,Descricao) values ( 'Cargo.Visualizar','Visualizar Cargos');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT 	DISTINCT P.Id AS PermissaoId,  " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Cargo%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");

                comandos.Add("UPDATE parametros SET Valor = '1.0.1.3' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);



                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool AplicarPatch1014()
        {
            String DataAgora = DateTime.Now.ToShortDateString(); //data = 01/12/2014

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add(" alter table movimentacaoestoque change Saida Saida decimal (14,5)  NULL , change Entrada Entrada decimal (14,5)  NULL; ");
                comandos.Add("alter table notaentradaitens change quantidade quantidade decimal (14,5)  NULL;");
                comandos.Add("alter table estoqueemtransito change Quantidade Quantidade decimal (14,5)  NOT NULL;");
                comandos.Add(" alter table itenspedidocompra change QtdAtendida QtdAtendida decimal (14,5)  NULL;");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('IMAGEM_REL_PEDIDO_COMPRA','2',NULL,'2');");

                comandos.Add("alter table produtos change QtdPacote QtdPacote decimal (10,4) DEFAULT '50.0000 ' NULL , change TempoPacote TempoPacote decimal (10,4) DEFAULT '0.7000' NULL;");

                comandos.Add(" CREATE TABLE versao (Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " Cnpj varchar(50) NOT NULL, " +
                             " Dia varchar(50) NOT NULL, " +
                             " Bloqueado enum('NAO','SIM') COLLATE utf8_unicode_ci NOT NULL DEFAULT 'NAO', " +
                             " PRIMARY KEY (Id) " +
                             " ) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8  ;");



                comandos.Add(" insert into versao(Cnpj, Dia, Bloqueado) " +
                             " SELECT empresas.CNPJ, " + "'" + DataAgora + "'" + " ,'NAO' from empresas where empresas.CNPJ <> ''");

                comandos.Add("UPDATE versao SET versao.Cnpj = replace(replace(Replace(versao.Cnpj,'-',''),'.',''),'/','')");

                comandos.Add("UPDATE parametros SET Valor = '1.0.1.4' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch1015()
        {
            // liberação em 07-05-2020

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("alter table setores change IdDepartamento IdDepartamento int (11)  NULL;");

                comandos.Add(" CREATE TABLE fichafaccao ( " +
                            "id INT(11) NOT NULL AUTO_INCREMENT PRIMARY KEY, " +
                            "idFicha INT(11) NOT NULL, " +
                            "idFaccao INT(11) NOT NULL, " +
                            "valorPeca DECIMAL(14, 4) NOT NULL, " +
                            "FOREIGN KEY(idFicha) REFERENCES fichatecnica(Id), " +
                            "FOREIGN KEY(idFaccao) REFERENCES colaboradores(id)); ; ");

                comandos.Add("UPDATE parametros SET Valor = '1.0.1.5' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch1016()
        {
            // liberação em 07-05-2020

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("alter table contadorescodigo change Prefixo Prefixo varchar (10)  NULL;");

                comandos.Add("insert into contadorescodigo(Nome,Descricao,Prefixo,ContadorAtual,CasasDecimais,Ativo) values ('TabPrecoPCP','Contador da tabela de preço de produção','','1','3','1'); ");

                comandos.Add("alter table vestillo.itenspedidocompra add column Excedente decimal (10,4)  NULL;");

                comandos.Add("alter table vestillo.notaentradaitens add column Excedente decimal (10,4)  NULL;");

                comandos.Add("UPDATE parametros SET Valor = '1.0.1.6' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool AplicarPatch2000()
        {
            // liberação em 07-05-2020

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("ALTER TABLE contaspagar ADD IdCheque int(11);");

                comandos.Add("insert into permissoes(Chave, Descricao) values('FichaTecnica.FichaFaccao', 'Incluir Facção na ficha selecionada');");

                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT 	DISTINCT P.Id AS PermissaoId,  " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'FichaTecnica.FichaFaccao%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.0.0' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch2001()
        {
            // liberação em 02-06-2020

            List<string> comandos = new List<string>();
            try
            {
                //Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);



                comandos.Add("UPDATE parametros SET Valor = '2.0.0.1' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                //Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch2002()
        {
            // liberação em 02-06-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                contador = _repository.RegistroExiste("select count(*) as contador FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'colaboradores' AND COLUMN_NAME = 'DebitoAntigo'");
                if (contador == 0)
                {
                    comandos.Add("alter table colaboradores add column DebitoAntigo decimal(10, 2) DEFAULT '0.00' NULL;");
                }

                comandos.Add("insert into permissoes(Chave,Descricao) values ('Cheque.Relatorio','Acesso ao Rel de cheques');");

                comandos.Add(" INSERT INTO Permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT P.Id, G.Id FROM 	permissoes P, grupos G WHERE P.Chave LIKE 'Cheque.Relatorio%' " +
                             " AND P.Id NOT IN (SELECT PG.Permissaoid FROM Permissoesgrupo PG WHERE PG.grupoid = G.id);");


                contador = _repository.RegistroExiste("select count(*) as contador FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'atividades' AND COLUMN_NAME = 'RefTitulo'");
                if (contador == 0)
                {
                    comandos.Add("alter table atividades add column RefTitulo varchar (30)  NULL ;");
                }

                contador = _repository.RegistroExiste("select count(*) as contador FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'atividades' AND COLUMN_NAME = 'ValorTitulo'");
                if (contador == 0)
                {
                    comandos.Add("alter table atividades add column ValorTitulo decimal (10,4)  NULL;");
                }


                contador = _repository.RegistroExiste("select count(*) as contador FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'atividades' AND COLUMN_NAME = 'IdTitulo'");
                if (contador == 0)
                {
                    comandos.Add("alter table atividades add column IdTitulo int (11) DEFAULT '0' NULL ;");
                }

                contador = _repository.RegistroExiste("select count(*) as contador FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'contasreceber' AND COLUMN_NAME = 'PossuiAtividade'");
                if (contador == 0)
                {
                    comandos.Add("alter table contasreceber add column PossuiAtividade int (10) DEFAULT '0' NOT NULL;");
                }


                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('EMITIR_NFCE_DIRETO','2',NULL,'1');");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('NOME_IMPRESSORA_NFCE','Elgin',NULL,'1');");


                comandos.Add("UPDATE parametros SET Valor = '2.0.0.2' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch2003()
        {
            // liberação em 27-07-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('EMPRESA_NAOUSA_NFCE','2','1','2');");

                comandos.Add(" alter table bancos add column NomeBanco int (11)  NULL,add column carteira int (11)  NULL,add column remessa int (11)  NULL, add column retorno int (11),add column contabanco varchar (50)  NULL,add column DigitoContabanco varchar (50)  NULL, " +
                             " add column Agenciabanco varchar (50)  NULL,add column DigitoAgenciabanco varchar (50)  NULL, add column Convenio varchar (100)  NULL,add column CodigoTransmissao varchar (100)  NULL, add column DiretorioRemessa varchar (1000)  NULL, " +
                             " add column Multa decimal (10,4)  NULL, add column Juros decimal (10,4),add column TarifaBoleto decimal (10,4),add column DiasTolerancia int (11)  NULL,add column DiasBaixa int (11)  NULL,add column TipoProtesto int (11)  NULL, " +
                             " add column DiasParaProtesto int (11)  NULL,add column InstrucaoNaBaixa int (1), add column MensagemCaixa longtext NULL;");

                comandos.Add(" CREATE TABLE contadornossonumero ( " +                                                            
                       " id int(11) NOT NULL AUTO_INCREMENT, " +
                       " IdBanco int(11) NOT NULL, " +
                       " NumeracaoAtual int(11) DEFAULT NULL, " +
                       " PRIMARY KEY(id), " +
                       " KEY FK_contadornossonumero_Banco(IdBanco), " +
                       " CONSTRAINT FK_contadornossonumero_Banco FOREIGN KEY(IdBanco) REFERENCES bancos(id) " +
                     " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; "); 

                comandos.Add("insert into contadornossonumero(IdBanco,NumeracaoAtual) values ('1009','0');");

                comandos.Add("alter table contasreceber add column PossuiBoleto int (10) DEFAULT '0' NOT NULL;");

                comandos.Add("alter table contasreceber add column BancoPortador int (11)  NULL;");

                comandos.Add("alter table contasreceber add column RemessaGerada int(11) NOT NULL DEFAULT '0';");

                comandos.Add("CREATE TABLE instrucoesporbancos (  " +
                             " Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " IdBanco int(11) NOT NULL, " +
                             " IdInstrucao int(11) NOT NULL, " +
                             " Descricao varchar(255) NOT NULL, " +
                             " PRIMARY KEY(Id) " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,1, 'Remessa');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,2, 'Pedido de baixa');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,4, 'Concessão de abatimento');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,5, 'Cancelamento de abatimento concedido');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,6, 'Alteração de vencimento');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,7, 'Alteração do controle do participante');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,8, 'Alteração de seu número');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,9, 'Pedido de protesto (emite aviso ao sacado após xx dias do vencimento, e envia ao cartório após 5 dias úteis)');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,10, 'Não protestar(inibe protesto automático, quando houver instrução permanente na conta corrente)');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,11, 'Pedido de Protesto Falimentar');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,18, 'Sustar protesto');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,30, 'Exclusão do sacador avalista');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,31, 'Alteração de outros dados');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,34, 'Baixa por ter sido pago diretamente ao cedente');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,35, 'Cancelamento de instrução');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,37, 'Alteração do vencimento e sustar protesto');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,38, 'Cedente não concorda com alegação do sacado');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,47, 'Cedente solicita dispensa de juros');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,66, 'Entrada em negativação expressa');");
                comandos.Add("insert into instrucoesporbancos(IdBanco,IdInstrucao, Descricao) values (341,67, 'Não Negativar(Inibe entrada em negativação quando houver)');");



                comandos.Add(" CREATE TABLE instrucoesdosboletos ( " +                                                           
                        " Id int(11) NOT NULL AUTO_INCREMENT, " +
                        " IdEmpresa int(11) NOT NULL, " +
                        " DataEmissao datetime DEFAULT NULL, " +
                        " IdBoleto int(11) NOT NULL, " +
                        " IdBanco int(11) NOT NULL, " +
                        " IdInstrucao int(11) NOT NULL, " +
                        " RemessaGerada int(11) NOT NULL DEFAULT '0', " +
                        " PRIMARY KEY(Id), " +
                        " KEY FK_instrucoesdosboletos_banco(IdBanco), " +
                        " CONSTRAINT FK_instrucoesdosboletos_banco FOREIGN KEY(IdBanco) REFERENCES bancos(id) " +
                        " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");



                comandos.Add(" CREATE TABLE boletosgerados ( " +                                                      
                  " Id int(11) NOT NULL AUTO_INCREMENT, " +
                  " IdEmpresa int(11) NOT NULL, " +
                  " DataEmissaoBoleto datetime DEFAULT NULL," +
                  " idBanco int(11) NOT NULL, " +
                  " idTitulo int(11) NOT NULL, " +
                  " idCliente int(11) NOT NULL, " +
                  " Carteira varchar(10) NOT NULL, " +
                  " Variacao int(10) DEFAULT NULL, " +
                  " DiasProtesto int(10) DEFAULT NULL, " +
                  " NossoNumero varchar(10) NOT NULL, " +
                  " DvNossoNumero varchar(10) NOT NULL, " +
                  " Juros decimal(10, 2) DEFAULT NULL, " +
                  " DiasBaixaDevolucao int(10) DEFAULT NULL, " +
                  " TipoProtesto int(10) DEFAULT NULL, " +
                  " NumDocumento varchar(10) DEFAULT NULL, " +
                  " PRIMARY KEY(Id), " +
                  " KEY FK_boletosgerados(idBanco), " +
                  " CONSTRAINT FK_boletosgerados FOREIGN KEY(idBanco) REFERENCES bancos(id) " +
                  " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8;");

                comandos.Add("insert into permissoes(Chave,Descricao) values ('RelEstoqueConsulta','Acesso na conulta gestão de estoques');");

                comandos.Add(" INSERT INTO Permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT P.Id, G.Id FROM 	permissoes P, grupos G WHERE P.Chave LIKE 'RelEstoqueConsulta%' " +
                             " AND P.Id NOT IN (SELECT PG.Permissaoid FROM Permissoesgrupo PG WHERE PG.grupoid = G.id);");
                

                comandos.Add("UPDATE parametros SET Valor = '2.0.0.3' WHERE Chave = 'VersaoBanco';"); //PROXIMO MUDAR PRA 2.0.0.5

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;
            }
            catch (VestilloException ex)
            {
                return true;
            }
        }


        private bool AplicarPatch2005()
        {
            // liberação em 21-08-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("CREATE TABLE contadorremessa ( " +
                   " id int(11) NOT NULL AUTO_INCREMENT," +
                   " IdBanco int(11) NOT NULL," +
                   " NumeracaoAtual int(11) DEFAULT NULL," +
                   " UltimoArquivoGerado varchar(100) DEFAULT NULL," +
                   " PRIMARY KEY(id)," +
                   " KEY FK_contadorRemessa_Banco(IdBanco)," +
                   " CONSTRAINT FK_contadorRemessa_Banco FOREIGN KEY(IdBanco) REFERENCES bancos(id)" +
                   " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");                               


                contador = _repository.RegistroExiste("select count(*) as contador FROM bancos WHERE id = 1009");
                if (contador > 0)
                {
                    comandos.Add("delete from contadorremessa;");
                    comandos.Add("insert into contadorremessa(IdBanco,NumeracaoAtual,UltimoArquivoGerado) values (1009,'0','050820A');");
                }
                else
                {
                    comandos.Add("delete from contadorremessa;");
                    comandos.Add("insert into contadorremessa(IdBanco,NumeracaoAtual,UltimoArquivoGerado) values (999,'0','050820A');");
                }

                contador = _repository.RegistroExiste("select count(*) as contador FROM parametros WHERE Chave ='ALTERA_TITULO_OPERACAO'");
                if (contador <= 0)
                {
                    comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('ALTERA_TITULO_OPERACAO','2',NULL,'2');");
                }

                comandos.Add("insert into permissoes(Chave, Descricao) values('ContasAReceber.Boleto', 'Libera impressão do boleto');");

                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                            " SELECT  DISTINCT P.Id AS PermissaoId, " +
                            " G.Id AS GrupoId " +
                            " FROM Permissoes P " +
                            "  INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                            " WHERE P.Chave LIKE 'ContasAReceber.Boleto%' AND " +
                            " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add("alter table colaboradores add column ContatoCobranca varchar (200)  NULL  after DebitoAntigo, " + 
                            " add column TelefoneCobranca varchar (200)  NULL  after ContatoCobranca, " + 
                            " add column EmailCobranca varchar (300)  NULL  after TelefoneCobranca;");

                comandos.Add("insert into permissoes(Chave,Descricao) values ('GestaoCompras','Gestão de Compras Relatório');");

                comandos.Add(" INSERT INTO Permissoesgrupo (PermissaoId, GrupoId) " + 
                            " SELECT P.Id, G.Id FROM  permissoes P, grupos G WHERE P.Chave LIKE 'GestaoCompras%' " +
                            " AND P.Id NOT IN(SELECT PG.Permissaoid FROM Permissoesgrupo PG WHERE PG.grupoid = G.id); ");



                comandos.Add("UPDATE parametros SET Valor = '2.0.0.5' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool AplicarPatch2006()
        {
            // liberação em 14-09-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("CREATE TABLE UsuarioEmail ( " +
                " Id int(11) NOT NULL AUTO_INCREMENT, " +
                " IdUsuario int(11) NOT NULL, " +
                " Dia varchar(50) NOT NULL," +
                " Executou enum('NAO','SIM') CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL DEFAULT 'NAO', " +
                " PRIMARY KEY(Id) " +
                " ) ENGINE=InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8;");

                comandos.Add("CREATE TABLE controlaemailcobranca ( " +                 
                       " Id int(11) NOT NULL AUTO_INCREMENT, " +
                       "   Dias int(11) DEFAULT NULL, " +
                       "   TipoEnvio int(11) DEFAULT NULL, " +
                       "   TextoEmail longtext, " +
                       "   Ativo tinyint(1) DEFAULT NULL, " +
                       "   PRIMARY KEY(Id) " +
                       " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add(" alter table contasreceber add column UltimoEmail datetime NULL  after RemessaGerada; ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('UTILIZA_EMAIL_COBRANCA','2',NULL,'1') ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('IMPRIMIR_BOLETO','2',NULL,'1'); ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('QTD_VIAS_NFCE','1',NULL,'1'); ");

                comandos.Add(" insert into controlaemailcobranca(Dias,TipoEnvio,TextoEmail,Ativo) values ('10','0','Caro cliente, faltam 10 dias para o vencimento do seu título.',1); ");

                comandos.Add(" insert into controlaemailcobranca(Dias,TipoEnvio,TextoEmail,Ativo) values ('0','1','Caro cliente, lembre que seu título vence hoje, mantenha seus pagamentos em dia.',1); ");

                comandos.Add(" insert into controlaemailcobranca(Dias,TipoEnvio,TextoEmail,Ativo) values ('10','2','Caro cliente, seu título se encontra vencido a 10 dias, acerte seus pagamentos e mantenha seu crédito.',1); ");

                comandos.Add(" insert into controlaemailcobranca(Dias,TipoEnvio,TextoEmail,Ativo) values ('0','3','Caro cliente, seu título se encontra vencido a mais de 30 dias, acerte seus pagamentos e mantenha seu crédito.',1);");

                comandos.Add(" insert into usuarioemail(IdUsuario,Dia,Executou) values ('2','14/09/2020','SIM'); ");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('Cobranca','Controla período de envio de e-mail');");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('Cobranca.Visualizar','Visualiza  período de envio de e-mail');");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('Cobranca.Incluir','Inclui Controle período de envio de e-mail');");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('Cobranca.Alterar','Altera Controle período de envio de e-mail');");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('Cobranca.Excluir','Exclui Controle período de envio de e-mail');");

                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                              " SELECT    DISTINCT P.Id AS PermissaoId, " +
                              " G.Id AS GrupoId " +
                              " FROM Permissoes P " +
                              " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                              " WHERE P.Chave LIKE 'Cobranca%' AND " +
                              " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('DEFINIR_TEMPO_PACOTE','0,70',NULL,'1');");

                comandos.Add("alter table produtos change TempoPacote TempoPacote decimal (10,4)  NULL;");


                comandos.Add("insert into permissoes (Chave, Descricao)values('CreditosClientes.Incluir','Incluir créditos para clientes');");

                comandos.Add("insert into permissoes (Chave, Descricao)values('CreditosClientes.Excluir','Excluir créditos para clientes');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'CreditosClientes%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("alter table creditoscliente add column InclusaoManual tinyint (1) DEFAULT '0' NOT NULL;");

                comandos.Add("alter table formularios add column TipoSistema tinyint (1) DEFAULT '1' NOT NULL  COMMENT '1 gestão,2 produção, 3 Ambos';");

                comandos.Add("UPDATE parametros SET Valor = '2.0.0.6' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch2007()
        {
            // liberação em 22-10-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add(" CREATE TABLE transferencia ( " +
                             " Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " Idempresa int(11) NOT NULL, " +
                             " idAlmoxarifadoOrigem int(11) DEFAULT NULL, " +
                             " idAlmoxarifadoDestino int(11) DEFAULT NULL, " +
                             " TipoTransferencia tinyint(1) DEFAULT NULL, " +
                             " IdCliente int(11)  NULL, " +
                             " referencia varchar(10) DEFAULT NULL, " +
                             " DataInclusao datetime DEFAULT NULL, " +
                             " Usuario varchar(100) DEFAULT NULL, " +
                             " Obs longtext, " +
                             " totalitens int(11) DEFAULT NULL, " +
                             " NotasTransferidas  longtext   NULL, " +
                             " IdsDasNotas longtext   NULL, " +
                             " PRIMARY KEY(Id), " +
                             " KEY FK_transferencia_Empresa(Idempresa), " +
                             " KEY FK_transferencia_AlmoxarifadoOrigem(idAlmoxarifadoOrigem), " +
                             " KEY FK_transferencia_AlmoxarifadoDestino(idAlmoxarifadoDestino), " +
                             " KEY FK_transferencia_Cliente(IdCliente), " +
                             " CONSTRAINT FK_transferencia_AlmoxarifadoOrigem FOREIGN KEY(idAlmoxarifadoOrigem) REFERENCES almoxarifados(id), " +
                             " CONSTRAINT FK_transferencia_AlmoxarifadoDestino FOREIGN KEY(idAlmoxarifadoDestino) REFERENCES almoxarifados(id), " +
                             " CONSTRAINT FK_transferencia_Cliente FOREIGN KEY(IdCliente) REFERENCES colaboradores(id), " +
                             " CONSTRAINT FK_transferencia_Empresa FOREIGN KEY(Idempresa) REFERENCES empresas(Id) " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add(" CREATE TABLE transferenciaitens ( " +
                             "  Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " Idtransferencia int(11) NOT NULL, " +
                             " iditem int(11) NOT NULL, " +
                             " idcor int(11) DEFAULT NULL, " +
                             " idtamanho int(11) DEFAULT NULL, " +
                             " quantidade decimal(10, 4) DEFAULT NULL, " +
                             " PRIMARY KEY(Id), " +
                             " KEY FK_transferenciaitens_transferencia(Idtransferencia), " +
                             " CONSTRAINT FK_transferenciaitens_transferencia FOREIGN KEY(Idtransferencia) REFERENCES transferencia(Id) " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");


                comandos.Add(" insert into contadorescodigo(Nome,Descricao,Prefixo,ContadorAtual,CasasDecimais,Ativo) values ('Transferencia','Contador de transferências','','0','10','1');");

                comandos.Add("alter table formularios change ID ID int(11)  NOT NULL AUTO_INCREMENT , add primary key(ID);");

                comandos.Add(" insert into formularios(NomeForm,TipoSistema) values ('Transferência de Itens','3'); ");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('Transferencia','Exibe a ela de Transferencia');");
                comandos.Add(" insert into permissoes (Chave, Descricao)values('Transferencia.Incluir','Incluir Transferencia');");
                comandos.Add(" insert into permissoes (Chave, Descricao)values('Transferencia.Imprimir','Imprimir Listagem de  Transferencia');");
                comandos.Add(" insert into permissoes (Chave, Descricao)values('Transferencia.Visualizar','Visualizar Listagem de  Transferencia');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Transferencia%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add(" alter table nfe add column Transferida tinyint (1) DEFAULT '0' NOT NULL;");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('ESTOQUE_PADRAO_NFCE','1','1','1'); ");

               

                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Balanceamentos',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Balanceamento de Produção',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Balanceamento de Produto',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Calendário',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Cargos',2); ");

                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Controle de Falta',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Departamentos',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Despesas Fixa e Variável',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Destinos',2); ");

                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Exceção de Calendário',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Ficha Técnica de Operação',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Ficha Tecnica Material',2); ");



                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Funcionários',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Ocorrências',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Operacão Padrão',2); ");


                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Pacotes de Produção',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Percentuais da Empresa',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Plano Anual',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Prêmios',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Tabela de Preços PCP',2); ");
                comandos.Add("insert into formularios (NomeForm, TipoSistema)	values	('Ordem de Produção',2); ");



                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Cor'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Usuarios'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Tamanho'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Unidade de Medida'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Cond.Pagamento'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Grupo dos Itens'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Almoxarifado'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Tipo de Item'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Segmento'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Coleção'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Fornecedor'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Permissões de Usuário'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Itens'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Empresas'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Colaborador'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Grupo de Permissao'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Parâmetros'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Estoque'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Servico'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Pedido de Venda'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Nota de Entrada'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Pedido de Compra'; ");
                comandos.Add("update formularios set TipoSistema = '3' WHERE  NomeForm='Log Sistema'; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.0.7' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool AplicarPatch2009() // FAZER AMANHÃ IEIE GLUGLU
        {
            // liberação em 22-10-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);


                comandos.Add(" delete from  parametros WHERE Chave='SISTEMAS_CONTRATADOS';");

                comandos.Add(" insert into parametros 	(Chave, Valor, EmpresaId, VisaoCliente) SELECT 'SISTEMAS_CONTRATADOS',1,id,2 from empresas; ");

                contador = _repository.RegistroExiste("select count(*) as contador FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'faixapremio' AND COLUMN_NAME = 'ValorMinuto'");
                if (contador == 1)
                {
                    comandos.Add(" ALTER TABLE faixapremio MODIFY COLUMN ValorMinuto decimal(10,5); ");
                }


                comandos.Add("UPDATE parametros SET Valor = '2.0.0.9' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private bool AplicarPatch2010() 
        {
            // liberação em 25-11-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                
                contador = _repository.RegistroExiste("select count(*) as contador FROM parametros where Chave = 'USA_TABELA_PCP'");
                if (contador == 0)
                {
                    comandos.Add(" insert into parametros (Chave, Valor, EmpresaId, VisaoCliente) SELECT 'USA_TABELA_PCP',2,id,2 from empresas; ");
                }

                comandos.Add("alter table nfe drop foreign key  FK_nfe_TabelaPreco;");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('TabPreco.Analitica','Imprimir Listagem de  Analitica');");
                comandos.Add(" insert into permissoes (Chave, Descricao)values('TabPreco.Sintetica','Imprimir Listagem de  Sintetica');");


                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'TabPreco%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.1.0' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2011() //11/12/2020
        {
            // liberação em 25-11-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                
                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('UTILIZA_PRECO_OPERACAO','2',NULL,'2');");

                comandos.Add(" alter table operacaopadrao add column ValorOperacao decimal (10,4) DEFAULT '0' NOT NULL;");
                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente)SELECT 'BOLETO_NA_REDE','2', empresas.Id,2 from empresas;");


                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) SELECT 'UNIDADE_BOLETO_NA_REDE','B:', empresas.Id,2 from empresas; ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('CONTROLA_EAN_DUPLICADO','1',NULL,'2'); ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('USUARIO_EAN_DUPLICADO','2',NULL,'2'); ");

                comandos.Add(" INSERT INTO parametros(Chave, Valor, VisaoCliente)VALUES ('IMPRIMIR_ETIQUETA_COMPACT', '2', '1');");

                comandos.Add(" CREATE TABLE atualizaleitordepreco ( " +
                         " Id int(11) NOT NULL AUTO_INCREMENT, " +
                         " IdUsuario int(11) NOT NULL, " +
                         " idtabelaPreco int(11) DEFAULT NULL, " +
                         " DataCriacao datetime DEFAULT NULL, " +
                         " DiretorioArquivo longtext, " +
                         " PRIMARY KEY(Id), " + 
                         " KEY FK_AtualizaLeitorDePreco_usuario(IdUsuario), " +
                         " KEY FK_AtualizaLeitorDePreco_tabelapreco(idtabelaPreco), " +
                         " CONSTRAINT FK_AtualizaLeitorDePreco_tabelapreco FOREIGN KEY(idtabelaPreco) REFERENCES tabelaspreco(Id), " +
                         " CONSTRAINT FK_AtualizaLeitorDePreco_usuario FOREIGN KEY(IdUsuario) REFERENCES usuarios(Id) " +
                         " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");


               comandos.Add(" insert into atualizaleitordepreco(IdUsuario,idtabelaPreco,DataCriacao,DiretorioArquivo) values ('2',null,'2020-12-07 15:29:17','C:|Leitor'); ");  

               comandos.Add("UPDATE parametros SET Valor = '2.0.1.1' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2012() //20/01/2021
        {
            // liberação em 25-11-2020
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("INSERT INTO parametros(Chave, Valor, VisaoCliente) VALUES('ALTERA_TEMPO_REL_FICHA', '2', '1');");

                comandos.Add(" insert into permissoes (Chave, Descricao)values('RelOcorrencias','Acesso ao Rel de Ocorrências');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'RelOcorrencias%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.1.2' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2013() //12/02/2021
        {
            // liberação em 12-02-2021
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('REALIZA_BAIXA_PARCIAL','2',NULL,'2');");

                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) SELECT 'ESTOQUE_PADRAO_DEVOLUCAO','5',id,'2' FROM empresas;");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) SELECT 'USA_ESTOQUE_DEVOLUCAO','2',id,'2' FROM empresas; ");



                comandos.Add(" INSERT INTO permissoes (Chave, Descricao) VALUES ('CustoProdutoSintetico', 'Relatório Custo Produto Sintetico');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'CustoProdutoSintetico%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add(" INSERT INTO permissoes (Chave, Descricao) VALUES ('ControlePacote', 'Relatório Controle por Pacote');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'ControlePacote%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");



                comandos.Add("UPDATE parametros SET Valor = '2.0.1.3' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                _repository.AcertaTabelaFichaExcecoes();                

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2014() //26/02/2021
        {
            // liberação em 26-02-2021
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("insert into permissoes(Chave, Descricao) values('FichaTecnica.ExibeTotais', 'Define se o valor das fichas será exibido ao usuário');");

                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) SELECT P.Id AS PermissaoId, G.Id AS GrupoId FROM Permissoes P " +  
                             " INNER JOIN grupos G ON G.Nome <> '' " +
                             " WHERE P.Chave LIKE 'FichaTecnica.ExibeTotais%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("insert into permissoes(Chave, Descricao) values('FichaTecnicaMaterial.ExibeTotais', 'Define se o valor das fichas será exibido ao usuário');");

                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) SELECT P.Id AS PermissaoId, G.Id AS GrupoId FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome <> '' " +
                             " WHERE P.Chave LIKE 'FichaTecnicaMaterial.ExibeTotais%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("ALTER TABLE pedidovenda ADD TipoFrete int(11) NOT NULL; ");

                comandos.Add(" ALTER TABLE pedidocompra ADD TipoFrete int(11) NOT NULL;  ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.1.4' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);
                

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2015() //19/03/2021
        {
           
            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("ALTER TABLE itensliberacaopedidovenda ADD SemEmpenho int(1) NOT NULL;");

                comandos.Add("INSERT INTO parametros (Chave, Valor, VisaoCliente) VALUES ('LIBERACAO_SEM_EMPENHO', 2, 2); ");              

                comandos.Add("UPDATE parametros set parametros.Valor = 1 WHERE parametros.Chave = 'ATUALIZA_TEMPO_FICHAS'; ");

                comandos.Add("insert into permissoes(Chave, Descricao) values('CreditosFornecedores.Excluir', 'Excluir crédito com fornecedor');");
                comandos.Add("insert into permissoes(Chave, Descricao) values('CreditosFornecedores.Incluir', 'Incluir crédito com fornecedor');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                            " SELECT    DISTINCT P.Id AS PermissaoId, " +
                            " G.Id AS GrupoId " +
                            " FROM Permissoes P " +
                            " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                            " WHERE P.Chave LIKE 'CreditosFornecedores.Excluir%' AND " +
                            " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                            " SELECT    DISTINCT P.Id AS PermissaoId, " +
                            " G.Id AS GrupoId " +
                            " FROM Permissoes P " +
                            " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                            " WHERE P.Chave LIKE 'CreditosFornecedores.Incluir%' AND " +
                            " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("alter table creditofornecedor add column InclusaoManual tinyint (1) DEFAULT '0' NOT NULL;");


                comandos.Add("UPDATE parametros SET Valor = '2.0.1.5' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2016() //05/04/2021
        {

            int contador = 0;

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("alter table tipodocumentos change descricao descricao varchar (100)  NULL;");

                comandos.Add("insert into tipodocumentos(id,idempresa,idbanco,idempresabanco,referencia,descricao) values ('99',NULL,NULL,NULL,'DEP','*Depósito');");

                comandos.Add("insert into tipodocumentos(id,idempresa,idbanco,idempresabanco,referencia,descricao) values ('100',NULL,NULL,NULL,'PIX','*Pagamento Instantâneo (PIX)');");

                comandos.Add("insert into tipodocumentos(id,idempresa,idbanco,idempresabanco,referencia,descricao) values ('101',NULL,NULL,NULL,'TRB','*Transferência bancária, Carteira Digital');");

                comandos.Add("insert into tipodocumentos(id,idempresa,idbanco,idempresabanco,referencia,descricao) values ('102',NULL,NULL,NULL,'CSH','*Programa de fidelidade, Cashback, Crédito Virtual');");                
                
                comandos.Add("UPDATE parametros SET Valor = '2.0.1.6' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2017() //15/04/2021
        {                       

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("insert into parametros(chave,valor,EmpresaId,VisaoCliente)select 'USA_REL_PEDIDO_LOJA','2',empresas.Id,'2' from empresas;");

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('Sobre', 'Acesso a Tela Sobre');");

                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) SELECT P.Id AS PermissaoId, G.Id AS GrupoId FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome <> '' " +
                             " WHERE P.Chave LIKE 'Sobre%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.1.7' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2018() //15/04/2021
        {

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

               
                comandos.Add("UPDATE parametros SET Valor = '2.0.1.8' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2019() //30/04/2021
        {

            List<string> comandos = new List<string>();
            try
            {
                Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                comandos.Add("CREATE TABLE observacaoproduto ( " +
                             " Id INT(11) NOT NULL AUTO_INCREMENT, " +
                             " ProdutoId INT(11) NOT NULL, " +
                             " Observacao LONGTEXT, " +
                             " PRIMARY KEY(Id), " +
                             " KEY FK_ObservacaoProduto_Produtos(ProdutoId), " +
                             " CONSTRAINT FK_ObservacaoProduto_Produtos FOREIGN KEY(ProdutoId) REFERENCES produtos(Id) " +
                             " ) ENGINE = INNODB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add("alter table creditoscliente add column ObsGeral longtext  NULL;");
                comandos.Add("alter table creditofornecedor add column ObsGeral longtext   NULL;");
               

                _repository.ExecutarComandos(comandos);
                comandos = new List<string>();

                _repository.AtualizaObsDaFicha();

                comandos.Add("UPDATE parametros SET Valor = '2.0.1.9' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2020() //24/05/2021
        {

            List<string> comandos = new List<string>();
            try
            {
                //Funcoes.ExibirMensagem("Atenção: Iremos atualizar seu banco de dados e o sistema não responderá até a finalização !! Aguarde a mensagem de conclusão.", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                

                _repository.LiberarEmpenhoAMais();

                _repository.AcertarEmpenho();


                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('USA_ORDENACAO_FIXA','2',NULL,'2');");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('USER_ESCOLHE_TABELA','2',NULL,'2');");
                comandos.Add("alter table pedidovenda add column OpcaoTabelaPreco int (1) DEFAULT '0' NOT NULL ;");
                comandos.Add("alter table nfe add column OpcaoTabelaPreco int (1) DEFAULT '0' NOT NULL ;");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('USA_PRIORIDADE_ORDEM','2',NULL,'2');");
                comandos.Add("alter table itensordemproducao add column DataPrevisao date   NULL  after Status;");
                comandos.Add("insert into permissoes (Chave, Descricao)values('OrdemProducao.MudarData','Altera a data de previsão do item');");
                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'OrdemProducao.MudarData%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) SELECT 'USA_TIPOMOV_PADRAO','2',empresas.Id,'2' FROM empresas;");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) SELECT 'TIPO_MOV_PADRAO','501',empresas.Id,'2' FROM empresas;");
                comandos.Add("ALTER TABLE ordemproducaomateriais ADD EmpenhoProducao DECIMAL(10,4) NOT NULL;");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('NotaEntrada.ExibirPedido', 'Acesso a Tela de exibição de pedidos atendidos a Nota de Entrada');");

                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'NotaEntrada.ExibirPedido%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("CREATE TABLE balancoestoque ( " +
                              " Id INT(11) NOT NULL AUTO_INCREMENT, " +
                              " Referencia VARCHAR(15) NOT NULL," +
                              " AlmoxarifadoId INT(11) NOT NULL, " +
                              " Status INT(11) NOT NULL, " +
                              " DataInicio DATETIME NOT NULL, " +
                              " DataFinalizacao DATETIME DEFAULT NULL, " +
                              " UserId INT(11) NOT NULL, " +
                              " ZerarEmpenho TINYINT(1) NOT NULL, " +
                              " QuantidadeZerada TINYINT(1) NOT NULL, " +
                              " ColecaoId INT(11) DEFAULT NULL, " +
                              " CatalogoId INT(11) DEFAULT NULL, " +
                              " PRIMARY KEY(Id), " +
                              " KEY FK_BalancoEstoque_Usuario(UserId), " +
                              " KEY FK_BalancoEstoque_Almoxarifado(AlmoxarifadoId), " +
                              " CONSTRAINT FK_BalancoEstoque_Usuario FOREIGN KEY(UserId) REFERENCES usuarios(Id), " +
                              " CONSTRAINT FK_BalancoEstoque_Almoxarifado FOREIGN KEY(AlmoxarifadoId) REFERENCES almoxarifados(id)" +
                              " ) ENGINE = INNODB AUTO_INCREMENT = 0 DEFAULT CHARSET = utf8; ");

                comandos.Add(" CREATE TABLE balancoestoqueitens ( " +
                             " Id INT(11) NOT NULL AUTO_INCREMENT, " +
                             " BalancoEstoqueId INT(11) NOT NULL, " +
                             " ProdutoId INT(11) NOT NULL, " +
                             " CorId INT(11) DEFAULT NULL, " +
                             " TamanhoId INT(11) DEFAULT NULL, " +
                             " Quantidade DECIMAL(14, 5) NOT NULL, " +
                             " Divergencia DECIMAL(14, 5) DEFAULT NULL, " +
                             "  PRIMARY KEY(Id), " +
                             "  KEY FK_BalancoEstoqueItens_BalancoEstoque(BalancoEstoqueId), " +
                             "  KEY FK_BalancoEstoqueItens_Produto(ProdutoId), " +
                             "  KEY FK_BalancoEstoqueItens_Cor(CorId), " +
                             "  KEY FK_BalancoEstoqueItens_Tamanho(TamanhoId), " +
                             "  CONSTRAINT FK_BalancoEstoqueItens_BalancoEstoque FOREIGN KEY(BalancoEstoqueId) REFERENCES balancoestoque(Id), " +
                             "  CONSTRAINT FK_BalancoEstoqueItens_Produto FOREIGN KEY(ProdutoId) REFERENCES produtos(Id), " +
                             "  CONSTRAINT FK_BalancoEstoqueItens_Cor FOREIGN KEY(CorId) REFERENCES cores(Id), " +
                             "  CONSTRAINT FK_BalancoEstoqueItens_Tamanho FOREIGN KEY(TamanhoId) REFERENCES tamanhos(Id) " +
                             " ) ENGINE = INNODB AUTO_INCREMENT = 0 DEFAULT CHARSET = utf8; ");

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('BalancoEstoque', 'Acesso a Tela de Balanço de Estoque');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('BalancoEstoque.Visualizar', 'Visualizar Balanço de Estoque');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('BalancoEstoque.Incluir', 'Incluir Balanço de Estoque');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('BalancoEstoque.Alterar', 'Alterar Balanço de Estoque');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('BalancoEstoque.Excluir', 'Exluir Balanço de Estoque');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('BalancoEstoque.Finalizar', 'Finalizar Balanço de Estoque');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('BalancoEstoque.Imprimir', 'Acesso ao Relatório de Balanço de Estoque')");
                comandos.Add(" alter table ordemproducaomateriais change quantidadeempenhada quantidadeempenhada decimal (14,5)  NULL , " + 
                             " change quantidadenecessaria quantidadenecessaria decimal (14,5)  NULL , change quantidadebaixada quantidadebaixada decimal (14,5)  NULL , " +
                             " change EmpenhoProducao EmpenhoProducao decimal (14,5) DEFAULT '0' NOT NULL  ");


                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId" +
                             " FROM Permissoes P" +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador'" +
                             " WHERE P.Chave LIKE 'BalancoEstoque%' AND" +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("INSERT INTO contadorescodigo (Nome, Descricao, Prefixo, ContadorAtual, CasasDecimais, Ativo) VALUES('BalancoEstoque', 'Referência no cadastro de Balanço de Estoque', '', 0, 8, 0); ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.2.0' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2021() //24/05/2021
        {

            List<string> comandos = new List<string>();
            try
            {
                
                comandos.Add("ALTER TABLE balancoestoque CHANGE CatalogoId Catalogo TINYINT(1) DEFAULT NULL;");
                comandos.Add("ALTER TABLE balancoestoque CHANGE ColecaoId Colecao TINYINT(1) DEFAULT NULL;");
                comandos.Add("ALTER TABLE balancoestoqueitens ADD ColecaoId INT(11) DEFAULT NULL;");
                comandos.Add("ALTER TABLE balancoestoqueitens ADD CatalogoId INT(11) DEFAULT NULL;");                

                comandos.Add(" CREATE TABLE buscapreco ( " +
                             "  Id int(11) NOT NULL AUTO_INCREMENT, " +
                             "  EAN varchar(99) NOT NULL, " +
                             " Item varchar(300) NOT NULL, " +
                             "  Preco decimal(10, 2) DEFAULT NULL, " +
                             "  PRIMARY KEY(Id, EAN) " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 0 DEFAULT CHARSET = utf8; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.2.1' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2022() //08/06/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('TipoDocumento', 'Acesso a Tela de Tipos de Pagamentos');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('TipoDocumento.Visualizar', 'Visualizar Tipos de Pagamentos');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('TipoDocumento.Alterar', 'Alterar Tipos de Pagamentos');");
                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('TipoDocumento.Excluir', 'Excluir Tipos de Pagamentos');");

                comandos.Add(" UPDATE permissoes SET Descricao = 'Incluir Tipos de Pagamento' WHERE Chave = 'TipoDocumento.Incluir'; ");


                comandos.Add("INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'TipoDocumento%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('LogAcoes', 'Acesso a Tela de Log');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'LogAcoes%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.2.2' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2023() // 23/06/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) SELECT 'USA_PORTAL_REPRESENTANTE','2',empresas.Id ,'2' FROM empresas;");

                comandos.Add("alter table colaboradores add column VinculaPortal int (1) DEFAULT '0' NOT NULL  after EmailCobranca;");

                comandos.Add("CREATE TABLE UsuariosPortal (  " +
                            " Id int(11) NOT NULL AUTO_INCREMENT, " +
                            " Nome varchar(255) NOT NULL, " +
                            " Login varchar(60) NOT NULL, " +
                            " Senha varchar(255) NOT NULL, " +
                            " DataCadastro datetime DEFAULT NULL, " +
                            " Ativo int(1) NOT NULL, " +
                            " IdVendedor int(11) NOT NULL, " +
                            " PRIMARY KEY(Id), " +
                            " KEY IX_usuariosPortal_Login(Login), " +
                            " KEY FK_usuariosPortal_Vendedor(IdVendedor), " +
                            " CONSTRAINT FK_usuariosPortal_Vendedor FOREIGN KEY(IdVendedor) REFERENCES colaboradores(id) " +
                            " ) ENGINE = InnoDB AUTO_INCREMENT = 0 DEFAULT CHARSET = utf8; ");

                comandos.Add("alter table tipomovimentacoes add column IntegraIpiIcms int (1)  NULL  after PercDiferimento, add column PercIpi decimal (10,2)  NULL  after IntegraIpiIcms;");

                comandos.Add("alter table nfeitens add column IntegraIpi int (11)  NULL  after VrIpiDevolvido, add column PercentIpi decimal (10,2)  NULL  after IntegraIpi;");

                comandos.Add("alter table nfeitens add column BaseIpi decimal (10,2)  NULL  after PercentIpi, add column ValorIpi decimal (10,2)  NULL  after BaseIpi;");

                comandos.Add("alter table nfe add column ValorIpi decimal (10,2)  NULL;");

                comandos.Add("insert into permissoes (Chave, Descricao) values ('RelListagemNfe', 'Acesso ao Relatório de Listagem de NFe, no menu Doc.Eletrônicos')");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'RelListagemNfe%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) values ('RelPlanoContas', 'Acesso ao Relatório Plano de Contas, no menu Financeiro')");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId)  " +
                             " SELECT    DISTINCT P.Id AS PermissaoId,  " +
                             " G.Id AS GrupoId  " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador'  " +
                             " WHERE P.Chave LIKE 'RelPlanoContas%' AND  " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) values ('RelDespesasReceitas', 'Acesso ao Relatório Despesas x Receitas, no menu Financeiro');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId)  " +
                             " SELECT    DISTINCT P.Id AS PermissaoId,  " +
                             " G.Id AS GrupoId  " +
                             " FROM Permissoes P  " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador'  " +
                             " WHERE P.Chave LIKE 'ContasAReceber%' AND  " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");
              

                comandos.Add("INSERT INTO  permissoes (Chave, Descricao) values ('RotaVisita', 'Acesso à tela de Rota de Visitas, no menu Cadastros');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId)  " +
                            " SELECT    DISTINCT P.Id AS PermissaoId,  " +
                            " G.Id AS GrupoId  " +
                            " FROM Permissoes P  " +
                            " INNER JOIN grupos G ON G.Nome = 'Administrador'  " +
                            " WHERE P.Chave LIKE 'Cor%' AND  " +
                            " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

               

                comandos.Add("INSERT INTO  permissoes (Chave, Descricao) values ('RotaVisita.Alterar', 'Alterar cadastros em Rota de Visitas');");              

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('RotaVisita.Visualizar', 'Acesso à visualização dos cadastros de Rota de Visitas')");              

                comandos.Add("INSERT INTO permissoes (Chave, Descricao) VALUES ('RotaVisita.Excluir', 'Excluir cadastros de Rota de Visitas')");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId)  " +
                            " SELECT    DISTINCT P.Id AS PermissaoId,  " +
                            " G.Id AS GrupoId  " +
                            " FROM Permissoes P  " +
                            " INNER JOIN grupos G ON G.Nome = 'Administrador'  " +
                            " WHERE P.Chave LIKE 'RotaVisita%' AND  " +
                            " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("INSERT INTO parametros (Chave, Valor, EmpresaId, VisaoCliente) VALUES ('LIMITA_TOTAL_PREMIO', 2, NULL, 2);");


                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Administradora de Cartões, no menu Cadastros'  WHERE Chave = 'AdmCartao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Administradora de Cartões 'WHERE Chave = 'AdmCartao.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Administradora de Cartões' WHERE Chave = 'AdmCartao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Administradora de Cartões' WHERE Chave = 'AdmCartao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso a tela de Almoxarifado, no menu Pré-Cadastros' WHERE Chave = 'Almoxarifado'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Almoxarifado' WHERE Chave = 'Almoxarifado.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Almoxarifado'  WHERE Chave = 'Almoxarifado.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Almoxarifado'  WHERE Chave = 'Almoxarifado.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Almoxarifado' WHERE Chave = 'Almoxarifado.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Balanceamento, no menu Planejamento' WHERE Chave = 'Balanceamento'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastro de Balanceamento' WHERE Chave = 'Balanceamento.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastro de Balanceamento'  WHERE Chave = 'Balanceamento.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastro de Balanceamento'  WHERE Chave = 'Balanceamento.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastro de Balanceamento' WHERE Chave = 'Balanceamento.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela do Balanço de Estoque' WHERE Chave = 'BalancoEstoque'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastro do Balanço de Estoque' WHERE Chave = 'BalancoEstoque.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Exluir cadastro do Balanço de Estoque' WHERE Chave = 'BalancoEstoque.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Finalizar Balanço de Estoque' WHERE Chave = 'BalancoEstoque.Finalizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório do Balanço de Estoque, na tela do Balanço de Estoque'  WHERE Chave = 'BalancoEstoque.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastro no Balanço de Estoque' WHERE Chave = 'BalancoEstoque.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastro no Balanço de Estoque' WHERE Chave = 'BalancoEstoque.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Banco, no menu Cadastros' WHERE Chave = 'Banco'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastro de Banco' WHERE Chave = 'Banco.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastro de Banco' WHERE Chave = 'Banco.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastro de Banco' WHERE Chave = 'Banco.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastro de Banco' WHERE Chave = 'Banco.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Borderô de Cheques/Duplicatas, no menu Financeiro'  WHERE Chave = 'BorderoCheque'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros em Borderô de Cheques/Duplicatas' WHERE Chave = 'BorderoCheque.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Baixar títulos no Borderô de Cheques/Duplicatas' WHERE Chave = 'BorderoCheque.Baixar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Estornar baixas em Borderô de Cheques/Duplicatas' WHERE Chave = 'BorderoCheque.Estornar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros no Borderô de Cheques/Duplicatas' WHERE Chave = 'BorderoCheque.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros no Borderô de Cheques/Duplicatas' WHERE Chave = 'BorderoCheque.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastro no Borderô de Cheques/Duplicatas' WHERE Chave = 'BorderoCheque.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Caixas, no menu Cadastros' WHERE Chave = 'Caixas'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de abertura de Caixas' WHERE Chave = 'Caixas.Abrir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Caixas' WHERE Chave = 'Caixas.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Caixas' WHERE Chave = 'Caixas.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de fechamento de Caixas' WHERE Chave = 'Caixas.Fechar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Caixas' WHERE Chave = 'Caixas.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à movimentação dos Caixas' WHERE Chave = 'Caixas.Movimento'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à visualização dos cadastros de Caixas' WHERE Chave = 'Caixas.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Calendário, no menu Cadastros' WHERE Chave = 'Calendario'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Calendário' WHERE Chave = 'Calendario.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Calendário' WHERE Chave = 'Calendario.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Calendário' WHERE Chave = 'Calendario.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros Calendario' WHERE Chave = 'Calendario.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Cargos, no menu Pré-Cadastros' WHERE Chave = 'Cargo'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Cargos' WHERE Chave = 'Cargo.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Cargos' WHERE Chave = 'Cargo.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Cargos' WHERE Chave = 'Cargo.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Cargos' WHERE Chave = 'Cargo.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Catálogos, no menu Pré-Cadastros'  WHERE Chave = 'Catalogo'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Catálogos' WHERE Chave = 'Catalogo.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Copiar cadastros de Catálogos' WHERE Chave = 'Catalogo.Copiar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Catálogos' WHERE Chave = 'Catalogo.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Catálogos' WHERE Chave = 'Catalogo.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Catálogos' WHERE Chave = 'Catalogo.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Cheques, no menu Financeiro'  WHERE Chave = 'Cheque'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Cheques' WHERE Chave = 'Cheque.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Compensar Cheques' WHERE Chave = 'Cheque.Compensar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Devolver Cheques' WHERE Chave = 'Cheque.Devolver'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Cheques' WHERE Chave = 'Cheque.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Cheques' WHERE Chave = 'Cheque.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Prorrogar Cheques' WHERE Chave = 'Cheque.Prorrogar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de cheques, na tela de Cheques' WHERE Chave = 'Cheque.Relatorio'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Resgatar Cheques' WHERE Chave = 'Cheque.Resgatar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Cheques' WHERE Chave = 'Cheque.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Controle do período de envio de e-mail de cobrança' WHERE Chave = 'Cobranca'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros do Controle período de envio de e-mail de cobrança' WHERE Chave = 'Cobranca.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros do Controle período de envio de e-mail de cobrança' WHERE Chave = 'Cobranca.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros do Controle período de envio de e-mail de cobrança' WHERE Chave = 'Cobranca.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros do período de envio de e-mail de cobrança' WHERE Chave = 'Cobranca.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Colaborador, no menu de Cadastros' WHERE Chave = 'Colaborador'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Colaborador' WHERE Chave = 'Colaborador.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Colaborador' WHERE Chave = 'Colaborador.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Colaborador' WHERE Chave = 'Colaborador.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Colaborador' WHERE Chave = 'Colaborador.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar abas Cobrança, Referência e Financeiro, no cadastro de colaborador' WHERE Chave = 'Colaborador.VisualizarDadosFinanceiros'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Coleção de Itens' WHERE Chave = 'Colecao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Coleção de Itens' WHERE Chave = 'Colecao.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Coleção de Itens' WHERE Chave = 'Colecao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Coleção de Itens' WHERE Chave = 'Colecao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Coleção de Itens' WHERE Chave = 'Colecao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Comissões, no menu Financeiro' WHERE Chave = 'ComissaoVendedor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Comissões' WHERE Chave = 'ComissaoVendedor.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Comissões' WHERE Chave = 'ComissaoVendedor.Cancelar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Imprimir Listagem do Período, na tela de Comissões' WHERE Chave = 'ComissaoVendedor.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Liberar Pagamento, na tela de Comissões' WHERE Chave = 'ComissaoVendedor.Liberar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Comissões' WHERE Chave = 'ComissaoVendedor.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Contadores de Código, no menu Configurações' WHERE Chave = 'Contadores'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros do Contadores de Código' WHERE Chave = 'Contadores.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros do Contadores de Código' WHERE Chave = 'Contadores.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Contas a Pagar, no menu Financeiro' WHERE Chave = 'ContasAPagar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar títulos de Contas a Pagar' WHERE Chave = 'ContasAPagar.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Baixa Lote na tela de Contas a Pagar' WHERE Chave = 'ContasAPagar.Baixalote'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Baixar na tela de Contas a Pagar' WHERE Chave = 'ContasAPagar.Baixar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir títulos de Contas a Pagar' WHERE Chave = 'ContasAPagar.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir títulos de Contas a Pagar' WHERE Chave = 'ContasAPagar.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Incluir Lote na tela de Contas a Pagar' WHERE Chave = 'ContasAPagar.IncluirLote'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Contas a Pagar' WHERE Chave = 'ContasAPagar.Relatorio'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Visualização da Baixa na tela de Contas a Pagar' WHERE Chave = 'ContasAPagar.VisBaixa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar títulos de Contas a Pagar' WHERE Chave = 'ContasAPagar.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Contas a Receber, no menu Financeiro' WHERE Chave = 'ContasAReceber'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar títulos de Contas a Receber' WHERE Chave = 'ContasAReceber.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Baixa Lote na tela de Contas a Receber' WHERE Chave = 'ContasAReceber.Baixalote'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Baixar na tela de Contas a Receber' WHERE Chave = 'ContasAReceber.Baixar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir Boleto na tela de Contas a Receber' WHERE Chave = 'ContasAReceber.Boleto'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir títulos de Contas a Receber' WHERE Chave = 'ContasAReceber.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir títulos de Contas a Receber' WHERE Chave = 'ContasAReceber.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Incluir Lote na tela de Contas a Receber' WHERE Chave = 'ContasAReceber.IncluirLote'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Contas a Receber' WHERE Chave = 'ContasAReceber.Relatorio'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Visualização da Baixa na tela de Contas a Receber' WHERE Chave = 'ContasAReceber.VisBaixa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar títulos de Contas a Receber' WHERE Chave = 'ContasAReceber.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Controle por Pacote, no menu Relatórios de Produção' WHERE Chave = 'ControlePacote'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Controle de Prêmio, no menu Cadastros' WHERE Chave = 'ControlePremio'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Controle de Prêmio' WHERE Chave = 'ControlePremio.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Controle de Prêmio' WHERE Chave = 'ControlePremio.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Controle de Prêmio' WHERE Chave = 'ControlePremio.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Controle de Prêmio' WHERE Chave = 'ControlePremio.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Cores, no menu Pré-Cadastros' WHERE Chave = 'Cor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Cores' WHERE Chave = 'Cor.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Cores' WHERE Chave = 'Cor.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Cores' WHERE Chave = 'Cor.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Cores' WHERE Chave = 'Cor.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Créditos de Clientes, no menu Financeiro' WHERE Chave = 'CreditosClientes'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Créditos de Clientes' WHERE Chave = 'CreditosClientes.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Créditos de Clientes' WHERE Chave = 'CreditosClientes.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Créditos de Clientes' WHERE Chave = 'CreditosClientes.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Créditos de Clientes' WHERE Chave = 'CreditosClientes.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Créditos de Fornecedores, no menu Financeiro' WHERE Chave = 'CreditosFornecedores'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Créditos de Fornecedores' WHERE Chave = 'CreditosFornecedores.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Créditos de Fornecedores' WHERE Chave = 'CreditosFornecedores.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Créditos de Fornecedores' WHERE Chave = 'CreditosFornecedores.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Créditos de Fornecedores' WHERE Chave = 'CreditosFornecedores.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Curva Abc, no menu Documentos Eletrônicos' WHERE Chave = 'CurvaAbc'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Custo de Consumo, no menu Relatórios de Produção' WHERE Chave = 'CustoConsumo'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Custo Produto Analítico, no menu Relatórios Gerais' WHERE Chave = 'CustoProdutoAnalitico'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Custo Produto Sintético, no menu Relatórios Gerais' WHERE Chave = 'CustoProdutoSintetico'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Permite que o usuário tenha acesso ao Dash de todos os vendedores' WHERE Chave = 'DashboardVendedor.VisaoAdministrador'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Dash Cobrança / Lista de Atividades Cobrança' WHERE Chave = 'DashFinanceiro'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Dash de Vendedores, no menu de Crm' WHERE Chave = 'DashVendedor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'Departamento'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'Departamento.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'Departamento.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'Departamento.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'Departamento.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Despesas Fixas Variáveis, no menu Cadastros' WHERE Chave = 'DespesaFixaVariavel'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Despesas Fixas Variáveis' WHERE Chave = 'DespesaFixaVariavel.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Despesas Fixas Variáveis' WHERE Chave = 'DespesaFixaVariavel.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Despesas Fixas Variáveis'  WHERE Chave = 'DespesaFixaVariavel.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Despesas Fixas Variáveis' WHERE Chave = 'DespesaFixaVariavel.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Destino do Material, no menu Pré-Cadastros'  WHERE Chave = 'Destino'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Destino do Material' WHERE Chave = 'Destino.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Destino do Material'  WHERE Chave = 'Destino.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Destino do Material'  WHERE Chave = 'Destino.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Destino do Material' WHERE Chave = 'Destino.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Devolução de Itens, no menu Estoque' WHERE Chave = 'Devolucao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Devolução de Itens' WHERE Chave = 'Devolucao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Imprimir registros de Devolução de Itens'  WHERE Chave = 'Devolucao.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Devolução de Itens'  WHERE Chave = 'Devolucao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Devolução de Itens' WHERE Chave = 'Devolucao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Empresa, no menu Cadastros' WHERE Chave = 'Empresa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastro da Empresa' WHERE Chave = 'Empresa.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastro da Empresa' WHERE Chave = 'Empresa.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Entrega, no menu Pré-Cadastros' WHERE Chave = 'Entrega'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Entrega' WHERE Chave = 'Entrega.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Entrega'  WHERE Chave = 'Entrega.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Entrega' WHERE Chave = 'Entrega.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Controle de Estoque, no menu Estoque' WHERE Chave = 'Estoque'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Movimentar Estoque' WHERE Chave = 'Estoque.MovMan'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Estoque, na tela de Consulta de Estoque' WHERE Chave = 'Estoque.Relatorio'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Transferir Movimentação' WHERE Chave = 'Estoque.Trans'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar Movimentações de Estoque' WHERE Chave = 'Estoque.VisMov'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Matéria-Prima, no menu Relatórios Gerais' WHERE Chave = 'EstoqueMateriaPrima'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Estoque de Produto Produzido, no menu Relatórios Gerais' WHERE Chave = 'EstoqueProdutoProduzido'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Fechamento do dia, no menu Documentos Eletrônicos' WHERE Chave = 'FechaDia'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Ficha Técnica de Operações e ao Relatório de Ficha Técnica' WHERE Chave = 'FichaTecnica'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros na Ficha Técnica de Operações' WHERE Chave = 'FichaTecnica.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros na Ficha Técnica de Operações'  WHERE Chave = 'FichaTecnica.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Define se o valor das fichas será exibido ao usuário' WHERE Chave = 'FichaTecnica.ExibeTotais'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Facção na tela de Ficha Técnica de Operações' WHERE Chave = 'FichaTecnica.FichaFaccao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros na Ficha Técnica de Operações' WHERE Chave = 'FichaTecnica.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Mantenção na tela de Ficha Técnica de Operações' WHERE Chave = 'FichaTecnica.Manutencao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros na Ficha Técnica de Operações' WHERE Chave = 'FichaTecnica.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Ficha Técnica de Materiais, no menu Cadastros' WHERE Chave = 'FichaTecnicaMaterial'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros na Ficha Técnica de Materiais' WHERE Chave = 'FichaTecnicaMaterial.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros na Ficha Técnica de Materiais'  WHERE Chave = 'FichaTecnicaMaterial.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Define se o valor das fichas será exibido ao usuário' WHERE Chave = 'FichaTecnicaMaterial.ExibeTotais'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros na Ficha Técnica de Materiais' WHERE Chave = 'FichaTecnicaMaterial.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Manutenção na tela de Ficha Técnica de Materiais' WHERE Chave = 'FichaTecnicaMaterial.Manutencao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros na Ficha Técnica de Materiais' WHERE Chave = 'FichaTecnicaMaterial.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Fluxo de Caixa, no menu Financeiro'  WHERE Chave = 'FluxoCaixa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir na tela de Fluxo de Caixa' WHERE Chave = 'FluxoCaixa.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso  botão de Simulação na tela de Fluxo de Caixa'  WHERE Chave = 'FluxoCaixa.Simular'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Funcionários, no menu Cadastro' WHERE Chave = 'Funcionario'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Funcionários' WHERE Chave = 'Funcionario.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Funcionários'  WHERE Chave = 'Funcionario.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Funcionários'  WHERE Chave = 'Funcionario.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Funcionários' WHERE Chave = 'Funcionario.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Gerar Etiquetas, no menu Cadastros' WHERE Chave = 'GerarEtiqueta'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Interativo de Gestão de Compras, no menu Planejamento' WHERE Chave = 'GestaoCompras'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Grupo de Itens, no menu Pré-Cadastros' WHERE Chave = 'GrupoDeProduto'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Grupo De Produto' WHERE Chave = 'GrupoDeProduto.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Grupo De Produto'  WHERE Chave = 'GrupoDeProduto.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Grupo De Produto'  WHERE Chave = 'GrupoDeProduto.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Grupo De Produto' WHERE Chave = 'GrupoDeProduto.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Grupos de Produção, no menu Pré-Cadastros' WHERE Chave = 'GruposProducao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Grupos Produção' WHERE Chave = 'GruposProducao.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Grupos Produção'  WHERE Chave = 'GruposProducao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Grupos Produção'  WHERE Chave = 'GruposProducao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Grupos Produção' WHERE Chave = 'GruposProducao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Inutilizar, no menu Documentos Eletrônicos' WHERE Chave = 'Inutilizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Lançamentos de Produção, no menu Lançamentos' WHERE Chave = 'LancamentosProducao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Lista de Compras, no menu Relatórios Gerais' WHERE Chave = 'ListaCompra'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Listagem de Faturamento, no menu Documentos Eletrônicos' WHERE Chave = 'ListagemFatVendedor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Mapa, no menu Planejamento' WHERE Chave = 'Mapa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Movimentação do Banco, no menu Financeiro' WHERE Chave = 'MovimentacaoBanco'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros da Movimentação do Banco' WHERE Chave = 'MovimentacaoBanco.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros da Movimentação do Banco'  WHERE Chave = 'MovimentacaoBanco.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Transferir registros da Movimentação do Banco' WHERE Chave = 'MovimentacaoBanco.Transferir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros da Movimentação do Banco'  WHERE Chave = 'MovimentacaoBanco.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Movimentos da Operação, no menu Cadastros' WHERE Chave = 'Movimentos'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Movimentos da Operação' WHERE Chave = 'Movimentos.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Movimentos da Operação'  WHERE Chave = 'Movimentos.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Movimentos da Operação'  WHERE Chave = 'Movimentos.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Movimentos da Operação' WHERE Chave = 'Movimentos.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Natureza Financeira, no menu Cadastros' WHERE Chave = 'NaturezaFinanceira'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Natureza Financeira' WHERE Chave = 'NaturezaFinanceira.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Natureza Financeira'  WHERE Chave = 'NaturezaFinanceira.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Natureza Financeira'  WHERE Chave = 'NaturezaFinanceira.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Natureza Financeira' WHERE Chave = 'NaturezaFinanceira.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de NFCe, no menu Documentos Eletrônicos' WHERE Chave = 'NFCe'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar valor do produto na NFCe' WHERE Chave = 'NFCe.AlterarValor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Cancelar NFCe'  WHERE Chave = 'NFCe.Cancelar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Emitir NFCe' WHERE Chave = 'NFCe.Emitir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Excluir NFCe' WHERE Chave = 'NFCe.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir Danfce' WHERE Chave = 'NFCe.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir Romaneio da NFCe' WHERE Chave = 'NFCe.ImprimirCupom'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir vendas em NFCe' WHERE Chave = 'NFCe.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar vendas em NFCe' WHERE Chave = 'NFCe.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de NFe, no menu Documentos Eletrônicos' WHERE Chave = 'NFe'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registro em Nfe' WHERE Chave = 'NFe.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar valor do produto NFe' WHERE Chave = 'NFe.AlterarValor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Cancelar NFe' WHERE Chave = 'NFe.Cancelar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de CCe - Nota Complementar na tela de Nfe' WHERE Chave = 'NFe.Corrigir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Emitir Nfe' WHERE Chave = 'NFe.Emitir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Excluir Nfe'  WHERE Chave = 'NFe.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir Nfe'  WHERE Chave = 'NFe.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Incluir Nfe'  WHERE Chave = 'NFe.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Visualizar Nfe' WHERE Chave = 'NFe.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Nota de Entrada, no menu Compras' WHERE Chave = 'NotaEntrada'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Nota de Entrada' WHERE Chave = 'NotaEntrada.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Nota de Entrada'  WHERE Chave = 'NotaEntrada.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Exibir Pedidos Atendidos, em Nota de Entrada' WHERE Chave = 'NotaEntrada.ExibirPedido'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir Relatório, em Nota de Entrada' WHERE Chave = 'NotaEntrada.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Nota de Entrada' WHERE Chave = 'NotaEntrada.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Nota de Entrada' WHERE Chave = 'NotaEntrada.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Tipos de Ocorrências, no menu Pré-Cadastros' WHERE Chave = 'Ocorrencias'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Ocorrências' WHERE Chave = 'Ocorrencias.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Ocorrências'  WHERE Chave = 'Ocorrencias.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Ocorrências'  WHERE Chave = 'Ocorrencias.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Operação Padrão/Máquinas, no menu Cadastros' WHERE Chave = 'Operacao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Operação Padrão/Máquinas' WHERE Chave = 'Operacao.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Operação Padrão/Máquinas'  WHERE Chave = 'Operacao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Operação Padrão/Máquinas'  WHERE Chave = 'Operacao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Operação Padrão/Máquinas' WHERE Chave = 'Operacao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Ordem de Produção, no menu Lançamentos' WHERE Chave = 'OrdemProducao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Ordem de Produção' WHERE Chave = 'OrdemProducao.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Ordem de Produção'  WHERE Chave = 'OrdemProducao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Finalizar Ordem de Produção' WHERE Chave = 'OrdemProducao.Finalizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir Relatório, em Ordem de Produção' WHERE Chave = 'OrdemProducao.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Ordem de Produção' WHERE Chave = 'OrdemProducao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Liberar Ordem de Produção' WHERE Chave = 'OrdemProducao.Liberar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Liberar Empenho da Ordem de Produção' WHERE Chave = 'OrdemProducao.LiberarEmpenho'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar a data de previsão de finalização da produção'  WHERE Chave = 'OrdemProducao.MudarData'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Pacotes de Produção na tela de Ordem de Produção' WHERE Chave = 'OrdemProducao.Pacotes'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Ordem de Produção' WHERE Chave = 'OrdemProducao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de OP Matérias-Primas' WHERE Chave = 'OrdemProducaoMateriaPrima'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela Pacotes de Produção' WHERE Chave = 'PacotesProducao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir Pacotes de Produção' WHERE Chave = 'PacotesProducao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir Pacotes de Produção' WHERE Chave = 'PacotesProducao.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir Pacotes de Produção' WHERE Chave = 'PacotesProducao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar Pacotes de Produção' WHERE Chave = 'PacotesProducao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Parâmetros, no menu Configurações' WHERE Chave = 'Parametros'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Parâmetros' WHERE Chave = 'Parametros.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Parâmetros' WHERE Chave = 'Parametros.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Pedido de Compra'  WHERE Chave = 'PedidoCompra'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Pedido de Compra' WHERE Chave =  'PedidoCompra.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Pedido de Compra'  WHERE Chave ='PedidoCompra.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Finalizar Pedido de Compra' WHERE Chave = 'PedidoCompra.Finalizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Pedido de Compra' WHERE Chave = 'PedidoCompra.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Pedido de Compra' WHERE Chave = 'PedidoCompra.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Pedido de Venda e ao Relatório de Pedido de Venda' WHERE Chave = 'PedidoVenda'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Pedido de Venda' WHERE Chave = 'PedidoVenda.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar o valor de comissão do vendedor' WHERE Chave = 'PedidoVenda.AlterarComissaoVendedor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar valor do produto do Pedido de Venda' WHERE Chave = 'PedidoVenda.AlterarValor'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Bloquear/Liberar Pedido de Venda' WHERE Chave = 'PedidoVenda.Bloquear'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Imprimir Conferêcia do Pedido de Venda' WHERE Chave = 'PedidoVenda.Conferencia'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Pedido de Venda' WHERE Chave = 'PedidoVenda.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Finalizar Pedido de Venda' WHERE Chave = 'PedidoVenda.Finalizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Pedido de Venda' WHERE Chave = 'PedidoVenda.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Pedido de Venda'  WHERE Chave = 'PedidoVenda.Observacao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Pedido de Venda' WHERE Chave = 'PedidoVenda.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Conferência de Pedido, no menu Vendas' WHERE Chave = 'PedidoVendaConferencia'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Percentuais da Empresa, no menu Cadastros'  WHERE Chave = 'PercentuaisEmpresa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'PercentuaisEmpresa.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'PercentuaisEmpresa.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso às telas de Permissões, Grupo de Permissões e Estados, no menu Configurações' WHERE Chave = 'Permissao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar registros de Permissões e Grupo de Permissões' WHERE Chave = 'Permissao.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir registros de Permissões e Grupo de Permissões'  WHERE Chave = 'Permissao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Permissões e Grupo de Permissões'  WHERE Chave = 'Permissao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Permissões e Grupo de Permissões' WHERE Chave = 'Permissao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Plano de Produção Anual, no menu Planejamento' WHERE Chave = 'PlanoAnual'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Plano de Contas, no menu Financeiro' WHERE Chave = 'PlanoContas'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso' WHERE Chave = 'PlanoContas.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso'  WHERE Chave = 'PlanoContas.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso'  WHERE Chave = 'PlanoContas.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Suspenso'  WHERE Chave = 'PlanoContas.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Prêmio de Funcionário, no menu Relatórios de Produção' WHERE Chave = 'PremioFuncionario'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Produção de Empresa, no menu Relatórios de Produção' WHERE Chave = 'ProducaoEmpresa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Produção de Funcionário, no menu Relatórios de Produção' WHERE Chave = 'ProducaoFuncionario'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Itens, no menu Cadastros' WHERE Chave = 'Produto'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Itens' WHERE Chave = 'Produto.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Itens'  WHERE Chave = 'Produto.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Itens'  WHERE Chave = 'Produto.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Manutenção na tela de Itens' WHERE Chave = 'Produto.Manutencao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Itens' WHERE Chave = 'Produto.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Receitas Futuras, no menu Financeiro' WHERE Chave = 'ReceitasFuturas'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Relatório de Acompanhamento de Catálogo, no menu Vendas'  WHERE Chave = 'Relatorios.AcompanhamentoCatalogo'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao relatório Gestão de Estoque na tela de Controle de Estoque' WHERE Chave = 'RelEstoqueConsulta'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório de Ocorrências, no menu Relatórios de Produção' WHERE Chave = 'RelOcorrencias'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Pedido de Compras, no menu Relatórios Gerais'  WHERE Chave = 'RelPedidoCompra'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Segmento De Itens, no menu Pré-Cadastros' WHERE Chave = 'SegmentoDeProduto'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Segmento De Itens' WHERE Chave = 'SegmentoDeProduto.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Segmento De Itens'  WHERE Chave = 'SegmentoDeProduto.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Segmento De Itens'  WHERE Chave = 'SegmentoDeProduto.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Segmento De Itens' WHERE Chave = 'SegmentoDeProduto.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Setores, no menu Pré-Cadastros' WHERE Chave = 'Setores'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Setores' WHERE Chave = 'Setores.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Setores'  WHERE Chave = 'Setores.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Setores'  WHERE Chave = 'Setores.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Setores' WHERE Chave = 'Setores.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à Tela Sobre, no menu Sobre' WHERE Chave = 'Sobre'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao Relatório Status de Produção, no menu Relatórios de Produção' WHERE Chave = 'StatusEmpresa'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Tabela de Preço, no menu Vendas no módulo de Gestão'  WHERE Chave = 'TabPreco'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Tabela de Preço' WHERE Chave = 'TabPreco.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Impressão Analítica de Tabela de Preço no módulo de Produção' WHERE Chave = 'TabPreco.Analitica'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Tabela de Preço' WHERE Chave = 'TabPreco.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Tabela de Preço'  WHERE Chave = 'TabPreco.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão Impressão Sintética de Tabela de Preço no módulo de Produção' WHERE Chave = 'TabPreco.Sintetica'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Tabela de Preço' WHERE Chave = 'TabPreco.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Tamanho, no menu Pré-Cadastros' WHERE Chave = 'Tamanho'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Tamanho' WHERE Chave = 'Tamanho.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Tamanho'  WHERE Chave = 'Tamanho.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Tamanho'  WHERE Chave = 'Tamanho.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Tamanho' WHERE Chave = 'Tamanho.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Tipo de Atividades, no menu Cadastros' WHERE Chave = 'TipoAtividade'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Tipo de Atividades' WHERE Chave = 'TipoAtividade.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Tipo de Atividades'  WHERE Chave = 'TipoAtividade.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Tipo de Atividades'  WHERE Chave = 'TipoAtividade.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Tipo de Atividades' WHERE Chave = 'TipoAtividade.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Tipo de Itens, no menu Pré-Cadastros' WHERE Chave = 'TipoDeProduto'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Tipo De Produto' WHERE Chave = 'TipoDeProduto.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Tipo De Produto'  WHERE Chave = 'TipoDeProduto.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Tipo De Produto'  WHERE Chave = 'TipoDeProduto.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar Tipo De Produto' WHERE Chave = 'TipoDeProduto.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Tipos de Pagamento, no menu Cadastros' WHERE Chave = 'TipoDocumento'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Tipos de Pagamento' WHERE Chave = 'TipoDocumento.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Tipos de Pagamento'  WHERE Chave = 'TipoDocumento.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Tipos de Pagamento'  WHERE Chave = 'TipoDocumento.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Tipos de Pagamento'  WHERE Chave = 'TipoDocumento.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Tipo de Movimentação, no menu Cadastros' WHERE Chave = 'TipoMovimentacao'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Tipos de Movimentação' WHERE Chave = 'TipoMovimentacao.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Tipos de Movimentação'  WHERE Chave = 'TipoMovimentacao.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Tipos de Movimentação'  WHERE Chave = 'TipoMovimentacao.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Tipos de Movimentação' WHERE Chave = 'TipoMovimentacao.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Transferência Entre Empresas, no menu Estoque'  WHERE Chave = 'Transferencia'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso ao botão de Imprimir em Transferência Entre Empresas'  WHERE Chave = 'Transferencia.Imprimir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir registros de Transferência Entre Empresas' WHERE Chave = 'Transferencia.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar registros de Transferência Entre Empresas' WHERE Chave = 'Transferencia.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso as Rotinas de boletos' WHERE Chave = 'TratarBoletos'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Unidade de Medida, no menu Pré-Cadastros' WHERE Chave = 'UnidadeDeMedida'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Unidade de Medida' WHERE Chave = 'UnidadeDeMedida.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Unidade de Medida'  WHERE Chave = 'UnidadeDeMedida.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Unidade de Medida'  WHERE Chave = 'UnidadeDeMedida.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Unidade de Medida' WHERE Chave = 'UnidadeDeMedida.Visualizar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Acesso à tela de Usuário, no menu Configurações' WHERE Chave = 'Usuario'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Alterar cadastros de Usuário' WHERE Chave = 'Usuario.Alterar'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Excluir cadastros de Usuário'  WHERE Chave = 'Usuario.Excluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Incluir cadastros de Usuário'  WHERE Chave = 'Usuario.Incluir'; ");
                comandos.Add(" update  permissoes set permissoes.Descricao = 'Visualizar cadastros de Usuário' WHERE Chave = 'Usuario.Visualizar'; ");
                
                comandos.Add("UPDATE parametros SET Valor = '2.0.2.3' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2024() //22/07/2021
        {

            List<string> comandos = new List<string>();
            try
            {
                
                comandos.Add("insert into formularios(NomeForm,TipoSistema) values ('Usuários do Portal','1');");
                comandos.Add("insert into permissoes (Chave, Descricao) values ('UsuarioPortal', 'Acesso aos usuário do Portal do representante, no menu Configurações');");
                comandos.Add("insert into permissoes (Chave, Descricao) values ('UsuarioPortal.Visualizar', 'Visualizar usuário do portal');");
                comandos.Add("insert into permissoes (Chave, Descricao) values ('UsuarioPortal.Incluir', 'Visualizar usuário do portal');");

                comandos.Add("insert into permissoes (Chave, Descricao) values ('UsuarioPortal.Alterar', 'Visualizar usuário do portal'); ");

                comandos.Add("insert into permissoes(Chave, Descricao) values('UsuarioPortal.Excluir', 'Visualizar usuário do portal');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'UsuarioPortal%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");
                               
                comandos.Add(" alter table tipomovimentacoes add column Ativo tinyint   NULL  after PercIpi; ");

                comandos.Add("update tipomovimentacoes set Ativo = 1;");
                comandos.Add("alter table tipomovimentacoes add column DescontaIcmsBase int (1)  NULL  after Ativo;");
                comandos.Add("update parametros set parametros.Valor = '2' where parametros.Chave = 'DECONTA_ICMS_PIS_COFINS'; ");

                comandos.Add(" INSERT INTO parametros (Chave, Valor, EmpresaId, VisaoCliente) VALUES ('PERCENTUAIS_EMPRESAS', 2, NULL, 2);");

                comandos.Add(" ALTER TABLE grupooperacoes  ADD KEY FK_GRUPOOPERACOES_GRUPOPACOTE_idx (grupopacoteId); ");

                comandos.Add("UPDATE parametros SET Valor = '2.0.2.4' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2025() //17/08/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("CREATE TABLE premiopartida ( " +
                              "Id int(11) NOT NULL AUTO_INCREMENT, " +
                              "Referencia varchar(45) NOT NULL, " +
                              "Descricao varchar(200) NOT NULL, " +
                              "PremioGrupo int(1) DEFAULT NULL, " +
                              "PremioAssiduidade int(1) DEFAULT NULL, " +
                              "PremioIndividual int(1) DEFAULT NULL, " +
                              "AssTipo int(1) DEFAULT NULL, " +
                              "AssValor decimal(10, 5) DEFAULT NULL, " +
                              "IndTipo int(1) NULL, " +
                              "IndValor decimal(10, 5) DEFAULT NULL, " +
                              "IndMinimo decimal(10, 5) DEFAULT NULL, " +
                              "IndMaximo decimal(10, 5) DEFAULT NULL, " +
                              "IndValPartida decimal(10, 5) DEFAULT NULL, " +
                              "GruTipo int(1) NULL, " +
                              "GruValor decimal(10, 5) DEFAULT NULL, " +
                              "GruMinimo decimal(10, 5) DEFAULT NULL, " +
                              "GruMaximo decimal(10, 5) DEFAULT NULL, " +
                              "GruValPartida decimal(10, 5) DEFAULT NULL, " +
                              "PRIMARY KEY(Id)  " +
                              ") ENGINE = InnoDB DEFAULT CHARSET = utf8; ");

                comandos.Add("create table premiopartidafuncionarios ( " +
                              "Id int(11) NOT NULL AUTO_INCREMENT, " +
                              "IdFuncionario int(11) NOT NULL, " +
                              "IdPremio int(11) NOT NULL, " +
                              "PRIMARY KEY(Id), " +
                              "KEY FK_premiopartidafuncionarios_Funcionarios(IdFuncionario), " +
                              "KEY FK_premiopartidafuncionarios_PremioPartida(IdPremio), " +
                              "CONSTRAINT FK_premiopartidafuncionarios_Funcionarios FOREIGN KEY(IdFuncionario) REFERENCES funcionarios(Id), " +
                              "CONSTRAINT FK_premiopartidafuncionarios_PremioPartida FOREIGN KEY(IdPremio) REFERENCES premiopartida(Id) " +
                              ")ENGINE = InnoDB DEFAULT CHARSET = utf8; ");

                comandos.Add("INSERT INTO parametros (Chave, Valor, EmpresaId, VisaoCliente) VALUES ('USA_VALOR_PARTIDA', 2, NULL, 2);");

                comandos.Add("UPDATE parametros SET Valor = '2.0.2.5' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2026() //17/08/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("ALTER TABLE operacaooperadora ADD KEY FK_operacaooperadora_pacote (PacoteId);");

                comandos.Add("alter table totaiscaixas add column PixDep decimal (14,5) DEFAULT '0' NOT NULL;");

                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('CALCULA_COMISSAO_PARCELA', '2', NULL, '2');");

                comandos.Add("ALTER TABLE bancos ADD COLUMN PadraoParaVendas tinyint(1) NOT NULL DEFAULT '0';");               

                comandos.Add("alter table colaboradores add column DiaVencimento int (10) DEFAULT '0' NOT NULL , add column ValorMensalidade decimal (10,2) DEFAULT '0' NOT NULL  ");

                comandos.Add("insert into permissoes(Chave, Descricao) values('Colaborador.Vencimentos', 'Incluir titulos baseado em contratos');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Colaborador.Vencimentos%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add("UPDATE parametros SET Valor = '2.0.2.6' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2027() //06/09/2021
        {

            List<string> comandos = new List<string>();
            try
            {
                                
                comandos.Add("UPDATE parametros SET Valor = '2.0.2.7' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2028() //25/09/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("alter table pacotes add column observacao varchar (20000)  NULL  after concluido");

                comandos.Add("CREATE TABLE tipoartigo(" +
                                            " Id int(11) NOT NULL AUTO_INCREMENT, " +
                                            " Descricao varchar(300) NOT NULL, " +
                                            " Ativo tinyint(1) NOT NULL, " +
                                            " PRIMARY KEY(Id) " +
                                            " ) ENGINE = InnoDB DEFAULT CHARSET = utf8");

                comandos.Add("CREATE TABLE etqcomposicao (  " +
                             " Id int(11) NOT NULL AUTO_INCREMENT,  " +
                             " ProdutoId int(11) DEFAULT NULL,  " +
                             " TipoArtigoId int(11) NOT NULL,  " +
                             " DescComposicao varchar(2000) DEFAULT NULL,  " +
                             " DataCriacaoAlteracao datetime NOT NULL,  " +
                             " UsuarioCriacaoAlteracaoId int(11) NOT NULL,  " +
                             " Numero int(11) DEFAULT NULL, " +
                             " PRIMARY KEY(Id),  " +
                             " KEY FK_EtqComposicao_Produtos_Vendedor(ProdutoId),  " +
                             " KEY FK_Atividades_Usuarios_Criador(UsuarioCriacaoAlteracaoId),  " +
                             " KEY FK_Atividades_TipoArtigo(TipoArtigoId)  " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8 ");

                comandos.Add("ALTER TABLE contaspagar ADD COLUMN Prazo INT(11) NOT NULL;");

                comandos.Add("insert into tipoartigo(Descricao,Ativo) values ('TECIDO PRINCIPAL','1');");
                comandos.Add("insert into tipoartigo(Descricao,Ativo) values ('TECIDO ESTAMPADO','1');");
                comandos.Add("insert into tipoartigo(Descricao,Ativo) values ('TULE','1');");
                comandos.Add("insert into tipoartigo(Descricao,Ativo) values ('FORRO','1');");
                comandos.Add("insert into tipoartigo(Descricao,Ativo) values ('BOJO','1');");
                comandos.Add("insert into tipoartigo(Descricao,Ativo) values ('FORRO BOJO','1');");


                comandos.Add("UPDATE parametros SET Valor = '2.0.2.8' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2029() //25/09/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("alter table tipomovimentacoes add column PercDiferimentoFcp decimal(10, 2)  NULL;");

                comandos.Add("UPDATE parametros SET Valor = '2.0.2.9' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2030() //15/10/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("alter table pedidovenda add column DescPercent decimal (10,2) DEFAULT '0' NOT NULL  after OpcaoTabelaPreco, add column DescValor decimal (10,2) DEFAULT '0' NOT NULL  after DescPercent;");

                comandos.Add("alter table creditofornecedor add column IdDevolucao int (11)  NULL  after IdFornecedor;");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('OBS_CLIENTE_NOTA','2',NULL,'1');");

                comandos.Add("ALTER TABLE fichatecnicadomaterialitem MODIFY COLUMN custocalculado DECIMAL(18,8);");

                comandos.Add("alter table colaboradores add column Bonificado decimal (10,2) DEFAULT '0.00' NOT NULL  after ValorMensalidade, " +
                             " add column Pis decimal (10,2) DEFAULT '0.00' NOT NULL  after Bonificado, add column Cofins decimal (10,2) DEFAULT '0.00' NOT NULL  after Pis, " +
                             " add column Sefaz decimal (10,2) DEFAULT '0.00' NOT NULL  after Cofins");

                comandos.Add("ALTER TABLE despesafixavariavel ADD COLUMN AutomatizarContasPagar TINYINT(1) NOT NULL DEFAULT 0; ");
                comandos.Add("ALTER TABLE despesafixavariavelmes ADD COLUMN NaturezaFinanceiraId INT(11); ");
                comandos.Add("ALTER TABLE despesafixavariavelmes ADD INDEX FK_despesafixavariavelmes_naturezafinanceira(NaturezaFinanceiraId); ");

                comandos.Add("insert into permissoes(Chave, Descricao) values('RelExtratoBancario', 'Permite ao usuário emitir relatório de extrato');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'RelExtratoBancario%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('FINALIZA_PACOTE_FACCAO','2',NULL,'2');");

                comandos.Add("ALTER TABLE PACOTES ADD COLUMN qtdproduzida INT (11) AFTER quantidade;");

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.0' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2031() // 04/11/2021
        {

            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('DESCONSIDERA_BANCO_PADRAO','2',NULL,'2');");

                comandos.Add("ALTER TABLE fichatecnica ADD COLUMN DataAlteracao DATETIME DEFAULT NULL, ADD COLUMN UserId INT(11) DEFAULT NULL after DataAlteracao;");
                comandos.Add("ALTER TABLE fichatecnicadomaterial ADD COLUMN DataAlteracao DATETIME DEFAULT NULL, ADD COLUMN UserId INT(11) DEFAULT NULL after DataAlteracao;");

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('OCULTA_SEG_UM_FICHA','2',NULL,'2');");

                comandos.Add(" UPDATE despesafixavariavelmes " +
                            "   SET Despesa = (SELECT descricao FROM naturezasfinanceiras WHERE naturezasfinanceiras.id = despesafixavariavelmes.NaturezaFinanceiraId)  " +
                            "   WHERE NaturezaFinanceiraId > 0 AND NaturezaFinanceiraId IS NOT NULL; ");

                comandos.Add("insert into permissoes(Chave, Descricao) values('Funcionario.Imprimir', 'Permite ao usuário emitir relatório');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Funcionario.Imprimir%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('STATUS_CORTE','2',NULL,'2');");

                comandos.Add("insert into permissoes(Chave, Descricao) VALUES('OrdemProducao.EnviarCorte', 'Permite ao usuário enviar a ordem para corte');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'OrdemProducao.EnviarCorte%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                //Ficha Técnica do Material Browse
                string pathFicha = AppDomain.CurrentDomain.BaseDirectory + "58.xml";
                if (System.IO.File.Exists(pathFicha))
                {
                    System.IO.File.Delete(pathFicha);

                    comandos.Add("DELETE FROM formulariolayout WHERE FormId = 58 ;");

                }

                //Ficha Técnica Browse
                string pathFichaMaterial = AppDomain.CurrentDomain.BaseDirectory + "80.xml";
                if (System.IO.File.Exists(pathFichaMaterial))
                {
                    System.IO.File.Delete(pathFichaMaterial);

                    comandos.Add("DELETE FROM formulariolayout WHERE FormId = 80 ;");

                }

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.1' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2032() // 26/11/2021
        {
            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('DESCONTAR_DESONERADO','2',NULL,'2');");

                comandos.Add("alter table versao add column IdNaWeb int (1)  NULL , add column BaseWeb varchar (100)  NULL  after IdNaWeb, add column IdEmpresa int (11) DEFAULT '0' NOT NULL  after BaseWeb;");

                comandos.Add("alter table pendencias add column IdEmpresa int (11)  NOT NULL  after Tipo;");

                comandos.Add("ALTER TABLE ordemproducao ADD COLUMN observacaomateriais VARCHAR(2000);");

                comandos.Add("ALTER TABLE produtividade CHANGE COLUMN Tempo Tempo DECIMAL(10,5); ");

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('TABELA_PRECO_MANUAL','2',NULL,'2');");

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('CONSIDERA_PRODUTIVIDADE','2',NULL,'2');");

                comandos.Add("insert into permissoes(Chave, Descricao) values('Produto.Planilha', 'Permite ao usuário incluir produto por planilha');");

                comandos.Add("INSERT INTO parametros(Chave, Valor, EmpresaId, VisaoCliente) VALUES('PADRAO_PLANILHA_PRODUTO', '1', NULL, '2');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Produto.Planilha%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");



                comandos.Add("UPDATE parametros SET Valor = '2.0.3.2' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2033() // 26/11/2021
        {
            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("INSERT INTO parametros(Chave,Valor,EmpresaId,VisaoCliente) VALUES ('HABILITA_DESCONTO_AVAL','2',NULL,'2');");

                comandos.Add("ALTER TABLE nfe ADD COLUMN percentaval DECIMAL(10,2), ADD COLUMN valoraval DECIMAL(10,2);");

                comandos.Add("ALTER TABLE nfe ADD COLUMN avalRateado INT(1) DEFAULT 0;");

                comandos.Add("INSERT INTO tipodocumentos (id, referencia, descricao) VALUES (999, 'AVAL', '*Desconto com Aval');");                

                comandos.Add("insert into permissoes(Chave, Descricao) values('Estoque.Importar', 'Permite ao usuário fazer o acerto do estoque por planilha');");

                comandos.Add("insert into permissoes(Chave, Descricao) values('Estoque.Exportar', 'Permite ao usuário exportar o estoque para planilha');");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Estoque.Importar%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'Estoque.Exportar%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add(" CREATE TABLE descricaomedida ( " +
                   " Id int(11) NOT NULL AUTO_INCREMENT, " +
                   " Descricao varchar(300) NOT NULL, " +
                   " Ativo tinyint(1) NOT NULL, " +
                   " PRIMARY KEY(Id) " +
                   " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add(" CREATE TABLE MedidasProduto ( " +                                        
                 " Id int(11) NOT NULL AUTO_INCREMENT, " +
                 " ProdutoId int(11) DEFAULT NULL, " +
                 " DescricaoMedidaId int(11) DEFAULT NULL, " +
                 " TamanhoId int(11) NOT NULL, " +
                 " Medida decimal(10, 2) NOT NULL, " +
                 " Tolerancia varchar(2000) DEFAULT NULL, " +
                 " PRIMARY KEY(Id), " +
                 " KEY FK_MedidasProduto_Produtos(ProdutoId), " +
                 " KEY FK_MedidasProduto_Tamanho(TamanhoId), " +
                 " KEY FK_MedidasProduto_DescricaoMedida(DescricaoMedidaId), " +
                 " CONSTRAINT `FK_DescricaoMedida` FOREIGN KEY(`DescricaoMedidaId`) REFERENCES `descricaomedida` (`Id`), " +
                 " CONSTRAINT `FK_Produto` FOREIGN KEY(`ProdutoId`) REFERENCES `produtos` (`Id`),   " +
                 " CONSTRAINT `FK_Tamanho` FOREIGN KEY(`TamanhoId`) REFERENCES `tamanhos` (`Id`)        " +
                 " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('COPIA_OBS_FATURAMENTO','2',NULL,'2'); ");
 

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.3' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //ATT 2022 Uhuuuuu, gloria a Deus !!!
        private bool AplicarPatch2034() // 
        {
            List<string> comandos = new List<string>();
            try
            {
                
                comandos.Add(" alter table nfe add column idOrdemProducao int (11)  NULL after idtabela; ");

                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('NFCe.Alterar', 'Acesso ao botão de Alterar NFCe'); ");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT    DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'NFCe.Alterar%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('NAO_EXIBE_GRADE_ITEM','2',NULL,'2');");


                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('NotaEntradaFaccao', 'Acesso a rotina nota de entrada facção'); ");

                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('NotaEntradaFaccao.Visualizar', 'Visualizar a tela nota de entrada facção'); ");

                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('NotaEntradaFaccao.Incluir', 'Incluir nota de entrada facção'); ");

                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('NotaEntradaFaccao.Excluir', 'Excluir nota de entrada facção'); ");
                

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                           " G.Id AS GrupoId " +
                           " FROM Permissoes P " +
                           " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                           " WHERE P.Chave LIKE 'NotaEntradaFaccao%' AND " +
                           " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");


                comandos.Add(" CREATE TABLE notaentradafaccao ( " +                                                                                     
                             " Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " Idempresa int(11) NOT NULL, " +
                             " idtransportadora int(11) DEFAULT NULL, " +
                             " idAlmoxarifado int(11) DEFAULT NULL, " +
                             " IdColaborador int(11) NOT NULL, " +
                             " IdOrdemProducao int(11) NOT NULL,   " +
                             " Serie int(11) DEFAULT NULL, " +
                             " referencia varchar(10) DEFAULT NULL, " +
                             " numero varchar(10) DEFAULT NULL, " +
                             " DataInclusao datetime DEFAULT NULL, " +
                             " DataEmissao datetime DEFAULT NULL, " +
                             " frete decimal(10, 2) DEFAULT NULL, " +
                             " seguro decimal(10, 2) DEFAULT NULL, " +
                             " despesa decimal(10, 2) DEFAULT NULL, " +
                             " descontopercent decimal(10, 2) DEFAULT NULL, " +
                             " valdesconto decimal(10, 2) DEFAULT NULL, " +
                             " observacao text, " +
                             " descontoitem decimal(10, 2) DEFAULT NULL, " +
                             " baseicms decimal(10, 2) DEFAULT NULL, " +
                             " valoricms decimal(10, 2) DEFAULT NULL, " +
                             " baseipi decimal(10, 2) DEFAULT NULL, " +
                             " valoripi decimal(10, 2) DEFAULT NULL, " +
                             " totalnota decimal(10, 2) DEFAULT NULL, " +
                             " totalitens decimal(10, 2) DEFAULT NULL, " +
                             " totalprodutos decimal(10, 2) DEFAULT NULL, " +
                             " descontoValor decimal(10, 2) DEFAULT NULL, " +                             
                             " TotalmenteDevolvida int(11) DEFAULT NULL, " +
                             " PossuiDevolucao int(11) DEFAULT NULL, " +                             
                             " PRIMARY KEY(Id), " +
                             " KEY FK_notaentradafaccao_Empresa(Idempresa), " +
                             " KEY FK_notaentradafaccao_Transportadora(idtransportadora), " +
                             " KEY FK_notaentradafaccao_Almoxarifado(idAlmoxarifado), " +
                             " KEY FK_notaentradafaccao_Colaborador(IdColaborador), " +
                             " CONSTRAINT FK_notaentradafaccao_Almoxarifado FOREIGN KEY(idAlmoxarifado) REFERENCES almoxarifados(id), " +
                             " CONSTRAINT FK_notaentradafaccao_Colaborador FOREIGN KEY(IdColaborador) REFERENCES colaboradores(id), " +
                             " CONSTRAINT FK_notaentradafaccao_Empresa FOREIGN KEY(Idempresa) REFERENCES empresas(Id), " +
                             " CONSTRAINT FK_notaentradafaccao_Ordem FOREIGN KEY (IdOrdemProducao) REFERENCES ordemproducao (id)," +
                             " CONSTRAINT FK_notaentradafaccao_Transportadora FOREIGN KEY(idtransportadora) REFERENCES colaboradores(id) " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add(" CREATE TABLE notaentradafaccaoitens ( " +
                             " Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " IdNota int(11) NOT NULL, " +
                             " IdTipoMov int(11) NOT NULL, " +
                             " NumItem int(11) NOT NULL, " +
                             " CalculaIcms int(11) NOT NULL, " +
                             " CalculaIpi int(11) DEFAULT NULL, " +
                             " IdItemNaOrdem int(11) DEFAULT NULL, " +
                             " iditem int(11) NOT NULL, " +
                             " idcor int(11) DEFAULT NULL, " +
                             " idtamanho int(11) DEFAULT NULL, " +
                             " quantidadeOp decimal(14, 5) DEFAULT NULL, " +
                             " quantidadeEntrega decimal(14, 5) DEFAULT NULL, " +
                             " quantidadeProduzida decimal(14, 5) DEFAULT NULL, " +
                             " quantidadeAvaria decimal(14, 5) DEFAULT NULL, " +
                             " quantidadeDefeito decimal(14, 5) DEFAULT NULL, " +
                             " precoproduzido decimal(10, 4) DEFAULT NULL, " +
                             " precoavaria decimal(10, 4) DEFAULT NULL, " +
                             " precodefeito decimal(10, 4) DEFAULT NULL, " +
                             " preco decimal(10, 4) DEFAULT NULL, " +
                             " total decimal(10, 2) DEFAULT NULL, " +
                             " DescontoRateado decimal(10, 2) DEFAULT NULL, " +
                             " BaseIcmsRateado decimal(10, 2) DEFAULT NULL, " +
                             " Percenticms decimal(10, 2) DEFAULT NULL, " +
                             " ValorIcmsRateado decimal(10, 2) DEFAULT NULL, " +
                             " BaseIpiRateado decimal(10, 2) DEFAULT NULL, " +
                             " Percentipi decimal(10, 2) DEFAULT NULL, " +
                             " ValorIpiRateado decimal(10, 2) DEFAULT NULL, " +
                             " DespesaRateio decimal(10, 2) DEFAULT NULL, " +
                             " FreteRateio decimal(10, 2) DEFAULT NULL, " +
                             " SeguroRateio decimal(10, 2) DEFAULT NULL, " +
                             " Qtddevolvida decimal(10, 5) DEFAULT NULL, " +
                             " Excedente decimal(10, 4) DEFAULT NULL, " +                             
                             " PRIMARY KEY(Id), " +
                             " KEY FK_notaentradafaccaoitens_Nota(IdNota), " +
                             " KEY FK_notaentradafaccaoitens_Item(iditem), " +
                             " KEY FK_notaentradafaccaoitens_Cor(idcor), " +
                             " KEY FK_notaentradafaccaoitens_Tamanho(idtamanho), " +
                             " CONSTRAINT FK_notaentradafaccaoitens_Cor FOREIGN KEY(idcor) REFERENCES cores(Id), " +
                             " CONSTRAINT FK_notaentradafaccaoitens_Item FOREIGN KEY(iditem) REFERENCES produtos(Id), " +
                             " CONSTRAINT FK_notaentradafaccaoitens_Nota FOREIGN KEY(IdNota) REFERENCES notaentradafaccao(Id), " +
                             " CONSTRAINT FK_notaentradafaccaoitens_Tamanho FOREIGN KEY(idtamanho) REFERENCES tamanhos(Id)" +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add("insert into contadorescodigo(Id,Nome,Descricao,Prefixo,ContadorAtual,CasasDecimais,Ativo) values ( NULL,'NotaEntradaFaccao','Referência no cadastro de Nota de Entrada','','0','8','1');");

                comandos.Add("alter table itensordemproducao add column QuantidadeAvaria decimal (10,0) DEFAULT '0' NOT NULL  after DataPrevisao, add column QuantidadeDefeito decimal (10,0) DEFAULT '0' NOT NULL  after QuantidadeAvaria;");

                comandos.Add("alter table contaspagar add column IdNotaEntradaFaccao int (11)  NULL  after IdNotaEntrada;");

                comandos.Add("alter table pedidovendaconferencia add column VirouPedidoDci int (1) DEFAULT '0' NOT NULL  after UsuarioId;");

                
                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('ConferenciaSemEmpenho', 'Acesso à tela de Conferência de Pedido Sem Empenho, no menu Vendas'); ");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                          " SELECT    DISTINCT P.Id AS PermissaoId, " +
                          " G.Id AS GrupoId " +
                          " FROM Permissoes P " +
                          " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                          " WHERE P.Chave LIKE 'ConferenciaSemEmpenho' AND " +
                          " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.4' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2035() // 04/03
        {
            List<string> comandos = new List<string>();
            try
            {
                comandos.Add("alter table ordemproducao add column IdColaborador int (11)  NULL  after AlmoxarifadoId;");
                comandos.Add("alter table ordemproducao add constraint FK_ordemproducao_Colaboradores foreign key(IdColaborador)references colaboradores (id) on delete restrict  on update restrict;");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('FILTRA_ITEM_DESCRICAO','2',NULL,'2');");
                comandos.Add("alter table nfeitens add column QtdAcimaDaOp decimal (10,4)  NULL  after ValorIpi;");
                comandos.Add("insert into parametros (Chave, Valor, EmpresaId, VisaoCliente) SELECT 'DEFINE_SERIE_NFE','1',empresas.Id,1 from empresas");
                comandos.Add("CREATE TABLE AtividadeFaccao (  " +                                                                            
                             " Id int(11) NOT NULL AUTO_INCREMENT, " +
                             "  Idempresa int(11) NOT NULL, " +
                             "   Referencia varchar(10) NOT NULL, " +
                             "  Descricao varchar(200) NOT NULL, " +
                             "  Ativo tinyint(1) DEFAULT NULL, " +
                             "  ValorOperacao decimal(10, 2) NOT NULL DEFAULT '0.00', " +
                             "  PRIMARY KEY(Id), " +
                             "  KEY FK_AtividadeFaccao_Empresa(Idempresa), " +
                             "  CONSTRAINT FK_AtividadeFaccao_Empresa FOREIGN KEY(Idempresa) REFERENCES empresas(Id)  " +
                             "  ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");
                comandos.Add("insert into contadorescodigo(Nome,Descricao,Prefixo,ContadorAtual,CasasDecimais,Ativo) values ('AtividadeFaccao','Referencia no cadastro de Atividade Facção',NULL,'0','8','1');");
                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('AtividadeFaccao', 'Acesso à tela de Atividade de Facção, no menu cadastro');");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                          " SELECT    DISTINCT P.Id AS PermissaoId, " +
                          " G.Id AS GrupoId " +
                          " FROM Permissoes P " +
                          " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                          " WHERE P.Chave LIKE 'AtividadeFaccao' AND " +
                          " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; "); 
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('CUSTO_FICHA_EXCECAO','2',NULL,'2');");
                comandos.Add("alter table contaspagar add column Notinha int (1) DEFAULT '0' NOT NULL  after Prazo");
                comandos.Add(" CREATE TABLE contaspagarfaccao( " +
                            " Id int(11) NOT NULL AUTO_INCREMENT, " +
                            " IdContasPagar int(11) NOT NULL, " +
                            " NumLinha int(11) NOT NULL, " +
                            " IdAtividade int(11) NOT NULL, " +
                            " Quantidade int(11) DEFAULT NULL, " +
                            " Preco decimal(10, 2) DEFAULT NULL, " +
                            " Total decimal(10, 2) DEFAULT NULL, " +
                            " PRIMARY KEY(Id), " +
                            " KEY FK_contaspagarfaccao(IdContasPagar), " +
                            " CONSTRAINT FK_contaspagarfaccao FOREIGN KEY(IdContasPagar) REFERENCES contaspagar(Id) " +
                            " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8  ");
                comandos.Add("alter table rotavisitas add column CorDaRota varchar (200)  NULL  after ativo");
                comandos.Add("alter table contaspagar add column Vale decimal (10,2)  NULL  after Notinha, add column Fator1 decimal (10,2)  NULL  after Vale, add column Fator2 decimal (10,2)  NULL  after Fator1, add column TotalFator decimal (10,2)  NULL  after Fator2;");
                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('LIMITE_CREDITO_PADRAO', '0', NULL, '2')");
                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('OCULTA_OBS_ROMANEIO', '2', NULL, '2');");
                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('OPERAÇÃO_FICHA_AUTOMATICA', '0', NULL, '2');");
                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('INCLUI_FICHA_AUTOMATICA', '2', NULL, '2');");
                comandos.Add(" alter table ordemproducao add column DataPrevisaoFinalizacao datetime;");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('COR_ROTA','2',NULL,'2');");
                comandos.Add("update ordemproducao set DataPrevisaoFinalizacao = DataEmissao;");

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.5' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);


                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }


        private bool AplicarPatch2036() // 18/03
        {
            List<string> comandos = new List<string>();
            try
            {
                comandos.Add(" CREATE TABLE ProdutoFicha (Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " ProdutoId int(11) DEFAULT NULL, " +
                             " MaterialId int(11) NOT NULL, " +
                             " quantidade decimal(18, 8) DEFAULT NULL, " +
                             " custo decimal(18, 8) NOT NULL, " +
                             " preco decimal(18, 8) DEFAULT NULL, " +
                             " valor decimal(18, 8) NOT NULL, " +
                             " Numero int(11) DEFAULT NULL, " +
                             " PRIMARY KEY(Id), " +
                             " KEY FK_ProdutoFicha_Produto_(ProdutoId) " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('Colaborador.Zap', 'Cria Link do Zap para envio'); ");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                                           " G.Id AS GrupoId " +
                                           " FROM Permissoes P " +
                                           " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                                           " WHERE P.Chave LIKE 'Colaborador.Zap%' AND " +
                                           " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("alter table produtos add column DescricaoAlternativa varchar (80)  NULL  after TempoPacote ");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('DESC_ALTERNATIVA','2',NULL,'1')");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('USA_FICHA_PRODUTO','2',NULL,'2')");

                comandos.Add("alter table produtos add column ValorMaterial decimal(18, 8) DEFAULT '0' NOT NULL  after DescricaoAlternativa;");

                comandos.Add("alter table movimentacaoestoque add column IdPacote int(11), add column CancelamentoPacote int(1) default 0;");

                comandos.Add("alter table colaboradores add column IdFuncionario int(11);");

                comandos.Add("alter table colaboradores change nome nome varchar (250)  NULL  COLLATE utf8_general_ci , change razaosocial razaosocial varchar (250)  NULL  COLLATE utf8_general_ci ;");
                
                comandos.Add("insert into colaboradores(razaosocial, IdFuncionario, IdEmpresa, Nome, RegistroGeral, Cep, Endereco, Complemento, Bairro, IdEstado, Ddd, Telefone, DataNascimento, Observacao, Referencia, Fornecedor, Ativo) " +
                             " select Nome, Id, EmpresaId, Nome, RG, CEP, Endereco, Complemento, Bairro, EstadoId, DDD, Telefone, DataNascimento, Obs, " +
                             "      IF(LENGTH(Referencia) < (select CHARACTER_MAXIMUM_LENGTH from information_schema.columns where table_schema = DATABASE() AND table_name = 'colaboradores' AND COLUMN_NAME = 'referencia'), " +
                             "          CONCAT('F', Referencia), Referencia), 1, 1 " +
                             " from funcionarios" +
                             " where ifnull((select count(*) from colaboradores where idFuncionario = funcionarios.Id),0) = 0; ");

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.6' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool AplicarPatch2037() // 27/04
        {
            List<string> comandos = new List<string>();
            try
             {
                comandos.Add(" alter table fichatecnica add column QuebraManual varchar(255); ");
                comandos.Add(" alter table fichatecnicadomaterial add column QuebraManual varchar(255); ");

                //Excluir Layout Ficha Técnica do Material Browse
                string pathFicha = AppDomain.CurrentDomain.BaseDirectory + "58.xml";
                if (System.IO.File.Exists(pathFicha))
                {
                    System.IO.File.Delete(pathFicha);
                    comandos.Add("DELETE FROM formulariolayout WHERE FormId = 58 ;");
                }

                //Excluir Layout Ficha Técnica Browse
                string pathFichaMaterial = AppDomain.CurrentDomain.BaseDirectory + "80.xml";
                if (System.IO.File.Exists(pathFichaMaterial))
                {
                    System.IO.File.Delete(pathFichaMaterial);
                    comandos.Add("DELETE FROM formulariolayout WHERE FormId = 80 ;");
                }

                comandos.Add(" alter table ordemproducao add column IdColecao int(11)  NULL  after IdColaborador; ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('OCULTA_PLANO_REL','2',NULL,'1'); ");

                comandos.Add(" CREATE TABLE planejamento (      " +
                             "   Id int(11) NOT NULL AUTO_INCREMENT, " +
                             "  EmpresaId int(11) DEFAULT NULL, " +
                             "   Referencia varchar(10) NOT NULL, " +
                             "   Descricao varchar(50) NOT NULL, " +
                             "   UsuarioCriacao varchar(50) NOT NULL, " +
                             "   DataCriacao datetime NOT NULL, " +
                             "   UsuarioAlteracao varchar(50)  NULL, " +
                             "   DataAlteracao datetime DEFAULT NULL, " +
                             "   Observacao varchar(2000) DEFAULT NULL, " +
                             "   PRIMARY KEY(Id) " +
                             " ) ENGINE = InnoDB DEFAULT CHARSET = utf8; ");

                comandos.Add(" CREATE TABLE planejamentoitens (          " +           
                             "    Id int(11) NOT NULL AUTO_INCREMENT, " +
                             "    PlanejamentoId int(11) NOT NULL, " +
                             "    OrdensIds text NOT NULL, " +
                             "    OrdensRefs text NOT NULL, " +
                             "    Semana varchar(50) NOT NULL, " +
                             "    TempoSemana decimal(10, 2) DEFAULT NULL, " +
                             "    PRIMARY KEY(Id) " +
                             "  ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");

                comandos.Add(" insert into contadorescodigo(Nome,Descricao,Prefixo,ContadorAtual,CasasDecimais,Ativo) values ('Planejamento','Referencia no cadastro de planejamento','','0','10','1'); ");

                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('Planejamento', 'Acesso a tela de Planejmaneto de Produção');  ");
                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('Planejamento.Visualizar', 'Visualiza a tela de Planejmaneto de Produção'); ");
                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('Planejamento.Incluir', 'Libera a inclusão de Planejmaneto de Produção'); ");
                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('Planejamento.Alterar', 'Libera a alteração de Planejmaneto de Produção'); ");
                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('Planejamento.Excluir', 'Exclui o Planejmaneto de Produção');  ");

                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) "+
                             "             SELECT    DISTINCT P.Id AS PermissaoId, "+
                             "             G.Id AS GrupoId "+
                             "             FROM Permissoes P "+
                             "             INNER JOIN grupos G ON G.Nome = 'Administrador' "+
                             "             WHERE P.Chave LIKE 'Planejamento%' AND "+
                             "             (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");




                comandos.Add(" CREATE TABLE BalanceamentoSemanal ( " +
                             "  Id int(11) NOT NULL AUTO_INCREMENT, " +
                             "  EmpresaId int(11) DEFAULT NULL, " +
                             "  Referencia varchar(10) NOT NULL, " +
                             "  Descricao varchar(50) NOT NULL, " +
                             "  IdPlanejamento int(11) NOT NULL, " +
                             "  Eficiencia decimal(10, 2) DEFAULT NULL, " +
                             "  Aproveitamento decimal(10, 2) DEFAULT NULL, " +
                             "  Presenca decimal(10, 2) DEFAULT NULL, " +
                             "  PRIMARY KEY(Id)" +
                             "  ) ENGINE = InnoDB DEFAULT CHARSET = utf8; ");

                 
                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('BalanceamentoSemanal', 'Acesso a tela de Balanceamento Semanal');");
                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('BalanceamentoSemanal.Visualizar', 'Visualiza a tela de Balanceamento Semanal');");
                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('BalanceamentoSemanal.Incluir', 'Libera a inclusão de Balanceamento Semanal');");
                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('BalanceamentoSemanal.Alterar', 'Libera a alteração de Balanceamento Semanal');");
                comandos.Add("INSERT INTO permissoes(Chave, Descricao) VALUES('BalanceamentoSemanal.Excluir', 'Exclui o Balanceamento Semanal');");

                comandos.Add("  INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             "  SELECT    DISTINCT P.Id AS PermissaoId, " +
                             "  G.Id AS GrupoId " +
                             "  FROM Permissoes P " +
                             "  INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             "  WHERE P.Chave LIKE 'BalanceamentoSemanal%' AND " +
                             "  (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add(" CREATE TABLE balanceamentosemanalitens ( " +          
                             " Id int(11) NOT NULL AUTO_INCREMENT, " +
                             " SetorId int(11) NOT NULL, " +
                             " BalanceamentoId int(11) NOT NULL, " +
                             " PessoasTrabalhando decimal(10, 2) DEFAULT NULL, " +
                             " TempoNecessario decimal(10, 2) DEFAULT NULL, " +
                             " JornadaSemanal decimal(10, 2) DEFAULT NULL, " +
                             " ResultadoTempo decimal(10, 2) DEFAULT NULL, " +
                             " Aproveitamento decimal(10, 2) DEFAULT NULL, " +
                             " Eficiencia decimal(10, 2) DEFAULT NULL, " +
                             " Presenca decimal(10, 2) DEFAULT NULL, " +
                             " PRIMARY KEY(Id) " +
                             " ) ENGINE = InnoDB DEFAULT CHARSET = utf8; ");

                comandos.Add(" CREATE TABLE BalanceamentoSemanaItensDetalhes ( " +              
                             "  Id int(11) NOT NULL AUTO_INCREMENT, " +
                             "  BalanceamentoId int(11) NOT NULL, " +
                             "  Semanas varchar(50) NOT NULL, " +
                             "  Jornada int(10) DEFAULT NULL, " +
                             "  Pecas int(10) DEFAULT NULL, " +
                             "  PessoasTrabalhando decimal(10, 2) DEFAULT NULL, " +
                             "  SetorId int(10) DEFAULT NULL, " +
                             "  QtdMaq  decimal(10, 2) DEFAULT NULL, " +
                             "  PRIMARY KEY(Id) " +
                             " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8; ");


                comandos.Add("update empresas set cnpj = null where cnpj = '31.584.436/0001-07';");


                comandos.Add("UPDATE parametros SET Valor = '2.0.3.7' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private bool AplicarPatch2038() // 23/05
        {
            List<string> comandos = new List<string>();
            try
            {                

                comandos.Add("ALTER TABLE produtos add column SomentePrecoVenda int (1) DEFAULT '0' NOT NULL  after ValorMaterial;");

                comandos.Add("ALTER TABLE colaboradores ADD COLUMN Guia INT(1), ADD COLUMN TipoComissaoGuia INT(1), ADD COLUMN ComissaoGuia DECIMAL(10,2), ADD COLUMN ObsGuia TEXT CHARACTER SET utf8 COLLATE utf8_general_ci;");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('OBRIGA_GUIA_NFCE','2',NULL,'1');");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('DEFINE_TIPO_NFCE','0',NULL,'1');");

                comandos.Add("ALTER TABLE nfce ADD COLUMN IdGuia INT(11) AFTER idvendedor, ADD COLUMN TipoNFCe INT(1) DEFAULT 0 NOT NULL AFTER idTabelaPreco, ADD COLUMN Observacao TEXT CHARACTER SET utf8 COLLATE utf8_general_ci, ADD COLUMN totaldevolvido DECIMAL(10,2) AFTER totaloriginal;");

                comandos.Add("ALTER TABLE comissoesvendedor ADD COLUMN idGuia INT(11) AFTER idvendedor, DROP FOREIGN KEY FK_comissoesvendedor_Vendedor ; ");

                comandos.Add("DROP INDEX FK_comissoesvendedor_Vendedor ON comissoesvendedor; ");

                comandos.Add("ALTER TABLE nfceitens ADD COLUMN Devolucao INT(1) DEFAULT 0; ");

                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('NFCe.ImprimirCredito', 'Acesso a impressão de crédito de cliente gerado pela NFCe'); ");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                                           " G.Id AS GrupoId " +
                                           " FROM Permissoes P " +
                                           " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                                           " WHERE P.Chave LIKE 'NFCe.ImprimirCredito%' AND " +
                                           " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add("ALTER TABLE creditoscliente ADD COLUMN IdNfceQuitado INT(11); ");

                comandos.Add("ALTER TABLE produtos ADD COLUMN PrecoAtacado DECIMAL(10,4) AFTER PrecoVenda, ADD COLUMN PrecoPromocional DECIMAL(10,4) AFTER PrecoAtacado, ADD COLUMN InicioPromocao datetime AFTER PrecoPromocional, ADD COLUMN FimPromocao datetime AFTER InicioPromocao ; ");

                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('ListaMateriaisOP', 'Acesso ao Relatório de Lista de Materiais Baseado em OP'); ");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                                           " G.Id AS GrupoId " +
                                           " FROM Permissoes P " +
                                           " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                                           " WHERE P.Chave LIKE 'ListaMateriaisOP%' AND " +
                                           " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0; ");

                comandos.Add(" UPDATE parametros set chave = 'REL_OP_PONTOCERTO' where chave = 'OCULTA_PLANO_REL'; ");

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.8' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2039() // 30/05
        {
            List<string> comandos = new List<string>();
            try
            {   

                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('PacotesProducao.Observacao', 'Acesso a tela de observação das operações'); ");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                                           " G.Id AS GrupoId " +
                                           " FROM Permissoes P " +
                                           " INNER JOIN grupos G ON G.Nome like '%%' " +
                                           " WHERE P.Chave LIKE 'PacotesProducao.Observacao%'; ");

                comandos.Add(" ALTER TABLE contasreceber ADD COLUMN Prazo INT(11) NOT NULL DEFAULT 0; ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('USA_PRECO_COMPRA_FACCAO','2',NULL,'1'); ");

                comandos.Add(" ALTER TABLE fichafaccao ADD COLUMN precoCompra DECIMAL(14,4) NOT NULL DEFAULT 0; ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('OBRIGA_CHEQUE_NFCE','1',NULL,'1'); ");

                comandos.Add(" alter table totaiscaixas add column idNfce INT(11); ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('SALVA_OBS_CHEQUE_CLIENTE','2',NULL,'1'); ");

                comandos.Add(" ALTER TABLE cheques ADD COLUMN Observacao TEXT COLLATE utf8_general_ci;  ");

                comandos.Add(" update fichafaccao set precocompra = valorpeca;  ");

                comandos.Add("UPDATE parametros SET Valor = '2.0.3.9' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2040() // 19/06
        {
            List<string> comandos = new List<string>();
            try
            {
                //Excluir Layout Pedido Compra
                string pathFicha = AppDomain.CurrentDomain.BaseDirectory + "32.xml";
                if (System.IO.File.Exists(pathFicha))
                {
                    System.IO.File.Delete(pathFicha);
                    comandos.Add("DELETE FROM formulariolayout WHERE FormId = 32 ;");
                }

                //Excluir Layout Ordem
                string pathFichaMaterial = AppDomain.CurrentDomain.BaseDirectory + "54.xml";
                if (System.IO.File.Exists(pathFichaMaterial))
                {
                    System.IO.File.Delete(pathFichaMaterial);
                    comandos.Add("DELETE FROM formulariolayout WHERE FormId = 54 ;");
                }


                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('NFCe.LimparPagamento', 'Acesso botão de Limpar Pagamento na alteração de NFCe'); ");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                                           " G.Id AS GrupoId " +
                                           " FROM Permissoes P " +
                                           " INNER JOIN grupos G ON G.Nome like '%%' " +
                                           " WHERE P.Chave LIKE 'NFCe.LimparPagamento%'; ");

                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('NFCe.IncluirDesconto', 'Acesso a inclusão de desconto na NFCe'); ");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                                           " G.Id AS GrupoId " +
                                           " FROM Permissoes P " +
                                           " INNER JOIN grupos G ON G.Nome like '%%' " +
                                           " WHERE P.Chave LIKE 'NFCe.IncluirDesconto%'; ");

                comandos.Add(" ALTER TABLE totaiscaixas ADD COLUMN idNfe INT(11); ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('ALTERA_NFCE_VENDA','2',NULL,'1');");

                comandos.Add(" ALTER TABLE nfe ADD COLUMN DescNfce DECIMAL (10,2) DEFAULT 0; ");

                comandos.Add(" insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('AGRUPA_REL_PREVENDA','2',NULL,'1'); ");

                comandos.Add(" UPDATE produtos set produtos.Fatorconversao = 1 where produtos.Fatorconversao = 0; ");

                comandos.Add("UPDATE parametros SET Valor = '2.0.4.0' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2041() // 29/06/2022
        {
            List<string> comandos = new List<string>();
            try
            {
                comandos.Add(" INSERT INTO permissoes(Chave, Descricao) VALUES('NFCe.PermiteFinalizar', 'Permite finalizar NFCe'); ");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                           " SELECT    DISTINCT P.Id AS PermissaoId, " +
                                           " G.Id AS GrupoId " +
                                           " FROM Permissoes P " +
                                           " INNER JOIN grupos G ON G.Nome like '%%' " +
                                           " WHERE P.Chave LIKE 'NFCe.PermiteFinalizar%'; ");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('OBRIGAGRADEITEM','2',NULL,'2');");

                comandos.Add("Alter table contaspagarfaccao add column DataAtividade datetime NULL  after Total;");
                comandos.Add("Alter table vestillo.atividadefaccao add column Milheiro tinyint(1) DEFAULT '1' NOT NULL  after ValorOperacao;");
                comandos.Add("UPDATE parametros SET Valor = '2.0.4.1' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPatch2042() //15/07/2022
        {
            List<string> comandos = new List<string>();
            try
            {
                int contador = 0;
                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM setores WHERE setores.Abreviatura = '*FAC'");
                if (contador == 0)
                {
                    comandos.Add("insert into setores(Abreviatura,Descricao,IdDepartamento,Ativo,Balanceamento) values ('*FAC','*Faccao','1','1','0');;");
                }

                comandos.Add(" insert into setores(Id,Abreviatura,Descricao,IdDepartamento,Ativo,Balanceamento) values (2000,'INT','*Interno','1','1','0'); ");          

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('ATUALIZA_PROTHEUS','2',NULL,'2');");

                comandos.Add("CREATE TABLE TemposProtheus (" +
                " Id int(11) NOT NULL AUTO_INCREMENT, " +
                " Produto varchar(30) NOT NULL," +
                " TempoTotal decimal(12, 6) NOT NULL DEFAULT '0.000000'," +
                " TempoMenosInterno decimal(12, 6) NOT NULL DEFAULT '0.000000'," +
                " DataAlteracao TIMESTAMP DEFAULT CURRENT_TIMESTAMP," +
                " Status TINYINT(1) DEFAULT 0," +
                " PRIMARY KEY(Id)" +
                " ) ENGINE = InnoDB AUTO_INCREMENT = 1; ");

                comandos.Add("alter table funcionarios add column ExecutaOperacaoManual tinyint(1) DEFAULT '1' NOT NULL  after Referencia;");
                comandos.Add("alter table funcionarios add column RelogioDePonto varchar(40)  NULL after ExecutaOperacaoManual;");



                comandos.Add("UPDATE parametros SET Valor = '2.0.4.2' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2043() //29/07/2022
        {
            List<string> comandos = new List<string>();
            try
            {                
                comandos.Add("UPDATE parametros SET Valor = '2.0.4.3' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2044() //12/08/2022
        {
            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("ALTER TABLE pedidovenda add column ObservacaoTransf varchar (300)  NULL  after DescValor;");
                comandos.Add("INSERT INTO permissoes(Chave,Descricao) values ('PedidoVenda.Transferir','Permite transferir Pedidos');");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT     DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'PedidoVenda.Transferir%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");
                comandos.Add("ALTER TABLE nfe add column EmpresaTrocada int (1) DEFAULT '0' NOT NULL  after DescNfce, add column NomeEmpresaTrocada varchar (200)  NULL  after EmpresaTrocada;");
                comandos.Add("insert into parametros(Id,Chave,Valor,EmpresaId,VisaoCliente) values ( NULL,'FUNC_OPERACAO_AUTO','20',NULL,'1');");
                comandos.Add("ALTER TABLE pacotes add column UsuarioLote varchar (50)  NULL  after observacao, add column FinalizouLote int (1) DEFAULT '0' NOT NULL  after UsuarioLote;");

                comandos.Add("alter table almoxarifados add column EnviarEcommerce  boolean DEFAULT '0' NOT NULL;");
                comandos.Add("alter table produtos add DataAlteracao DATETIME DEFAULT CURRENT_TIMESTAMP;");
                comandos.Add("alter table produtos add column EnviarEcommerce boolean DEFAULT '0' NOT NULL;");
                comandos.Add("alter table produtos add DataUltimoEnvioTray DATETIME;");
                comandos.Add("alter table produtos add TrayId varchar(20);");
                comandos.Add("alter table produtos add DataUltimoEnvioBling DATETIME;");
                comandos.Add("alter table produtos add BlingId varchar(20);");
                comandos.Add("alter table produtos add CodigoBarrarEcommerce VARCHAR(20);");

                comandos.Add("alter table produtodetalhes ADD DataAlteracao DATETIME DEFAULT CURRENT_TIMESTAMP;");
                comandos.Add("alter table produtodetalhes add DataUltimoEnvioTray DATETIME;");
                comandos.Add("alter table produtodetalhes add TrayId varchar(20);");
                comandos.Add("alter table produtodetalhes add DataUltimoEnvioBling DATETIME;");
                comandos.Add("alter table produtodetalhes add BlingId varchar(20);");


                comandos.Add("INSERT INTO parametros(chave, valor, VisaoCliente) values('TRAY_URL_API', 'https://trayparceiros.commercesuite.com.br/web_api/auth', 2);");
                comandos.Add("INSERT INTO parametros(chave, valor, VisaoCliente) values('TRAY_CONSUMER_KEY_API', '5d3db9ff40adfac74b0cefb2fa6144c1800a4432cf99ad323a9f86ef488b1140', 2);");
                comandos.Add("INSERT INTO parametros(chave, valor, VisaoCliente) values('TRAY_CONSUMER_SECRET_API', 'f1fcfe53f93237a4aa84d9df273a8996d1953bd418777a9381b69992ddf25f05', 2);");
                comandos.Add("INSERT INTO parametros(chave, valor, VisaoCliente) values('TRAY_APLICATIVO_CODE_API', 'a929fc35c8438752c7a8b99dd55e42900b480ebf6fa9b9a867332fab6a2d1608', 2);");

                comandos.Add("INSERT INTO parametros(chave, valor, VisaoCliente) values('DATA_IMP_PEDIDOS_TRAY', '', 2);");
                comandos.Add("INSERT INTO parametros(chave, valor, VisaoCliente) values('DATA_IMP_PEDIDOS_BLING', '', 2);");

                comandos.Add("INSERT INTO parametros (chave, valor, VisaoCliente) values ('BLING_KEY_API', 'bca90da93505a3a3703ebf5aa9caba6842d2a01d169c5d2b6606dd391762a1c90c86cab5', 2);");

                comandos.Add("alter table pedidovenda add TrayId varchar(20);");
                comandos.Add("alter table pedidovenda add BlingId varchar(20);");
                comandos.Add("alter table pedidovenda add DataImportacao datetime;");


                comandos.Add("alter table estoque ADD DataAlteracao DATETIME DEFAULT CURRENT_TIMESTAMP;");
                comandos.Add("alter table estoque add DataUltimoEnvioTray DATETIME;");
                comandos.Add("alter table estoque add DataUltimoEnvioBling DATETIME");



                comandos.Add("UPDATE parametros SET Valor = '2.0.4.4' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2045() //13/09/2022
        {
            List<string> comandos = new List<string>();
            try
            {

                comandos.Add("insert into parametros(Id,Chave,Valor,EmpresaId,VisaoCliente) values ( NULL,'INTEGRA_MARKET','2',NULL,'2');");
                comandos.Add("alter table nfce drop foreign key FK_Nfce_TabelaPreco;");

                int contador = 0;
                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM parametros WHERE parametros.chave = '*LANCA_OPERACAO_AUTO'");
                if (contador == 0)
                {
                    comandos.Add("insert into parametros(Id,Chave,Valor,EmpresaId,VisaoCliente) values ( NULL,'LANCA_OPERACAO_AUTO','2',NULL,'1');");
                }

                comandos.Add("alter table itensordemproducao add column ItemFinalizado int (1) DEFAULT '0' NOT NULL  after QuantidadeDefeito;");

                comandos.Add("alter table colaboradores add column TipoPix int (1)  NULL  after ObsGuia, add column Pix varchar (300)  NULL  after TipoPix;");

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('LIBERA_ORDEM_PARCIAL','1',NULL,'1');");

                comandos.Add("UPDATE parametros SET Valor = '2.0.4.5' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso !!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPatch2046()
        {
            List<string> comandos = new List<string>();
            try
            {
                if (VestilloSession.SistemasContratados != VestilloSession.Sistemas.GESTAO)
                {
                    comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('OBRIGA_TEMPO_PACOTE','1',NULL,'1');");
                }
                else
                {
                    comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('OBRIGA_TEMPO_PACOTE','2',NULL,'1');");
                }

                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('SALVAR_GRID','1',NULL,'1');");

                comandos.Add("INSERT INTO permissoes(Chave,Descricao) values ('RelPedidoCliente','Rel Pedido agrupado por cliente');");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT     DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'RelPedidoCliente%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");


                comandos.Add("UPDATE parametros SET Valor = '2.0.4.6' WHERE Chave = 'VersaoBanco';");



                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso!!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private bool AplicarPacth2047()
        {
            List<string> comandos = new List<string>();
            try
            {
                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('ROMANEIO_NA_REDE', '2', NULL, '1')");
                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('UNIDADE_ROMANEIO_NA_REDE', 'X:', NULL, '1')");
                comandos.Add("insert into parametros(Chave, Valor, EmpresaId, VisaoCliente) values('UTILIZA_COMBINACAO_NFE', '2', NULL, '1')");
                

                comandos.Add("UPDATE parametros SET Valor = '2.0.4.7' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso!!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPacth2048()
        {
            List<string> comandos = new List<string>();
            try
            {
                comandos.Add("alter table produtos add column DescricaoMarketPlace varchar (1000)  NULL  after CodigoBarrarEcommerce");
                comandos.Add("INSERT INTO permissoes(Chave,Descricao) values ('RelVendaCerta','Rel clientes sem compra por coleção');");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT     DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'RelVendaCerta%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");

                comandos.Add("UPDATE parametros SET Valor = '2.0.4.8' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso!!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                AbrirPdfAtualizacao();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPacth2049()
        {
            List<string> comandos = new List<string>();
            try
            {
                comandos.Add("alter table premios add column empresaid int (11)  NULL  after id;");
                comandos.Add("alter table premiopartida add column empresaid int (11)  NULL  after GruValPartida;");
                comandos.Add("alter table fichatecnicadomaterialitem add column idFornecedor int (11) DEFAULT '0' NOT NULL after custocalculado;");
                comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('ATUALIZA_FC_FORNECEDOR','2',NULL,'1');");
                int contador = 0;
                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM parametros WHERE parametros.chave = 'ACESSO_TS_WINDOWS'");
                if (contador == 0)
                {
                    comandos.Add("insert into parametros(Id,Chave,Valor,EmpresaId,VisaoCliente) values ( NULL,'ACESSO_TS_WINDOWS','2',NULL,'2');");
                }
                comandos.Add("UPDATE parametros SET Valor = '2.0.4.9' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso!!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                AbrirPdfAtualizacao();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool AplicarPacth2050()
        {
            List<string> comandos = new List<string>();
            try
            {
                int contador = 0;
                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM parametros WHERE parametros.chave = 'INCLUIR_ITENS_XML'");
                if (contador == 0)
                {
                    comandos.Add("insert into parametros(Id,Chave,Valor,EmpresaId,VisaoCliente) values ( NULL,'INCLUIR_ITENS_XML','2',NULL,'1');");
                }

                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM parametros WHERE parametros.chave = 'AGRUPAR_LIBERACAO_OP'");
                if (contador == 0)
                {
                    comandos.Add(" insert into parametros(Id, Chave, Valor, EmpresaId, VisaoCliente) values(NULL, 'AGRUPAR_LIBERACAO_OP', '2', NULL, '1')");
                }

                comandos.Add("INSERT INTO permissoes(Chave,Descricao) values ('RelAcompanhaOp','Rel Acompanha OP D Dos Santos');");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT     DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                             " WHERE P.Chave LIKE 'RelAcompanhaOp%' AND " +
                             " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");

                comandos.Add("INSERT INTO permissoes(Chave,Descricao) values ('AlterarValorItem','Exibe a aba de custo do item');");
                comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                             " SELECT     DISTINCT P.Id AS PermissaoId, " +
                             " G.Id AS GrupoId " +
                             " FROM Permissoes P " +
                             " INNER JOIN grupos G ON G.Nome <> '' " +
                             " WHERE P.Chave LIKE 'AlterarValorItem%';");

                comandos.Add("update contadorescodigo SET CasasDecimais=12 where nome  = 'OrdemProducao'");

                comandos.Add("UPDATE parametros SET Valor = '2.0.5.0' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso!!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                AbrirPdfAtualizacao();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPacth2051()
        {
            List<string> comandos = new List<string>();
            try
            {
                int contador = 0;
                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM parametros WHERE parametros.chave = 'EXIBIR_ESTOQUE_GRADE'");
                if (contador == 0)
                {
                    comandos.Add("insert into parametros(Chave,Valor,EmpresaId,VisaoCliente) values ('EXIBIR_ESTOQUE_GRADE','1',NULL,'1');");
                }

                contador = 0;
                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM parametros WHERE parametros.chave = 'INCLUIR_ITENS_XML'");
                if (contador == 0)
                {
                    comandos.Add("insert into parametros(Id,Chave,Valor,EmpresaId,VisaoCliente) values ( NULL,'INCLUIR_ITENS_XML','2',NULL,'1');");
                }

                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM parametros WHERE parametros.chave = 'AGRUPAR_LIBERACAO_OP'");
                if (contador == 0)
                {
                    comandos.Add(" insert into parametros(Id, Chave, Valor, EmpresaId, VisaoCliente) values(NULL, 'AGRUPAR_LIBERACAO_OP', '2', NULL, '1')");
                }


                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM permissoes WHERE permissoes.chave = 'RelAcompanhaOp'");
                if (contador == 0)
                {
                    comandos.Add("INSERT INTO permissoes(Chave,Descricao) values ('RelAcompanhaOp','Rel Acompanha OP D Dos Santos');");
                    comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                 " SELECT     DISTINCT P.Id AS PermissaoId, " +
                                 " G.Id AS GrupoId " +
                                 " FROM Permissoes P " +
                                 " INNER JOIN grupos G ON G.Nome = 'Administrador' " +
                                 " WHERE P.Chave LIKE 'RelAcompanhaOp%' AND " +
                                 " (SELECT COUNT(*) FROM permissoesgrupo P2 WHERE P2.PermissaoId = P.Id) = 0;");
                }


                contador = _repository.RegistroExiste("SELECT COUNT(*) as contador FROM permissoes WHERE permissoes.chave = 'AlterarValorItem'");
                if (contador == 0)
                {
                    comandos.Add("INSERT INTO permissoes(Chave,Descricao) values ('AlterarValorItem','Exibe a aba de custo do item');");
                    comandos.Add(" INSERT INTO permissoesgrupo (PermissaoId, GrupoId) " +
                                 " SELECT     DISTINCT P.Id AS PermissaoId, " +
                                 " G.Id AS GrupoId " +
                                 " FROM Permissoes P " +
                                 " INNER JOIN grupos G ON G.Nome <> '' " +
                                 " WHERE P.Chave LIKE 'AlterarValorItem%';");
                }

                comandos.Add("UPDATE parametros SET Valor = '2.0.5.1' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso!!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                AbrirPdfAtualizacao();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool AplicarPacth2052()
        {
            List<string> comandos = new List<string>();
            try
            {
                comandos.Add("INSERT INTO parametros(Id,Chave,Valor,EmpresaId,VisaoCliente) values ( NULL,'NFCE_SEM_CERTIFICADO','2',NULL,'1');");

                comandos.Add("UPDATE parametros SET Valor = '2.0.5.2' WHERE Chave = 'VersaoBanco';");

                _repository.ExecutarComandos(comandos);

                Funcoes.ExibirMensagem("Atualização concluída com sucesso!!", System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxButtons.OK);
                AbrirPdfAtualizacao();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void AbrirPdfAtualizacao()
        {
            try
            {
                string version = Application.ProductVersion;
                string pdf = String.Empty;

                string _startPath = Application.StartupPath;
                string meuProcesso = Process.GetCurrentProcess().ProcessName;
                if (meuProcesso.ToString().Substring(0, 8) == "Producao")
                {
                    pdf = _startPath + "\\Producao_" + version + ".pdf";

                    if (File.Exists(pdf) == true)
                    {
                        System.Diagnostics.Process.Start(pdf);
                    }                    
                }
                else
                {
                    pdf = _startPath + "\\Gestao_" + version + ".pdf";

                    if (File.Exists(pdf) == true)
                    {
                        System.Diagnostics.Process.Start(pdf);
                    }
                    
                }
            }
            catch (VestilloException ex)
            {
                Funcoes.ExibirErro(ex);
            }
        }

    }
}
