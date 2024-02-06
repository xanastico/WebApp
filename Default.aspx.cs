using System;
using System.Data;
using System.Web.UI;
using MySql.Data.MySqlClient;
using System.Web.UI.WebControls;

namespace Teste_WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
                {
                    ManageDataPanel.Visible = false;
                    LoginPanel.Visible = true;
                }
                else
                {
                    ManageDataPanel.Visible = true;
                    LoginPanel.Visible = false;
                    BindData();
                }
            }
        }

        // Método para conexão e exibição de dados
        public void BindData()
        {
            string cs = @"server=127.0.0.1; port=3306;userid=root;password=root;database=takethewind";
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();
                string sql = "SELECT * FROM `table2`";
                MySqlCommand cmd = new MySqlCommand(sql, con);

                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
        }

        // Método para verificar o login na table1
        private bool VerifyLogin(string username, string password)
        {
            string cs = @"server=127.0.0.1; port=3306;userid=root;password=root;database=takethewind";
            using (MySqlConnection con = new MySqlConnection(cs))
            {
                con.Open();

                string query = "SELECT COUNT(*) FROM `table1` WHERE `Username` = @username AND `Password` = @password";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                int userCount = Convert.ToInt32(cmd.ExecuteScalar());
                return userCount > 0; // Retorna verdadeiro se o login for válido
            }
        }

        // Método para lidar com o evento de clique no botão de login
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;

            if (VerifyLogin(username, password))
            {
                Session["LoggedIn"] = true;
                ManageDataPanel.Visible = true;
                LoginPanel.Visible = false;
                BindData();
            }
            else
            {
                LoginStatusLabel.Visible = true;
                LoginStatusLabel.Text = "Credenciais inválidas. Tente novamente.";
            }
        }

        // Método para inserir dados na table2
        protected void InsertVehicleButton_Click(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"])
            {
                string marca = MarcaTextBox.Text;
                string tipo = TipoTextBox.Text;
                int quantidade;

                if (!int.TryParse(QuantidadeTextBox.Text, out quantidade))
                {
                    // Se a quantidade não for um número válido, exibe uma mensagem e sai
                    Response.Write("<script>alert('Por favor, insira um valor numérico para a Quantidade.');</script>");
                    return;
                }

                try
                {
                    
                    int novoID = ObterProximoIDDisponivel(); // Método fictício para obter próximo ID disponível

                    string cs = @"server=127.0.0.1; port=3306;userid=root;password=root;database=takethewind";
                    using (MySqlConnection con = new MySqlConnection(cs))
                    {
                        con.Open();
                        string sql = "INSERT INTO `table2` (`ID`, `Marca`, `Tipo`, `Quantidade`) VALUES (@id, @marca, @tipo, @quantidade)";
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@id", novoID);
                        cmd.Parameters.AddWithValue("@marca", marca);
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@quantidade", quantidade);
                        cmd.ExecuteNonQuery();

                        BindData();
                        Response.Write("<script>alert('Inserido com Sucesso!');</script>");
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            else
            {
                Response.Write("<script>alert('Login inválido! Não autorizado a realizar esta ação.');</script>");
            }
        }


        private int ObterProximoIDDisponivel()
        {
            int proximoID = 0; // Valor inicial para o próximo ID

            try
            {
                string cs = @"server=127.0.0.1; port=3306;userid=root;password=root;database=takethewind";
                using (MySqlConnection con = new MySqlConnection(cs))
                {
                    con.Open();
                    string sql = "SELECT MAX(Id) FROM `table2`"; // Ajuste para usar a coluna 'Id'
                    MySqlCommand cmd = new MySqlCommand(sql, con);

                    var result = cmd.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        proximoID = Convert.ToInt32(result) + 1; // Incrementa o maior ID encontrado na tabela
                    }
                }
            }
            catch (MySqlException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return proximoID;
        }


        // Método para atualizar dados na table2
        protected void UpdateVehicleButton_Click(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"])
            {
                string id = UpdateIDTextBox.Text; // Pegar o ID do veículo atualizado
                string tipo = UpdateTipoTextBox.Text; // Pegar o novo tipo
                int quantidade;

                if (!int.TryParse(UpdateQuantidadeTextBox.Text, out quantidade))
                {
                    // Se a quantidade não for um número válido, exibe uma mensagem e sai
                    Response.Write("<script>alert('Por favor, insira um valor numérico para a Quantidade.');</script>");
                    return;
                }

                try
                {
                    string cs = @"server=127.0.0.1; port=3306;userid=root;password=root;database=takethewind";
                    using (MySqlConnection con = new MySqlConnection(cs))
                    {
                        con.Open();
                        string sql = "UPDATE `table2` SET `Tipo` = @tipo, `Quantidade` = @quantidade WHERE `ID` = @id";
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@tipo", tipo);
                        cmd.Parameters.AddWithValue("@quantidade", quantidade);
                        cmd.ExecuteNonQuery();

                        BindData();
                        Response.Write("<script>alert('Atualizado com Sucesso!');</script>");
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            else
            {
                Response.Write("<script>alert('Login inválido! Não autorizado a realizar esta ação.');</script>");
            }
        }

        // Método para excluir dados na table2 usando o ID
        protected void DeleteVehicleButton_Click(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && (bool)Session["LoggedIn"])
            {
                string id = DeleteIdTextBox.Text; // ID do veículo que será excluído

                try
                {
                    string cs = @"server=127.0.0.1; port=3306;userid=root;password=root;database=takethewind";
                    using (MySqlConnection con = new MySqlConnection(cs))
                    {
                        con.Open();
                        string sql = "DELETE FROM `table2` WHERE `ID` = @id";
                        MySqlCommand cmd = new MySqlCommand(sql, con);
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            BindData();
                            Response.Write("<script>alert('Apagado com Sucesso!');</script>");
                        }
                        else
                        {
                            Response.Write("<script>alert('Não foi possível encontrar o veículo para apagar. Verifique os dados inseridos.');</script>");
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
            else
            {
                Response.Write("<script>alert('Login inválido! Não autorizado a realizar esta ação.');</script>");
            }
        }

    }
}