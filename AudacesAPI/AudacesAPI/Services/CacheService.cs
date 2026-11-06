using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace TemplateAudacesApi.Services
{
    /// <summary>
    /// Cache em memória: busca os dados do banco apenas na primeira vez que são acessados.
    /// Nos acessos seguintes, retorna os dados já carregados sem ir ao banco.
    /// </summary>
    public static class CacheService
    {
        // ── Cores ──────────────────────────────────────────────────────────────
        private static List<Cor> _cores;
        public static List<Cor> Cores
        {
            get
            {
                if (_cores == null)
                    _cores = new CorRepository().GetAll().ToList();
                return _cores;
            }
        }
        // ── Almoxarifados ───────────────────────────────────────────────────────────
        private static List<Almoxarifado> _almoxarifados;
        public static List<Almoxarifado> Almoxarifados
        {
            get
            {
                if (_almoxarifados == null)
                    _almoxarifados = new AlmoxarifadoRepository().GetAll().ToList();
                return _almoxarifados;
            }
        }

        // ── Tamanhos ───────────────────────────────────────────────────────────
        private static List<Tamanho> _tamanhos;
        public static List<Tamanho> Tamanhos
        {
            get
            {
                if (_tamanhos == null)
                    _tamanhos = new TamanhoRepository().GetAll().ToList();
                return _tamanhos;
            }
        }

        // ── Coleções ───────────────────────────────────────────────────────────
        private static List<Colecao> _colecoes;
        public static List<Colecao> Colecoes
        {
            get
            {
                if (_colecoes == null)
                    _colecoes = new ColecaoRepository().GetAll().ToList();
                return _colecoes;
            }
        }

        // ── Grupos de Produto ──────────────────────────────────────────────────
        private static List<GrupProduto> _grupos;
        public static List<GrupProduto> Grupos
        {
            get
            {
                if (_grupos == null)
                    _grupos = new GrupProdutoRepository().GetAll().ToList();
                return _grupos;
            }
        }
        // ── Segmentos ─────────────────────────────────────────────────
        private static List<Segmento> _segmentos;
        public static List<Segmento> Segmentos
        {
            get
            {
                if (_segmentos == null)
                    _segmentos = new SegmentoRepository().GetAll().ToList();
                return _segmentos;
            }
        }
        
        // ── Catalogos ─────────────────────────────────────────────────
        private static List<Catalogo> _catalogos;
        public static List<Catalogo> Catalogos
        {
            get
            {
                if (_catalogos == null)
                    _catalogos = new CatalogoRepository().GetAll().ToList();
                return _catalogos;
            }
        }
        // ── Entregas ─────────────────────────────────────────────────
        private static List<Entrega> _entregas;
        public static List<Entrega> Entregas
        {
            get
            {
                if (_entregas == null)
                    _entregas = new EntregaRepository().GetAll().ToList();
                return _entregas;
            }
        } 
        
        // ── Griffes ─────────────────────────────────────────────────
        private static List<Griffe> _griffes;
        public static List<Griffe> Griffes
        {
            get
            {
                if (_griffes == null)
                    _griffes = new GriffeRepository().GetAll().ToList();
                return _griffes;
            }
        }
        
        // ── Unidades de Medida ─────────────────────────────────────────────────
        private static List<UniMedida> _unidades;
        public static List<UniMedida> Unidades
        {
            get
            {
                if (_unidades == null)
                    _unidades = new UniMedidaRepository().GetAll().ToList();
                return _unidades;
            }
        }
        

        // ── Colaboradores / Fornecedores ───────────────────────────────────────
        private static List<Colaborador> _colaboradores;
        public static List<Colaborador> Colaboradores
        {
            get
            {
                if (_colaboradores == null)
                    _colaboradores = new ColaboradorRepository().GetAll().ToList();
                return _colaboradores;
            }
        }
        
        // ── Familia  ───────────────────────────────────────
        private static List<Familia> _familias;
        public static List<Familia> Familias
        {
            get
            {
                if (_familias == null)
                    _familias = new FamiliaRepository().GetAll().ToList();
                return _familias;
            }
        }
        
        // ── Estilista  ───────────────────────────────────────
        private static List<Estilista> _estilistas;
        public static List<Estilista> Estilistas
        {
            get
            {
                if (_estilistas == null)
                    _estilistas = new EstilistaRepository().GetAll().ToList();
                return _estilistas;
            }
        }
        // ── SubGrupos  ───────────────────────────────────────
        private static List<SubGrupo> _subGrupos;
        public static List<SubGrupo> SubGrupos
        {
            get
            {
                if (_subGrupos == null)
                    _subGrupos = new SubGrupoRepository().GetAll().ToList();
                return _subGrupos;
            }
        }
        // ── Categoria  ───────────────────────────────────────
        private static List<Categoria> _categorias;
        public static List<Categoria> Categorias
        {
            get
            {
                if (_categorias == null)
                    _categorias = new CategoriaRepository().GetAll().ToList();
                return _categorias;
            }
        }
        // ── Sub-Categoria  ───────────────────────────────────────
        private static List<SubCategoria> _subCategorias;
        public static List<SubCategoria> subCategorias
        {
            get
            {
                if (_subCategorias == null)
                    _subCategorias = new SubcategoriaRepository().GetAll().ToList();
                return _subCategorias;
            }
        }
        
        
        // ── Modelista  ───────────────────────────────────────
        private static List<Modelista> _modelistas;
        public static List<Modelista> Modelistas
        {
            get
            {
                if (_modelistas == null)
                    _modelistas = new ModelistaRepository().GetAll().ToList();
                return _modelistas;
            }
        }
       
       

        // ── Helpers de busca (substitutos dos métodos do Utils) ────────────────
        public static Cor BuscarCor(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Cor();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Cores.FirstOrDefault(c => c.Id == id) ?? new Cor();
            }

            return Cores.FirstOrDefault(c =>
                string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                ?? new Cor();
        }
        
        public static Categoria BuscaCategoria(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Categoria();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Categorias.FirstOrDefault(c => c.Id == id) ?? new Categoria();
            }

            return Categorias.FirstOrDefault(c =>
                       string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Categoria();
        }

        public static Almoxarifado BuscarAlmoxarifado(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Almoxarifado();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Almoxarifados.FirstOrDefault(c => c.Id == id) ?? new Almoxarifado();
            }

            return Almoxarifados.FirstOrDefault(c =>
                       string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Almoxarifado();
        }
        
        public static Catalogo BuscarCatalogo(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Catalogo();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Catalogos.FirstOrDefault(c => c.Id == id) ?? new Catalogo();
            }

            return Catalogos.FirstOrDefault(c =>
                       string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Catalogo();
        }
        
        public static Entrega BuscaEntrega(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Entrega();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Entregas.FirstOrDefault(c => c.Id == id) ?? new Entrega();
            }

            return Entregas.FirstOrDefault(c =>
                       string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Entrega();
        }
        public static Estilista BuscarEstilista(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Estilista();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Estilistas.FirstOrDefault(c => c.Id == id) ?? new Estilista();
            }

            return Estilistas.FirstOrDefault(c =>
                       string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Estilista();
        }
        
        public static Familia BuscarFamilia(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Familia();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Familias.FirstOrDefault(c => c.Id == id) ?? new Familia();
            }

            return Familias.FirstOrDefault(c =>
                       string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Familia();
        }
        
        public static Griffe BuscarGriffe(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Griffe();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Griffes.FirstOrDefault(c => c.Id == id) ?? new Griffe();
            }

            return Griffes.FirstOrDefault(c =>
                       string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Griffe();
        }

        public static Tamanho BuscarTamanho(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Tamanho();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Tamanhos.FirstOrDefault(t => t.Id == id) ?? new Tamanho();
            }

            return Tamanhos.FirstOrDefault(t =>
                string.Equals(t.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                ?? new Tamanho();
        }

        public static Colecao BuscarColecao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Colecao();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Colecoes.FirstOrDefault(c => c.Id == id) ?? new Colecao();
            }

            return Colecoes.FirstOrDefault(c =>
                string.Equals(c.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                ?? new Colecao();
        }

        public static GrupProduto BuscarGrupo(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new GrupProduto();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Grupos.FirstOrDefault(g => g.Id == id) ?? new GrupProduto();
            }

            return Grupos.FirstOrDefault(g =>
                string.Equals(g.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                ?? new GrupProduto();
        }
        
        public static Modelista BuscarModelista(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Modelista();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Modelistas.FirstOrDefault(g => g.Id == id) ?? new Modelista();
            }

            return Modelistas.FirstOrDefault(g =>
                       string.Equals(g.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Modelista();
        }
        
        public static Segmento BuscarSegmento(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new Segmento();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Segmentos.FirstOrDefault(g => g.Id == id) ?? new Segmento();
            }

            return Segmentos.FirstOrDefault(g =>
                       string.Equals(g.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new Segmento();
        }
        
        public static SubCategoria BuscaSubCategoria(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new SubCategoria();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return subCategorias.FirstOrDefault(g => g.Id == id) ?? new SubCategoria();
            }

            return subCategorias.FirstOrDefault(g =>
                       string.Equals(g.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new SubCategoria();
        }
        
        public static SubGrupo BuscarSubGrupo(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new SubGrupo();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return SubGrupos.FirstOrDefault(g => g.Id == id) ?? new SubGrupo();
            }

            return SubGrupos.FirstOrDefault(g =>
                       string.Equals(g.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                   ?? new SubGrupo();
        }

        public static UniMedida BuscarUnidade(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao)) return new UniMedida();

            if (descricao.Contains("-"))
            {
                var vet = descricao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Unidades.FirstOrDefault(u => u.Id == id) ?? new UniMedida();
            }

            return Unidades.FirstOrDefault(u =>
                string.Equals(u.Descricao?.Trim(), descricao.Trim(), StringComparison.OrdinalIgnoreCase))
                ?? new UniMedida();
        }

        public static Colaborador BuscarFornecedor(string razao)
        {
            if (string.IsNullOrWhiteSpace(razao)) return new Colaborador();

            if (razao.Contains("-"))
            {
                var vet = razao.Split('-');
                if (int.TryParse(vet[0].Trim(), out int id))
                    return Colaboradores.FirstOrDefault(c => c.Id == id) ?? new Colaborador();
            }

            return Colaboradores.FirstOrDefault(c =>
                string.Equals(c.RazaoSocial?.Trim(), razao.Trim(), StringComparison.OrdinalIgnoreCase))
                ?? new Colaborador();
        }

        // ── Invalidar cache (forçar recarga na próxima vez) ────────────────────

        public static void InvalidarCores()       => _cores = null;
        public static void InvalidarTamanhos()    => _tamanhos = null;
        public static void InvalidarColecoes()    => _colecoes = null;
        public static void InvalidarGrupos()      => _grupos = null;
        public static void InvalidarUnidades()    => _unidades = null;
        public static void InvalidarColaboradores() => _colaboradores = null;
        public static void InvalidarAlmoxarifado() => _almoxarifados = null;
        public static void InvalidSegmentos ()  => _segmentos = null;
        public static void InvalidarCatalogos()  => _catalogos = null;
        public static void InvalidarFamilias() => _familias = null;
        public static void InvalidarGriffes()   => _griffes = null;
        public static void InvalidarEntregas() => _entregas = null; 
        

        public static void InvalidarTudo()
        {
            _cores         = null;
            _tamanhos      = null;
            _colecoes      = null;
            _grupos        = null;
            _unidades      = null;
            _colaboradores = null;
            _almoxarifados = null;  
            _segmentos = null;
            _catalogos = null;
            _entregas = null;
            _griffes = null;
            _familias = null; 
        }
    }
}