namespace ProjetoAgenda;

public partial class MainForm : Form
{
    private MongoDBService _mongoService;
    private DataGridView dgvPessoa;
    private TextBox txtNome, txtIdade, txtEmail;
    private Button btnSalvar, btnAtualizar, btnExcluir, btnLimpar;
    private string selectedId = null;

    public MainForm()
    {
        _mongoService = new MongoDBService();
       InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.Text = "Gerenciador de Contatos";
        this.Size = new Size(800, 600);
        this.StartPosition = FormStartPosition.CenterScreen;

        var lblNome = new Label(){ Text = "Nome", Location = new Point(20, 20) };
        var lblEmail = new Label(){ Text = "E-Mail", Location = new Point(20, 80) };
        var lblIdade = new Label() { Text = "Idade", Location = new Point(20, 50) };

        txtNome = new TextBox() { Location = new Point(120, 20), Size = new Size(200, 20) };
        txtIdade = new TextBox() { Location = new Point(120, 50), Size = new Size(200, 20) };
        txtEmail = new TextBox() { Location = new Point(120, 80), Size = new Size(200, 20) };

        btnSalvar = new Button()
        {
            Text = "Salvar",
            Location = new Point(20, 120),
            Size = new Size(80, 30)

        };
        btnSalvar.Click += async (s, e) => await SalvarPessoa();

        btnAtualizar = new Button()
        {
            Text = "Atualizar",
            Location = new Point(110, 120),
            Size = new Size(80, 30)

        };
        btnAtualizar.Click += async (s, e) => await AtualizarPessoa();

        btnExcluir = new Button()
        {
            Text = "Excluir",
            Location = new Point(200, 120),
            Size = new Size(80, 30)

        };
        btnExcluir.Click += async (s, e) => await ExcluirPessoa();

        btnLimpar = new Button()
        {
            Text = "Limpar",
            Location = new Point(290, 120),
            Size = new Size(80, 30)

        };
        btnLimpar.Click += (s, e) => LimparTela();

        dgvPessoa = new DataGridView()
        {

            Location = new Point(20, 170),
            Size = new Size(740, 250),
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            MultiSelect = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill

        };
        dgvPessoa.SelectionChanged += DgvPessoa_SelectionChanged;

        Controls.AddRange(new Control[] {
            lblNome, lblEmail, lblIdade,
            txtNome, txtEmail, txtIdade,
            btnSalvar, btnAtualizar, btnExcluir, btnLimpar,
            dgvPessoa

        });
        
    }

    private async Task SalvarPessoa()
    {
        try
        {
            var pessoa = new Pessoa
            {
                Nome = txtNome.Text,
                Idade = int.Parse(txtIdade.Text),
                Email = txtEmail.Text
            };
            await _mongoService.CriarPessoaAsync(pessoa);
            await LoadData();
            LimparTela();
            MessageBox.Show("Pessoa salva com sucesso!!!!");

        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao salvar: {ex.Message}");
        }
        
    }

    private async Task LoadData()
    {
        var pessoa = _mongoService.GetPessoasAsync();
        dgvPessoa.DataSource = pessoa;
    }

    private async Task AtualizarPessoa(){
        
    }

    private async Task ExcluirPessoa(){
        
    }

    private void LimparTela(){
        txtEmail.Text = "";
        txtIdade.Text = "";
        txtNome.Text = "";
    }

    private void DgvPessoa_SelectionChanged(object sender, EventArgs e)
    {
        if(dgvPessoa.CurrentRow != null){
            var pessoa = (Pessoa)dgvPessoa.CurrentRow.DataBoundItem;
            selectedId = pessoa.Id;
            txtNome.Text = pessoa.Nome;
            txtEmail.Text = pessoa.Email;
            txtIdade.Text = pessoa.Idade.ToString();
        }
    }

}
